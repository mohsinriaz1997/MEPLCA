using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.Account
{
    public partial class Login
    {
        private bool Loading = false;

        [Inject]
        public IUserProfile _userProfileService { get; set; }

        [Inject]
        public NavigationManager navigation { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }


        MstUserProfile oModel = new MstUserProfile();
        List<MstUserProfile> oMstUserProfile = new List<MstUserProfile>();

        //async Task<MstUserProfile> validatelogin()
        //{
        //    try
        //    {
        //        _processing = true;
        //        var res = await _userProfileService.ValidateLogin(oUser);

        //        if (res != null)
        //        {
        //            navigation.NavigateTo("/Dashboard");
        //            _processing = false;
        //            return oUser;
        //        }
        //        else
        //        {
        //            oUser = new MstUserProfile();
        //            Snackbar.Add("Credentials doesn't match", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
        //            _processing = false;
        //            return null;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //        return null;
        //    }

        //}


        private async Task<MstUserProfile> ValidateLogin()
        {
            try
            {
                Loading = true;
                var res = new MstUserProfile();
                //oMstUserProfile = await _userProfileService.GetAllData();
                await Task.Delay(1);
                var usercode = oModel.UserCode;
                if (!string.IsNullOrWhiteSpace(oModel.UserCode) && !string.IsNullOrWhiteSpace(oModel.Password))
                {
                    //oModel.UserCode = "";
                    //oModel.Email = "";
                    //oModel.FlgActive = true;
                    //oModel.FlgPasswordRequest = false;
                    res = await _userProfileService.ValidateLogin(oModel);
                    if (res != null)
                    {
                        //res.Password = oModel.Password;
                        int pass = string.Compare(oModel.Password, res.Password, StringComparison.Ordinal);
                        if (pass != 0)
                        {
                            Snackbar.Add("Username/Password incorrect", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        else
                        {
                            Snackbar.Add("Welcome: " + res.UserCode, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                            navigation.NavigateTo("/Dashboard", forceLoad: true);
                        }

                    }
                    else
                    {
                        Snackbar.Add("Username/Password incorrect", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                else
                {
                    Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                }
                Loading = false;
                return res;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
                return null;
            }
        }


        async void GotoForgotPage()
        {
            try
            {
                //_processingForget = true;
                await Task.Delay(4000);
                //_processingForget = false;
                navigation.NavigateTo("/forgetPassword", forceLoad: true);
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
    }
}