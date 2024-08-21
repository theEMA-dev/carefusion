using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetEnv;
namespace Carefusion.Core
{
    public abstract class Utilities
    {
        public class DateOnlyJsonConverter : JsonConverter<DateOnly>
        {
            private const string Format = "yyyy-MM-dd";

            public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateOnly.ParseExact(reader.GetString() ?? string.Empty, Format);
            }

            public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(Format));
            }
        }

        public class NotFoundException(string message) : Exception(message)
        {
        }

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
}
