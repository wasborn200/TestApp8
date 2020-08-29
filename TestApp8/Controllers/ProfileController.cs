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
    [Authorize]
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            int accountId = getAccountIdFromCookie();
            ProfileModel profileList = getProfileList(accountId);
            ProfileViewModel vm = new ProfileViewModel();
            vm.Name = profileList.Name;
            vm.Email = profileList.Email;
            vm.Prefucture = profileList.Prefucture;
            vm.Address = profileList.Address;

            return View("index", vm);
        }

        [HttpGet]
        public ActionResult Edit(string name, string email, int? prefucture, string address)
        {

            ProfileViewModel vm = new ProfileViewModel();
            vm.Name = name;
            vm.Email = email;
            if(!(prefucture == null))
            {
            vm.Prefucture = (int)prefucture;
            }
            vm.Address = address;

            ViewBag.SelectOptions = new SelectListItem[] {
            new SelectListItem() { Value="jQuery Tips", Text="Tips" },
            new SelectListItem() { Value="jQuery リファレンス ", Text="jQuery リファレンス " },
             new SelectListItem() { Value="jQuery サンプル集 ", Text="jQuery サンプル集 " }
            };



            return View("edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(ProfileViewModel vm)
        {

            // valueのほうが値に入っているからstringにしてintに変更する必要があると思われる
            string name = vm.Name;
            string str = vm.Test;
            return View("index");
        }

        private ProfileModel getProfileList(int accountId)
        {
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();
            try
            {
                ProfileDao dao = new ProfileDao();
                ProfileModel profileList = dao.getProfileList(dbAccess, cmd, accountId);

                dbAccess.close();
                return profileList;
            }
            catch (DbException)
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