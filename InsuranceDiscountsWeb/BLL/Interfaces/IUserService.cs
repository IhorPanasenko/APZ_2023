using Core.Models;
using InsuranceDiscountsWeb.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface  IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterModel registerModel);
        Task<UserManagerResponse> LoginUserAsync(LoginModel loginModel);
    }
}
