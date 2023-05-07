using Core.Models;
using Core.Models.UpdateModels;
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
        public Task<List<AppUser>> GetAllUsers();
        public Task<AppUser?> GetUserByEmail(string email);
        public Task<AppUser?> GetUserById(string userId);
        public Task<bool> UpdateUser(UpdateAppUserModel user);
        public Task<bool> DeleteUser(string email);
    }
}
