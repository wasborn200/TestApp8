using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TestApp8.Dao
{
    public class DbAccess
    {
        public SqlConnection sqlCon { get; set; }
        public SqlTransaction sqlTran { get; set; }
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

        public void beginTransaciton()
        {
            try
            {
                if(this.sqlCon.State != ConnectionState.Open)
                {
                    this.sqlCon.Open();
                }
                this.sqlTran = this.sqlCon.BeginTransaction();
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

        public int executeNonQuery(SqlCommand cmd)
        {
            try
            {
                if(this.sqlCon.State != ConnectionState.Open)
                {
                    this.sqlCon.Open();
                }
                if(this.sqlTran != null)
                {
                    cmd.Transaction = this.sqlTran;
                }
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}