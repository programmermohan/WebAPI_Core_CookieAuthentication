using CookieAuthentication_CoreWebAPI.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CookieAuthentication_CoreWebAPI.Common
{
    public class DbUtility : IDbUtility
    {
        private readonly string _connectionString;
        private readonly ContextService contextService;

        public DbUtility(IOptions<ContextService> options)
        {
            contextService = options.Value;
            _connectionString = contextService.GetConnectionString();
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, Func<SqlDataReader, T> map, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            List<T> results = new List<T>();

            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.CommandType = commandType;

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(map(reader));
                    }
                }
            }

            return results;
        }

        public async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.CommandType = commandType;

                await conn.OpenAsync();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<object?> ExecuteScalarAsync(string query, SqlParameter[]? parameters = null, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.CommandType = commandType;

                await conn.OpenAsync();
                return await cmd.ExecuteScalarAsync();
            }
        }
    }
}
