using Blazored.LocalStorage;
using CA.UI.Interfaces.AdministrationData;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CA.API.Models;
using CA.UI.General;
using System.Diagnostics;

namespace CA.UI.Pages.AdministrationDataSetup
{
    public partial class Stage
    {
        #region Inject Service

        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public IDialogService Dialog { get; set; }
        [Inject]
        public IStage _stageService { get; set; }
        [Inject]
        public IUserProfile _userProfileService { get; set; }
        [Inject]
        public NavigationManager navigation { get; set; }
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        #endregion

        #region Variable

        private bool Loading = false;

        private string LoginUserCode = "";
        private IEnumerable<MstUserProfile> AuthorizerNames { get; set; } = new HashSet<MstUserProfile>();

        MstStage oModel = new MstStage();
        private List<MstStage> oList = new List<MstStage>();
        MstUserProfile oUser = new MstUserProfile();
        List<MstUserProfile> oUserList = new List<MstUserProfile>();

        DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        #endregion

        #region Function

        private async Task OpenDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "Stage");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstStage)result.Data;
                    oModel = res;
                    AuthorizerNames = new HashSet<MstUserProfile>();
                    List<MstUserProfile> oList = new List<MstUserProfile>();
                    foreach (var item in oModel.MstStageDetails)
                    {
                        MstUserProfile LineUser = new MstUserProfile();
                        LineUser = oUserList.Where(a => a.Id == item.FkUserId).FirstOrDefault();
                        oList.Add(LineUser);                       
                    }
                    AuthorizerNames = oList;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        async Task<List<MstUserProfile>> GetAllUsers()
        {
            try
            {
                oUserList = await _userProfileService.GetAllData();
                return oUserList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private string GetAuthorizersSelection(List<string> selectedValues)
        {
            try
            {
                if (selectedValues.Count < 1)
                {
                    return $"Please choose Authorizer";
                }
                return $"{selectedValues.Count} Authorizer{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<ApiResponseModel> Save()
        {
            try
            {
                Loading = true;
                var res = new ApiResponseModel();
                await Task.Delay(3);
                if (!string.IsNullOrWhiteSpace(Convert.ToString(oModel.Id)))
                {
                    try
                    {
                        Loading = true;
                        var res1 = new ApiResponseModel();
                        await Task.Delay(1);
                        oList = await _stageService.GetAllData();
                        if (oModel.StageName == null || oModel.NoOfApproval == null || oModel.NoOfRejection == null || AuthorizerNames == null && AuthorizerNames.Count() == 0)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oList.Where(x => x.StageName.ToLower()  == oModel.StageName.ToLower()).Count() > 0 )
                        {
                            Snackbar.Add("Stage Name already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (AuthorizerNames != null && AuthorizerNames.Count() > 0)
                        {
                            oModel.MstStageDetails.Clear();
                            foreach (var Line in AuthorizerNames)
                            {
                                MstStageDetail oLine = new MstStageDetail();
                                oLine.FkUserId = Line.Id;
                                oLine.FlgType = true;
                                oModel.MstStageDetails.Add(oLine);
                            }
                        }
                        if (oModel != null && oModel.MstStageDetails != null && oModel.MstStageDetails.Count > 0)
                        {
                            if (oModel.Id == 0)
                            {
                                oModel.CreatedBy = LoginUserCode;
                                oModel.CreatedDate = DateTime.Now;
                                res1 = await _stageService.Insert(oModel);
                            }
                            else
                            {
                                oModel.UpdatedBy = LoginUserCode;
                                oModel.UpdatedDate = DateTime.Now;
                                res1 = await _stageService.Update(oModel);
                            }
                        }
                        else
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (res1 != null && res1.Id == 1)
                        {
                            Snackbar.Add(res1.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                            await Task.Delay(1000);
                            Navigation.NavigateTo("/Stage", forceLoad: true);
                        }
                        else
                        {
                            Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
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

        #endregion

        #region Initialize

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
                if (Session != null)
                {
                    LoginUserCode = Session.UserCode;
                    Loading = true;
                    await GetAllUsers();
                    Loading = false;
                }
                else
                {
                    navigation.NavigateTo("/Login", true);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        #endregion
    }
}
