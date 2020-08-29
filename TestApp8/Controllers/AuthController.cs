using TestApp8.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using TestApp8.Dao;
using System.Windows.Forms;

namespace TestApp8.Controllers
{
    public class AuthController : Controller
    {

        #region ログイン・ログアウト

        /// <summary>
        /// ログイン画面
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
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(AuthViewModel vm)
        {
            if (isMatchAccount(vm))
            {
                string encTicket = setAccountIdatCookie(vm);
                Response.Cookies.Add(new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                return RedirectToAction("Index", "Home");

            }
            else
            {
                // ユーザー認証　失敗
                this.ModelState.AddModelError(string.Empty, "指定されたユーザー名またはパスワードが正しくありません。");
                return this.View(vm);
            }
        }

        /// <summary>
        /// クッキーにアカウントIDを登録する
        ///TODO true=remember me　false=永続的ではない　後に機能分け実装
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private string setAccountIdatCookie(AuthViewModel vm)
        {
            vm.AccountId = getAccountId(vm);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                vm.Name, DateTime.Now, DateTime.Now.AddMinutes(30),
                true, vm.AccountId.ToString(), FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);

            return encTicket;
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


        #endregion

        #region アカウント登録

        /// <summary>
        /// アカウント登録画面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// アカウント登録処理
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(AuthViewModel vm)
        {
            try
            {
                registerAccount(vm);
                TempData["message"] = "アカウントが登録されました";
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "アカウントを登録することが出来ませんでした。");
            }
            return this.View(vm);
        }

        #endregion

        #region 独自メソッド

        /// <summary>
        /// アカウント認証
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private bool isMatchAccount(AuthViewModel vm)
        {
            //SQLServerの接続開始
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();
            try
            {
                AuthDao dao = new AuthDao();
                if(dao.isMatchAccount(dbAccess, vm, cmd) > 0)
                {
                    dbAccess.close();
                    return true;
                }
                else
                {
                    dbAccess.close();
                    return false;
                }
            }
            catch
            {
                dbAccess.close();
                throw;
            }
        }

        /// <summary>
        /// アカウントID取得
        /// </summary>
        /// <param name="vm">アカウント認証ビューモデル</param>
        /// <returns></returns>
        private int getAccountId(AuthViewModel vm)
        {
            //SQLServerの接続開始
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();
            try
            {
                AuthDao dao = new AuthDao();
                int accountid = dao.getAccountId(dbAccess, vm, cmd);
                dbAccess.close();
                return accountid;
            }
            catch
            {
                dbAccess.close();
                throw;
            }
        }

        /// <summary>
        /// アカウント登録処理
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private static void registerAccount(AuthViewModel vm)
        {
            //SQLServerの接続開始
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();

            dbAccess.beginTransaciton();
            try
            {
                AuthDao dao = new AuthDao();
                if(dao.registerAccount(vm, cmd, dbAccess) > 0)
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

        #endregion

    }

}