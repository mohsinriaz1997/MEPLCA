using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using Microsoft.AspNetCore.Components;

namespace CA.UI.Shared
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;

        [Inject]
        public IUserProfile _UserProfileService { get; set; }

        [Inject]
        public IApprovalSetup _ApprovalSetupService { get; set; }


        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        public int LoginUserID = 0;


        List<UserAuthorization> AuthMenus;


        List<DocApproval> oUserAlertList = new List<DocApproval>();
        DocApproval oModel = new DocApproval();

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        async Task<List<DocApproval>> GetAllPendingDoc()
        {
            try
            {
                oUserAlertList = await _ApprovalSetupService.GetAlerts(LoginUserID, "Pending");
                return oUserAlertList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        protected override async Task OnInitializedAsync()
        {

            try
            {
                var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
                if (Session != null)
                {
                    LoginUserID = Session.Id;
                    var res = await _UserProfileService.FetchUserAuth(Session.Id);
                    AuthMenus = res?.Where(x => x.UserRights != 1).ToList();
                    await GetAllPendingDoc();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex.Message);
            }
        }
    }
}
