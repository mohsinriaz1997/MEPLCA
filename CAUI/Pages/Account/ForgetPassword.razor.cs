using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.Account
{
    public partial class ForgetPassword
    {
        #region InjectService

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IUserProfile _mstUser { get; set; }

        #endregion

        #region Variables

        bool Loading = false;
        private bool DisplayEmail = true;
        private bool DisplayOTP = false;
        private bool DisplayChangePassword = false;
        private static System.Timers.Timer aTimer;
        private int counter = 120;
        private string TimerMessage = "";
        private string ConfirmPassword = "";

        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        MstUserProfile oModel = new MstUserProfile();
        PasswordReset oPassword = new PasswordReset();

        #endregion

        #region Functions

        void VisiblePassword()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }

        private async Task<ApiResponseModel> SendOTP()
        {
            try
            {
                Loading = true;
                var res = new ApiResponseModel();
                await Task.Delay(1);
                if (!string.IsNullOrWhiteSpace(oModel.EmailId))
                {
                    oModel.UserName = "";
                    oModel.UserCode = "";
                    oModel.Password = "";
                    oModel.FlgActive = true;
                    oModel.FlgPasswordRequest = false;
                    res = await _mstUser.GenerateOTP(oModel);

                    if (res != null && res.Id == 1)
                    {
                        Snackbar.Add(res.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                        DisplayOTP = true;
                        DisplayEmail = false;
                        StartTimer();
                        //Navigation.NavigateTo("/Dashboard", forceLoad: true);
                    }
                    else
                    {
                        Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                else
                {
                    Snackbar.Add("Please enter Email", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
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

        private async Task<ApiResponseModel> AuthenticateOTP()
        {
            try
            {
                Loading = true;
                var res = new ApiResponseModel();
                await Task.Delay(1);
                if (!string.IsNullOrWhiteSpace(oModel.EmailId))
                {
                    oPassword.Email = oModel.EmailId;
                    oPassword.UserCode = "";
                    oPassword.FlgActive = true;
                    res = await _mstUser.AuthenticateOTP(oPassword);

                    if (res != null && res.Id == 1)
                    {
                        Snackbar.Add(res.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                        DisplayChangePassword = true;
                        DisplayOTP = false;
                        DisplayEmail = false;
                        //Navigation.NavigateTo("/Dashboard", forceLoad: true);
                    }
                    else
                    {
                        Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    oModel.FlgActive = true;
                }
                else
                {
                    Snackbar.Add("Please enter Email", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
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

        private async Task<ApiResponseModel> ChangePassword()
        {
            try
            {
                Loading = true;
                var res = new ApiResponseModel();
                await Task.Delay(1);
                if (!string.IsNullOrWhiteSpace(oModel.Password) && !string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    if (oModel.Password == ConfirmPassword)
                    {
                        oModel.UserCode = "";
                        oModel.UserName = "";
                        oModel.FlgActive = true;
                        oModel.FlgPasswordRequest = true;
                        res = await _mstUser.ChangePassword(oModel);

                        if (res != null && res.Id == 1)
                        {
                            Snackbar.Add(res.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                            Navigation.NavigateTo("/Login", forceLoad: true);
                        }
                        else
                        {
                            Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                    }
                    else
                    {
                        Snackbar.Add("Password doesn't match.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                else
                {
                    Snackbar.Add("Please enter Email", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
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

        void RequestNew()
        {
            try
            {
                Loading = true;
                oModel.EmailId = "";
                oPassword.EncryptKey = "";
                DisplayEmail = true;
                DisplayOTP = false;
                DisplayChangePassword = false;
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        #endregion

        #region Events

        public void StartTimer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += CountDownTimer;
            aTimer.Enabled = true;
        }
        public void CountDownTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (counter > 0)
            {
                counter -= 1;
                TimerMessage = "Time remaining: " + counter + " seconds...";
            }
            else
            {
                aTimer.Enabled = false;
                TimerMessage = "OTP Expired, request a new one.";
            }
            InvokeAsync(StateHasChanged);
        }

        #endregion

    }
}