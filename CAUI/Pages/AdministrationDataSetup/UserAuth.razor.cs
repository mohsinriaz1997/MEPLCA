using Blazored.LocalStorage;
using CA.API.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Dialog;

namespace CA.UI.Pages.AdministrationDataSetup
{
    public partial class UserAuth
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

        #endregion

        #region Variables

        private bool Loading = false;

        private TableGroupDefinition<VMUserAuthorization> groupDef { get; set; }
        MstUserProfile oUser = new MstUserProfile();
        List<MstUserProfile> oSelectedUserList = new List<MstUserProfile>();

        VMUserAuthorization oVm = new VMUserAuthorization();
        List<VMUserAuthorization> oVmList = new List<VMUserAuthorization>();

        IEnumerable<VMUserAuthorization> oVmListTable = new List<VMUserAuthorization>();


        MstMenu oMstMenu = new MstMenu();
        List<MstMenu> oMenuList = new List<MstMenu>();

        List<UserAuthorization> oUserAuthorizationList = new List<UserAuthorization>();
        UserAuthorization oUserAuthorization = new UserAuthorization();

        DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        #endregion

        #region Function

        //async Task<List<MstUserProfile>> GetAllUsers()
        //{
        //    try
        //    {
        //        oUserList = await _UserProfileService.GetAllData();
        //        return oUserList;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //        return null;
        //    }
        //}

        async Task<List<MstMenu>> GetMenu()
        {
            try
            {
                oMenuList.Clear();
                oVmList.Clear();
                oMenuList = await _UserProfileService.GetAllMenu();
                string MenuParentName = "";
                int ID = 1;
                foreach (var item in oMenuList)
                {
                    oVm = new VMUserAuthorization();
                    if (item.MenuName == "Administration" || item.MenuParent == 1)
                    {
                        if (item.MenuParent == 0)
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.MenuId;
                            oVm.MenuParent = item.MenuName;
                            MenuParentName = oVm.MenuParent;
                        }
                        else
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.MenuId;
                            oVm.MenuParent = MenuParentName;
                            oVm.MenuChild = item.MenuName;
                        }
                        oVm.RightsValue = 1;
                    }
                    else if (item.MenuName == "Master Data" || item.MenuParent == 9)
                    {
                        if (item.MenuParent == 0)
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.MenuId;
                            oVm.MenuParent = item.MenuName;
                            MenuParentName = oVm.MenuParent;
                        }
                        else
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.MenuId;
                            oVm.MenuParent = MenuParentName;
                            oVm.MenuChild = item.MenuName;
                        }
                        oVm.RightsValue = 1;
                    }
                    else if (item.MenuName == "Cost Allocation" || item.MenuParent == 18)
                    {
                        if (item.MenuParent == 0)
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.MenuId;
                            oVm.MenuParent = item.MenuName;
                            MenuParentName = oVm.MenuParent;
                        }
                        else
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.MenuId;
                            oVm.MenuParent = MenuParentName;
                            oVm.MenuChild = item.MenuName;
                        }
                        oVm.RightsValue = 1;
                    }
                    oVmList.Add(oVm);
                    ID++;
                }
                oVmListTable = oVmList;
                return oMenuList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
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

        private async Task OpenCopyDialog(DialogOptions options)
        {
            try
            {
                if (oUser.UserCode != null)
                {
                    var parameters = new DialogParameters();
                    parameters.Add("DialogFor", "UsersCopy");
                    var dialog = Dialog.Show<DlgUsers>("", parameters, options);
                    var result = await dialog.Result;
                    if (!result.Cancelled)
                    {
                        var res = result.Data;
                        oSelectedUserList = (List<MstUserProfile>)result.Data;
                        //foreach (var item in result.Data)
                        //{

                        //}
                        //oUserAuthorizationList = (UserAuthorization)result.Data;
                    }
                }

            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }


        async Task<List<UserAuthorization>> FetchUserAuth()
        {
            Loading = true;

            var LoginuserID = oUser.Id;
            oUserAuthorizationList = await _UserProfileService.FetchUserAuth(LoginuserID);
            if (oUserAuthorizationList.Count > 0)
            {
                int ID = 1;
                string MenuParentName = "";
                oVmList.Clear();
                foreach (var item in oUserAuthorizationList)
                {
                    oVm = new VMUserAuthorization();
                    if (item.MenuName == "Administration" || item.MenuParent == 1)
                    {
                        if (item.MenuParent == 0)
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.Id;
                            oVm.MenuParent = item.MenuName;
                            MenuParentName = oVm.MenuParent;
                        }
                        else
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.Id;
                            oVm.MenuParent = MenuParentName;
                            oVm.MenuChild = item.MenuName;
                        }
                        oVm.RightsValue = (int)item.UserRights;
                    }
                    else if (item.MenuName == "Master Data" || item.MenuParent == 9)
                    {
                        if (item.MenuParent == 0)
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.Id;
                            oVm.MenuParent = item.MenuName;
                            MenuParentName = oVm.MenuParent;
                        }
                        else
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.Id;
                            oVm.MenuParent = MenuParentName;
                            oVm.MenuChild = item.MenuName;
                        }
                        oVm.RightsValue = (int)item.UserRights;
                    }
                    else if (item.MenuName == "Cost Allocation" || item.MenuParent == 18)
                    {
                        if (item.MenuParent == 0)
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.Id;
                            oVm.MenuParent = item.MenuName;
                            MenuParentName = oVm.MenuParent;
                        }
                        else
                        {
                            oVm.ID = ID;
                            oVm.MenuID = (int)item.Id;
                            oVm.MenuParent = MenuParentName;
                            oVm.MenuChild = item.MenuName;
                        }
                        oVm.RightsValue = (int)item.UserRights;
                    }
                    oVmList.Add(oVm);
                    ID++;
                }
                oVmListTable = oVmList;
            }
            else
            {
                await GetMenu();
            }
            Loading = false;
            return oUserAuthorizationList;
        }

        async void SaveUserAuthorization()
        {
            try
            {
                Loading = true;
                if (string.IsNullOrWhiteSpace(oUser.UserCode))
                {
                    Snackbar.Add("Please select User first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    Loading = false;
                }
                else
                {
                    oUserAuthorizationList.Clear();
                    if (oSelectedUserList.Count == 0)
                    {
                        foreach (var item in oVmListTable)
                        {
                            oUserAuthorization = new UserAuthorization();
                            oUserAuthorization.FkuserId = oUser.Id;
                            oUserAuthorization.UserRights = item.RightsValue;
                            oUserAuthorization.FkmenuId = item.MenuID;
                            oUserAuthorizationList.Add(oUserAuthorization);
                        }
                    }
                    else
                    {
                        foreach (var SUser in oSelectedUserList)
                        {
                            foreach (var item in oVmListTable)
                            {
                                oUserAuthorization = new UserAuthorization();
                                oUserAuthorization.FkuserId = SUser.Id;
                                oUserAuthorization.UserRights = item.RightsValue;
                                oUserAuthorization.FkmenuId = item.MenuID;
                                oUserAuthorizationList.Add(oUserAuthorization);
                            }
                        }
                    }
                    var res = await _UserProfileService.AddUserAuthorization(oUserAuthorizationList);
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
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
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

                    await GetMenu();

                    //await GetAllUsers();

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
