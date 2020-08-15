using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TestApp8.DataModels;
using TestApp8.Models;

namespace TestApp8.Dao
{
    public class AuthDao
    {
        public AuthModel getPassword(AuthViewModel vm, SqlConnection dbaccess, SqlCommand cmd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT");
            sb.Append(" PASSWORD");
            sb.Append(" FROM");
            sb.Append(" ACCOUNT");
            sb.Append(" WHERE");
            sb.Append($" ID = '{vm.Id}'");
            cmd.CommandText = sb.ToString();

            AuthModel loginuser = new AuthModel();
            using (var sdr = cmd.ExecuteReader())
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        loginuser.Password = sdr["PASSWORD"].ToString();
                    }
                }
            }
            return loginuser;
        }
    }
}