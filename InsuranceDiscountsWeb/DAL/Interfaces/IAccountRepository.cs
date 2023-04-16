﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAccountRepository
    {
        public Task<bool> Register(RegisterModel registerModel);
        public Task<string> LogIn(LoginModel loginModel);
    }
}