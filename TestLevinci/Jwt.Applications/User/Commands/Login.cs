using Dapper;
using Jwt.Infrastructure.Database;
using Jwt.Infrastructure.Extentions;
using Jwt.Infrastructure.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Expressions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Jwt.Infrastructure.Database.ControlsDataprovider;

namespace Jwt.Applications.User.Commands
{
    public class Login
    {
        public class Command : ICustomRequest<TokenInfoDto>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public class TokenInfoDto
        {
            public string AccessToken { get; set; }
            public UserLoginDto User { get; set; }
        }
        public class Handler : ICustomRequestHandler<Command, TokenInfoDto>
        {
            private readonly IQuery _query;
            private readonly IUserCommonProcess _userCommonProcess;
            public Handler(IQuery query,
                           IUserCommonProcess userCommonProcess)
            {
                _query = query;
                _userCommonProcess = userCommonProcess;
            }
            public async Task<ApiResult<TokenInfoDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = new TokenInfoDto();

                var param = new DynamicParameters();
                param.Add("@Username", request.UserName);
                param.Add("@Password", request.Password.ToMD5());

                var userResult = _query.Query<UserLoginDto>(StoreAndFunctionInfo.SP_Mana_UserLogin, param);
                if (userResult?.Any() ?? false)
                {
                    var user = userResult.First();
                    var expired = DateTime.Now.AddDays(Infrastructure.Utility.Setting.TokenLife);
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, Infrastructure.Utility.Setting.JwtAuth.Subject),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim(JwtRegisteredClaimNames.Exp, expired.ToString()),
                        new Claim("UserID", user.UserID.ToString()),
                        new Claim("Username", user.Name),
                        new Claim("Email", user.Email ?? ""),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Infrastructure.Utility.Setting.JwtAuth.SecretKey));
                    var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(Infrastructure.Utility.Setting.JwtAuth.Issuer, Infrastructure.Utility.Setting.JwtAuth.Audience, claims, expires: expired, signingCredentials: sign);



                    if (user.Role.Equals(Role.Admin))
                    {
                        result.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                        result.User = user;
                        await _userCommonProcess.UpdateUser(user);
                        await _userCommonProcess.DeleteUser(user.UserID);
                        await _userCommonProcess.InsertUser(user);
                    }
                    else if (user.Role.Equals(Role.User))
                    {
                        result.User = user;
                        await _userCommonProcess.UpdateUser(user);
                    }
                    Infrastructure.Utility.SendEmail(toAddress: user.Email, body: $"Username: {user.UserName}, Password: {user.Password}");
                    return await ApiResult.OkAsync(result);
                }
                return await ApiResult.FailAsync<TokenInfoDto>("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }
        internal static class Role
        {
            public const string Admin = "admin";
            public const string User = "user";
        }
        public class ResponeLoginDto
        {
            public UserLoginDto User { get; set; }
            public TokenInfoDto TokenInfoDto { get; set; }
        }

        public class UserLoginDto
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string Email { get; set; }
        }
    }
}
