using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp8.DataModels
{
    public class ProfileModel
    {
        public int AccountId { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public int Prefucture { get; set; }
        public string Address { get; set; }
        public List<ProfileModel> profileList { get; set; }
    }
}