using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.AdministrationDataSetup
{
    public partial class ApprovalSetup
    {

        #region Inject Service

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public IApprovalSetup _approvalSetupService { get; set; }

        [Inject]
        public NavigationManager navigation { get; set; }

        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        [Inject]
        public IUserProfile _userProfileService { get; set; }

        [Inject]
        public IStage _stageService { get; set; }

        #endregion

        #region Variable

        private IEnumerable<MstUserProfile> UserNames { get; set; } = new HashSet<MstUserProfile>();
        private IEnumerable<MstForm> DocNames { get; set; } = new HashSet<MstForm>();
        private IEnumerable<MstStage> StageNames { get; set; } = new HashSet<MstStage>();


        MstApprovalSetup oModel = new MstApprovalSetup();
        List<MstApprovalSetup> oList = new List<MstApprovalSetup>();
        MstUserProfile oUser = new MstUserProfile();
        List<MstUserProfile> oUserList = new List<MstUserProfile>();

        MstForm oDoc = new MstForm();
        List<MstForm> oDocList = new List<MstForm>();

        MstStage oStage = new MstStage();
        List<MstStage> oStageList = new List<MstStage>();

        List<MstStage> oStageAddList = new List<MstStage>();
        MstApprovalTerm oMstApprovalTerms = new MstApprovalTerm();

        MstApprovalStage oApprovalStage = new MstApprovalStage();
        List<MstApprovalStage> oApprovalStageAddList = new List<MstApprovalStage>();

        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };

        Func<MstStage, string> converter = p => p?.StageName;

        bool IsDisabledQuery = false;
        private string LoginUserCode = "";
        private bool Loading = false;

        DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

       

        #endregion

        #region Function 

        private async Task OpenDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "ApprovalSetup");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstApprovalSetup)result.Data;
                    oModel = res;

                    UserNames = new HashSet<MstUserProfile>();
                    List<MstUserProfile> oList = new List<MstUserProfile>();
                    foreach (var item in oModel.MstApprovalOriginators)
                    {
                        MstUserProfile LineUser = new MstUserProfile();
                        LineUser = oUserList.Where(a => a.Id == item.OriginatorId).FirstOrDefault();
                        oList.Add(LineUser);
                    }
                    UserNames = oList;
                    DocNames = new HashSet<MstForm>();
                    List<MstForm> oListForm = new List<MstForm>();
                    if(oModel.FlgSalesQuatation == true)
                    {
                        MstForm LineDoc = new MstForm();
                        LineDoc = oDocList.Where(x => x.FormCode == 1).FirstOrDefault();
                        oListForm.Add(LineDoc);
                    }
                    if (oModel.FlgMonthlyVohcalc == true)
                    {
                        MstForm LineDoc = new MstForm();
                        LineDoc = oDocList.Where(x => x.FormCode == 2).FirstOrDefault();
                        oListForm.Add(LineDoc);
                    }
                    if (oModel.FlgItemPriceList == true)
                    {
                        MstForm LineDoc = new MstForm();
                        LineDoc = oDocList.Where(x => x.FormCode == 3).FirstOrDefault();
                        oListForm.Add(LineDoc);
                    }
                    if (oModel.FlgResourceMasterData == true)
                    {
                        MstForm LineDoc = new MstForm();
                        LineDoc = oDocList.Where(x => x.FormCode == 4).FirstOrDefault();
                        oListForm.Add(LineDoc);
                    }
                    if (oModel.FlgMonthlyFohdriverRateCalc == true)
                    {
                        MstForm LineDoc = new MstForm();
                        LineDoc = oDocList.Where(x => x.FormCode == 5).FirstOrDefault();
                        oListForm.Add(LineDoc);
                    }
                    if (oModel.FlgMonthlyFohcostCalc == true)
                    {
                        MstForm LineDoc = new MstForm();
                        LineDoc = oDocList.Where(x => x.FormCode == 6).FirstOrDefault();
                        oListForm.Add(LineDoc);
                    }
                    if (oModel.FlgFohrateCalc == true)
                    {
                        MstForm LineDoc = new MstForm();
                        LineDoc = oDocList.Where(x => x.FormCode == 7).FirstOrDefault();
                        oListForm.Add(LineDoc);
                    }
                    if (oModel.FlgVariableOverheadCost == true)
                    {
                        MstForm LineDoc = new MstForm();
                        LineDoc = oDocList.Where(x => x.FormCode == 8).FirstOrDefault();
                        oListForm.Add(LineDoc);
                    }
                    DocNames = oListForm;
                    oApprovalStageAddList = oModel.MstApprovalStages.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        async Task<List<MstForm>> GetApprovalDocs()
        {
            try
            {
                oDocList = await _approvalSetupService.GetApprovalDocs();
                return oDocList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
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

        async Task<List<MstStage>> GetAllStages()
        {
            try
            {
                oStageList = await _stageService.GetAllData();
                return oStageList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }

        }

        private string GetOriginatorSelection(List<string> selectedValues)
        {
            try
            {
                if (selectedValues.Count < 1)
                {
                    return $"Please choose Originator";
                }
                return $"{selectedValues.Count} Originator{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private string GetDocumentSelection(List<string> selectedValues)
        {
            try
            {
                if (selectedValues.Count < 1)
                {
                    return $"Please choose Document";
                }
                return $"{selectedValues.Count} Document{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private string GetStagesSelection(List<string> selectedValues)
        {
            try
            {
                if (selectedValues.Count < 1)
                {
                    return $"Please choose Stage";
                }
                return $"{selectedValues.Count} Stage{(selectedValues.Count > 1 ? "s have" : " has")} been selected";
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<List<MstApprovalStage>> AddRow()
        {
            try
            {
                MstApprovalStage One = new()
                {
                    FkstageId = oStage.Id,
                    FkstageName = oStage.StageName
                };
                
                if (One.FkstageId != null)
                {
                    if (oApprovalStageAddList.Count == 0)
                    {
                        One.ApprovalPriority = 1;
                    }
                    else
                    {
                        foreach (var a in oApprovalStageAddList)
                        {
                            One.ApprovalPriority = a.ApprovalPriority + 1;
                        }
                    }
                    var chkStage = oApprovalStageAddList.Find(x => x.FkstageId == oStage.Id);
                    if (chkStage == null)
                    {
                        oApprovalStageAddList.Add(One);
                    }
                    else
                    {
                        Snackbar.Add("Stage already exists in the list.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    oStage = new MstStage();
                    return oApprovalStageAddList;
                }
                else
                {
                    Snackbar.Add("Select Stage.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public void RemoveRecord(int ID)
        {
            try
            {
                var res = oApprovalStageAddList.Find(x => x.Id == ID);
                oApprovalStageAddList.Remove(res);
                int count = 1;
                foreach (var a in oApprovalStageAddList)
                {
                    a.ApprovalPriority = count++;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task<ApiResponseModel> SaveApproval()
        {
            try
            {
                Loading = true;
                var res1 = new ApiResponseModel();
                oList = await _approvalSetupService.GetAllData();
                if (oModel == null || string.IsNullOrWhiteSpace(oModel.Name) || UserNames == null || UserNames.Count() < 1 || DocNames == null || DocNames.Count() < 1 || oApprovalStageAddList.Count == 0) 
                {
                    Loading = false;
                    Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return null;
                }
                if (oList.Where(x => x.Name.ToLower() == oModel.Name.ToLower()).Count() > 0)
                {
                    Snackbar.Add("Approval Name already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    Loading = false;
                    return null;
                }
                else
                {
                    Loading = true;
                    if (UserNames != null && UserNames.Count() > 0)
                    {
                        oModel.MstApprovalOriginators.Clear();
                        foreach (var Line in UserNames)
                        {
                            MstApprovalOriginator oLine = new MstApprovalOriginator();
                            oLine.OriginatorId = Line.Id;
                            oModel.MstApprovalOriginators.Add(oLine);
                        }
                    }
                    if (DocNames != null && DocNames.Count() > 0)
                    {
                        oModel.FlgSalesQuatation = false;
                        oModel.FlgMonthlyVohcalc = false;
                        oModel.FlgItemPriceList = false;
                        oModel.FlgResourceMasterData = false;
                        oModel.FlgMonthlyFohdriverRateCalc = false;
                        oModel.FlgMonthlyFohcostCalc = false;
                        oModel.FlgFohrateCalc = false;
                        oModel.FlgVariableOverheadCost = false;
                        foreach (var Line in DocNames)
                        {
                            switch (Line.FormCode)
                            {
                                case 1:
                                    oModel.FlgSalesQuatation = true;
                                    break;
                                case 2:
                                    oModel.FlgMonthlyVohcalc = true;
                                    break;
                                case 3:
                                    oModel.FlgItemPriceList = true;
                                    break;
                                case 4:
                                    oModel.FlgResourceMasterData = true;
                                    break;
                                case 5:
                                    oModel.FlgMonthlyFohdriverRateCalc = true;
                                    break;
                                case 6:
                                    oModel.FlgMonthlyFohcostCalc = true;
                                    break;
                                case 7:
                                    oModel.FlgFohrateCalc = true;
                                    break;
                                case 8:
                                    oModel.FlgVariableOverheadCost = true;
                                    break;
                            }
                        }
                    }
                    oModel.MstApprovalStages = oApprovalStageAddList;
                    oModel.MstApprovalTerms.Clear();
                    MstApprovalTerm term = new MstApprovalTerm();
                    term.Always = IsDisabledQuery;
                    if (term.Always == true)
                    {
                        term.TermQuery = "-";
                    }
                    else
                    {
                        term.TermQuery = oMstApprovalTerms.TermQuery;
                    }
                    oModel.MstApprovalTerms.Add(term);
                    if (oModel.Id == 0)
                    {
                        oModel.CreatedBy = LoginUserCode;
                        oModel.CreatedDate = DateTime.Now;
                        res1 = await _approvalSetupService.Insert(oModel);
                    }
                    else
                    {
                        oModel.UpdatedBy = LoginUserCode;
                        oModel.UpdatedDate = DateTime.Now;
                        res1 = await _approvalSetupService.Update(oModel);
                    }
                    //if (res != null && res.Message.Contains("Can't update Approval setup, documents decisions pending.") || res.Message.Contains("Failed to Saved Successfully."))
                    //{
                    //    Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.DoneAll; });
                    //}
                    //else if (res != null && res.Message.Contains("Saved Successfully.") || res.Message.Contains("Update Successfully."))
                    //{
                    if (res1 != null && res1.Id == 1)
                    {
                        Snackbar.Add(res1.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                        await Task.Delay(1000);
                        Navigation.NavigateTo("/ApprovalSetup", forceLoad: true);
                    }
                    else
                    {
                        Snackbar.Add(res1.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        Loading = false;
                        return null;
                    }
                    //}
                    //else
                    //{
                    //    Snackbar.Add("An error occurred.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    //}
                    Loading = false;
                    return res1;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        #endregion

        #region Initialized

        protected async override Task OnInitializedAsync()
        {
            try
            {
                Loading = true;
                var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
                if (Session != null)
                {
                    LoginUserCode = Session.UserCode;
                    await GetAllUsers();
                    await GetApprovalDocs();
                    await GetAllStages();
                    oModel.FlgActive = true;
                    IsDisabledQuery = true;
                }
                else
                {
                    Navigation.NavigateTo("/Login", forceLoad: true);
                }
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        #endregion
    }
}
