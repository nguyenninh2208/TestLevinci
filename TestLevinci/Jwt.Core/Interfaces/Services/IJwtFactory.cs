using System.Threading.Tasks;
using Jwt.Core.Domain.Entities;

namespace Jwt.Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName);
    }
}