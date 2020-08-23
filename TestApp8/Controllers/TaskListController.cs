using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestApp8.Dao;
using TestApp8.DataModels;
using TestApp8.Models;

namespace TestApp8.Controllers
{
    public class TaskListController : Controller
    {
        /// <summary>
        /// タスク一覧画面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int accountId = getAccountId();

            List<TaskListModel> taskList = getTaskList(accountId);

            List<TaskListViewModel> taskListViewList = new List<TaskListViewModel>();
            foreach (var item in taskList)
            {
                TaskListViewModel taskListViewModel = new TaskListViewModel();
                taskListViewModel.Title = item.Title;
                taskListViewModel.Memo = item.Memo;
                taskListViewList.Add(taskListViewModel);
            }

            return View("index", taskListViewList);
        }

        /// <summary>
        /// アカウントID取得
        /// </summary>
        /// <returns></returns>
        public int getAccountId()
        {
            FormsIdentity id = (FormsIdentity)HttpContext.User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            string MyUserData = ticket.UserData;
            return int.Parse(MyUserData);
        }

        /// <summary>
        /// タスク一覧取得
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private List<TaskListModel> getTaskList(int accountId)
        {
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();
            try
            {
                TaskDao dao = new TaskDao();
                List<TaskListModel> taskList = dao.getTaskList(accountId, dbAccess, cmd);

                dbAccess.close();
                return taskList;
            }
            catch (DbException)
            {
                dbAccess.close();
                throw;
            }
        }
    }

}