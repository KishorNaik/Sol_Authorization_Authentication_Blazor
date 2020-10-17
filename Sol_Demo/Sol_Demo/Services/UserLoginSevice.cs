using Sol_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Services
{
    public sealed class UserLoginSevice : IUserLoginService
    {
        Task<UserModel> IUserLoginService.LoginAsync(UserModel userModel)
        {
            return Task.Run(() =>
            {
                // Demo Purpose
                string tempEmailId = "kishor@gmail.com";
                string tempPassword = "123";

                if (userModel.EmailId == tempEmailId && userModel.Password == tempPassword)
                {
                    userModel.Password = null;
                    userModel.Role = "Admin";
                }
                else
                {
                    userModel = null;
                }

                return userModel;
            });
        }
    }
}