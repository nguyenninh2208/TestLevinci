using System.Collections.Generic;
using System.Threading.Tasks;
using Jwt.Core.Domain.Entities;
using Jwt.Core.Domain.Entities.Requests;
using Jwt.Core.Domain.Entities.Responses;

namespace Jwt.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest message);
        Task<List<User>> GetUsers();
    }
}