using Core.Models;
using DAL.Interfaces;
using InsuranceDiscountsWeb.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class UserRepository: IUserRepository
    {
        private UserManager<IdentityUser> userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel registerModel)
        {
            if(registerModel == null)
            {
                throw new ArgumentNullException("RegisterModel is null");
            }

            if(registerModel.Password != registerModel.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Confirm apassword doesn't match the password",
                    IsSuccess = false
                };
            }

            var identityUser = new IdentityUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email
            };

            var result = await userManager.CreateAsync(identityUser, registerModel.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created Succefully",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "User didn't created",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
