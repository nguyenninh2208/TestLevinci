using Dapper;
using Jwt.Infrastructure.Database;
using Jwt.Infrastructure.Extentions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jwt.Applications.User.Commands.Login;

namespace Jwt.Applications.User
{
    public class UserCommonProcess : IUserCommonProcess
    {
        private readonly IQuery _query;
        public UserCommonProcess(IQuery query)
        {
            _query = query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdateUser(UserLoginDto user)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@pUserID", user.UserID, DbType.Int64, ParameterDirection.Input);
                parameter.Add("@pUsername", user.UserName, DbType.String, ParameterDirection.Input);
                parameter.Add("@pName", user.Name, DbType.String, ParameterDirection.Input);
                parameter.Add("@pRole", user.Role, DbType.String, ParameterDirection.Input);
                parameter.Add("@pEmail", user.Email, DbType.Int32, ParameterDirection.Input);

                var queryResult = await _query.QueryAsync<BaseResultDto>(Infrastructure.Utility.Setting.ConnectionString, 
                    ControlsDataprovider.StoreAndFunctionInfo.SP_Mana_UserUpdate, 
                    parameter);

                var result = queryResult.FirstOrDefault();

                if (result != null && result.ResultId == 1)

                    return (true, "Cập nhật thành công.");
                else
                    return (false, "Cập nhật thất bại.");
            }
            catch (Exception ex)
            {
                return (false, "Cập nhật thất bại.");
            }
        }
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<(bool, string)> InsertUser(UserLoginDto user)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@pUsername", user.UserName, DbType.String, ParameterDirection.Input);
                parameter.Add("@pPassword", user.Name.ToMD5(), DbType.String, ParameterDirection.Input);
                parameter.Add("@pName", user.Role, DbType.String, ParameterDirection.Input);
                parameter.Add("@pRole", user.Email, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@pEmail", user.Email, DbType.Int32, ParameterDirection.Input);

                var queryResult = await _query.QueryAsync<BaseResultDto>(Infrastructure.Utility.Setting.ConnectionString, 
                    ControlsDataprovider.StoreAndFunctionInfo.SP_Mana_UserUpdate, 
                    parameter);

                var result = queryResult.FirstOrDefault();

                if (result != null && result.ResultId == 1)

                    return (true, "Thêm thành công.");
                else
                    return (false, "Thêm thất bại.");
            }
            catch (Exception ex)
            {
                return (false, "Thêm thất bại.");
            }
        }
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<(bool, string)> DeleteUser(int userID)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@pUserID", userID, DbType.Int32, ParameterDirection.Input);

                var queryResult = await _query.QueryAsync<BaseResultDto>(Infrastructure.Utility.Setting.ConnectionString, 
                    ControlsDataprovider.StoreAndFunctionInfo.SP_Mana_UserDelete, 
                    parameter);

                var result = queryResult.FirstOrDefault();

                if (result != null && result.ResultId == 1)

                    return (true, "Xóa thành công.");
                else
                    return (false, "Xóa thất bại.");
            }
            catch (Exception ex)
            {
                return (false, "Xóa thất bại.");
            }
        }
        internal class BaseResultDto
        {
            //
            // Summary:
            //     1: success còn lại: thất bại
            public int ResultId { get; set; }

            //
            // Summary:
            //     Message
            public string Message { get; set; }
        }
    }
}
