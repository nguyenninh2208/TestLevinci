using System.Collections.Generic;

namespace Jwt.Infrastructure.Database
{
    public interface IQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataprovider"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string dataprovider, object param = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Execute(string sql, object param = null);
        /// <summary>
        /// Query Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql, object param = null, int commandTimeout = 20);
        //
        // Parameters:
        //   connectionString:
        //
        //   sql:
        //
        //   param:
        //
        //   commandTimeout:
        Task<int> ExecuteAsync(string connectionString, string sql, object param = null, int commandTimeout = 20);
    }
}
