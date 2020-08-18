using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TestApp8.Dao
{
    public class DbAccess
    {
        public SqlConnection sqlCon { get; set; }
        public DbAccess()
        {
            try
            {
                string sConnection = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
                this.sqlCon = new SqlConnection(sConnection);
                this.sqlCon.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void close()
        {
            this.sqlCon.Close();
            this.sqlCon.Dispose();
        }

        public DataTable executeQuery(SqlCommand cmd)
        {
            try
            {
                SqlDataReader read = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(read);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}