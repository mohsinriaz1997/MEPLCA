using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.RegularExpressions;

namespace CA.UI.Pages.MasterDataSetup
{
    public partial class GroupSetup
    {
        #region InjectService
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }
        [Inject]
        public IUserProfile _mstUserProfile { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IGroupSetupMaster _mstGroupSetup { get; set; }

        #endregion InjectService

        #region Variables
        private string LoginUserCode = "";
        private bool Loading = false;
        private string searchString1 = "";

        private bool FilterFunc(MstGroupSetup element) => FilterFunc(element, searchString1);

        private MstGroupSetup oModel = new MstGroupSetup();
        private IEnumerable<MstGroupSetup> oList = new List<MstGroupSetup>();

        #endregion Variables

        #region Functions

        private async Task<ApiResponseModel> Save()
        {
            try
            {
                var code = oModel.Code;
                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
                Loading = true;
                var res = new ApiResponseModel();
                await Task.Delay(3);
                if (!string.IsNullOrWhiteSpace(oModel.Description))
                {
                    if (!regexItem.IsMatch(code))
                    {
                        Snackbar.Add("Code  Must be in proper formet ", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        Loading = false;
                        return null;
                    }
                    if (oList.Where(x => x.Code == oModel.Code).Count() > 0)
                    {
                        Snackbar.Add("Code already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        Loading = false;
                        return null;
                    }
                    if (oList.Where(x => x.Description == oModel.Description).Count() > 0)
                    {
                        Snackbar.Add("Description already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        Loading = false;
                        return null;
                    }
                    else
                    {
                        if (oModel.Id == 0)
                        {
                            //oModel.Type = "aa";
                            res = await _mstGroupSetup.Insert(oModel, "manager");
                        }
                        else
                        {
                            res = await _mstGroupSetup.Update(oModel, "manager");
                        }
                    }
                    if (res != null && res.Id == 1)
                    {
                        Snackbar.Add(res.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                        await Task.Delay(3000);
                        Navigation.NavigateTo("/GroupSetup", forceLoad: true);
                    }
                    else
                    {
                        Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    oModel.FlgActive = true;
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

        private async void Reset()
        {
            try
            {
                Loading = true;
                await Task.Delay(3);
                Navigation.NavigateTo("/GroupSetup", forceLoad: true);
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }

        private async Task GetAllDesignation()
        {
            try
            {
                oList = await _mstGroupSetup.GetAllData();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private bool FilterFunc(MstGroupSetup element, string searchString1)
        {

            if (string.IsNullOrWhiteSpace(searchString1))
                return true;
            if (element.Description.Contains(searchString1, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.FlgActive.Equals(searchString1))
                return true;
            return false;



        }

        public void RemoveRecord(int LineNum)
        {
            try
            {
                var res = oList.Where(x => x.Id != LineNum);
                oList = res;
                if (oList.Count() == 0)
                {
                    oModel = new MstGroupSetup();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void EditRecord(int LineNum)
        {
            try
            {
                var res = oList.Where(x => x.Id == LineNum).FirstOrDefault();
                if (res != null)
                {
                    oModel.Code = res.Code;
                    oModel.Id = res.Id;
                    oModel.Description = res.Description;
                    oModel.Type = res.Type;
                    oModel.FlgActive = res.FlgActive;
                    oList = oList.Where(x => x.Id != LineNum);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        #endregion Functions

        #region Events

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Loading = true;
                oModel.FlgActive = true;
                await GetAllDesignation();
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
        //        if (Session != null)
        //        {
        //            var res = await _mstUserProfile.FetchUserAuth(Session.Id);
        //            if (res.Where(x => x.MenuName == "Group Setup" && x.UserRights != 1).ToList().Count > 0)
        //            {
        //                LoginUserCode = Session.UserCode;
        //            }
        //            else
        //            {
        //                Navigation.NavigateTo("/Login", forceLoad: true);
        //            }
        //        }
        //        else
        //        {
        //            Navigation.NavigateTo("/Login", forceLoad: true);
        //        }

        //    }
        //}

        #endregion Events
    }
}