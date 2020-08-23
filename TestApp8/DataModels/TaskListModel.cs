using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp8.DataModels
{
    public class TaskListModel
    {
        public int TaskId { get; set; }
        public int AccountId { get; set; }

        public string Title { get; set; }

        public string Memo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}