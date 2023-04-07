﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class User :IdentityUser
    {
        public int UserId { get;set; }
        public string FirstName { get;set; }
        public string LastName { get;set; }
        public string Email { get; set; }
        public string PhoneNumbers { get; set; }  
    }
}
