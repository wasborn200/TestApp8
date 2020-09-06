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
    public class ProfileEditController : Controller
    {
        public ActionResult Index(ProfileViewModel vm)
        {
            ProfileEditViewModel profileEditViewModel = new ProfileEditViewModel();

            profileEditViewModel.Name = vm.Name;
            profileEditViewModel.Email = vm.Email;

            // 都道県ドロップダウンを取得
            var selectOptions = Util.PrefuctureDropDown.getSelectListItem();
            ViewBag.SelectOptions = selectOptions;

            // Prefuctureから値を取得し、ドロップダウンを選択した状態にする

            if (!(vm.Prefucture == null))
            {
                string pname = vm.Prefucture;
                string pnumber = vm.Prefucture = selectOptions.Where(p => p.Text == pname).First().Value;
                profileEditViewModel.Prefucture = pnumber;
            }

            profileEditViewModel.Address = vm.Address;


            // 登録されている都道府県のselectedをtrueにしようとしたが、keyが無いといわれ失敗
            //var selectOptions = Util.PrefuctureDropDown.getSelectListItem();
            //string pname = vm.Prefucture;
            //string prefucture = vm.Prefucture = selectOptions.Where(p => p.Text == pname).First().Value;
            //var selected = selectOptions.Where(p => p.Value == prefucture).FirstOrDefault();
            //selected.Selected = true;

            return View(profileEditViewModel);
        }

        public ActionResult Edit(ProfileEditViewModel vm)
        {

            try
            {
                int accountId = getAccountIdFromCookie();
                vm.AccountId = accountId;
                editProfile(vm);
                TempData["message"] = "プロフィールが変更されました";
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "プロフィールを変更することが出来ませんでした。");
            }
            return RedirectToAction("Index", "Profile");
        }


        private static void editProfile(ProfileEditViewModel vm)
        {
            //SQLServerの接続開始
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();

            dbAccess.beginTransaciton();
            try
            {
                ProfileDao dao = new ProfileDao();
                if (dao.editProfile(vm, cmd, dbAccess) > 0)
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

        private int getAccountIdFromCookie()
        {
            FormsIdentity id = (FormsIdentity)HttpContext.User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            string MyUserData = ticket.UserData;
            TempData["message"] = $"アカウントID：{MyUserData}";
            int accountId = int.Parse(ticket.UserData);
            return accountId;
        }

    }
}