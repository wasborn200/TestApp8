﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp8.DataModels
{
    public class AuthModel
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}