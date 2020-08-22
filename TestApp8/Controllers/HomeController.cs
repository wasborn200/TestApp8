using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp8.Dao;
using TestApp8.DataModels;
using TestApp8.Models;

namespace TestApp8.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// ホーム画面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<AuthModel> authList = getAccountList();
            List<AuthViewModel> authViewList = new List<AuthViewModel>();
            foreach (var item in authList)
            {
                AuthViewModel authModel = new AuthViewModel();
                authModel.Name = item.Name;
                authModel.Email = item.Email;
                authViewList.Add(authModel);
            }
            return View("index", authViewList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// アカウントリスト取得
        /// </summary>
        /// <returns></returns>
        private List<AuthModel> getAccountList()
        {
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();
            try
            {
                AuthDao dao = new AuthDao();
                List<AuthModel> authList = dao.getAccountList(dbAccess, cmd);

                dbAccess.close();
                return authList;
            }
            catch (DbException)
            {
                dbAccess.close();
                throw;
            }
        }
    }
}