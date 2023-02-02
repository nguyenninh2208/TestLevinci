using Jwt.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;


namespace Jwt.Infrastructure.JwtHandler
{
    public static class JwtEventHandler
    {
        public static Func<JwtBearerChallengeContext, Task> OnChallengeHandler()
        {
            return context =>
            {
                context.HandleResponse();
                var response = ApiResult.Error("Unauthorized.",
                    "Unauthorized",
                    code: ApiResultCode.Unauthorize);

                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response)).Wait();
                return Task.CompletedTask;
            };
        }
    }
}
