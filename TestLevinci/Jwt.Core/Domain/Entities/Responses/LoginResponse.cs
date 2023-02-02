using System.Collections.Generic;
using Jwt.Core.Interfaces;

namespace Jwt.Core.Domain.Entities.Responses
{
    public class LoginResponse : ResponseMessage
    {
        public LoginResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success,
            message)
        {
            Errors = errors;
        }

        public LoginResponse(AccessToken accessToken, string refreshToken, bool success = false, string message = null)
            : base(success, message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public AccessToken AccessToken { get; }

        public string RefreshToken { get; }

        public IEnumerable<Error> Errors { get; }
    }
}