using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestApp8.Models;

namespace TestApp8.DataModels
{
    public class ProfileEditModel
    {
        public int AccountId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public string Prefucture { get; set; }
        public string Address { get; set; }

        public string Test { get; set; }
        public List<ProfileViewModel> profileViewList { get; set; }
    }
}