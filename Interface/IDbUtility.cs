using Microsoft.Data.SqlClient;
using System.Data;

namespace CookieAuthentication_CoreWebAPI.Interface
{
    public interface IDbUtility
    {
        Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, Func<SqlDataReader, T> map, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text);
        Task<int> ExecuteNonQueryAsync(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text);
        Task<object?> ExecuteScalarAsync(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text);
    }
}
