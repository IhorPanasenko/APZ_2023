using Core.Models;
using InsuranceDiscountsWeb.Managers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    { 
        public Task<List<AppUser>> GetAllUsers();
        public Task<AppUser?> GetUserByEmail(string email);
        public Task<AppUser?> GetUserById(string userId);
        public Task<bool> UpdateUser(AppUser user);
        public Task<bool> DeleteUser(AppUser user);
    }
}
