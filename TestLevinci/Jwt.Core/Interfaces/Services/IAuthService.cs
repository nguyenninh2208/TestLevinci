using System.Threading.Tasks;
using Jwt.Core.Domain.Entities.Requests;
using Jwt.Core.Domain.Entities.Responses;

namespace Jwt.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest message);

        Task<ExchangeRefreshTokenResponse> ExchangeRefreshTokenAsync(ExchangeRefreshTokenRequest message);
    }
}