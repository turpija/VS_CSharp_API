﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.MVC.Models
{
    public class PersonView
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}