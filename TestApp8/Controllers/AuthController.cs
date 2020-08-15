using TestApp8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using TestApp8.DataModels;
using System.Text;
using TestApp8.Dao;

namespace TestApp8.Controllers
{
    public class AuthController : Controller
    {
        /// <summary>
        /// ログイン　表示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(AuthViewModel vm)
        {
            if (isMatchAccount(vm))
            {
                // ユーザー認証　成功
                FormsAuthentication.SetAuthCookie(vm.Id, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // ユーザー認証　失敗
                this.ModelState.AddModelError(string.Empty, "指定されたユーザー名またはパスワードが正しくありません。");
                return this.View(vm);
            }
        }

        public ActionResult SingIn(AuthViewModel vm)
        {

        }

        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Auth", "Index");
        }

        private static bool isMatchAccount(AuthViewModel vm)
        {
            AuthModel loginuser = getPassword(vm);
            if (vm.Password == loginuser.Password)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static AuthModel getPassword(AuthViewModel vm)
        {
            try
            {
                AuthModel loginuser;
                //SQLServerの接続開始
                SqlConnection dbaccess = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                SqlCommand cmd = dbaccess.CreateCommand();
                dbaccess.Open();
                try
                {
                    AuthDao dao = new AuthDao();
                    loginuser = dao.getPassword(vm, dbaccess, cmd);
                }
                catch
                {
                    throw;
                }
                dbaccess.Close();
                return loginuser;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}