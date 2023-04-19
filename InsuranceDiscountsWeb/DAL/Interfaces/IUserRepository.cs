using Core.Models;
using InsuranceDiscountsWeb.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterModel registerModel);
        Task<UserManagerResponse> LoginUserAsync(LoginModel loginModel);
        //Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
    }
}
