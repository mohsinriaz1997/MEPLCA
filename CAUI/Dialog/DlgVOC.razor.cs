using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using CA.UI.Interfaces.MasterData;
using CA.UI.Interfaces.SAPData;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using MudBlazor;

namespace CA.UI.Dialog
{
    public partial class DlgVOC
    {
        #region InjectService


        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public ISAPData _SAPService { get; set; }

        [Inject]
        public IResourceMasterData _mstRescourceMaster { get; set; }

        [Inject]
        public IActivityType _mstActivityType { get; set; }

        [Inject]
        public ILaborRate _mstLaborRate { get; set; }

        [Inject]
        public IVOC _mstVOCCalculation { get; set; }

        #endregion

        #region Parameter


        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public TrnsVoc oModelVoc { get; set; } = new TrnsVoc();

        [Parameter]
        public MstResource oModelResource { get; set; } = new MstResource();

        [Parameter]
        public string DialogFor { get; set; }

        [Parameter]
        public List<TrnsVocactivityDetail> oDetailActivity { get; set; } = new List<TrnsVocactivityDetail>();

        #endregion

        #region Variables

        private bool Loading = false;

        private List<string> clickedEvents = new();

        private string searchString1 = "";

        private string searchString2 = "";
        
        private string searchString10 = "";

        private int selectedRowNumber = -1;

        private MudTable<SAPModels> _table;

        private MudTable<MstResource> _table1;

        private MudTable<TrnsVoc> _tableVOC;

        private bool FilterFunc1(SAPModels element) => FilterFunc(element, searchString1);

        private bool FilterFuncRes(MstResource element1) => FilterFuncRes(element1, searchString2);

        private bool FilterFuncVOC(TrnsVoc elementVOC) => FilterFuncVOC(elementVOC, searchString10);

        private SAPModels oSAPModels = new SAPModels();
        private List<SAPModels> SAPModelsList = new List<SAPModels>();
        
        private HashSet<SAPModels> selectedItems1 = new HashSet<SAPModels>();

        private MstResource oModelResourceForVOC = new MstResource();
        private List<MstResource> oResourceForVOCList = new List<MstResource>();

        private MstResourceDetail oModelResouceType = new MstResourceDetail();
        private List<MstResourceDetail> oResouceTypeList = new List<MstResourceDetail>();
        private List<MstResourceDetail> oMstResourceDetailList = new List<MstResourceDetail>();

        private MstActivityType oModelActivityType = new MstActivityType();
        private List<MstActivityType> oActivityTypeList = new List<MstActivityType>();

        private TrnsVocactivityDetail oModelVOCActivity = new TrnsVocactivityDetail();

        private TrnsVocmachineDetail mstVocMachineDetail = new TrnsVocmachineDetail();

        private TrnsVoclaborDetail mstVocLaborDetail = new TrnsVoclaborDetail();

        private MstLabourRateDetail oModelLabourRateDetail = new MstLabourRateDetail();
        private List<MstLabourRateDetail> oMstLaborRateDetailList = new List<MstLabourRateDetail>();

        private TrnsVocelectricityDetail mstVocElecticcityDetail = new TrnsVocelectricityDetail();

        private TrnsVocdyesAndMoldDetail mstVocDyesDetail = new TrnsVocdyesAndMoldDetail();

        private TrnsVoctoolsDetail mstVocToolsDetail = new TrnsVoctoolsDetail();

        private TrnsVocgasolineDetail mstVocGasolineDetail = new TrnsVocgasolineDetail();

        private TrnsVoc oModelTrnsVoc = new TrnsVoc();
        private List<TrnsVoc> oTrnsVoc = new List<TrnsVoc>();

        private void Cancel() => MudDialog.Cancel();

        #endregion

        #region Functions

        private bool FilterFunc(SAPModels element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.ItemCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }

        private bool FilterFuncRes(MstResource element1, string searchString2)
        {
            if (string.IsNullOrWhiteSpace(searchString2))
                return true;
            if (element1.ResrListName.Contains(searchString2, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element1.FlgActive.Equals(searchString2))
                return true;
            return false;
        }

        private bool FilterFuncVOC(TrnsVoc elementVOC, string searchString10)
        {
            if (string.IsNullOrWhiteSpace(searchString10))
                return true;

            if (elementVOC.DocNum.Equals(searchString10))
                return true;
            return false;
        }

        private string SelectedRowClassFunc(SAPModels element, int rowNumber)
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

        private string SelectedRowClassFuncRes(MstResource element, int rowNumber)
        {

            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_table1.SelectedItem != null && _table1.SelectedItem.Equals(element))
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

        private string SelectedRowClassFunVOC(TrnsVoc elementVOC, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableVOC.SelectedItem != null && _tableVOC.SelectedItem.Equals(elementVOC))
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

        private void PageChanged(int i)
        {
            try
            {
                _table.NavigateTo(i - 1);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private async Task<List<SAPModels>> GetFinishedGoodsItem()
        {
            SAPModelsList.Clear();
            string Clause = "ItmsGrpCod = '105'";
            SAPModelsList = await _SAPService.GetItemsFromSAP(Clause);
            return SAPModelsList;
        }

        private async Task GetAllRescourceMaster()
        {
            try
            {
                oResourceForVOCList = await _mstRescourceMaster.GetAllData();
                if (oResourceForVOCList?.Count == 0 || oResourceForVOCList == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllActivityType()
        {
            try
            {
                oActivityTypeList = await _mstActivityType.GetAllData();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllLaborDesciption()
        {
            try
            {
                oMstLaborRateDetailList = await _mstLaborRate.GetAllDataDetail();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task GetAllMachine()
        {
            try
            {
                oMstResourceDetailList = await _mstRescourceMaster.GetAllDataMachine();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task GetAllDyes()
        {
            try
            {
                oMstResourceDetailList = await _mstRescourceMaster.GetAllDataDyes();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task GetAllTools()
        {
            try
            {
                oMstResourceDetailList = await _mstRescourceMaster.GetAllDataTools();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task GetAllGasoline()
        {
            try
            {
                oMstResourceDetailList = await _mstRescourceMaster.GetAllDataGasoline();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllVOC()
        {
            try
            {
                oTrnsVoc = await _mstVOCCalculation.GetAllData();
                if (oTrnsVoc?.Count == 0 || oTrnsVoc == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task<IEnumerable<MstActivityType>> SearchActivityType(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oActivityTypeList.Select(o => new MstActivityType
                    {
                        Id = o.Id,
                        Description = o.Description
                    }).ToList();
                var res = oActivityTypeList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new MstActivityType
                {
                    Id = x.Id,
                    Description = x.Description
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }

            //mstLabourRateList.FkcostTypeId = oModelCostType.Description;
        }

        private async Task<IEnumerable<MstResourceDetail>> searchResourceDescription(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceDetailList.Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Fkid = o.Fkid,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    Fkid = x.Fkid,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        private async Task<IEnumerable<MstResourceDetail>> searchResourceDescriptionMachine(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceDetailList.Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Fkid = o.Fkid,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                //var res = oMstResourceDetailList.Where(x => x.ResrDescription .ToUpper().Contains(value.ToUpper())).ToList();
                var res = (from a in oMstResourceDetailList
                          where a.ResrDescription == "Machine"
                          select a).ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    Fkid = x.Fkid,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        private async Task<IEnumerable<MstResourceDetail>> searchResourceDescriptionDyes(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceDetailList.Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Fkid = o.Fkid,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                //var res = oMstResourceDetailList.Where(x => x.ResrDescription .ToUpper().Contains(value.ToUpper())).ToList();
                var res = (from a in oMstResourceDetailList
                          where a.ResrDescription == "Dyes"
                           select a).ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    Fkid = x.Fkid,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        private async Task<IEnumerable<MstResourceDetail>> searchResourceDescriptionTools(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceDetailList.Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Fkid = o.Fkid,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                //var res = oMstResourceDetailList.Where(x => x.ResrDescription .ToUpper().Contains(value.ToUpper())).ToList();
                var res = (from a in oMstResourceDetailList
                          where a.ResrDescription == "Dyes"
                           select a).ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    Fkid = x.Fkid,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        private async Task<IEnumerable<MstResourceDetail>> searchResourceDescriptionGasoline(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceDetailList.Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Fkid = o.Fkid,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                //var res = oMstResourceDetailList.Where(x => x.ResrDescription .ToUpper().Contains(value.ToUpper())).ToList();
                var res = (from a in oMstResourceDetailList
                          where a.ResrDescription == "Gasoline"
                           select a).ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    Fkid = x.Fkid,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<IEnumerable<MstLabourRateDetail>> searchLabourRateLabor(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstLaborRateDetailList.Select(o => new MstLabourRateDetail
                    {
                        Id = o.Id,
                        Fkid = o.Fkid,
                        Description = o.Description
                    }).ToList();
                var res = oMstLaborRateDetailList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new MstLabourRateDetail
                {
                    Id = x.Id,
                    Fkid = x.Fkid,
                    Description = x.Description
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        #region ActivityTab Calculation

        private void onChangeActivityCycleTimeSec()
        {
            try
            {
                if (oModelVOCActivity.ActualTimeCycle == null)
                {
                    oModelVOCActivity.ActualTimeCycle = 0;
                }
                if (oModelVOCActivity.IncFactor == null)
                {
                    oModelVOCActivity.IncFactor = 0;
                }
                oModelVOCActivity.CycleTimeSec = DataConversion.ValueRound2(Convert.ToDecimal(oModelVOCActivity.ActualTimeCycle * oModelVOCActivity.IncFactor));
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        #endregion

        #region VOCMachineTab Calculation

        private void onChangeVOCMachineTypeCycleTimeSec()
        {
            try
            {
                var query = oDetailActivity.Where(x => x.FkactivityTypeId == oModelActivityType.Id).Select(x => x.CycleTimeSec).FirstOrDefault();
                if (query != null)
                    mstVocMachineDetail.CycleTimeSec = DataConversion.ValueRound2(Convert.ToDecimal(query));
                else
                    mstVocMachineDetail.CycleTimeSec = 0;
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeVOCMachineTypeCost()
        {
            try
            {
                var cost = oResouceTypeList.Where(x => x.ResrDescription == oModelResouceType.ResrDescription).FirstOrDefault();
                if (cost != null)
                    mstVocMachineDetail.Cost = DataConversion.ValueRound2((decimal)cost.Cost);
                else
                    mstVocMachineDetail.Cost = 0;
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void onChangeVOCMachineTypeNoofWorkerAndNos()
        {
            try
            {
                mstVocMachineDetail.NoOfWorkers = DataConversion.ValueRound2((decimal)((decimal)((mstVocMachineDetail.CycleTimeSec * oModelVoc.MonthlyVolume) / 3600) / oModelVoc.WorkingDays / oModelVoc.PerDayShiftHrs));
                mstVocMachineDetail.Nos = DataConversion.ValueRound2((decimal)(mstVocMachineDetail.NoOfWorkers / oModelVoc.NoOfShift));
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void onChangeVOCMachineTabTotal()
        {
            try
            {
                var total = (mstVocMachineDetail.Cost * mstVocMachineDetail.Nos) / oModelVoc.MonthlyVolume * 12 * mstVocMachineDetail.LifeYears;
                mstVocMachineDetail.Total = DataConversion.ValueRound2(Convert.ToDecimal(total));
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        #endregion

        #region Labor Calculation

        private void onChangeVOCLaborTypeCycleTimeSec()
        {
            try
            {
                var query = oDetailActivity.Where(x => x.FkactivityTypeId == oModelActivityType.Id).Select(x => x.CycleTimeSec).FirstOrDefault();
                if (query != null)
                    mstVocLaborDetail.CycleTimeSec = DataConversion.ValueRound2(Convert.ToDecimal(query));
                else
                    mstVocLaborDetail.CycleTimeSec = 0;
                mstVocLaborDetail.NoOfWorkers = DataConversion.ValueRound2((decimal)(((mstVocLaborDetail.CycleTimeSec * oModelVoc.MonthlyVolume) /3600) / oModelVoc.WorkingDays / oModelVoc.PerDayShiftHrs));
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeVOCLaborTypeWageRates()
        {
            try
            {
                var query = oMstLaborRateDetailList.Where(x => x.Id == oModelLabourRateDetail.Id).Select(x => x.WageRate).FirstOrDefault();
                if (query != null)
                    mstVocLaborDetail.WageRate = DataConversion.ValueRound2(Convert.ToDecimal(query));
                else
                    mstVocLaborDetail.WageRate = 0;
                mstVocLaborDetail.CostPerSec = DataConversion.ValueRound2((decimal)(mstVocLaborDetail.WageRate / oModelVoc.PerDayShiftHrs / oModelVoc.WorkingDays));
                mstVocLaborDetail.Total = DataConversion.ValueRound2((decimal)((mstVocLaborDetail.WageRate * mstVocLaborDetail.NoOfWorkers) / oModelVoc.MonthlyVolume));
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        #endregion

        #region Electricity Calculation

        private void onChangeVOCElectricityCycleTimeSec()
        {
            try
            {
                var query = oDetailActivity.Where(x => x.FkactivityTypeId == oModelActivityType.Id).Select(x => x.CycleTimeSec).FirstOrDefault();
                if (query != null)
                    mstVocElecticcityDetail.CycleTimeSec = DataConversion.ValueRound2(Convert.ToDecimal(query));
                else
                    mstVocElecticcityDetail.CycleTimeSec = 0;
                mstVocElecticcityDetail.NoOfWorkers = DataConversion.ValueRound2((decimal)(((mstVocElecticcityDetail.CycleTimeSec * oModelVoc.MonthlyVolume) / 3600) / oModelVoc.WorkingDays / oModelVoc.PerDayShiftHrs));
                mstVocElecticcityDetail.NoOfMachine = DataConversion.ValueRound2((decimal)(mstVocElecticcityDetail.NoOfWorkers / oModelVoc.NoOfShift));
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeVOCElectricityKWPerHours()
        {
            try
            {
                mstVocElecticcityDetail.KwperHrs = DataConversion.ValueRound2((decimal)(mstVocElecticcityDetail.ActualKwperHrs * mstVocElecticcityDetail.IncFactor));
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeVOCElectricityCostPerSec()
        {
            try
            {
                mstVocElecticcityDetail.CostPerSec = DataConversion.ValueRound2((decimal)((mstVocElecticcityDetail.KwperHrs * mstVocElecticcityDetail.RatePerUnit)/3600));
                mstVocElecticcityDetail.Total = DataConversion.ValueRound2((decimal)(mstVocElecticcityDetail.CostPerSec * mstVocElecticcityDetail.CycleTimeSec));
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        #endregion

        #region Dyes And Mold Calculation

        private void onChangeVOCDyesAndMoldCycleTimeSec()
        {
            try
            {
                var query = oDetailActivity.Where(x => x.FkactivityTypeId == oModelActivityType.Id).Select(x => x.CycleTimeSec).FirstOrDefault();
                if (query != null)
                    mstVocDyesDetail.CycleTimeSec = DataConversion.ValueRound2(Convert.ToDecimal(query));
                else
                    mstVocDyesDetail.CycleTimeSec = 0;
                mstVocDyesDetail.NoOfWorkers = DataConversion.ValueRound2((decimal)(((mstVocDyesDetail.CycleTimeSec * oModelVoc.MonthlyVolume) / 3600) / oModelVoc.WorkingDays / oModelVoc.PerDayShiftHrs));
                mstVocDyesDetail.Nos = DataConversion.ValueRound2((decimal)(mstVocDyesDetail.NoOfWorkers / oModelVoc.NoOfShift));
                mstVocDyesDetail.Total = DataConversion.ValueRound2((decimal)(mstVocDyesDetail.Cost / mstVocDyesDetail.Nos));

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeVOCDyesAndMoldTypeCost()
        {
            try
            {
                var cost = oResouceTypeList.Where(x => x.ResrDescription == oModelResouceType.ResrDescription).FirstOrDefault();
                if (cost != null)
                    mstVocDyesDetail.Cost = DataConversion.ValueRound2((decimal)cost.Cost);
                else
                    mstVocDyesDetail.Cost = 0;
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }


        #endregion

        #region Tools Calculation

        private void onChangeVOCToolsCycleTimeSec()
        {
            try
            {
                var query = oDetailActivity.Where(x => x.FkactivityTypeId == oModelActivityType.Id).Select(x => x.CycleTimeSec).FirstOrDefault();
                if (query != null)
                    mstVocToolsDetail.CycleTimeSec = DataConversion.ValueRound2(Convert.ToDecimal(query));
                else
                    mstVocToolsDetail.CycleTimeSec = 0;
                mstVocToolsDetail.NoOfWorkers = DataConversion.ValueRound2((decimal)(((mstVocToolsDetail.CycleTimeSec * oModelVoc.MonthlyVolume) / 3600) / oModelVoc.WorkingDays / oModelVoc.PerDayShiftHrs));
                mstVocToolsDetail.Nos = DataConversion.ValueRound2((decimal)(mstVocToolsDetail.NoOfWorkers / oModelVoc.NoOfShift));
                mstVocToolsDetail.Total = DataConversion.ValueRound2((decimal)(mstVocToolsDetail.Cost / mstVocToolsDetail.Nos));

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeVOCToolsCost()
        {
            try
            {
                var cost = oResouceTypeList.Where(x => x.ResrDescription == oModelResouceType.ResrDescription).FirstOrDefault();
                if (cost != null)
                    mstVocToolsDetail.Cost = DataConversion.ValueRound2((decimal)cost.Cost);
                else
                    mstVocToolsDetail.Cost = 0;
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        #endregion

        #region Gasoline Calculation

        private void onChangeVOCGasolineCycleTimeSec()
        {
            try
            {
                var query = oDetailActivity.Where(x => x.FkactivityTypeId == oModelActivityType.Id).Select(x => x.CycleTimeSec).FirstOrDefault();
                if (query != null)
                    mstVocGasolineDetail.CycleTimeSec = DataConversion.ValueRound2(Convert.ToDecimal(query));
                else
                    mstVocGasolineDetail.CycleTimeSec = 0;
                mstVocGasolineDetail.NoOfWorkers = DataConversion.ValueRound2((decimal)(((mstVocGasolineDetail.CycleTimeSec * oModelVoc.MonthlyVolume) / 3600) / oModelVoc.WorkingDays / oModelVoc.PerDayShiftHrs));
                mstVocGasolineDetail.Nos = DataConversion.ValueRound2((decimal)(mstVocGasolineDetail.NoOfWorkers / oModelVoc.NoOfShift));
                mstVocGasolineDetail.Total = DataConversion.ValueRound2((decimal)(mstVocGasolineDetail.Cost / mstVocGasolineDetail.Nos));

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeVOCGasolineCost()
        {
            try
            {
                var cost = oResouceTypeList.Where(x => x.ResrDescription == oModelResouceType.ResrDescription).FirstOrDefault();
                if (cost != null)
                    mstVocGasolineDetail.Cost = DataConversion.ValueRound2((decimal)cost.Cost);
                else
                    mstVocGasolineDetail.Cost = 0;
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        #endregion

        private async void Submit()
        {
            try
            {
                if (DialogFor == "VOCFinishedGoodsItem")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOCResourceMasterData")
                {
                    if (oModelResourceForVOC.Id > 0)
                    {

                        MudDialog.Close(DialogResult.Ok<MstResource>(oModelResourceForVOC));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOCActivityTab")
                {
                    if (oModelActivityType.Id == 0 || oModelVOCActivity.IncFactor == null || oModelVOCActivity.CycleTimeSec == null)
                    {
                        Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return;
                    }
                    oModelVOCActivity.FkactivityTypeId = oModelActivityType.Id;
                    oModelVOCActivity.FkactivityDescription = oModelActivityType.Description;
                    MudDialog.Close(DialogResult.Ok<TrnsVocactivityDetail>(oModelVOCActivity));
                }
                if (DialogFor == "VOCMachineTab")
                {
                    if (oModelActivityType.Id == 0 )
                    {
                        Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return;
                    }
                    mstVocMachineDetail.FkactivityTypeId = oModelActivityType.Id;
                    mstVocMachineDetail.FkactivityDescription = oModelActivityType.Description;
                    mstVocMachineDetail.MachineType = oModelResouceType.ResrDescription;
                    MudDialog.Close(DialogResult.Ok<TrnsVocmachineDetail>(mstVocMachineDetail));
                }
                if (DialogFor == "VOCLaborTab")
                {
                    if (oModelActivityType.Id == 0 || oModelLabourRateDetail.Fkid == 0)
                    {
                        Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return;
                    }
                    mstVocLaborDetail.FkactivityTypeId = oModelActivityType.Id;
                    mstVocLaborDetail.FkactivityDescription = oModelActivityType.Description;
                    mstVocLaborDetail.FklaborRateId = (int)oModelLabourRateDetail.Fkid;
                    mstVocLaborDetail.LaborDescription = oModelLabourRateDetail.Description;
                    MudDialog.Close(DialogResult.Ok<TrnsVoclaborDetail>(mstVocLaborDetail));
                }
                if (DialogFor == "VOCElectricityTab")
                {
                    if (oModelActivityType.Id == 0 )
                    {
                        Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return;
                    }
                    mstVocElecticcityDetail.FkactivityTypeId = oModelActivityType.Id;
                    mstVocElecticcityDetail.FkactivityDescription = oModelActivityType.Description;
                    mstVocElecticcityDetail.MachineType = oModelResouceType.ResrDescription;
                    MudDialog.Close(DialogResult.Ok<TrnsVocelectricityDetail>(mstVocElecticcityDetail));
                }
                if (DialogFor == "VOCDyesAndMoldTab")
                {
                    if (oModelActivityType.Id == 0 || oModelResouceType.ResrDescription == null)
                    {
                        Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return;
                    }
                    mstVocDyesDetail.FkactivityTypeId = oModelActivityType.Id;
                    mstVocDyesDetail.FkactivityDescription = oModelActivityType.Description;
                    mstVocDyesDetail.DyesAndMold = oModelResouceType.ResrDescription;
                    MudDialog.Close(DialogResult.Ok<TrnsVocdyesAndMoldDetail>(mstVocDyesDetail));
                }
                if (DialogFor == "VOCToolsTab")
                {
                    if (oModelActivityType.Id == 0 )
                    {
                        Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return;
                    }
                    mstVocToolsDetail.FkactivityTypeId = oModelActivityType.Id;
                    mstVocToolsDetail.FkactivityDescription = oModelActivityType.Description;
                    mstVocToolsDetail.ToolsType = oModelResouceType.ResrDescription;
                    MudDialog.Close(DialogResult.Ok<TrnsVoctoolsDetail>(mstVocToolsDetail));
                }
                if (DialogFor == "VOCGasolineTab")
                {
                    if (oModelActivityType.Id == 0 )
                    {
                        Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return;
                    }
                    mstVocGasolineDetail.FkactivityTypeId = oModelActivityType.Id;
                    mstVocGasolineDetail.FkactivityDescription = oModelActivityType.Description;
                    mstVocGasolineDetail.GasolineType = oModelResouceType.ResrDescription;
                    MudDialog.Close(DialogResult.Ok<TrnsVocgasolineDetail>(mstVocGasolineDetail));
                }
                if (DialogFor == "VOCMaster")
                {
                    if (oModelTrnsVoc.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsVoc>(oModelTrnsVoc));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Events

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Loading = true;
                if (DialogFor == "VOCFinishedGoodsItem")
                {
                    await GetFinishedGoodsItem();
                    //await GetRawItems();
                }
                if (DialogFor == "VOCResourceMasterData")
                {
                    await GetAllRescourceMaster();
                }
                if (DialogFor == "VOCActivityTab")
                {
                    oModelVOCActivity.ActualTimeCycle = 0;
                    oModelVOCActivity.CycleTimeSec = 0;
                    oModelVOCActivity.IncFactor = 0;
                }
                if (DialogFor == "VOCMachineTab")
                {
                    await GetAllMachine();
                    oResouceTypeList = oModelResource.MstResourceDetails.Where(x => x.TypeOfResr == "Machine").ToList();
                    foreach (var item in oResouceTypeList)
                    {
                        oModelResouceType.Id = item.Id;
                        oModelResouceType.ResrDescription = item.ResrDescription;
                    }
                }
                if (DialogFor == "VOCLaborTab")
                {
                    await GetAllLaborDesciption();
                }
                if (DialogFor == "VOCElectricityTab")
                {
                    oResouceTypeList = oModelResource.MstResourceDetails.Where(x => x.TypeOfResr == "Electricity").ToList();
                    foreach (var item in oResouceTypeList)
                    {
                        oModelResouceType.Id = item.Id;
                        oModelResouceType.ResrDescription = item.ResrDescription;
                    }
                }
                if (DialogFor == "VOCDyesAndMoldTab")
                {
                    await GetAllDyes();
                    oResouceTypeList = oModelResource.MstResourceDetails.Where(x => x.TypeOfResr == "Dyes").ToList();
                    foreach (var item in oResouceTypeList)
                    {
                        oModelResouceType.Id = item.Id;
                        oModelResouceType.ResrDescription = item.ResrDescription;
                    }
                }
                if (DialogFor == "VOCToolsTab")
                {
                    await GetAllTools();
                    oResouceTypeList = oModelResource.MstResourceDetails.Where(x => x.TypeOfResr == "Tools").ToList();
                    foreach (var item in oResouceTypeList)
                    {
                        oModelResouceType.Id = item.Id;
                        oModelResouceType.ResrDescription = item.ResrDescription;
                    }
                }
                if (DialogFor == "VOCGasolineTab")
                {
                    await GetAllGasoline();
                    oResouceTypeList = oModelResource.MstResourceDetails.Where(x => x.TypeOfResr == "Gasoline").ToList();
                    foreach (var item in oResouceTypeList)
                    {
                        oModelResouceType.Id = item.Id;
                        oModelResouceType.ResrDescription = item.ResrDescription;
                    }
                }
                if (DialogFor == "VOCMaster")
                {
                    await GetAllVOC();
                }
                await GetAllActivityType();
                Loading = false;
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }

        }

        private void RowClickEvent(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                selectedItems1.Add(oSAPModels);
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }

        private void RowClickEventRes(TableRowClickEventArgs<MstResource> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventVOC(TableRowClickEventArgs<TrnsVoc> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        #endregion
    }
}
