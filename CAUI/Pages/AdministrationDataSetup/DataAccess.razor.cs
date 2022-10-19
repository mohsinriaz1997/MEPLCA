using Blazored.LocalStorage;
using CA.API.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Dialog;

namespace CA.UI.Pages.AdministrationDataSetup
{
    public partial class DataAccess
    {
        #region Inject Service

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };

        [Inject]
        public IUserProfile _UserProfileService { get; set; }
        [Inject]
        public NavigationManager navigation { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }

        #endregion

        #region Variables

        private bool Loading = false;

        MstUserProfile oUser = new MstUserProfile();

        DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        private IEnumerable<UserDataAccess> oList = new List<UserDataAccess>();

        private HashSet<UserDataAccess> selectedCostType = new HashSet<UserDataAccess>();

        private List<UserDataAccess> oListDataAccess = new List<UserDataAccess>();

        MudTable<UserDataAccess> TableRef { get; set; }


        #endregion

        #region Function

        private TableGroupDefinition<UserDataAccess> _groupDefinition = new()
        {
            GroupName = "Form Name ",
            Indentation = false,
            Expandable = true,
            IsInitiallyExpanded = true,
            Selector = (e) => e.FormName

        };

        private async Task GetAllCostType()
        {
            try
            {
                oList = await _UserProfileService.GetAllFormAndCostType(oUser.Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task OpenAddDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "Users");
                var dialog = Dialog.Show<DlgUsers>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstUserProfile)result.Data;
                    oUser = res;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }


        //async Task<List<UserDataAccess>> FetchUserDataAccess()
        //async Task FetchUserDataAccess()
        //{
        //Loading = true;
        //oList = await _UserProfileService.GetAllUserDataAccess(oUser.Id);
        //if (oListDataAccess.Count() > 0)
        //{
        //    foreach(var item in oListDataAccess)
        //    {
        //        if((bool)item.FlgAccess)
        //        {
        //            selectedCostType.Add(item);
        //            //TableRef.SetSelectedItem(item);
        //            TableRef.Context.Selection.Add(item);
        //        }
        //    }
        //    //TableRef.SelectedItems = selectedCostType;
        //    //StateHasChanged();
        //    TableRef.SelectedItemsChanged.InvokeAsync(selectedCostType);
        //    _= InvokeAsync(StateHasChanged);
        //}
        //else
        //{
        //    //await GetMenu();
        //}
        //Loading = false;
        //return oList;
        //}

        async void SaveUserAuthorization()
        {
            try
            {
                Loading = true;
                if (string.IsNullOrWhiteSpace(oUser.UserCode))
                {
                    Snackbar.Add("Please select User first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    Loading = false;
                    return;
                }
                //else if (selectedUsers.Count == 0)
                //{
                //    Snackbar.Add("Please select", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                //    Loading = false;
                //}
                List<UserDataAccess> oUserDataAccessList = new List<UserDataAccess>();
                foreach (var item in oList)
                {
                    //var result = selectedUsers.Where(x => x.FormCode == item.FormCode && x.FkCostId == item.FkCostId).ToList();
                    if ((bool)item.FlgAccess)
                    //if (result.Count > 0)
                    {
                        UserDataAccess oUserDataAccess = new UserDataAccess();
                        oUserDataAccess.FormCode = item.FormCode;
                        oUserDataAccess.FormName = item.FormName;
                        oUserDataAccess.FkCostId = item.FkCostId;
                        oUserDataAccess.Description = item.Description;
                        oUserDataAccess.FkUserId = oUser.Id;
                        oUserDataAccess.FlgAccess = true;
                        oUserDataAccessList.Add(oUserDataAccess);
                    }
                    else
                    {
                        UserDataAccess oUserDataAccess = new UserDataAccess();
                        oUserDataAccess.FormCode = item.FormCode;
                        oUserDataAccess.FormName = item.FormName;
                        oUserDataAccess.FkCostId = item.FkCostId;
                        oUserDataAccess.Description = item.Description;
                        oUserDataAccess.FlgAccess = false;
                        oUserDataAccess.FkUserId = oUser.Id;
                        oUserDataAccessList.Add(oUserDataAccess);
                    }
                }
                var res = await _UserProfileService.AddUserDataAccess(oUserDataAccessList);
                if (!res.Message.Contains("Failed"))
                {
                    Snackbar.Add(res.Message, Severity.Normal, (options) => { options.Icon = Icons.Sharp.DoneAll; });
                    await Task.Delay(1000);
                    navigation.NavigateTo(navigation.Uri, forceLoad: true);
                }
                else
                {
                    Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.DoneAll; });
                }
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        async Task CheckboxSetting()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex.Message);
            }
        }

        #endregion

        #region Initialized

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
                if (Session != null)
                {
                    Loading = true;
                    await GetAllCostType();
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
