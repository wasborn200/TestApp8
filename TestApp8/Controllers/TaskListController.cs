using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using TestApp8.Dao;
using TestApp8.DataModels;
using TestApp8.Models;

namespace TestApp8.Controllers
{
    [Authorize]
    public class TaskListController : Controller
    {
        /// <summary>
        /// タスク一覧画面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int accountId = getAccountId();
            List<TaskListViewModel> taskListViewList = search(accountId);

            return View("index", taskListViewList);
        }

        public ActionResult Delete(TaskListViewModel vm)
        {
            try
            {
                string id = (string)RouteData.Values["id"];
                vm.TaskId = int.Parse(id);
                deleteTask(vm);

                int accountId = getAccountId();
                List<TaskListViewModel> taskListViewList = search(accountId);
                TempData["deletemessage"] = "タスクを削除しました。";
                return View("index", taskListViewList);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "タスクを削除することが出来ませんでした。");
                throw;
            }

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

        private List<TaskListViewModel> search(int accountId)
        {
            List<TaskListModel> taskList = getTaskList(accountId);

            List<TaskListViewModel> taskListViewList = new List<TaskListViewModel>();
            foreach (var item in taskList)
            {
                TaskListViewModel taskListViewModel = new TaskListViewModel();
                taskListViewModel.TaskId = item.TaskId;
                taskListViewModel.Title = item.Title;
                taskListViewModel.Memo = item.Memo;
                taskListViewList.Add(taskListViewModel);
            }

            return taskListViewList;
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
        private static void deleteTask(TaskListViewModel vm)
        {
            //SQLServerの接続開始
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();

            dbAccess.beginTransaciton();
            try
            {
                TaskDao dao = new TaskDao();
                if (dao.deleteTask(vm, cmd, dbAccess) > 0)
                {
                    dbAccess.sqlTran.Commit();
                }
                else
                {
                    dbAccess.sqlTran.Rollback();
                }

                dbAccess.close();
            }
            catch (Exception)
            {
                dbAccess.close();
                throw;
            }
        }
    }

}