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
    public partial class ActivityType
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
        public IActivityType _mstActivityType { get; set; }

        #endregion InjectService

        #region Variables
        private string LoginUserCode = "";
        private bool Loading = false;
        private string searchString1 = "";

        private bool FilterFunc(MstActivityType element) => FilterFunc(element, searchString1);

        private MstActivityType oModel = new MstActivityType();
        private IEnumerable<MstActivityType> oList = new List<MstActivityType>();

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
                if (!string.IsNullOrWhiteSpace(oModel.Code))
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
                            res = await _mstActivityType.Insert(oModel, "manager");
                        }
                        else
                        {
                            res = await _mstActivityType.Update(oModel, "manager");
                        }
                    }
                    if (res != null && res.Id == 1)
                    {
                        Snackbar.Add(res.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                        await Task.Delay(3000);
                        Navigation.NavigateTo("/ActivityType", forceLoad: true);
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
                Navigation.NavigateTo("/ActivityType", forceLoad: true);
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }

        private async Task GetAllActivityType()
        {
            try
            {
                oList = await _mstActivityType.GetAllData();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private bool FilterFunc(MstActivityType element, string searchString1)
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
                    oModel = new MstActivityType();
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
                    oModel.Id = res.Id;
                    oModel.Code = res.Code;
                    oModel.Description = res.Description;
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
                await GetAllActivityType();
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
        //            if (res.Where(x => x.MenuName == "Activity Type" && x.UserRights != 1).ToList().Count > 0)
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