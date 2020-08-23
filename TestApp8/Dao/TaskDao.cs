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
    public class TaskDao
    {
        /// <summary>
        /// アカウント登録
        /// </summary>
        /// <param name="dbAccess">DBアクセス設定</param>
        /// <param name="vm">タスクビューモデル</param>
        /// <param name="cmd">SQLクエリ</param>
        /// <returns></returns>
        public int createTask(TaskViewModel vm, SqlCommand cmd, DbAccess dbAccess)
        {
            cmd.CommandText = this.getCreateTaskQuery(vm);

            return dbAccess.executeNonQuery(cmd);

        }

        public List<TaskListModel> getTaskList(int accountId, DbAccess dbAccess, SqlCommand cmd)
        {
            cmd.CommandText = this.getTaskListSelectQuery(accountId);

            DataTable dt = new DataTable();
            dt = dbAccess.executeQuery(cmd);

            List<TaskListModel> taskList = this.getTaskListBindDataTable(dt);

            return taskList;
        }

        private List<TaskListModel> getTaskListBindDataTable(DataTable dt)
        {
            List<TaskListModel> authList = new List<TaskListModel>();

            foreach (DataRow dr in dt.Rows)
            {
                TaskListModel taskListModel = new TaskListModel();

                if (!(dr["TITLE"] is DBNull))
                {
                    taskListModel.Title = Convert.ToString(dr["TITLE"]);
                }
                if (!(dr["MEMO"] is DBNull))
                {
                    taskListModel.Memo = Convert.ToString(dr["MEMO"]);
                }
                authList.Add(taskListModel);

            }
            return authList;
        }


        /// <summary>
        /// アカウント登録用クエリ作成
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private string getCreateTaskQuery(TaskViewModel vm)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" INSERT INTO")
              .Append(" TASK")
              .Append(" (ACCOUNT_ID,")
              .Append(" TITLE,")
              .Append(" MEMO)")
              .Append(" VALUES")
              .Append($" ('{vm.AccountId}',")
              .Append($" '{vm.Title}',")
              .Append($" '{vm.Memo}')");

            return sb.ToString();
        }

        private string getTaskListSelectQuery(int accountID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT")
              .Append(" TITLE,")
              .Append(" MEMO")
              .Append(" FROM")
              .Append(" TASK")
              .Append(" WHERE")
              .Append($" ACCOUNT_ID = '{accountID}'");

            return sb.ToString();
        }
    }
}