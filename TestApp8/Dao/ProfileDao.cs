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
    public class ProfileDao
    {
        public ProfileModel getProfileList(DbAccess dbAccess, SqlCommand cmd, int accountId)
        {
            cmd.CommandText = this.getProfileListSelectQuery(accountId);

            DataTable dt = new DataTable();
            dt = dbAccess.executeQuery(cmd);

            ProfileModel profileList = this.getProfileListBindDataTable(dt);

            return profileList;
        }

        private string getProfileListSelectQuery(int accountId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT")
              .Append(" NAME,")
              .Append(" EMAIL,")
              .Append(" PREFUCTURE,")
              .Append(" ADDRESS")
              .Append(" FROM")
              .Append(" ACCOUNT")
              .Append(" WHERE")
              .Append($" ACCOUNT_ID = {accountId}");

            return sb.ToString();
        }

        private ProfileModel getProfileListBindDataTable(DataTable dt)
        {

            ProfileModel profileList = new ProfileModel();
            DataRow dr = dt.Rows[0];

            if (!(dr["NAME"] is DBNull))
            {
                profileList.Name = Convert.ToString(dr["NAME"]);
            }
            if (!(dr["EMAIL"] is DBNull))
            {
                profileList.Email = Convert.ToString(dr["EMAIL"]);
            }
            if (!(dr["PREFUCTURE"] is DBNull))
            {
                profileList.Prefucture = Convert.ToString(dr["PREFUCTURE"]);
            }
            if (!(dr["ADDRESS"] is DBNull))
            {
                profileList.Address = Convert.ToString(dr["ADDRESS"]);
            }

            return profileList;
        }


        public int editProfile(ProfileEditViewModel vm, SqlCommand cmd, DbAccess dbAccess)
        {
            cmd.CommandText = this.getEditQuery(vm);

            return dbAccess.executeNonQuery(cmd);

        }

        private string getEditQuery(ProfileEditViewModel vm)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" UPDATE")
              .Append(" ACCOUNT")
              .Append(" SET")
              .Append($" NAME = '{vm.Name}',")
              .Append($" EMAIL = '{vm.Email}',")
              .Append($" PREFUCTURE = '{vm.Prefucture}',")
              .Append($" ADDRESS = '{vm.Address}'")
              .Append(" WHERE")
              .Append($" ACCOUNT_ID = '{vm.AccountId}'");

            return sb.ToString();
        }
    }
}