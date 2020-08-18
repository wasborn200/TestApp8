using System;
using System.Collections.Generic;
using System.Data;
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
        public List<AuthModel> getPassword2(DbAccess dbAccess, SqlCommand cmd)
        {
            cmd.CommandText = this.getSelectQuery();

            DataTable dt = new DataTable();
            dt = dbAccess.executeQuery(cmd);

            List<AuthModel> paidList = this.getListBindDataTable(dt);

            return paidList;
        }

        private string getSelectQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT")
              .Append(" NAME,")
              .Append(" EMAIL")
              .Append(" FROM")
              .Append(" ACCOUNT");
            return sb.ToString();
        }

        private List<AuthModel> getListBindDataTable(DataTable dt)
        {
            List<AuthModel> authList = new List<AuthModel>();

            foreach (DataRow dr in dt.Rows)
            {
                AuthModel authListModel = new AuthModel();

                if(!(dr["NAME"] is DBNull))
                {
                    authListModel.Name = Convert.ToString(dr["NAME"]);
                }
                if (!(dr["EMAIL"] is DBNull))
                {
                    authListModel.Email = Convert.ToString(dr["EMAIL"]);
                }
                authList.Add(authListModel);

            }
            return authList;
        } 

        #region ネット検索より作成

        public AuthModel getPassword(AuthViewModel vm, SqlCommand cmd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT");
            sb.Append(" PASSWORD");
            sb.Append(" FROM");
            sb.Append(" ACCOUNT");
            sb.Append(" WHERE");
            sb.Append($" NAME = '{vm.Name}'");
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


        public void InsertAccount(AuthViewModel vm, SqlCommand cmd)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO");
            sb.Append(" ACCOUNT");
            sb.Append(" (NAME,");
            sb.Append(" PASSWORD)");
            sb.Append(" VALUES");
            sb.Append($" ('{vm.Name}',");
            sb.Append($" '{vm.Password}')");
            cmd.CommandText = sb.ToString();

            cmd.ExecuteNonQuery();

        }

        #endregion
    }
}