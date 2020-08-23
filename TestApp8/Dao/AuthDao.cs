using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TestApp8.DataModels;
using TestApp8.Models;

namespace TestApp8.Dao
{
    public class AuthDao
    {
        #region データ取得

        /// <summary>
        /// アカウント認証
        /// </summary>
        /// <param name="dbAccess">DBアクセス設定</param>
        /// <param name="vm">認証ビューモデル</param>
        /// <param name="cmd">SQLクエリ</param>
        /// <returns></returns>
        public int isMatchAccount(DbAccess dbAccess, AuthViewModel vm, SqlCommand cmd)
        {
            cmd.CommandText = this.getAccountSelectQuery(vm);

            DataTable dt = new DataTable();
            dt = dbAccess.executeQuery(cmd);

            int result = getCountAccountBindTable(dt);

            return result;
        }

        public int getAccountId(DbAccess dbAccess, AuthViewModel vm, SqlCommand cmd)
        {
            cmd.CommandText = this.getAccountIdSelectQuery(vm);

            DataTable dt = new DataTable();
            dt = dbAccess.executeQuery(cmd);

            int accountid = getAccountIdBindTable(dt);
            return accountid;
        }

        /// <summary>
        /// アカウントリスト取得
        /// </summary>
        /// <param name="dbAccess">DBアクセス設定</param>
        /// <param name="cmd">SQLクエリ</param>
        /// <returns></returns>
        public List<AuthModel> getAccountList(DbAccess dbAccess, SqlCommand cmd)
        {
            cmd.CommandText = this.getAccountListSelectQuery();

            DataTable dt = new DataTable();
            dt = dbAccess.executeQuery(cmd);

            List<AuthModel> paidList = this.getAccountListBindDataTable(dt);

            return paidList;
        }

        #endregion

        #region 登録

        /// <summary>
        /// アカウント登録
        /// </summary>
        /// <param name="dbAccess">DBアクセス設定</param>
        /// <param name="vm">認証ビューモデル</param>
        /// <param name="cmd">SQLクエリ</param>
        /// <returns></returns>
        public int registerAccount(AuthViewModel vm, SqlCommand cmd, DbAccess dbAccess)
        {
            cmd.CommandText = this.getRegisterQuery(vm);

            return dbAccess.executeNonQuery(cmd);

        }

        #endregion

        #region データテーブル

        /// <summary>
        /// データテーブル型からint型に変換
        /// </summary>
        /// <param name="dt">データテーブル</param>
        /// <returns></returns>
        private int getCountAccountBindTable(DataTable dt)
        {

            DataRow dr = dt.Rows[0];
            int result = Convert.ToInt32(dr["COUNT"]);

            return result;
        }

        private int getAccountIdBindTable(DataTable dt)
        {

            DataRow dr = dt.Rows[0];
            int result = Convert.ToInt32(dr["ACCOUNT_ID"]);

            return result;
        }

        /// <summary>
        /// データテーブル型からList<AuthModel>に変換
        /// </summary>
        /// <param name="dt">データテーブル</param>
        /// <returns></returns>
        private List<AuthModel> getAccountListBindDataTable(DataTable dt)
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

        #endregion

        #region クエリ

        /// <summary>
        /// アカウント認証用クエリ作成
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private string getAccountSelectQuery(AuthViewModel vm)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT")
              .Append(" COUNT(Name) AS COUNT")
              .Append(" FROM")
              .Append(" ACCOUNT")
              .Append(" WHERE")
              .Append($" NAME = '{vm.Name}'")
              .Append($" AND PASSWORD = '{vm.Password}'");

            return sb.ToString();
        }

        private string getAccountIdSelectQuery(AuthViewModel vm)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT")
              .Append(" ACCOUNT_ID")
              .Append(" FROM")
              .Append(" ACCOUNT")
              .Append(" WHERE")
              .Append($" NAME = '{vm.Name}'");

            return sb.ToString();
        }

        /// <summary>
        /// アカウントリスト作成用クエリ作成
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private string getAccountListSelectQuery()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT")
              .Append(" NAME,")
              .Append(" EMAIL")
              .Append(" FROM")
              .Append(" ACCOUNT");

            return sb.ToString();
        }

        /// <summary>
        /// アカウント登録用クエリ作成
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private string getRegisterQuery(AuthViewModel vm)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" INSERT INTO")
              .Append(" ACCOUNT")
              .Append(" (NAME,")
              .Append(" PASSWORD,")
              .Append(" EMAIL)")
              .Append(" VALUES")
              .Append($" ('{vm.Name}',")
              .Append($" '{vm.Password}',")
              .Append($" '{vm.Email}')");

            return sb.ToString();
        }

        #endregion
    }
}