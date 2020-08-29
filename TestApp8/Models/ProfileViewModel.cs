using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp8.Models
{
    public class ProfileViewModel
    {
        public int AccountId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "都道府県")]
        public int Prefucture { get; set; }
        [Display(Name = "住所")]
        public string Address { get; set; }
        public List<ProfileViewModel> profileViewList { get; set; }
    }
}