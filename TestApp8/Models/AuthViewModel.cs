using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp8.Models
{
    public class AuthViewModel
    {
        public int AccountId { get; set; }

        [Display(Name = "ユーザーネーム")]
        [Required(ErrorMessage = "ユーザーネームは必須入力です。")]
        public string Name { get; set; }

        [Display(Name = "パスワード")]
        [Required(ErrorMessage = "パスワードは必須入力です。")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Emailは必須入力です。")]
        public string Email { get; set; }

        public List<AuthViewModel> authViewList { get; set; }

    }
}