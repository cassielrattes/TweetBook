using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Contracts
{
    public static class ApiRoutes
    {

        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Posts {
            public const string Get = Base + "/post/{postId}";
            public const string Update = Base + "/post/{postId}";
            public const string Delete = Base + "/post/{postId}";
            public const string GetAll = Base + "/posts";
            public const string Create = Base + "/post";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string RefreshToken = Base + "/identity/refreshtoken";
        }
    }
}
