using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp8.Models
{
    public class AuthViewModel
    {
        [Display(Name = "ユーザーネーム")]
        [Required(ErrorMessage = "ユーザーネームは必須入力です。")]
        public string Name { get; set; }

        [Display(Name = "パスワード")]
        [Required(ErrorMessage = "パスワードは必須入力です。")]
        public string Password { get; set; }

    }
}