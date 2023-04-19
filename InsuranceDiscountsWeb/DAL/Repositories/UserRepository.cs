using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using InsuranceDiscountsWeb.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class UserRepository: IUserRepository
    {
        private UserManager<IdentityUser> userManager;
        private IConfiguration configuration;
        private IMailRepository mailRepository;
        public UserRepository(UserManager<IdentityUser> userManager, IConfiguration configuration, IMailRepository mailRepository)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mailRepository = mailRepository;
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            return null;
        }

        public Task<UserManagerResponse> LoginUserAsync(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Task<UserManagerResponse> RegisterUserAsync(RegisterModel registerModel)
        {
            throw new NotImplementedException();
        }
    }
}
