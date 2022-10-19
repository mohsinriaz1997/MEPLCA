using CA.UI.Interfaces.AdministrationData;
using CA.API.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CA.UI.Pages.AdministrationDataSetup;
using CA.UI.General;

namespace CA.UI.Dialog
{
    public partial class DlgUsers
    {
        #region InjectService 

        [Inject]
        public IUserProfile _UserProfile { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        #endregion

        #region Variables

        bool Loading = false;

        private string searchString1 = "";

        private int selectedRowNumber = -1;

        private List<string> clickedEvents = new();

        private MudTable<MstUserProfile> _table;

        private bool FilterFunc1(MstUserProfile element) => FilterFunc(element, searchString1);

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string DialogFor { get; set; }


        MstUserProfile oUserProfileModels = new MstUserProfile();
        List<MstUserProfile> UserProfileList = new List<MstUserProfile>();

        private HashSet<MstUserProfile> selectedUsers = new HashSet<MstUserProfile>();


        void Cancel() => MudDialog.Cancel();

        #endregion

        #region Functions   

        private void PageChanged(int i)
        {
            _table.NavigateTo(i - 1);
        }

        private bool FilterFunc(MstUserProfile element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.UserCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }

        async Task<List<MstUserProfile>> GetAllUsers()
        {
            UserProfileList.Clear();
            UserProfileList = await _UserProfile.GetAllData();
            return UserProfileList;
        }

        private void Submit()
        {
            try
            {
                if (DialogFor == "Users")
                {
                    if (oUserProfileModels.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstUserProfile>(oUserProfileModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "UsersCopy")
                {
                    if (selectedUsers.Count > 0)
                    {
                        List<MstUserProfile> oSelectedUsers = new List<MstUserProfile>();
                        foreach (var item in selectedUsers)
                        {
                            MstUserProfile s = new MstUserProfile();
                            s.Id = (int)item.Id;
                            oSelectedUsers.Add(s);
                        }
                        MudDialog.Close(DialogResult.Ok<List<MstUserProfile>>(oSelectedUsers));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private string SelectedRowClassFunc(MstUserProfile element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_table.SelectedItem != null && _table.SelectedItem.Equals(element))
            {
                selectedRowNumber = rowNumber;
                clickedEvents.Add($"Selected Row: {rowNumber}");
                return "selected";
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region Events

        protected async override Task OnInitializedAsync()
        {
            Loading = true;
            if (DialogFor == "Users")
            {
                await GetAllUsers();
            }
            if (DialogFor == "UsersCopy")
            {
                await GetAllUsers();
            }
            Loading = false;
        }

        private void RowClickEvent(TableRowClickEventArgs<MstUserProfile> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        #endregion
    }
}
