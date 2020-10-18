using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Sol_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sol_Demo.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService sessionStorageService = null;

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            this.sessionStorageService = sessionStorageService;
        }

        private ClaimsIdentity GetClaimsIdentity(UserModel userModel)
        {
            ClaimsIdentity claimsIdentity;
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userModel.EmailId));
            claims.Add(new Claim(ClaimTypes.Role, userModel.Role));

            claimsIdentity = new ClaimsIdentity(claims, "apiauth_type");
            return claimsIdentity;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity claimsIdentity = null;
            var sessionUserModel = await this.sessionStorageService.GetItemAsync<String>("UserModel");

            if (sessionUserModel != null)
            {
                var userModel = JsonConvert.DeserializeObject<UserModel>(sessionUserModel);
                claimsIdentity = GetClaimsIdentity(userModel);
            }
            else
            {
                claimsIdentity = new ClaimsIdentity();
            }

            var user = new ClaimsPrincipal(claimsIdentity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(UserModel userModel)
        {
            var claimsIdentity = GetClaimsIdentity(userModel);

            var user = new ClaimsPrincipal(claimsIdentity);

            base.NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLogout()
        {
            sessionStorageService.RemoveItemAsync("UserModel");

            var claimsIdentity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(claimsIdentity);

            base.NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}