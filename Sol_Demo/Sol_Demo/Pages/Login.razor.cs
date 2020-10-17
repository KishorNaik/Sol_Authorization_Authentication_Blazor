using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Sol_Demo.Data;
using Sol_Demo.Models;
using Sol_Demo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Pages
{
    public partial class Login
    {
        #region Constructor

        public Login()
        {
            UserM = new UserModel();
        }

        #endregion Constructor

        #region Public Method

        [Inject]
        public IUserLoginService UserLoginService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationSP { get; set; }

        [Inject]
        public NavigationManager NavigationM { get; set; }

        [Inject]
        public ISessionStorageService SessionStorage { get; set; }

        #endregion Public Method

        #region Private Method

        private UserModel UserM { get; set; }

        private String ErrorMessage { get; set; }

        #endregion Private Method

        #region Private Event Handler

        private async Task OnLogIn(EditContext editContext)
        {
            var flag = editContext.Validate();
            if (flag == false) return;

            // Call User Login service (Assume that Here you are call Login Api)
            var response = await UserLoginService?.LoginAsync(this.UserM);

            if (response != null)
            {
                ((CustomAuthenticationStateProvider)AuthenticationSP).MarkUserAsAuthenticated(response);

                var userModelJson = JsonConvert.SerializeObject(response);
                await SessionStorage.SetItemAsync<String>("UserModel", userModelJson);

                NavigationM.NavigateTo("/index");
            }
            else
            {
                ErrorMessage = "User Name & Password Does not Match";
                base.StateHasChanged();
            }
        }

        #endregion Private Event Handler
    }
}