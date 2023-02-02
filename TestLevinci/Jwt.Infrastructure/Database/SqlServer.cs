using Dapper;
using Jwt.Infrastructure.Configuations;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace Jwt.Infrastructure.Database
{
    public class SqlServer : IQuery
    {
        private static AppSettings _appSettings = AppSettingServices.Get;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sql, object param = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Utility.Setting.ConnectionString))
                {
                    conn.Open();
                    return conn.Execute(sql, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storeNname"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string storeName, object param = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Utility.Setting.ConnectionString))
                {
                    conn.Open();
                    return conn.Query<T>(storeName, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Query Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var result = await conn.QueryAsync<T>(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                    return result.ToList();
                }
        }  //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string connectionString, string sql, object param = null, int commandTimeout = 20)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var result = await conn.ExecuteAsync(sql, param, commandType: CommandType.Text, commandTimeout: commandTimeout);
                return result;
            }
        }
    }
}
