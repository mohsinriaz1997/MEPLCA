using CA.API.Models;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.General
{
    public partial class DetailDialog
    {
        #region InjectService

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IResourceMasterData _mstResource { get; set; }

        [Inject]
        public IGroupSetupMaster _mstGroupSetupMaster { get; set; }

        [Inject]
        public ICostDriversType _mstCostDriverType { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }

        [Inject]
        public IActivityType _mstActivityType { get; set; }

        [Parameter]
        public MstSalesPriceListDetail SalesPriceList { get; set; }

        private MstSalesPriceList oModel = new MstSalesPriceList();
        private List<MstSalesPriceListDetail> oDetail = new List<MstSalesPriceListDetail>();
        private List<MstItemPriceListDetail> oDetailitem = new List<MstItemPriceListDetail>();
        private MstSalesPriceListDetail mstSalesPriceListDetail = new MstSalesPriceListDetail();

        private TrnsFohdriverRatesDetail mstFohdriverRatesDetail = new TrnsFohdriverRatesDetail();
        private TrnsFohdriverRate mstFohdriveRate = new TrnsFohdriverRate();

        private TrnsVohmachineDetail mstVohmachineDetail = new TrnsVohmachineDetail();
        private TrnsVohmachineDetail mstVohmachineRate = new TrnsVohmachineDetail();

        private TrnsVohlabourDetail mstVohLaborDetail = new TrnsVohlabourDetail();
        private TrnsVohlabourDetail mstVohLaborRate = new TrnsVohlabourDetail();

        private TrnsVohelectricityDetail mstVohElecticityDetail = new TrnsVohelectricityDetail();
        private TrnsVohelectricityDetail mstVohElecticityRate = new TrnsVohelectricityDetail();

        private TrnsVohtoolsDetail mstVohToolsDetail = new TrnsVohtoolsDetail();
        private TrnsVohtoolsDetail mstVohToolsRate = new TrnsVohtoolsDetail();

        private TrnsVohgasolineDetail mstVohGasolineDetail = new TrnsVohgasolineDetail();
        private TrnsVohgasolineDetail mstVohgasolineRate = new TrnsVohgasolineDetail();

        private TrnsVocactivityDetail oModelVOCActivity = new TrnsVocactivityDetail();
        private TrnsVocactivityDetail mstVocactivityRate = new TrnsVocactivityDetail();

        //TrnsVocgasolineDetail mstVocGasolineDetail = new TrnsVocgasolineDetail();
        //TrnsVocgasolineDetail mstVocGasolineRate = new TrnsVocgasolineDetail();

        private TrnsVocmachineDetail mstVocMachineDetail = new TrnsVocmachineDetail();
        private TrnsVocmachineDetail mstVocMachineRate = new TrnsVocmachineDetail();

        private TrnsVocelectricityDetail mstVocElecticcityDetail = new TrnsVocelectricityDetail();
        private TrnsVocelectricityDetail mstVocElecticcityRate = new TrnsVocelectricityDetail();

        private TrnsVocdyesAndMoldDetail mstVocDyesDetail = new TrnsVocdyesAndMoldDetail();
        private TrnsVocdyesAndMoldDetail mstVocDyesRate = new TrnsVocdyesAndMoldDetail();

        private TrnsVoctoolsDetail mstVocToolsDetail = new TrnsVoctoolsDetail();
        private TrnsVoctoolsDetail mstVocToolsRate = new TrnsVoctoolsDetail();

        private TrnsVocgasolineDetail mstVocGasolineDetail = new TrnsVocgasolineDetail();
        private TrnsVocgasolineDetail mstVocGasolineRate = new TrnsVocgasolineDetail();

        private TrnsVohdyesAndMoldDetail mstVohDyesDetail = new TrnsVohdyesAndMoldDetail();
        private TrnsVohdyesAndMoldDetail mstVohDyesRate = new TrnsVohdyesAndMoldDetail();

        private MstItemPriceList mstItemPriceList = new MstItemPriceList();
        private MstItemPriceListDetail mstItemPriceListDetail = new MstItemPriceListDetail();

        private TrnsSalesQuatationDetail mstSalesQuatationDetail = new TrnsSalesQuatationDetail();
        //TrnsSalesQuatationDetail mstSalesQuatationDetail = new TrnsSalesQuatationDetail();

        private TrnsDirectMaterialDetail mstTrnsDirectMaterialDetail = new TrnsDirectMaterialDetail();

        private MstLabourRateDetail mstLabourRateList = new MstLabourRateDetail();
        private TrnsVoclaborDetail mstVocLaborDetail = new TrnsVoclaborDetail();

        private TrnsFohrate oModelTrnsFohrate = new TrnsFohrate();
        private TrnsFohcost oModelTrnsFohCost = new TrnsFohcost();
        private TrnsFohratesDetail oModelListTrnsFohrate = new TrnsFohratesDetail();
        private TrnsFohcostDetail oModelListTrnsFohCost = new TrnsFohcostDetail();
        private List<TrnsFohratesDetail> oDetailListTrnsFohrate = new List<TrnsFohratesDetail>();
        private List<TrnsFohratesDetail> oDetailTrnsFohrate = new List<TrnsFohratesDetail>();

        private List<MstItemPriceListDetail> oDetailList = new List<MstItemPriceListDetail>();

        #endregion InjectService

        #region Variables

        private bool Loading = false;

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public MstResourceDetail oDetailParaTax { get; set; } = new MstResourceDetail();
        [Parameter]
        public MstResource oDetailPara { get; set; } = new MstResource();
        [Parameter]
        public TrnsFohratesDetail oDetailFohRate { get; set; } = new TrnsFohratesDetail();
       [Parameter]
        public TrnsFohcostDetail oDetailFohCost { get; set; } = new TrnsFohcostDetail();

        [Parameter]
        public List<TrnsVocactivityDetail> oDetailActivity { get; set; } = new List<TrnsVocactivityDetail>();

        [Parameter]
        public TrnsVoc oModelVoc { get; set; } = new TrnsVoc();

        [Parameter]
        public MstResource oModelResource { get; set; } = new MstResource();

        [Parameter]
        public MstLabourRateDetail oDetailLaborRate { get; set; } = new MstLabourRateDetail();

        [Parameter]
        public MstItemPriceListDetail oDetailParaItem { get; set; } = new MstItemPriceListDetail();

        [Parameter]
        public MstSalesPriceListDetail oDetailParaSales { get; set; } = new MstSalesPriceListDetail();

        [Parameter]
        public TrnsFohdriverRatesDetail oDetailFohdriverRatesDetail { get; set; } = new TrnsFohdriverRatesDetail();
        [Parameter]
        public TrnsDirectMaterial oDetailDirectMaterial { get; set; } = new TrnsDirectMaterial();

        [Parameter]
        public string DialogFor { get; set; }

        [Parameter]
        public string ProductCode { get; set; }

        [Parameter]
        public string year { get; set; }

        [Parameter]
        public string month { get; set; }
        private string searchString1 = "";
        private List<MstCostDriversType> oCostDriversTypeList = new List<MstCostDriversType>();
        private MstCostDriversType oModelCostDriversType = new MstCostDriversType();

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();

        private MstActivityType oModelActivityType = new MstActivityType();
        private List<MstActivityType> oActivityTypeList = new List<MstActivityType>();

        private MstResourceDetail oModelResouceType = new MstResourceDetail();
        private List<MstResourceDetail> oResouceTypeList = new List<MstResourceDetail>();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };
        private TrnsVocactivityDetail mstVocactivityDetail = new TrnsVocactivityDetail();
        //private TrnsVocactivityDetail mstVocactivityRate = new TrnsVocactivityDetail();

        private MstResourceDetail oModelDetail = new MstResourceDetail();
        private TrnsVocactivityDetail oModelActivityDetail = new TrnsVocactivityDetail();
        private TrnsDirectMaterial omodel = new TrnsDirectMaterial();
        private TrnsFohcost omodelTrnsFohcost = new TrnsFohcost();
        private TrnsDirectMaterialDetail oModelDetailTrns = new TrnsDirectMaterialDetail();
        private TrnsVocactivityDetail oModelDetailTrnsActivity = new TrnsVocactivityDetail();
        private TrnsFohcostDetail oModelDetailFohcostDetail = new TrnsFohcostDetail();
        private MstItemPriceListDetail oModelDetailItem = new MstItemPriceListDetail();
        private MstLabourRateDetail oModelLaborRateDetail = new MstLabourRateDetail();

        private MstGroupSetup oModelMstGroupSetup1 = new MstGroupSetup();
        private MstResourceDetail oModelMstResourceDetail = new MstResourceDetail();
        private MstGroupSetup oModelMstGroupSetup2 = new MstGroupSetup();
        private MstGroupSetup oModelMstGroupSetup3 = new MstGroupSetup();
        private MstGroupSetup oModelMstGroupSetup4 = new MstGroupSetup();
        private List<MstGroupSetup> oMstGroupSetupList1 = new List<MstGroupSetup>();
        private List<MstResourceDetail> oMstResourceDyesDetailList = new List<MstResourceDetail>();
        private List<MstGroupSetup> oMstGroupSetupList2 = new List<MstGroupSetup>();
        private List<MstGroupSetup> oMstGroupSetupList3 = new List<MstGroupSetup>();
        private List<MstGroupSetup> oMstGroupSetupList4 = new List<MstGroupSetup>();

        //List<MstResourceDetail> oMstResourceDyesDetailList = new List<MstResourceDetail>();
        private List<MstResourceDetail> oMstResourceMachineDetailList = new List<MstResourceDetail>();

        private MstLabourRateDetail oModelLabourRate = new MstLabourRateDetail();
        private List<MstLabourRateDetail> oMstLaborRateDetailList = new List<MstLabourRateDetail>();

        private List<MstResourceDetail> oMstResourceElectricityDetailList = new List<MstResourceDetail>();
        private List<MstResourceDetail> oMstResourceToolsDetailList = new List<MstResourceDetail>();
        private List<MstResourceDetail> oMstResourceGasolineDetailList = new List<MstResourceDetail>();

        private SAPModels oSAPModels = new SAPModels();
        private List<SAPModels> SAPModelsList = new List<SAPModels>();
        private MudTable<SAPModels> _table;

        private int selectedRowNumber = -1;

        private List<string> clickedEvents = new();
        private void Cancel() => MudDialog.Cancel();
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
        private void RowClickEvent(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                clickedEvents.Add("Row has been clicked");

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
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
        #endregion Variables

        #region Functions

        private async Task<IEnumerable<MstCostType>> SearchCostType(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oCostTypeList.Select(o => new MstCostType
                    {
                        Id = o.Id,
                        Description = o.Description
                    }).ToList();
                var res = oCostTypeList.Where(x => x.FlgActive == true && x.Description.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new MstCostType
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
        }
        private async Task GetAllCostType()
        {
            try
            {
                oCostTypeList = await _mstCostType.GetAllData();
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
                var res = oActivityTypeList.Where(x => x.FlgActive == true && x.Description.ToUpper().Contains(value.ToUpper())).ToList();
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

        public void onChangeVOCMachineTypeNoofWorkerAndNos()
        {
            try
            {
                mstVocMachineDetail.NoOfWorkers = (decimal)((decimal)((mstVocMachineDetail.CycleTimeSec * oModelVoc.MonthlyVolume) / 3600) / oModelVoc.WorkingDays / oModelVoc.PerDayShiftHrs);
                mstVocMachineDetail.Nos = (decimal)((decimal)mstVocMachineDetail.NoOfWorkers / oModelVoc.NoOfShift);
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void noOfworkersElecticity()
        {
            try
            {
            var MonthlyVolume = oModelVoc.MonthlyVolume;
            var cyccletimesec = mstVocElecticcityDetail.CycleTimeSec;
            var WorkingDays = oModelVoc.WorkingDays;
            var PerDayShiftHours = oModelVoc.PerDayShiftHrs;
            var formula = (cyccletimesec * MonthlyVolume) / 3600 / WorkingDays / PerDayShiftHours;
            mstVocElecticcityDetail.NoOfWorkers = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void noOfworkersDyes()
        {
            try
            {
            var MonthlyVolume = oModelVoc.MonthlyVolume;
            var cyccletimesec = mstVocDyesDetail.CycleTimeSec;
            var WorkingDays = oModelVoc.WorkingDays;
            var PerDayShiftHours = oModelVoc.PerDayShiftHrs;
            var formula = (cyccletimesec * MonthlyVolume) / 3600 / WorkingDays / PerDayShiftHours;
            mstVocDyesDetail.NoOfWorkers = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void noOfworkersGasoline()
        {
            try
            {
            var MonthlyVolume = oModelVoc.MonthlyVolume;
            var cyccletimesec = mstVocGasolineDetail.CycleTimeSec;
            var WorkingDays = oModelVoc.WorkingDays;
            var PerDayShiftHours = oModelVoc.PerDayShiftHrs;
            var formula = (cyccletimesec * MonthlyVolume) / 3600 / WorkingDays / PerDayShiftHours;
            mstVocGasolineDetail.NoOfWorkers = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void noOfworkersTools()
        {
            try
            {
            var MonthlyVolume = oModelVoc.MonthlyVolume;
            var cyccletimesec = mstVocToolsDetail.CycleTimeSec;
            var WorkingDays = oModelVoc.WorkingDays;
            var PerDayShiftHours = oModelVoc.PerDayShiftHrs;
            var formula = (cyccletimesec * MonthlyVolume) / 3600 / WorkingDays / PerDayShiftHours;
            mstVocToolsDetail.NoOfWorkers = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void nos()
        {
            try
            {
            

            }
            catch (Exception ex)
            {
            Logs.GenerateLogs(ex);
            }
        }

        public void nosDyes()
        {
            try
            {
            var noofworkers = mstVocDyesDetail.NoOfWorkers;
            var noofshifts = oModelVoc.NoOfShift;
            var formula = noofworkers / noofshifts;
            mstVocDyesDetail.Nos = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void nosTools()
        {
            try
            {
            var noofworkers = mstVocToolsDetail.NoOfWorkers;
            var noofshifts = oModelVoc.NoOfShift;
            var formula = noofworkers / noofshifts;
            mstVocToolsDetail.Nos = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void KWH()
        {
            try
            {
            var ActualKWPerHour = mstVocElecticcityDetail.ActualKwperHrs;
            var incfactor = mstVocElecticcityDetail.IncFactor;
            var formula = ActualKWPerHour / incfactor;
            mstVocElecticcityDetail.KwperHrs = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void ECperSec()
        {
            try
            {
            var KWPerHour = mstVocElecticcityDetail.KwperHrs;
            var rate = mstVocElecticcityDetail.RatePerUnit;
            var formula = (KWPerHour * rate) / 3600;
            mstVocElecticcityDetail.KwperHrs = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void noofmachine()
        {
            try
            {
            var noofworkers = mstVocElecticcityDetail.NoOfWorkers;
            var noofshifts = oModelVoc.NoOfShift;
            var formula = noofworkers / noofshifts;
            mstVocElecticcityDetail.NoOfMachine = Convert.ToDecimal(formula);

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
                mstVocMachineDetail.Total = Convert.ToDecimal(total);
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void totalElectricitytab()
        {
            try
            {
            var cost = mstVocElecticcityDetail.CostPerSec;
            var cycletime = mstVocElecticcityDetail.CycleTimeSec;
            //var MonthlyVolume = oModelVoc.MonthlyVolume;
            //var lifeyear = mstVocMachineDetail.LifeYears;
            var formula = (cost * cycletime);
            mstVocElecticcityDetail.Total = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void totalDyestab()
        {
            try
            {
            var cost = mstVocDyesDetail.Cost;
            var nos = mstVocDyesDetail.Nos;
            //var MonthlyVolume = oModelVoc.MonthlyVolume;
            //var lifeyear = mstVocMachineDetail.LifeYears;
            var formula = (cost / nos);
            mstVocDyesDetail.Total = Convert.ToDecimal(formula);

            }
            catch (Exception ex) 
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void totalTooltab()
        {
            try
            {
            var cost = mstVocDyesDetail.Cost;
            var nos = mstVocDyesDetail.Nos;
            //var MonthlyVolume = oModelVoc.MonthlyVolume;
            //var lifeyear = mstVocMachineDetail.LifeYears;
            var formula = (cost / nos);
            mstVocDyesDetail.Total = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void totalToolstab()
        {
            try
            {
            var cost = mstVocToolsDetail.Cost;
            var nos = mstVocToolsDetail.Nos;
            //var MonthlyVolume = oModelVoc.MonthlyVolume;
            //var lifeyear = mstVocMachineDetail.LifeYears;
            var formula = (cost / nos);
            mstVocToolsDetail.Total = Convert.ToDecimal(formula);

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        public void Actvitycost()
        {
            try
            {
            var query = oDetailActivity.Where(x => x.FkactivityTypeId == oModelActivityType.Id).Select(x => x.CycleTimeSec).FirstOrDefault();
            if (query != null)
            {
                //oDetailActivity.Clear();
                mstVocMachineDetail.CycleTimeSec = Convert.ToDecimal(query);
            }
            else
            {
            }

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private async Task<IEnumerable<MstActivityType>> SearchActivityTypeMachine(string value)
        {
            try
            {
                await Task.Delay(1);
                //if (string.IsNullOrWhiteSpace(value))
                //    return oActivityTypeList.Select(o => new MstActivityType
                //    {
                //        Id = o.Id,
                //        Description = o.Description
                //    }).ToList();
                ////var query= oActivityTypeList.Select(x=>x.Description).Join(oDetailActivity., r => r.EmpId, p => p.EmpId, (r, p) => new { r.FirstName, r.LastName, p.DepartmentName });
                //var res = oActivityTypeList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();

                //return res.Select(x => new MstActivityType
                //{
                //    Id = x.Id,
                //    Description = x.Description
                //}).ToList();
                var activityTypes = (from a in oActivityTypeList
                               where a.FlgActive == true
                               select a
                             ).ToList();
                return activityTypes;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }

            //mstLabourRateList.FkcostTypeId = oModelCostType.Description;
        }

        //private async Task GetAllCostType()
        //{
        //    try
        //    {
        //        oCostTypeList = await _mstCostType.GetAllData();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //    }
        //}

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

        private async Task<IEnumerable<MstGroupSetup>> SearchGroupType1(string value)
        {
            try
            {
                await Task.Delay(1);
                //if (string.IsNullOrWhiteSpace(value))
                //    return oMstGroupSetupList1.Where(t => t.Type == "Group Type 1").Select(o => new MstGroupSetup
                //    {
                //        Id = o.Id,
                //        Description = o.Description
                //    }).ToList();
                //var res = oMstGroupSetupList1.Where(x => x.Description.ToUpper().Contains(value.ToUpper()) && x.Type == "Group Type 1").ToList();
                //return res.Select(x => new MstGroupSetup
                //{
                //    Id = x.Id,
                //    Description = x.Description
                //}).ToList();
                var activeGroupTypeList = (from a in oMstGroupSetupList1
                                     where a.FlgActive == true
                                     select a
             ).ToList();
                return activeGroupTypeList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<IEnumerable<MstGroupSetup>> SearchGroupType2(string value)
        {
            try
            {
                await Task.Delay(1);
                //if (string.IsNullOrWhiteSpace(value))
                //    return oMstGroupSetupList2.Where(t => t.Type == "Group Type 2").Select(o => new MstGroupSetup
                //    {
                //        Id = o.Id,
                //        Description = o.Description
                //    }).ToList();
                //var res = oMstGroupSetupList2.Where(x => x.Description.ToUpper().Contains(value.ToUpper()) && x.Type == "Group Type 2").ToList();
                //return res.Select(x => new MstGroupSetup
                //{
                //    Id = x.Id,
                //    Description = x.Description
                //}).ToList();
                var activeGroupTypeList2 = (from a in oMstGroupSetupList2
                                           where a.FlgActive == true
                                           select a).ToList();
                return activeGroupTypeList2;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<IEnumerable<MstGroupSetup>> SearchGroupType3(string value)
        {
            try
            {
                await Task.Delay(1);
                //if (string.IsNullOrWhiteSpace(value))
                //    return oMstGroupSetupList3.Where(t => t.Type == "Group Type 3").Select(o => new MstGroupSetup
                //    {
                //        Id = o.Id,
                //        Description = o.Description
                //    }).ToList();
                //var res = oMstGroupSetupList3.Where(x => x.Description.ToUpper().Contains(value.ToUpper()) && x.Type == "Group Type 3").ToList();
                //return res.Select(x => new MstGroupSetup
                //{
                //    Id = x.Id,
                //    Description = x.Description
                //}).ToList();
                var activeGroupTypeList3 = (from a in oMstGroupSetupList3
                                            where a.FlgActive == true
                                            select a).ToList();
                return activeGroupTypeList3;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<IEnumerable<MstGroupSetup>> SearchGroupType4(string value)
        {
            try
            {
                await Task.Delay(1);
                //if (string.IsNullOrWhiteSpace(value))
                //    return oMstGroupSetupList4.Where(t => t.Type == "Group Type 4").Select(o => new MstGroupSetup
                //    {
                //        Id = o.Id,
                //        Description = o.Description
                //    }).ToList();
                //var res = oMstGroupSetupList4.Where(x => x.Description.ToUpper().Contains(value.ToUpper()) && x.Type == "Group Type 4").ToList();
                //return res.Select(x => new MstGroupSetup
                //{
                //    Id = x.Id,
                //    Description = x.Description
                //}).ToList();
                var activeGroupTypeList4 = (from a in oMstGroupSetupList4
                                            where a.FlgActive == true
                                            select a).ToList();
                return activeGroupTypeList4;
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
                    return oMstResourceDyesDetailList.Where(t => t.TypeOfResr == "Dyes").Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        //Cost = o.Cost,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceDyesDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper()) && x.TypeOfResr == "Dyes").ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    //Cost = x.Cost,
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
                    return oMstResourceMachineDetailList.Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Fkid = o.Fkid,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceMachineDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper())).ToList();
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
                        Fkid = o.Fkid,
                        Description = o.Description,
                    }).ToList();
                var res = oMstLaborRateDetailList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new MstLabourRateDetail
                {
                    Fkid = x.Fkid,
                    Description = x.Description,
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<IEnumerable<MstResourceDetail>> searchResourceCostMachine(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceMachineDetailList.Where(t => t.TypeOfResr == "Dyes").Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Cost = o.Cost,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceMachineDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper()) && x.TypeOfResr == "Machine").ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    Cost = x.Cost,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        //private async Task<IEnumerable<MstResourceDetail>> searchResourceCostTools(string value)
        //{
        //    try
        //    {
        //        await Task.Delay(1);
        //        if (string.IsNullOrWhiteSpace(value))
        //            return oMstResourceMachineDetailList.Where(t => t.TypeOfResr == "Tools").Select(o => new MstResourceDetail
        //            {
        //                Id = o.Id,
        //                Cost = o.Cost,
        //                ResrDescription = o.ResrDescription
        //            }).ToList();
        //        var res = oMstResourceMachineDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper()) && x.TypeOfResr == "Tools").ToList();
        //        return res.Select(x => new MstResourceDetail
        //        {
        //            Id = x.Id,
        //            Cost = x.Cost,
        //            ResrDescription = x.ResrDescription
        //        }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //        return null;
        //    }
        //}
        private async Task<IEnumerable<MstResourceDetail>> searchResourceCostTools(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceMachineDetailList.Where(t => t.TypeOfResr == "Tools").Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Cost = o.Cost,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceMachineDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper()) && x.TypeOfResr == "Tools").ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    Cost = x.Cost,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<IEnumerable<MstResourceDetail>> searchResourceCostDyes(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceMachineDetailList.Where(t => t.TypeOfResr == "Dyes").Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        Cost = o.Cost,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceMachineDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper()) && x.TypeOfResr == "Dyes").ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    Cost = x.Cost,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        private async Task<IEnumerable<MstResourceDetail>> searchResourceDescriptionElectricity(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oMstResourceElectricityDetailList.Where(t => t.TypeOfResr == "Electricity").Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceElectricityDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper()) && x.TypeOfResr == "Electricity").ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
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
                    return oMstResourceToolsDetailList.Where(t => t.TypeOfResr == "Tools").Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceToolsDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper()) && x.TypeOfResr == "Tools").ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
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
                    return oMstResourceGasolineDetailList.Where(t => t.TypeOfResr == "Gasoline").Select(o => new MstResourceDetail
                    {
                        Id = o.Id,
                        ResrDescription = o.ResrDescription
                    }).ToList();
                var res = oMstResourceGasolineDetailList.Where(x => x.ResrDescription.ToUpper().Contains(value.ToUpper()) && x.TypeOfResr == "Gasoline").ToList();
                return res.Select(x => new MstResourceDetail
                {
                    Id = x.Id,
                    ResrDescription = x.ResrDescription
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        //private async Task GetAllResourceDesciption()
        //{
        //    try
        //    {
        //        oMstResourceDyesDetailList = await _mstResource.GetAllData();
        //        oMstResourceMachineDetailList = await _mstResource.GetAllData();
        //        oMstResourceElectricityDetailList = await _mstResource.GetAllData();
        //        oMstResourceToolsDetailList = await _mstResource.GetAllData();
        //        oMstResourceGasolineDetailList = await _mstResource.GetAllData();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //    }

        //}
        private async Task GetAllGroupSetup()
        {
            try
            {
                oMstGroupSetupList1 = await _mstGroupSetupMaster.GetAllData();
                oMstGroupSetupList2 = await _mstGroupSetupMaster.GetAllData();
                oMstGroupSetupList3 = await _mstGroupSetupMaster.GetAllData();
                oMstGroupSetupList4 = await _mstGroupSetupMaster.GetAllData();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllResourceDesciption()
        {
            try
            {
                //oMstResourceDyesDetailList = await _mstResource.GetAllDataDyes();
                //oMstResourceDyesDetailList.Where()
                oMstResourceMachineDetailList = await _mstResource.GetAllDataDyes();
                //oMstResourceElectricityDetailList = await _mstResource.GetAllData();
                oMstResourceToolsDetailList = await _mstResource.GetAllDataTools();
                oMstResourceGasolineDetailList = await _mstResource.GetAllDataGasoline();
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
                //oMstResourceElectricityDetailList = await _mstResource.GetAllData();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private void OnChange()
        {
            try
            {

                double cost = 0;
                if (oModelDetail.Rate == null)
                {
                    oModelDetail.Rate = 0;
                }
                if (oModelDetail.Price == null)
                {
                    oModelDetail.Price = 0;
                }
                if (oModelDetail.LandedFactor == null)
                {
                    oModelDetail.LandedFactor = 0;
                }
                cost = Convert.ToDouble(oModelDetail.Rate * oModelDetail.Price * oModelDetail.LandedFactor);
                oModelDetail.Cost = Convert.ToDecimal(cost);
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeCycleValuforMachine()
        {
            try
            {
                var cycleChange = mstVocactivityDetail.CycleTimeSec;
                cycleChange = mstVocMachineDetail.CycleTimeSec;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeCycleValuforGasoline()
        {
            try
            {
                var cycleChange = mstVocactivityDetail.CycleTimeSec;
                cycleChange = mstVocGasolineDetail.CycleTimeSec;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeCycleValuforTools()
        {
            try
            {
                var cycleChange = mstVocactivityDetail.CycleTimeSec;
                cycleChange = mstVocToolsDetail.CycleTimeSec;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeCycle()
        {
            try
            {
                decimal ChangeCycle;
                ChangeCycle = Convert.ToDecimal(mstVocactivityDetail.ActualTimeCycle * mstVocactivityDetail.IncFactor);
                mstVocactivityDetail.CycleTimeSec = ChangeCycle;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }

        }

        private void onChangeAmountPKR()
        {
            try
            {
                decimal ammountPkr;
                ammountPkr = Convert.ToInt32(oModelDetailTrns.ConsQty * oModelDetailTrns.UnitPricePkr);
                oModelDetailTrns.AmountPkr = ammountPkr;
                oModelDetailTrns.ChangeConsQty = oModelDetailTrns.ConsQty;
                oModelDetailTrns.ChangePricePkr = oModelDetailTrns.UnitPricePkr;
                //onChangeTotalMaterialCost();
                //onChangeTotalImpactCost();
                //onChangeTotalLocalCost();

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        //private void onChangeTotalMaterialCost()
        //{
        //    try
        //    {
        //        decimal totalMaterialCost;
        //        totalMaterialCost = (decimal)oModelDetailTrns.AmountPkr;
        //        oModelDetailTrns.TotalMaterialCost = totalMaterialCost;

        //    }
        //    catch (Exception ex)
        //    {

        //        Logs.GenerateLogs(ex);
        //    }
        //}

        //private void onChangeTotalImpactCost()
        //{
        //    try
        //    {
        //        decimal totalMaterialCost;
        //        totalMaterialCost = (decimal)oModelDetailTrns.AmountPkr;
        //        oModelDetailTrns.TotalImportCost = totalMaterialCost;

        //    }
        //    catch (Exception ex)
        //    {

        //        Logs.GenerateLogs(ex);
        //    }
        //}

        //private void onChangeTotalLocalCost()
        //{
        //    try
        //    {
        //        decimal localCost;
        //        localCost = Convert.ToDecimal(oModelDetailTrns.TotalMaterialCost - oModelDetailTrns.TotalLocalCost);
        //        oModelDetailTrns.TotalLocalCost = localCost;

        //    }
        //    catch (Exception ex)
        //    {

        //        Logs.GenerateLogs(ex);
        //    }
        //}
        private async Task OpenSapDataDialogBlanketAgreement(DialogOptions options, MstItemPriceListDetail mstItemPriceListDetail)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "BlanketAgreement");
                parameters.Add("ProductCode", mstItemPriceListDetail.ItemCode);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstItemPriceListDetail.BlanketAgreement = res.bpName;
                    mstItemPriceListDetail.BasicPrice = Convert.ToDecimal(res.BlUnitPrice);
                    mstItemPriceListDetail.Currency = res.BLCurrency;
                    if (mstItemPriceListDetail.Currency == "PKR")
                    {
                        mstItemPriceListDetail.Rate = 1;
                        var unitpricepkr = mstItemPriceListDetail.BasicPrice * mstItemPriceListDetail.Rate;
                        mstItemPriceListDetail.UnitPricePkr = unitpricepkr;

                    }
                    //mstItemPriceListDetail.ItemName = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private void OnChangeUnitPrice()
        {
            try
            {
                int unitprice = 0;
                unitprice = Convert.ToInt32(mstItemPriceListDetail.Rate * mstItemPriceListDetail.BasicPrice);
                mstItemPriceListDetail.UnitPricePkr = unitprice;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void OnChangeInsuranceCost()
        {
            try
            {
                decimal insurancePercent;
                //decimal FreightCost;
                insurancePercent = ((decimal)mstItemPriceListDetail.InsuranceCostPer) / 100;
                int insurancecost = 0;
                insurancecost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * insurancePercent);
                mstItemPriceListDetail.InsuranceCostValue = insurancecost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
            //insurancecost=
        }

        private void OnChangeFreightCost()
        {
            try
            {
                decimal ferightPercent;
                //decimal FreightCost;
                ferightPercent = ((decimal)mstItemPriceListDetail.FreightCostPer) / 100;
                int freightcost = 0;
                freightcost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * ferightPercent);
                mstItemPriceListDetail.FreightCostValue = freightcost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeLandedCost()
        {
            try
            {
                decimal landedCostPercent;
                landedCostPercent = ((decimal)mstItemPriceListDetail.LandingCostPer) / 100;
                int LandedCost = 0;
                LandedCost = (int)(Convert.ToDecimal(mstItemPriceListDetail.UnitPricePkr + mstItemPriceListDetail.FreightCostValue + mstItemPriceListDetail.InsuranceCostValue) * landedCostPercent);
                mstItemPriceListDetail.LandingCostValue = LandedCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeWarehouseCharges()
        {
            try
            {
                decimal WareHousePercent;
                WareHousePercent = ((decimal)mstItemPriceListDetail.WhchargesPer) / 100;
                int WarehouseCost = 0;
                WarehouseCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * WareHousePercent);
                mstItemPriceListDetail.WhchargesValue = WarehouseCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeYardPayment()
        {
            try
            {
                decimal yardPaymentPercent;
                yardPaymentPercent = ((decimal)mstItemPriceListDetail.YardPaymentPer) / 100;
                int yardPaymentCost = 0;
                yardPaymentCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * yardPaymentPercent);
                mstItemPriceListDetail.YardPaymentValue = yardPaymentCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeClearingCharges()
        {
            try
            {
                decimal clearingChargesPercent;
                clearingChargesPercent = ((decimal)mstItemPriceListDetail.CleaningChargesPer) / 100;
                int ClearingchargesCost = 0;
                ClearingchargesCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * clearingChargesPercent);
                mstItemPriceListDetail.CleaningChargesValue = ClearingchargesCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeImportValue()
        {
            try
            {
                //decimal ImportvaluePercent;
                //ImportvaluePercent = ((decimal)ImportValue) / 100;
                int ClearingchargesCost = 0;
                ClearingchargesCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr + mstItemPriceListDetail.LandingCostValue + mstItemPriceListDetail.InsuranceCostValue + mstItemPriceListDetail.FreightCostValue);
                mstItemPriceListDetail.ImportValue = ClearingchargesCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeCustomDuty()
        {
            try
            {
                decimal CustomDutyPercent;
                CustomDutyPercent = ((decimal)mstItemPriceListDetail.CustomDutyPer) / 100;
                int customDutyCost = 0;
                customDutyCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * CustomDutyPercent);
                mstItemPriceListDetail.CustomDutyValue = customDutyCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeRegulatory()
        {
            try
            {
                decimal RegulatoryPercent;
                RegulatoryPercent = ((decimal)mstItemPriceListDetail.RegulatoryDutyPer) / 100;
                int RegulatoryCost = 0;
                RegulatoryCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * RegulatoryPercent);
                mstItemPriceListDetail.RegulatoryDutyValue = RegulatoryCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeAditionalDuty()
        {
            try
            {
                decimal AditionalPercent;
                AditionalPercent = ((decimal)mstItemPriceListDetail.AdditionalDutyPer) / 100;
                int AditionalCost = 0;
                AditionalCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * AditionalPercent);
                mstItemPriceListDetail.AdditionalDutyValue = AditionalCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeExciseDuty()
        {
            try
            {
                decimal ExcisePercent;
                ExcisePercent = ((decimal)mstItemPriceListDetail.ExciseDutyPer) / 100;
                int AditionalCost = 0;
                AditionalCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr * ExcisePercent);
                mstItemPriceListDetail.ExciseDutyValue = AditionalCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeDuties()
        {
            try
            {
                int dutiesCost = 0;
                dutiesCost = Convert.ToInt32(mstItemPriceListDetail.CustomDutyValue + mstItemPriceListDetail.RegulatoryDutyValue + mstItemPriceListDetail.AdditionalDutyValue + mstItemPriceListDetail.ExciseDutyValue);
                mstItemPriceListDetail.DutiesValue = dutiesCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeSalesTax()
        {
            try
            {
                decimal salesTaxPercent;
                salesTaxPercent = ((decimal)mstItemPriceListDetail.SalesTaxPer) / 100;
                int AditionalCost = 0;
                AditionalCost = (int)(Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr + mstItemPriceListDetail.DutiesValue) * (salesTaxPercent));
                mstItemPriceListDetail.SalesTaxValue = AditionalCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private void onChangeIncomeTax()
        {
            try
            {
                int dutiesCost = 0;
                dutiesCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr + mstItemPriceListDetail.InsuranceCostValue + mstItemPriceListDetail.FreightCostValue + mstItemPriceListDetail.LandingCostValue + mstItemPriceListDetail.WhchargesValue + mstItemPriceListDetail.YardPaymentValue + mstItemPriceListDetail.CustomDutyValue + mstItemPriceListDetail.RegulatoryDutyValue + mstItemPriceListDetail.AdditionalDutyValue + mstItemPriceListDetail.ExciseDutyValue + mstItemPriceListDetail.CleaningChargesValue + mstItemPriceListDetail.SalesTaxValue + mstItemPriceListDetail.SalesTaxValue + mstItemPriceListDetail.Others);
                mstItemPriceListDetail.IncomeTaxValue = dutiesCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }
        private void onChangeFinalItemPrice()
        {
            try
            {
                //                Unit Price PKR + Insurance Cost Value +Freight Cost Value+
                //Landing Cost Value +Warehouse Charges Value +Yard
                //Payment Value +Custom Duty Value +Regulatory Duty Value
                //+Additional Duty Value +Excise Duty Value +Clearing
                //Charges Value +Sales Tax Value +Income Tax Value +Others
                int finalItemPrice = 0;
                finalItemPrice = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr + mstItemPriceListDetail.InsuranceCostValue + mstItemPriceListDetail.FreightCostValue + mstItemPriceListDetail.LandingCostValue + mstItemPriceListDetail.WhchargesValue + mstItemPriceListDetail.RegulatoryDutyValue + mstItemPriceListDetail.AdditionalDutyValue + mstItemPriceListDetail.ExciseDutyValue + mstItemPriceListDetail.CleaningChargesValue + mstItemPriceListDetail.SalesTaxValue + mstItemPriceListDetail.IncomeTaxValue + mstItemPriceListDetail.Others);
                mstItemPriceListDetail.FinalItemPrice = Convert.ToInt32(finalItemPrice);
                //int dutiesCost = 0;
                //dutiesCost = Convert.ToInt32(mstItemPriceListDetail.UnitPricePkr + mstItemPriceListDetail.InsuranceCostValue + mstItemPriceListDetail.FreightCostValue + mstItemPriceListDetail.LandingCostValue + mstItemPriceListDetail.WhchargesValue + mstItemPriceListDetail.YardPaymentValue + mstItemPriceListDetail.CustomDutyValue + mstItemPriceListDetail.RegulatoryDutyValue + mstItemPriceListDetail.AdditionalDutyValue + mstItemPriceListDetail.ExciseDutyValue + mstItemPriceListDetail.CleaningChargesValue + mstItemPriceListDetail.SalesTaxValue + mstItemPriceListDetail.SalesTaxValue + mstItemPriceListDetail.Others);
                //mstItemPriceListDetail.IncomeTaxValue = dutiesCost;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private async Task Submit()
        {
            try
            {
            await Task.Delay(2);
            if (DialogFor == "ResourceMaster")
            {
                if (string.IsNullOrWhiteSpace(oModelDetail.TypeOfResr) || oModelDetail.ResrDescription == null || oModelDetail.CurrCodeSap == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                MudDialog.Close(DialogResult.Ok<MstResourceDetail>(oModelDetail));
            }
            if (DialogFor == "MonthlyVohRlMachineTab")
            {
                if (mstVohmachineDetail.MachineVohamount == null || mstVohmachineDetail.MachineVohrate == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                //mstVocactivityDetail.FkactivityTypeId = oModelActivityType.Id;
                //mstVocactivityDetail.FkactivityDescription = oModelActivityType.Description;
                MudDialog.Close(DialogResult.Ok<TrnsVohmachineDetail>(mstVohmachineDetail));
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
                if (mstVocMachineDetail.CycleTimeSec == 0)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                mstVocMachineDetail.FkactivityTypeId = oModelActivityType.Id;
                mstVocMachineDetail.FkactivityDescription = oModelActivityType.Description;
                //mstVocMachineDetail.FkresourceId = (int)oModelResouceType.Fkid;
                mstVocMachineDetail.MachineType = (oModelResouceType.ResrDescription);
                //mstVocMachineDetail.Cost = Convert.ToDecimal(oModelResouceType.Cost);
                MudDialog.Close(DialogResult.Ok<TrnsVocmachineDetail>(mstVocMachineDetail));
            }
                if (DialogFor == "VOCLaborTab")
                {
                    if (mstVocLaborDetail.CycleTimeSec == 0)
                    {
                        Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return;
                    }
                    mstVocLaborDetail.FkactivityTypeId = oModelActivityType.Id;
                    mstVocLaborDetail.FkactivityDescription = oModelActivityType.Description;
                    mstVocLaborDetail.LaborDescription = oModelLabourRate.Description;
                    MudDialog.Close(DialogResult.Ok<TrnsVoclaborDetail>(mstVocLaborDetail));
                }
                if (DialogFor == "MonthlyVocRlElectricityTab")
            {
                if (mstVocElecticcityDetail.CycleTimeSec == 0)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                mstVocElecticcityDetail.FkactivityTypeId = oModelActivityType.Id;
                mstVocElecticcityDetail.MachineType = (oModelResouceType.Id.ToString());
                //mstVocElecticcityDetail.Cost =Convert.ToDecimal(oModelResouceType.Cost);
                MudDialog.Close(DialogResult.Ok<TrnsVocelectricityDetail>(mstVocElecticcityDetail));
            }
            if (DialogFor == "MonthlyVocRlDyesTab")
            {
                if (mstVocDyesDetail.CycleTimeSec == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                mstVocDyesDetail.FkactivityTypeId = oModelActivityType.Id;
                mstVocDyesDetail.DyesAndMold = (oModelResouceType.Id.ToString());
                mstVocDyesDetail.Cost = Convert.ToDecimal(oModelResouceType.Cost);
                //mstVocElecticcityDetail.Cost =Convert.ToDecimal(oModelResouceType.Cost);
                MudDialog.Close(DialogResult.Ok<TrnsVocdyesAndMoldDetail>(mstVocDyesDetail));
            }
            if (DialogFor == "MonthlyVocRlToolsTab")
            {
                if (mstVocDyesDetail.CycleTimeSec == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                mstVocToolsDetail.FkactivityTypeId = oModelActivityType.Id;
                //mstVocToolsDetail.DyesAndMold = (oModelResouceType.Id.ToString());
                mstVocToolsDetail.Cost = Convert.ToDecimal(oModelResouceType.Cost);
                //mstVocElecticcityDetail.Cost =Convert.ToDecimal(oModelResouceType.Cost);
                MudDialog.Close(DialogResult.Ok<TrnsVoctoolsDetail>(mstVocToolsDetail));
            }
            if (DialogFor == "MonthlyVocRlGasolineTab")
            {
                if (mstVocGasolineDetail.CycleTimeSec == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                mstVocGasolineDetail.FkactivityTypeId = oModelActivityType.Id;
                mstVocGasolineDetail.GasolineType = (oModelResouceType.Id.ToString());
                mstVocGasolineDetail.Cost = Convert.ToDecimal(oModelResouceType.Cost);
                //mstVocElecticcityDetail.Cost =Convert.ToDecimal(oModelResouceType.Cost);
                MudDialog.Close(DialogResult.Ok<TrnsVocgasolineDetail>(mstVocGasolineDetail));
            }
            if (DialogFor == "MonthlyFohDriverRatesCalculationRl")
            {
                if (string.IsNullOrWhiteSpace(mstFohdriverRatesDetail.ProductCode) || mstFohdriverRatesDetail.ProductName == null || mstFohdriverRatesDetail.DriverValue == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                MudDialog.Close(DialogResult.Ok<TrnsFohdriverRatesDetail>(mstFohdriverRatesDetail));
            }
            if (DialogFor == "LaborRate")
            {
                if (string.IsNullOrWhiteSpace(mstLabourRateList.Description))
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                mstLabourRateList.FkcostTypeId = oModelCostType.Id;
                MudDialog.Close(DialogResult.Ok<MstLabourRateDetail>(mstLabourRateList));
            }
            if (DialogFor == "SalePriceList")
            {
                if (string.IsNullOrWhiteSpace(mstSalesPriceListDetail.ProductCode) || mstSalesPriceListDetail.ProductDescription == null || mstSalesPriceListDetail.PerUnitSalesPrice == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                MudDialog.Close(DialogResult.Ok<MstSalesPriceListDetail>(mstSalesPriceListDetail));
            }
            //if (DialogFor == "ItemPriceListMaster")
            if (DialogFor == "ItemPriceList")
            {
                if (string.IsNullOrWhiteSpace(mstItemPriceListDetail.ItemCode) || mstItemPriceListDetail.ItemName == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                MudDialog.Close(DialogResult.Ok<MstItemPriceListDetail>(mstItemPriceListDetail));
            }
            if (DialogFor == "DirectMaterialRL")
            {
                if (string.IsNullOrWhiteSpace(oModelDetailTrns.ItemCode) || oModelDetailTrns.ItemName == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                MudDialog.Close(DialogResult.Ok<TrnsDirectMaterialDetail>(oModelDetailTrns));
            }
            if (DialogFor == "DirectMaterialFL")
            {
                if (string.IsNullOrWhiteSpace(oModelDetailTrns.ItemCode) || oModelDetailTrns.ItemName == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                MudDialog.Close(DialogResult.Ok<TrnsDirectMaterialDetail>(oModelDetailTrns));
            }
            if (DialogFor == "MonthlyFohCostRl")
            {
                if (string.IsNullOrWhiteSpace(oModelDetailFohcostDetail.AcctCode) || oModelDetailFohcostDetail.AcctDescription == null)
                {
                    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                MudDialog.Close(DialogResult.Ok<TrnsFohcostDetail>(oModelDetailFohcostDetail));
            }
            if (DialogFor == "FOHRateCalculationRl")
            {
                    //oModelListTrnsFohrate.FkcostDriversTypeId = Convert.ToInt32(oModelCostDriversType.Id);
                oModelListTrnsFohrate.FkcostDrivesTypeDescription = Convert.ToString(oModelCostDriversType.Description);
                //if ( Convert.ToDecimal((oModelListTrnsFohrate.Rate)))
                //{
                //    Snackbar.Add("Fill the required field(s).", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                //    return;
                //}
                MudDialog.Close(DialogResult.Ok<TrnsFohratesDetail>(oModelListTrnsFohrate));
            }

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSAPDataDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "Currency");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    var currencypkr = oDetailPara.CurrencySap;
                    oModelDetail.CurrCodeSap = res.CurrCode;
                    oModelDetail.CurrNameSap = res.CurrName;
                    DialogFor = "ResourceMaster";
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSAPDataDialogForItemLevel(DialogOptions options, string ProductCode)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("ProductCode", ProductCode);
                parameters.Add("DialogFor", "BomItemCode");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    oModelDetailTrns.ItemCode = res.BOMItemCode;
                    oModelDetailTrns.ItemName = res.BOMItemName;
                    oModelDetailTrns.ConsQty = Convert.ToDecimal(res.BOMQuantity);
                    oModelDetailTrns.Uom = res.BOMUOM;
                    //oModelDetailTrns.CurrNameSap = res.CurrName;
                    DialogFor = "DirectMaterialRL";
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private bool FilterFuncBomItem(SAPModels element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.BOMItemCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.BOMItemName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.BOMUOM.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }
        private bool FilterFuncBom(SAPModels element) => FilterFuncBomItem(element, searchString1);

        private async Task OpenSAPDataDialogForAccountDetail(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                //parameters.Add("ProductCode", ProductCode);
                parameters.Add("DialogFor", "AccountExpenseList");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    oModelDetailFohcostDetail.AcctCode = res.AcctCode;
                    oModelDetailFohcostDetail.AcctDescription = res.AcctName;
                    //oModelDetailTrns.ConsQty = Convert.ToDecimal(res.BOMQuantity);
                    //oModelDetailTrns.Uom = res.BOMUOM;
                    //oModelDetailTrns.CurrNameSap = res.CurrName;
                    DialogFor = "MonthlyFohCostRl";
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogForItemPriceList(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "ItemPriceListMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstItemPriceList)result.Data;
                    var ipl = res.MstItemPriceListDetails.ToList();
                    var currency = ipl[0].Currency;
                    var lc = ipl[0].LandingCostValue;
                    var unitprice = ipl[0].UnitPricePkr;
                    oModelDetailTrns.Currency = currency;
                    oModelDetailTrns.Lcfactor = lc;
                    oModelDetailTrns.UnitPricePkr = unitprice;
                    //oModelDetailTrns.UnitPriceFc = res.uni;
                    //oModelDetailTrns.Lcfactor = Convert.ToDecimal(ipl.);
                    //oModelDetailTrns.UnitPricePkr = Convert.ToDecimal(res.UnitPricePkr);
                    //oModelDetailTrns.CurrNameSap = res.CurrName;
                    DialogFor = "DirectMaterialRL";
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogForCostDriver(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "CostDriverListMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstCostDriversType)result.Data;

                    oModelCostDriversType.Code = res.Code;
                    oModelCostDriversType.Description = res.Description;

                    DialogFor = "MonthlyFohCostRl";
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogproductCode(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "Items");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstSalesPriceListDetail.ProductCode = res.Code;
                    mstSalesPriceListDetail.ProductDescription = res.Name;
                    DialogFor = "SalePriceList";
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForSalesPriceList(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "SalesItems");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstSalesPriceListDetail.ProductCode = res.ItemCode;
                    mstSalesPriceListDetail.ProductDescription = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogDirectMaterial(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "DirectMaterialMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsDirectMaterial)result.Data;
                    var import = res.TrnsDirectMaterialDetails.ToList();
                    //var updateimport = import.Select(x => x.TotalImportCost).FirstOrDefault();
                    //var updatelocal = import.Select(x => x.TotalLocalCost).FirstOrDefault();
                    //var updateMaterial = import.Select(x => x.TotalMaterialCost).FirstOrDefault();
                    //mstSalesQuatationDetail.ImportCost = Convert.ToInt32(updateimport);
                    //mstSalesQuatationDetail.LocalCost = Convert.ToInt32(updatelocal);
                    //mstSalesQuatationDetail.TotalRmcost = Convert.ToInt32(updateMaterial);
                    //mstSalesPriceListDetail.ProductDescription = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenSaveDataDialogDirectMaterialBom(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "DirectMaterialMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsDirectMaterial)result.Data;
                    var import = res.TrnsDirectMaterialDetails.ToList();
                    //var updateimport = import.Select(x => x.TotalImportCost).FirstOrDefault();
                    //var updatelocal = import.Select(x => x.TotalLocalCost).FirstOrDefault();
                    //var updateMaterial = import.Select(x => x.TotalMaterialCost).FirstOrDefault();
                    //mstSalesQuatationDetail.ImportCost = Convert.ToInt32(updateimport);
                    //mstSalesQuatationDetail.LocalCost = Convert.ToInt32(updatelocal);
                    //mstSalesQuatationDetail.TotalRmcost = Convert.ToInt32(updateMaterial);
                    //mstSalesPriceListDetail.ProductDescription = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogVOHMaster(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVoh)result.Data;
                    var import = res.TrnsVohmachineDetails.ToList();
                    var updateimport = import.Select(x => x.MachineVohrate).FirstOrDefault();
                    //var updatelocal = import.Select(x => x.TotalLocalCost).FirstOrDefault();
                    // var updateMaterial = import.Select(x => x.TotalMaterialCost).FirstOrDefault();
                    //mstSalesQuatationDetail.ImportCost = Convert.ToInt32(updateimport);
                    //mstSalesQuatationDetail.LocalCost = Convert.ToInt32(updatelocal);
                    //mstSalesQuatationDetail.TotalRmcost = Convert.ToInt32(updateMaterial);
                    //mstSalesPriceListDetail.ProductDescription = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForMonthlyFOH(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "FOHDriverRateItems");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstFohdriverRatesDetail.ProductCode = res.ItemCode;
                    mstFohdriverRatesDetail.ProductName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForMonthlyVOHMachine(DialogOptions options, string year, string month)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHMachineRateItems");
                parameters.Add("year", year);
                parameters.Add("month", month);

                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstVohmachineDetail.ProductCode = res.ItemCode;
                    mstVohmachineDetail.ProductName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForMonthlyVOHLabor(DialogOptions options, string year, string month)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHLaborRateItems");
                parameters.Add("year", year);
                parameters.Add("month", month);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstVohLaborDetail.ProductCode = res.ItemCode;
                    mstVohLaborDetail.ProductName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForMonthlyVOHElectricity(DialogOptions options, string year, string month)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHElectricityRateItems");
                parameters.Add("year", year);
                parameters.Add("month", month);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstVohElecticityDetail.ProductCode = res.ItemCode;
                    mstVohElecticityDetail.ProductName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForMonthlyVOHDyes(DialogOptions options, string year, string month)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHDyesRateItems");
                parameters.Add("year", year);
                parameters.Add("month", month);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstVohDyesDetail.ProductCode = res.ItemCode;
                    mstVohDyesDetail.ProductName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForMonthlyVOHTools(DialogOptions options, string year, string month)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHToolsRateItems");
                parameters.Add("year", year);
                parameters.Add("month", month);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstVohToolsDetail.ProductCode = res.ItemCode;
                    mstVohToolsDetail.ProductName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForMonthlyVOHGasoline(DialogOptions options, string year, string month)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHGasolineRateItems");
                parameters.Add("year", year);
                parameters.Add("month", month);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstVohGasolineDetail.ProductCode = res.ItemCode;
                    mstVohGasolineDetail.ProductName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogRMItemForItemPriceList(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "RawMaterialItemPriceList");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstItemPriceListDetail.ItemCode = res.ItemCode;
                    mstItemPriceListDetail.ItemName = res.ItemName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        //private async Task OpenSapDataDialogRMItemForItemPriceList(DialogOptions options)
        //{
        //    try
        //    {
        //        var parameters = new DialogParameters();
        //        parameters.Add("DialogFor", "SalesItems");
        //        var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
        //        var result = await dialog.Result;
        //        if (!result.Cancelled)
        //        {
        //            var res = (SAPModels)result.Data;
        //            mstItemPriceListDetail.ItemCode = res.ItemCode;
        //            mstItemPriceListDetail.ItemName = res.ItemName;
        //            DialogFor = res.DialogFor;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //    }
        //}

        private async Task<IEnumerable<MstCostDriversType>> SearchCostDriver(string value)
        {
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return oCostDriversTypeList.Select(o => new MstCostDriversType
                    {
                        Code = o.Code,
                        Description = o.Description
                    }).ToList();
                var res = oCostDriversTypeList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new MstCostDriversType
                {
                    Code = x.Code,
                    Description = x.Description
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
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
                decimal ChangeCycle;
                ChangeCycle = Convert.ToDecimal(oModelVOCActivity.ActualTimeCycle * oModelVOCActivity.IncFactor);
                oModelVOCActivity.CycleTimeSec = ChangeCycle;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllCostDriverType()
        {
            try
            {
                oCostDriversTypeList = await _mstCostDriverType.GetAllData();
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

                if (DialogFor == "ResourceMaster")
                {
                    if (oDetailPara.CurrencySap == "PKR")
                    {
                        oDetailParaTax.Rate = 0;
                    }
                    else
                    {
                        var rate = oDetailPara.RateSap;
                        oModelDetail.Rate = rate;
                    }
                    if (oDetailParaTax.TypeOfResr != null)
                    {
                        oModelDetail = oDetailParaTax;
                    }
                    else
                    {
                        //oModelDetail.Price = 0;
                        //oModelDetail.Cost = 0;
                        //oModelDetail.LandedFactor = 0;
                        //oModelDetail.Rate = 0;
                    }
                }
                //if (DialogFor == "BomItemCode")
                //{
                //    if (oDetailDirectMaterial.ProductCode != null)
                //    {
                //        omodel = oDetailDirectMaterial;
                //    }
                //    else
                //    {
                //        //oModelDetail.Price = 0;
                //        //oModelDetail.Cost = 0;
                //        //oModelDetail.LandedFactor = 0;
                //        //oModelDetail.Rate = 0;
                //    }
                //}
                if (DialogFor == "VOCActivityTab")
                {
                    //if (oDetailActivity.CycleTimeSec != null)
                    //{
                    //    oModelActivityDetail = oDetailActivity;
                    //}
                    //else
                    //{
                    oModelVOCActivity.ActualTimeCycle = 0;
                    oModelVOCActivity.CycleTimeSec = 0;
                    oModelVOCActivity.IncFactor = 0;
                    //    //oModelActivityDetail.Rate = 0;
                    //}
                }
                if (DialogFor == "VOCMachineTab")
                {
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
                if (DialogFor == "FOHRateCalculationRl")
                {
                    if (oDetailFohRate.Rate != null)
                    {
                        oModelListTrnsFohrate = oDetailFohRate;
                        oModelListTrnsFohrate.FkcostDriversTypeId = Convert.ToInt32(oModelCostDriversType.Code);
                        oModelTrnsFohrate.Fohrate = oModelTrnsFohrate.Fohrate;
                        await GetAllCostDriverType();

                    }
                    else
                    {
                        oModelTrnsFohrate.ProductName = null;
                        oModelTrnsFohrate.ProductCode = null;
                        oModelTrnsFohrate.DocNum = 0;
                        oModelTrnsFohrate.DocDate = null;
                        oModelTrnsFohrate.FkcostTypeId = null;

                        
                        
                        oModelTrnsFohrate.Fohrate = 0;
                        await GetAllCostDriverType();
                    }
                }
                if (DialogFor == "MonthlyFohCostRl")
                {
                    if (oDetailFohCost.AcctDescription != null)
                    {
                        //oModelListTrnsFohCost = oModelDetailFohcostDetail;
                        oDetailFohCost.CostDriver = Convert.ToString(oModelCostDriversType.Code);
                        oDetailFohCost.Fohamount = oModelDetailFohcostDetail.Fohamount;
                        oDetailFohCost.AcctDescription = oModelDetailFohcostDetail.AcctDescription;
                        oDetailFohCost.AcctCode = oModelDetailFohcostDetail.AcctCode;
                        oDetailFohCost.Amount = oModelDetailFohcostDetail.Amount;
                        oDetailFohCost.Vohamount = oModelDetailFohcostDetail.Vohamount;

                    }
                    else
                    {
                        //oModelTrnsFohCost.ProductName = null;
                        //oModelTrnsFohCost.ProductCode = null;
                        //oModelTrnsFohCost.DocNum = 0;
                        //oModelTrnsFohCost.DocDate = null;
                        //oModelTrnsFohCost.FkcostTypeId = null;


                        //oModelTrnsFohCost.Fohrate = 0;
                    }
                }
                if (DialogFor == "ItemPriceList")
                {
                    await GetAllGroupSetup();
                    if (oDetailParaItem.ItemCode != null)
                    {
                        mstItemPriceListDetail = oDetailParaItem;
                    }
                    else
                    {
                        //mstItemPriceListDetail.ItemCode = 0;
                        //mstItemPriceListDetail.ItemCode = 0;
                        //mstItemPriceListDetail. = 0;
                        //mstItemPriceListDetail.Rate = 0;
                    }
                }
                if (DialogFor == "SalePriceList")
                {
                    if (oDetailParaSales.ProductCode != null)
                    {
                        mstSalesPriceListDetail = oDetailParaSales;
                    }
                    else
                    {
                        //mstItemPriceListDetail.ItemCode = 0;
                        //mstItemPriceListDetail.ItemCode = 0;
                        //mstItemPriceListDetail. = 0;
                        //mstItemPriceListDetail.Rate = 0;
                    }
                }
                if (DialogFor == "MonthlyFohDriverRatesCalculationRl")
                {
                    if (oDetailFohdriverRatesDetail.ProductCode != null)
                    {
                        mstFohdriverRatesDetail = oDetailFohdriverRatesDetail;
                    }
                    else
                    {
                        //mstItemPriceListDetail.ItemCode = 0;
                        //mstItemPriceListDetail.ItemCode = 0;
                        //mstItemPriceListDetail. = 0;
                        //mstItemPriceListDetail.Rate = 0;
                    }
                }
                if (DialogFor == "LaborRate")
                {
                    await GetAllCostType();
                    if (oDetailLaborRate.Description != null)
                    {
                        mstLabourRateList = oDetailLaborRate;
                    }
                    else
                    {
                        //mstItemPriceListDetail.ItemCode = 0;
                        //mstItemPriceListDetail.ItemCode = 0;
                        //mstItemPriceListDetail. = 0;
                        //mstItemPriceListDetail.Rate = 0;
                    }
                }
                Loading = false;
                //await GetAllResourceDesciption();
                //await GetAllCostType();
                //await GetAllCostDriverType();
                //await GetAllActivityType();
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