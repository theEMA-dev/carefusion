﻿using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Carefusion.Core.Utilities;

public abstract class Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeaderName = "X-API-KEY";
        private const string ApiKeyQueryName = "api_key";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKey = Env.GetString("API_KEY") ?? Environment.GetEnvironmentVariable("API_KEY");

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                potentialApiKey = context.HttpContext.Request.Query[ApiKeyQueryName];
            }

            if (apiKey != null && (string.IsNullOrEmpty(potentialApiKey) || !apiKey.Equals(potentialApiKey)))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }

}