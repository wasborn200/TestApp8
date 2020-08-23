using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestApp8.Dao;
using TestApp8.Models;

namespace TestApp8.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        /// <summary>
        /// タスク作成画面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            TempData["username"] = User.Identity.Name;

            return View();
        }

        /// <summary>
        /// タスク作成
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(TaskViewModel vm)
        {
            try
            {
                vm.AccountId = getAccountId(); 
                createTask(vm);

                return RedirectToAction("index", "tasklist");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "タスクを作成することが出来ませんでした。");
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

        /// <summary>
        /// タスク登録処理
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private static void createTask(TaskViewModel vm)
        {
            //SQLServerの接続開始
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();

            dbAccess.beginTransaciton();
            try
            {
                TaskDao dao = new TaskDao();
                if (dao.createTask(vm, cmd, dbAccess) > 0)
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