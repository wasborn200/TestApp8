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
            if (!(profileList.Prefucture == null))
            {
                string pnumber = profileList.Prefucture;
                var selectoptions = Util.PrefuctureDropDown.getSelectListItem();
                vm.Prefucture = selectoptions.Where(p => p.Value == pnumber).First().Text;
            }
            vm.Address = profileList.Address;

            return View("index", vm);
        }

        public ActionResult Edit(string name, string email, string prefucture, string address)
        {

            ProfileViewModel vm = new ProfileViewModel();
            vm.Name = name;
            vm.Email = email;
            vm.Prefucture = prefucture;
            vm.Address = address;

            ViewBag.SelectOptions = Util.PrefuctureDropDown.getSelectListItem();

            return RedirectToAction("index", "ProfileEdit",vm);
        }

        //[HttpPost]
        //public ActionResult Edit(ProfileViewModel vm)
        //{

        //    // valueのほうが値に入っているからstringにしてintに変更する必要があると思われる
        //    string name = vm.Name;
        //    string str = vm.Prefucture;
        //    return View("index", "ProfileEdit", vm);
        //}

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