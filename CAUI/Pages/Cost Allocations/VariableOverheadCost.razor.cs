using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.Dialog;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using CA.UI.Interfaces.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.Cost_Allocations
{
    public partial class VariableOverheadCost
    {
        #region InjectService

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IVOC _mstVOC { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }
        [Inject]
        public IUserProfile _mstUserProfile { get; set; }
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }


        #endregion InjectService

        #region Parameter

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();

        [Parameter]
        public TrnsVocactivityDetail oDetailActivity { get; set; } = new TrnsVocactivityDetail();

        [Parameter]
        public TrnsVoc oVOC { get; set; } = new TrnsVoc();

        [Parameter]
        public string DialogFor { get; set; }

        #endregion

        #region Variables

        private bool Loading = false;

        private TrnsVoc oModel = new TrnsVoc();

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();

        private MstResource oModelResource = new MstResource();

        private List<TrnsVocactivityDetail> oActivityDetailList = new List<TrnsVocactivityDetail>();

        private List<TrnsVocmachineDetail> oDetailMachineDetailList = new List<TrnsVocmachineDetail>();

        private List<TrnsVoclaborDetail> oDetailLaborDetailList = new List<TrnsVoclaborDetail>();

        private List<TrnsVocelectricityDetail> oDetailElecticityDetailList = new List<TrnsVocelectricityDetail>();

        private List<TrnsVocdyesAndMoldDetail> oDetailDyesDetailList = new List<TrnsVocdyesAndMoldDetail>();

        private List<TrnsVoctoolsDetail> oDetailToolsDetailList = new List<TrnsVoctoolsDetail>();

        private List<TrnsVocgasolineDetail> oDetailGasolineDetailList = new List<TrnsVocgasolineDetail>();

        private UserDataAccess oModelMstUserProfile = new UserDataAccess();
        private List<UserDataAccess> oMstUserProfileList = new List<UserDataAccess>();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        private bool ForAnalysis = false;
        private string LoginUserCode = "";

        #endregion Variables

        #region Functions

        private async Task GetAllCostType()
        {
            try
            {
                oMstUserProfileList = await _mstUserProfile.GetAllFormAndCostTypesVariableOverHeadCost(LoginUserCode);
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task<IEnumerable<UserDataAccess>> SearchCostType(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstUserProfileList.Select(o => new UserDataAccess
                    {
                        FkCostId = o.FkCostId,
                        Description = o.Description
                    }).ToList();
                var res = oMstUserProfileList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new UserDataAccess
                {
                    FkCostId = x.FkCostId,
                    Description = x.Description
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task OpenSAPDataDialogFinishedGoodsItem(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOCFinishedGoodsItem");
                var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    oModel.ProductCode = res.ItemCode;
                    oModel.ProductName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogForResourceMasterData(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOCResourceMasterData");
                var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    if (result.Data != null)
                    {
                        oModelResource = (MstResource)result.Data;
                        oModel.FkresourceDocNum = oModelResource.DocNum;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogActivity(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOCActivityTab");
                var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVocactivityDetail)result.Data;
                    var chkData = oActivityDetailList.Where(x => x.FkactivityDescription == res.FkactivityDescription && x.FkactivityTypeId == res.FkactivityTypeId && x.ActualTimeCycle == res.ActualTimeCycle && x.IncFactor == res.IncFactor && x.CycleTimeSec == res.CycleTimeSec).FirstOrDefault();
                    if (chkData != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oActivityDetailList.Add(res);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogMachine(DialogOptions options, List<TrnsVocactivityDetail> trnsVocactivityDetail, TrnsVoc trnsVocs, MstResource mstResource)
        {
            try
            {
                if (OpenDialogValidation())
                {
                    var parameters = new DialogParameters();
                    parameters.Add("DialogFor", "VOCMachineTab");
                    parameters.Add("oDetailActivity", trnsVocactivityDetail);
                    parameters.Add("oModelVoc", trnsVocs);
                    parameters.Add("oModelResource", mstResource);
                    var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                    var result = await dialog.Result;
                    if (!result.Cancelled)
                    {
                        var res = (TrnsVocmachineDetail)result.Data;
                        var update = oDetailMachineDetailList.Where(x => x.FkactivityTypeId == res.FkactivityTypeId && x.MachineType == res.MachineType && x.Cost == res.Cost && x.CycleTimeSec == res.CycleTimeSec && x.NoOfWorkers == res.NoOfWorkers && x.Nos == res.Nos && x.LifeYears == res.LifeYears && x.Total == res.Total).FirstOrDefault();
                        if (update != null)
                        {
                            Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        else
                        {
                            oDetailMachineDetailList.Add(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogLabor(DialogOptions options, List<TrnsVocactivityDetail> trnsVocactivityDetail, TrnsVoc trnsVocs)
        {
            try
            {
                if (OpenDialogValidation())
                { 
                    var parameters = new DialogParameters();
                    parameters.Add("DialogFor", "VOCLaborTab");
                    parameters.Add("oDetailActivity", trnsVocactivityDetail);
                    parameters.Add("oModelVoc", trnsVocs);
                    var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                    var result = await dialog.Result;
                    if (!result.Cancelled)
                    {
                        var res = (TrnsVoclaborDetail)result.Data;
                        var update = oDetailLaborDetailList.Where(x => x.FkactivityTypeId == res.FkactivityTypeId && x.LaborDescription == res.LaborDescription && x.WageRate == res.WageRate && x.CycleTimeSec == res.CycleTimeSec && x.NoOfWorkers == res.NoOfWorkers && x.Total == res.Total).FirstOrDefault();
                        if (update != null)
                        {
                            Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        else
                        {
                            oDetailLaborDetailList.Add(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogElectricity(DialogOptions options, List<TrnsVocactivityDetail> trnsVocactivityDetail, TrnsVoc trnsVocs, MstResource mstResource)
        {
            try
            {
                if (OpenDialogValidation())
                {
                    var parameters = new DialogParameters();
                    parameters.Add("DialogFor", "VOCElectricityTab");
                    parameters.Add("oDetailActivity", trnsVocactivityDetail);
                    parameters.Add("oModelVoc", trnsVocs);
                    parameters.Add("oModelResource", mstResource);
                    var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                    var result = await dialog.Result;
                    if (!result.Cancelled)
                    {
                        var res = (TrnsVocelectricityDetail)result.Data;
                        var update = oDetailElecticityDetailList.Where(x => x.FkactivityTypeId == res.FkactivityTypeId && x.MachineType == res.MachineType && x.CycleTimeSec == res.CycleTimeSec && x.NoOfWorkers == res.NoOfWorkers && x.Total == res.Total).FirstOrDefault();
                        if (update != null)
                        {
                            Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        else
                        {
                            oDetailElecticityDetailList.Add(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogDyes(DialogOptions options, List<TrnsVocactivityDetail> trnsVocactivityDetail, TrnsVoc trnsVocs, MstResource mstResource)
        {
            try
            {
                if (OpenDialogValidation())
                {
                    var parameters = new DialogParameters();
                    parameters.Add("DialogFor", "VOCDyesAndMoldTab");
                    parameters.Add("oDetailActivity", trnsVocactivityDetail);
                    parameters.Add("oModelVoc", trnsVocs);
                    parameters.Add("oModelResource", mstResource);
                    var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                    var result = await dialog.Result;
                    if (!result.Cancelled)
                    {
                        var res = (TrnsVocdyesAndMoldDetail)result.Data;
                        var update = oDetailDyesDetailList.Where(x => x.FkactivityTypeId == res.FkactivityTypeId && x.DyesAndMold == res.DyesAndMold && x.CycleTimeSec == res.CycleTimeSec && x.NoOfWorkers == res.NoOfWorkers && x.Total == res.Total).FirstOrDefault();
                        if (update != null)
                        {
                            Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        else
                        {
                            oDetailDyesDetailList.Add(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogTools(DialogOptions options, List<TrnsVocactivityDetail> trnsVocactivityDetail, TrnsVoc trnsVocs, MstResource mstResource)
        {
            try
            {
                if (OpenDialogValidation())
                {
                    var parameters = new DialogParameters();
                    parameters.Add("DialogFor", "VOCToolsTab");
                    parameters.Add("oDetailActivity", trnsVocactivityDetail);
                    parameters.Add("oModelVoc", trnsVocs);
                    parameters.Add("oModelResource", mstResource);
                    var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                    var result = await dialog.Result;
                    if (!result.Cancelled)
                    {
                        var res = (TrnsVoctoolsDetail)result.Data;
                        var update = oDetailToolsDetailList.Where(x => x.FkactivityTypeId == res.FkactivityTypeId && x.ToolsType == res.ToolsType && x.CycleTimeSec == res.CycleTimeSec && x.NoOfWorkers == res.NoOfWorkers && x.Total == res.Total).FirstOrDefault();
                        if (update != null)
                        {
                            Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        else
                        {
                            oDetailToolsDetailList.Add(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogGasoline(DialogOptions options, List<TrnsVocactivityDetail> trnsVocactivityDetail, TrnsVoc trnsVocs, MstResource mstResource)
        {
            try
            {
                if (OpenDialogValidation())
                {
                    var parameters = new DialogParameters();
                    parameters.Add("DialogFor", "VOCGasolineTab");
                    parameters.Add("oDetailActivity", trnsVocactivityDetail);
                    parameters.Add("oModelVoc", trnsVocs);
                    parameters.Add("oModelResource", mstResource);
                    var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                    var result = await dialog.Result;
                    if (!result.Cancelled)
                    {
                        var res = (TrnsVocgasolineDetail)result.Data;
                        var update = oDetailGasolineDetailList.Where(x => x.FkactivityTypeId == res.FkactivityTypeId && x.CycleTimeSec == res.CycleTimeSec && x.NoOfWorkers == res.NoOfWorkers && x.Total == res.Total).FirstOrDefault();
                        if (update != null)
                        {
                            Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        else
                        {
                            oDetailGasolineDetailList.Add(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOCMaster");
                var dialog = Dialog.Show<DlgVOC>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVoc)result.Data;
                    oModel = res;
                    oModelCostType.Id = (int)oModel.FkcostTypeId;
                    oModelCostType.Description = oModel.CostTypeDesc;
                    oActivityDetailList = (List<TrnsVocactivityDetail>)oModel.TrnsVocactivityDetails;
                    oDetailMachineDetailList = (List<TrnsVocmachineDetail>)oModel.TrnsVocmachineDetails;
                    oDetailLaborDetailList = (List<TrnsVoclaborDetail>)oModel.TrnsVoclaborDetails;
                    oDetailElecticityDetailList = (List<TrnsVocelectricityDetail>)oModel.TrnsVocelectricityDetails;
                    oDetailDyesDetailList = (List<TrnsVocdyesAndMoldDetail>)oModel.TrnsVocdyesAndMoldDetails;
                    oDetailToolsDetailList = (List<TrnsVoctoolsDetail>)oModel.TrnsVoctoolsDetails;
                    oDetailGasolineDetailList = (List<TrnsVocgasolineDetail>)oModel.TrnsVocgasolineDetails;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecordActivity(int FkactivityTypeId)
        {
            try
            {
                var remove = oActivityDetailList.Where(x => x.FkactivityTypeId == FkactivityTypeId).FirstOrDefault();
                if (remove != null)
                {
                    oActivityDetailList.Remove(remove);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecordMachine(int FkactivityTypeId)
        {
            try
            {
                var remove = oDetailMachineDetailList.Where(x => x.FkactivityTypeId == FkactivityTypeId).FirstOrDefault();
                if (remove != null)
                {
                    oDetailMachineDetailList.Remove(remove);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecordLabor(int FkactivityTypeId)
        {
            try
            {
                var remove = oDetailLaborDetailList.Where(x => x.FkactivityTypeId == FkactivityTypeId).FirstOrDefault();
                if (remove != null)
                {
                    oDetailLaborDetailList.Remove(remove);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecordElectricity(int FkactivityTypeId)
        {
            try
            {
                var remove = oDetailElecticityDetailList.Where(x => x.FkactivityTypeId == FkactivityTypeId).FirstOrDefault();
                if (remove != null)
                {
                    oDetailElecticityDetailList.Remove(remove);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecordDyesAndMold(int FkactivityTypeId)
        {
            try
            {
                var remove = oDetailDyesDetailList.Where(x => x.FkactivityTypeId == FkactivityTypeId).FirstOrDefault();
                if (remove != null)
                {
                    oDetailDyesDetailList.Remove(remove);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecordTools(int FkactivityTypeId)
        {
            try
            {
                var remove = oDetailToolsDetailList.Where(x => x.FkactivityTypeId == FkactivityTypeId).FirstOrDefault();
                if (remove != null)
                {
                    oDetailToolsDetailList.Remove(remove);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecordGasoline(int FkactivityTypeId)
        {
            try
            {
                var remove = oDetailGasolineDetailList.Where(x => x.FkactivityTypeId == FkactivityTypeId).FirstOrDefault();
                if (remove != null)
                {
                    oDetailGasolineDetailList.Remove(remove);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private bool OpenDialogValidation()
        {

            if (oModel.PerDayShiftHrs == null)
            {
                Snackbar.Add("Enter Per Day Shift Hrs.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                return false;
            }
            if (oModel.WorkingDays == null)
            {
                Snackbar.Add("Enter Working Days.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                return false;
            }
            if (oModel.NoOfShift == null)
            {
                Snackbar.Add("Enter Number of Shifts.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                return false;
            }
            if (oModel.MonthlyVolume == null)
            {
                Snackbar.Add("Enter Monthly Volume.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                return false;
            }
            if (oModel.FkresourceDocNum == null)
            {
                Snackbar.Add("Select Resource Data.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                return false;
            }
            return true;
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

                        if (oModel.DocName == null || oModel.ProductCode == null || oModel.PerDayShiftHrs == null || oModel.WorkingDays == null || oModel.MonthlyVolume == null
                            || oModel.FkresourceDocNum == null 
                            || oActivityDetailList.Count() == 0 || oDetailMachineDetailList.Count() == 0 || oDetailLaborDetailList.Count() == 0 || oDetailElecticityDetailList.Count() == 0
                            || oDetailDyesDetailList.Count() == 0 || oDetailToolsDetailList.Count() == 0 || oDetailGasolineDetailList.Count() == 0)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oModel.Id != 0)
                        { 
                            oModel.TrnsVocactivityDetails.Clear();
                            oModel.TrnsVocmachineDetails.Clear();
                            oModel.TrnsVoclaborDetails.Clear();
                            oModel.TrnsVocelectricityDetails.Clear();
                            oModel.TrnsVocdyesAndMoldDetails.Clear();
                            oModel.TrnsVoctoolsDetails.Clear();
                            oModel.TrnsVocgasolineDetails.Clear();
                        }
                        oModel.FkcostTypeId = oModelCostType.Id;
                        oModel.TrnsVocactivityDetails = oActivityDetailList.ToList();
                        oModel.TrnsVocmachineDetails = oDetailMachineDetailList.ToList();
                        oModel.TrnsVoclaborDetails = oDetailLaborDetailList.ToList();
                        oModel.TrnsVocelectricityDetails = oDetailElecticityDetailList.ToList();
                        oModel.TrnsVocdyesAndMoldDetails = oDetailDyesDetailList.ToList();
                        oModel.TrnsVoctoolsDetails = oDetailToolsDetailList.ToList();
                        oModel.TrnsVocgasolineDetails = oDetailGasolineDetailList.ToList();
                        if (oModel.FlgDefault == null)
                            oModel.FlgDefault = false;
                        else
                            oModel.FlgDefault = true;
                        if (ForAnalysis)
                            oModel.ForAnalysis = "Yes";
                        else
                            oModel.ForAnalysis = "No";
                        if (oModel.Id == 0)
                        {
                            res1 = await _mstVOC.Insert(oModel);
                        }
                        else
                        {
                            res1 = await _mstVOC.Update(oModel);
                        }
                        if (res1 != null && res1.Id == 1)
                        {
                            Snackbar.Add(res1.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                            await Task.Delay(1000);
                            Navigation.NavigateTo("/VariableOverheadCost", forceLoad: true);
                        }
                        else
                        {
                            Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        oModelResource.FlgActive = true;
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

        private async void Reset()
        {
            try
            {
                Loading = true;
                await Task.Delay(1);
                Navigation.NavigateTo("/VariableOverheadCost", forceLoad: true);
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }

        #endregion Functions

        #region Events

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Loading = true;
                var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
                if (Session != null)
                {
                    //oModel.FlgActive = true;
                    //oModel.FlgDefaultResrMst = true;
                    LoginUserCode = Session.UserCode;
                    oModel.DocDate = DateTime.Now;
                    await GetAllCostType();
                }
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }

        #endregion Events
    }
}