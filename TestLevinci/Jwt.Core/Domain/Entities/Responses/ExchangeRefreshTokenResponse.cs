using Jwt.Core.Interfaces;

namespace Jwt.Core.Domain.Entities.Responses
{
    public class ExchangeRefreshTokenResponse : ResponseMessage
    {
        public ExchangeRefreshTokenResponse(bool success = false, string message = null) : base(success, message)
        {
        }

        public ExchangeRefreshTokenResponse(AccessToken accessToken, string refreshToken, bool success = false,
            string message = null) : base(success, message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public AccessToken AccessToken { get; }

        public string RefreshToken { get; }
    }
}