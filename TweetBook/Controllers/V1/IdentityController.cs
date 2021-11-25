using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Domain;
using TweetBook.Services;

namespace TweetBook.Controllers.V1
{
    public class IdentityController : Controller
    {

        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authReponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if(!authReponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authReponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse { 
                Token = authReponse.Token,
                RefreshToken = authReponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {

            var authReponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authReponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authReponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authReponse.Token,
                RefreshToken = authReponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {

            var authReponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authReponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authReponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authReponse.Token,
                RefreshToken = authReponse.RefreshToken
            });
        }


    }
}
