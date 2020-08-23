using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp8.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public int AccountId { get; set; }

        [Display(Name = "タイトル")]
        [Required(ErrorMessage = "タイトルは必須入力です。")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Memo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}