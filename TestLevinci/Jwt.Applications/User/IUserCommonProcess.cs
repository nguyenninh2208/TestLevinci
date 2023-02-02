using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jwt.Applications.User.Commands.Login;

namespace Jwt.Applications.User
{
    public interface  IUserCommonProcess
    {
        /// <summary>
        /// update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateUser(UserLoginDto user);
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<(bool, string)> InsertUser(UserLoginDto user);
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<(bool, string)> DeleteUser(int userID);
    }
}
