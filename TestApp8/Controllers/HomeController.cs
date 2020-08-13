using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp8.Models;

namespace TestApp8.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<HomeModel> homelist = new List<HomeModel>();
            using (var conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM test";
                conn.Open();
                using (var sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            var id = sdr["ID"];
                            var name = sdr["NAME"].ToString();
                            ViewData["id"] = id;
                            ViewData["name"] = name;
                        }
                    }
                }
            }
            return View();
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
    }
}