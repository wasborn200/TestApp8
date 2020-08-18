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
using System.Windows.Forms;

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
                FormsAuthentication.SetAuthCookie(vm.Name, true);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                // ユーザー認証　失敗
                this.ModelState.AddModelError(string.Empty, "指定されたユーザー名またはパスワードが正しくありません。");
                return this.View(vm);
            }
        }

        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(AuthViewModel vm)
        {
            try
            {
                InsertAccount(vm);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "アカウントを登録することが出来ませんでした。");
            }
            return this.View(vm);
        }

        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Auth");
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
                AuthModel loginuser;
                //SQLServerの接続開始
                SqlConnection dbaccess = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
                SqlCommand cmd = dbaccess.CreateCommand();
                dbaccess.Open();
                try
                {
                    AuthDao dao = new AuthDao();
                    loginuser = dao.getPassword(vm, cmd);
                }
                catch
                {
                    throw;
                }
                dbaccess.Close();
                return loginuser;
        }

        private static void InsertAccount(AuthViewModel vm)
        {
            //SQLServerの接続開始
            SqlConnection dbaccess = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            SqlCommand cmd = dbaccess.CreateCommand();
            dbaccess.Open();
            try
            {
                AuthDao dao = new AuthDao();
                dao.InsertAccount(vm, cmd);
            }
            catch (Exception)
            {
                throw;
            }
            dbaccess.Close();

        }

    }

}