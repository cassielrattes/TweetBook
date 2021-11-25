using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User == null)
            {
                return string.Empty;
            }
            var teste = httpContext.User.Claims.Single(x => x.Type == "Id").Value;


            return httpContext.User.Claims.Single(x => x.Type == "Id").Value;
        }

    }

}
