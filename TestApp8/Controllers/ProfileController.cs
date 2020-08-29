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
            List<ProfileModel> profileList = getProfileList(accountId);
            List<ProfileViewModel> profileViewList = new List<ProfileViewModel>();
            foreach (var item in profileList)
            {
                ProfileViewModel profileModel = new ProfileViewModel();
                profileModel.Name = item.Name;
                profileModel.Email = item.Email;
                profileViewList.Add(profileModel);
            }

            return View("index", profileViewList);
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

            return View("edit", vm);
        }

        private List<ProfileModel> getProfileList(int accountId)
        {
            DbAccess dbAccess = new DbAccess();
            SqlCommand cmd = dbAccess.sqlCon.CreateCommand();
            try
            {
                ProfileDao dao = new ProfileDao();
                List<ProfileModel> profileList = dao.getProfileList(dbAccess, cmd, accountId);

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