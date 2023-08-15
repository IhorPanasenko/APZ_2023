using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        public Task<bool> Register(RegisterModel registerModel);
        public Task<string> LogIn(LoginModel loginModel);
        public Task LogOut();
        public Task<string> ForgotPassword(string email);
        public Task<bool> ResetPassword(ResetPasswordModel resetPassword );
    }
}
