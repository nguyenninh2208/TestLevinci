﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Core.Domain.Entities;
using Jwt.Core.Domain.Entities.Requests;
using Jwt.Core.Domain.Entities.Responses;
using Jwt.Core.Interfaces.Gateways.Repositories;
using Jwt.Core.Interfaces.Services;

namespace Jwt.Core.Domain.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest message)
        {
            var response = await _userRepository.Create(message.FirstName, message.LastName, message.Email,
                message.UserName, message.Password);

            return response.Success
                ? new RegisterUserResponse(response.Id, true)
                : new RegisterUserResponse(response.Errors.Select(e => e.Description));
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.ListAll();
        }
    }
}