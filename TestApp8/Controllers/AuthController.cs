using TestApp8.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using TestApp8.Dao;

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
        public ActionResult Signin()
        {
            return View();
        }


        /// <summary>
        /// アカウント登録処理
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Signin(AuthViewModel vm)
        {
            try
            {
                InsertAccount(vm);
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
        /// アカウント登録処理
        /// </summary>
        /// <param name="vm">認証ビューモデル</param>
        /// <returns></returns>
        private static void InsertAccount(AuthViewModel vm)
        {
            //SQLServerの接続開始
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();

            dbAccess.beginTransaciton();
            try
            {
                AuthDao dao = new AuthDao();
                if(dao.InsertAccount(vm, cmd, dbAccess) > 0)
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