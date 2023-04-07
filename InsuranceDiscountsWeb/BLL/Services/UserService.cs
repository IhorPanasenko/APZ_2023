using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using InsuranceDiscountsWeb.Managers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private ILogger<UserService> logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
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
