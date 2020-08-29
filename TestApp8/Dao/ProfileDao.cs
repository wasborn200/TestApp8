using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TestApp8.DataModels;

namespace TestApp8.Dao
{
    public class ProfileDao
    {
        public List<ProfileModel> getProfileList(DbAccess dbAccess, SqlCommand cmd, int accountId)
        {
            cmd.CommandText = this.getProfileListSelectQuery(accountId);

            DataTable dt = new DataTable();
            dt = dbAccess.executeQuery(cmd);

            List<ProfileModel> profileList = this.getProfileListBindDataTable(dt);

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

        private List<ProfileModel> getProfileListBindDataTable(DataTable dt)
        {

            List<ProfileModel> profileList = new List<ProfileModel>();

            foreach (DataRow dr in dt.Rows)
            {
                ProfileModel profileListModel = new ProfileModel();

                if (!(dr["NAME"] is DBNull))
                {
                    profileListModel.Name = Convert.ToString(dr["NAME"]);
                }
                if (!(dr["EMAIL"] is DBNull))
                {
                    profileListModel.Email = Convert.ToString(dr["EMAIL"]);
                }
                if (!(dr["PREFUCTURE"] is DBNull))
                {
                    profileListModel.Prefucture = Convert.ToInt32(dr["PREFUCTURE"]);
                }
                if (!(dr["ADDRESS"] is DBNull))
                {
                    profileListModel.Address = Convert.ToString(dr["ADDRESS"]);
                }
                profileList.Add(profileListModel);

            }
            return profileList;
        }
    }
}