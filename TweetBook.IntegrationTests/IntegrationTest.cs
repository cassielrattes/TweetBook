using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using TweetBook.Data;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using System.Net.Http.Json;
using TweetBook.Domain;

namespace TweetBook.IntegrationTests
{
    public class IntegrationTest
    {

        protected readonly HttpClient httpClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            httpClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
        {
            var response = await httpClient.PostAsJsonAsync(ApiRoutes.Posts.Create, request);
            return await response.Content.ReadFromJsonAsync<PostResponse>();
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await httpClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "cassiel@gmail.com",
                Password = "123Cassiel@"
            });

            var registrationResponse = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }
    }
}
