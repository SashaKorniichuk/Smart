using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "x-api-key";
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!IsValidApiKey(context.HttpContext))
        {
            context.Result = new UnauthorizedResult();
        }
    }

    private bool IsValidApiKey(HttpContext httpContext)
    {
        string? apiKey = httpContext.Request.Headers[ApiKeyHeaderName];

        if (string.IsNullOrEmpty(apiKey))
        {
            return false;
        }

        string actualApiKey = httpContext.RequestServices.GetRequiredService<IConfiguration>().GetValue<string>("ApiKey")!;

        return actualApiKey == apiKey;
    }
}

