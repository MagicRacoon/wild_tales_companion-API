using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace LukeDino.Classes
{
    public class FirebaseAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly FirebaseTokenValidator _validator;

        public FirebaseAuthMiddleware(RequestDelegate next, FirebaseTokenValidator validator)
        {
            _next = next;
            _validator = validator;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrEmpty(authHeader) &&
                authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader["Bearer ".Length..].Trim();

                var validatedToken = await _validator.ValidateAsync(token);
                if (validatedToken != null)
                {
                    context.Items["FirebaseUser"] = validatedToken;
                    await _next(context);
                    return;
                }
            }

            var endpoint = context.GetEndpoint();
            var allowsAnonymous =
                endpoint?.Metadata?.Any(m => m is AllowAnonymousAttribute) ?? false;

            if (allowsAnonymous)
            {
                await _next(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }

}
