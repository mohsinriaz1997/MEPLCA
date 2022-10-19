using Blazored.LocalStorage;
using CA.UI.Interfaces.AdministrationData;
using CA.API.Models;
using CA.UI.General;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;
using Microsoft.Extensions.Configuration.UserSecrets;
using CA.UI.Dialog;

namespace CA.UI.Pages.AdministrationDataSetup
{
    public partial class DocumentApproval
    {
        private bool Loading = false;

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }


        [Inject]
        public IApprovalSetup _approvalSetupService { get; set; }

        [Inject]
        public NavigationManager navigation { get; set; }

        [Inject]
        public ILocalStorageService _localStorageService { get; set; }


        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };
        public string LoginUserCode = "";
        public int LoginUserID = 0;

        List<DocApproval> oUserAlertList = new List<DocApproval>();
        DocApproval oModel = new DocApproval();

        private MudTable<DocApproval> _table;

        private string searchString1 = "";

        private bool FilterFunc1(DocApproval element) => FilterFunc(element, searchString1);

        private void PageChanged(int i)
        {
            _table.NavigateTo(i - 1);
        }

        private bool FilterFunc(DocApproval element, string searchString1)
        {
            if (string.IsNullOrWhiteSpace(searchString1))
                return true;
            if (element.FkformName.Contains(searchString1, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.DocStatus.Contains(searchString1, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.FkdocNum.Equals(searchString1))
                return true;
            if (element.CreatedBy.Equals(searchString1))
                return true;
            if (element.CreatedDate.Equals(searchString1))
                return true;
            return false;
        }

        private async Task OpenDialog(DialogOptions options, int ID, string status)
        {
            try
            {
                var dialog = Dialog.Show<DlgDocumentApproval>("Document Approval", options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    Loading = true;
                    string remarks = "";
                    if (result.Data == null)
                    {
                        remarks = "";
                    }
                    else
                    {
                        remarks = result.Data.ToString();
                    }
                    var res = oUserAlertList.Find(x => x.Id == ID);
                    ApiResponseModel response = new ApiResponseModel();
                    oModel.Remarks = remarks;
                    oModel.DocStatus = status;
                    oModel.Id = res.Id;
                    oModel.FkapprovalId= res.FkapprovalId;
                    oModel.FkstageId = res.FkstageId;
                    oModel.FkformName = res.FkformName;
                    oModel.FkformId = res.FkformId;
                    oModel.FkuserId = res.FkuserId;
                    oModel.FkdocNum = res.FkdocNum;
                    oModel.UpdatedBy = LoginUserCode;
                    oModel.UpdatedDate = DateTime.Now;
                    oModel.CreatedBy = res.CreatedBy;
                    oModel.CreatedDate = res.CreatedDate;
                    oModel.FlgActive = false;
                    response = await _approvalSetupService.UpdateDocApproval(oModel);
                    Loading = false;
                    if (res != null)
                    {
                        Snackbar.Add(res.DocStatus == "Approved" ? "Accept Sucuessfully." : "Reject Sucuessfully.", Severity.Normal, (options) => { options.Icon = Icons.Sharp.DoneAll; });
                        await GetAllPendingDoc();
                    }
                    else
                    {
                        Snackbar.Add("An error occured.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        //private async Task ViewApprovalDocument(DialogOptions options, string DocNum)
        //{
        //    try
        //    {
        //        var parameters = new DialogParameters { ["DocNum"] = DocNum };
        //        var dialog = Dialog.Show<DlgDocApprovalView>("", parameters, options);
        //        var result = await dialog.Result;
        //        if (!result.Cancelled)
        //        {
        //            _processing = true;
        //            //res = await _administrationService.UpdateUserAlert(oModel);
        //            _processing = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UILog.GenerateLogs(ex);
        //    }
        //}

        async Task<List<DocApproval>> GetAllPendingDoc()
        {
            try
            {
                oUserAlertList = await _approvalSetupService.GetAlerts(LoginUserID, "Pending");
                return oUserAlertList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                Loading = true;
                var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
                if (Session != null)
                {
                    LoginUserID = Session.Id;
                    LoginUserCode = Session.UserCode;
                    await GetAllPendingDoc();
                    Loading = false;
                }
                else
                {
                    navigation.NavigateTo("/Dashboard");
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
    }
}
