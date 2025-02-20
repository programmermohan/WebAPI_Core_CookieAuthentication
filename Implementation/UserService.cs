using CookieAuthentication_CoreWebAPI.Helper;
using CookieAuthentication_CoreWebAPI.Interface;
using CookieAuthentication_CoreWebAPI.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CookieAuthentication_CoreWebAPI.Implementation
{
    public class UserService : IUserService
    {
        private readonly IDbUtility _dbUtility;
        public UserService(IDbUtility dbUtility)
        {
            _dbUtility = dbUtility;
        }
        public async Task<UserModel> IsValidUser(LoginModel loginModel)
        {
            SqlParameter[] parameters = [
                new SqlParameter("@Username", SqlDbType.NVarChar) { Value = loginModel.Username },
                new SqlParameter("@Password", SqlDbType.NVarChar) { Value = loginModel.Password }
            ];

            var result = await _dbUtility.ExecuteQueryAsync(Procedures.SP_VALIDUSER, reader => new UserModel
            {
                UserID = Convert.ToInt32(reader.GetValue("Id")),
                RoleID = Convert.ToInt32(reader.GetValue("RoleId")),
                Username = Convert.ToString(reader.GetValue("UserName")),
                Rolename = Convert.ToString(reader.GetValue("RoleName"))
            }, parameters, CommandType.StoredProcedure);
            var returnResult = result.First();
            return returnResult;
        }
    }
}
