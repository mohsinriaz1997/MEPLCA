using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.Cost_Allocations
{
    public partial class MonthlyVOHCalculation
    {
        #region InjectService

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        //[Inject]
        //public IResourceMasterData _mstUserProfiled { get; set; }
        [Inject]
        public IMonthlyVOHCalculation _mstUserProfiled { get; set; }

        [Inject]
        public ICostDriversType _mstCostDriverType { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }
        
        [Inject]
        public IVOC _mstVOC { get; set; }

        [Parameter]
        public string DialogFor { get; set; }
        [Parameter]
        public string year { get; set; }
        [Parameter]
        public string month { get; set; }

        #endregion InjectService

        #region Variables
        public string searchString;
        private bool Loading = false;
        private TrnsVohmachineDetail elementBeforeEdit;
        private TrnsVohlabourDetail elementBeforeEditLabour;
        private TrnsVohelectricityDetail elementBeforeEditElectricity;
        private TrnsVohdyesAndMoldDetail elementBeforeEditDyes;
        private TrnsVohtoolsDetail elementBeforeEditTools;
        private TrnsVohgasolineDetail elementBeforeEditGasoline;
        private bool blockSwitch = false;
        private bool blockSwitchLabour = false;
        private bool blockSwitchElectricity = false;
        private bool blockSwitchDyes = false;
        private bool blockSwitchTools = false;
        private List<string> editEvents = new();

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();
        private HashSet<TrnsVohmachineDetail> selectedItems1 = new HashSet<TrnsVohmachineDetail>();
        private HashSet<TrnsVohlabourDetail> selectedItemsLabour = new HashSet<TrnsVohlabourDetail>();
        private HashSet<TrnsVohelectricityDetail> selectedItemsElectricity = new HashSet<TrnsVohelectricityDetail>();
        private HashSet<TrnsVohdyesAndMoldDetail> selectedItemsDyes = new HashSet<TrnsVohdyesAndMoldDetail>();
        private HashSet<TrnsVohtoolsDetail> selectedItemsTools = new HashSet<TrnsVohtoolsDetail>();
        private HashSet<TrnsVohgasolineDetail> selectedItemsGasoline = new HashSet<TrnsVohgasolineDetail>();

        private TrnsVoh oModel = new TrnsVoh();
        private TrnsVoh oModelList = new TrnsVoh();
        private List<TrnsVohlabourDetail> oDetailLaborList = new List<TrnsVohlabourDetail>();
        private List<TrnsVohlabourDetail> oDetailLabor = new List<TrnsVohlabourDetail>();

        private List<TrnsVohmachineDetail> oDetailMachineList = new List<TrnsVohmachineDetail>();
        private TrnsVohmachineDetail oModelMachine = new TrnsVohmachineDetail();
        private List<TrnsVohmachineDetail> oDetailMachine = new List<TrnsVohmachineDetail>();

        private List<TrnsVohelectricityDetail> oDetailElecticityList = new List<TrnsVohelectricityDetail>();
        private List<TrnsVohelectricityDetail> oDetailElectricity = new List<TrnsVohelectricityDetail>();

        private List<TrnsVohdyesAndMoldDetail> oDetailDyesList = new List<TrnsVohdyesAndMoldDetail>();
        private List<TrnsVohdyesAndMoldDetail> oDetailDyes = new List<TrnsVohdyesAndMoldDetail>();

        private List<TrnsVohtoolsDetail> oDetailToolsList = new List<TrnsVohtoolsDetail>();
        private List<TrnsVohtoolsDetail> oDetailTools = new List<TrnsVohtoolsDetail>();

        private List<TrnsVohgasolineDetail> oDetailGasolineList = new List<TrnsVohgasolineDetail>();
        private List<TrnsVohgasolineDetail> oDetailGasoline = new List<TrnsVohgasolineDetail>();

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();
        
        private TrnsVoc oModelVOC = new TrnsVoc();
        private List<TrnsVoc> oVOCList = new List<TrnsVoc>();


        private YearModel oYearModel = new YearModel();
        private List<YearModel> oYearModelList = new List<YearModel>();

        private List<MstCostDriversType> oCostDriversTypeList = new List<MstCostDriversType>();
        private MstCostDriversType oModelCostDriversType = new MstCostDriversType();

        private MstSalesPriceList oModelSalesPriceList = new MstSalesPriceList();
        private List<MstSalesPriceListDetail> oDetailSalesPriceList = new List<MstSalesPriceListDetail>();
        private MstSalesPriceListDetail mstSalesPriceListDetail = new MstSalesPriceListDetail();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        #endregion Variables

        #region Functions

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

        private void OnDateChange()
        {
            oModel.DocDate = oModel.DocDate;
            //oModel.CurrencySap = null;
            //oModel.RateSap = null;
        }

        private async Task OpenAddDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "MonthlyVohRlMachineTab");
                parameters.Add("year", oYearModel.year);
                parameters.Add("month", oModel.Lmonth);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVohmachineDetail)result.Data;
                    var update = oDetailMachineList.Where(x => x.ProductCode == res.ProductCode && x.ProductName.ToLower() == res.ProductName.ToLower()).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oModel.Lmonth = oYearModel.year;
                        oDetailMachine.Add(res);
                        oDetailMachineList = oDetailMachine;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenSapDataDialogFGItemForMonthlyVOHMachine(DialogOptions options, TrnsVoh omdoel)
        {
            try
            {
                var parameters = new DialogParameters();
                var year = oYearModel.year;
                parameters.Add("DialogFor", "VOHMachineRateItems");
                parameters.Add("year", year);
                parameters.Add("month", oModel.Lmonth);

                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                oModel.Lyear = year;
                if (!result.Cancelled)
                {

                    

                    oModel.TrnsVohmachineDetails = (ICollection<TrnsVohmachineDetail>)result.Data;
                    foreach (var Line in oModel.TrnsVohmachineDetails)
                    {
                        var VOCData = (from a in oVOCList
                                       where a.ProductCode == Line.ProductCode && a.FlgDefault == true
                                       select a).FirstOrDefault();
                        
                        if (VOCData != null)
                        {
                            Line.MachineVohrate = VOCData.TrnsVocmachineDetails.Select(x => x.Total).Sum();
                            var vocMachineAmmount = Line.ProductQuantity * Line.MachineVohrate;
                            Line.MachineVohamount= vocMachineAmmount;
                        }
                        else
                        {
                            Line.MachineVohrate = 0;
                        }

                        Line.ProductName = Line.ProductName;
                        Line.ProductCode = Line.ProductCode;
                        Line.MachineVohrate = Line.MachineVohrate;
                        Line.MachineVohamount = Line.MachineVohamount;





                    }
                    oDetailMachineList = oModel.TrnsVohmachineDetails.ToList();
                    var sumFohRate = oDetailMachineList.Select(x => x.MachineVohrate);
                    var totalsumfohrate = sumFohRate.Sum();
                    oModel.TotalVohmachine = Convert.ToString(totalsumfohrate);
                    //oModel.TrnsVohmachineDetails =totalsumfohrate;
                    //var res = (SAPModels)result.Data;
                    //oModelMachine.ProductCode = res.ItemCode;
                    //oModelMachine.ProductName = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenSapDataDialogFGItemForMonthlyVOHLabor(DialogOptions options, TrnsVoh omodel)
        {
            try
            {
                var year = oYearModel.year;
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHLaborRateItems");
                parameters.Add("year", omodel.Lyear);
                parameters.Add("month", omodel.Lmonth);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                oModel.Lyear = year;
                if (!result.Cancelled)
                {
                    oModel.TrnsVohlabourDetails = (ICollection<TrnsVohlabourDetail>)result.Data;

                    foreach (var Line in oModel.TrnsVohlabourDetails)
                    {
                        var VOCData = (from a in oVOCList
                                       where a.ProductCode == Line.ProductCode && a.FlgDefault == true
                                       select a).FirstOrDefault();

                        if (VOCData != null)
                        {
                            Line.LabourVohrate = VOCData.TrnsVoclaborDetails.Select(x => x.Total).Sum();
                            var vocMachineAmmount = Line.ProductQuantity * Line.LabourVohrate;
                            Line.LabourVohamount = vocMachineAmmount;
                        }
                        else
                        {
                            Line.LabourVohrate = 0;
                        }

                        Line.ProductName = Line.ProductName;
                        Line.ProductCode = Line.ProductCode;
                        Line.LabourVohrate = Line.LabourVohrate;
                        Line.LabourVohamount = Line.LabourVohamount;
                    }
                    oDetailLaborList = oModel.TrnsVohlabourDetails.ToList();
                    var sumFohRate = oDetailLaborList.Select(x => x.LabourVohrate);
                    var totalsumfohrate = sumFohRate.Sum();
                    oModel.TotalVohlabor = Convert.ToString(totalsumfohrate);
                    //oModel.TrnsVohlabourDetails
                    //oModel.TrnsVohlabourDetails = Convert.ToDecimal(totalsumfohrate);
                    //var res = (SAPModels)result.Data;
                    //oModelMachine.ProductCode = res.ItemCode;
                    //oModelMachine.ProductName = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenSapDataDialogFGItemForMonthlyVOHElectricty(DialogOptions options, TrnsVoh omodel)
        {
            try
            {
                var year = oYearModel.year;
                oModel.Lyear = year;
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHElectricityRateItems");
                parameters.Add("year", omodel.Lyear);
                parameters.Add("month", omodel.Lmonth);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    oModel.TrnsVohelectricityDetails = (ICollection<TrnsVohelectricityDetail>)result.Data;
                    foreach (var Line in oModel.TrnsVohelectricityDetails)
                    {
                        //User already select item master and its detaail


                        //var itemDetail = (from a in oModel.TrnsVohmachineDetails

                        //                  select a).FirstOrDefault();

                        Line.ProductName = Line.ProductName;
                        Line.ProductCode = Line.ProductCode;
                        Line.ElectricityVohrate = Line.ElectricityVohrate;
                        Line.ElectricityVohamount = Line.ElectricityVohamount;





                    }
                    oDetailElecticityList = oModel.TrnsVohelectricityDetails.ToList();
                    var sumFohRate = oDetailElecticityList.Select(x => x.ElectricityVohrate);
                    var totalsumfohrate = sumFohRate.Sum();
                    oModel.TotalVohElectriccity = Convert.ToString(totalsumfohrate);
                    //var res = (SAPModels)result.Data;
                    //oModelMachine.ProductCode = res.ItemCode;
                    //oModelMachine.ProductName = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenSapDataDialogFGItemForMonthlyVOHDyes(DialogOptions options, TrnsVoh omodel)
        {
            try
            {
                var year = oYearModel.year;
                oModel.Lyear = year;
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHDyesRateItems");
                parameters.Add("year", omodel.Lyear);
                parameters.Add("month", omodel.Lmonth);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    oModel.TrnsVohdyesAndMoldDetails = (ICollection<TrnsVohdyesAndMoldDetail>)result.Data;
                    foreach (var Line in oModel.TrnsVohdyesAndMoldDetails)
                    {
                        var VOCData = (from a in oVOCList
                                       where a.ProductCode == Line.ProductCode && a.FlgDefault == true
                                       select a).FirstOrDefault();

                        if (VOCData != null)
                        {
                            Line.DyesAndMoldVohrate = (decimal)VOCData.TrnsVocdyesAndMoldDetails.Select(x => x.Total).Sum();
                            var vocMachineAmmount = Line.ProductQuantity * Line.DyesAndMoldVohrate;
                            Line.DyesAndMoldVohamount = (decimal)vocMachineAmmount;
                        }
                        else
                        {
                            Line.DyesAndMoldVohrate = 0;
                        }

                        Line.ProductName = Line.ProductName;
                        Line.ProductCode = Line.ProductCode;
                        Line.DyesAndMoldVohrate = Line.DyesAndMoldVohrate;
                        Line.DyesAndMoldVohamount = Line.DyesAndMoldVohamount;





                    }
                    oDetailDyesList = oModel.TrnsVohdyesAndMoldDetails.ToList();
                    var sumFohRate = oDetailDyesList.Select(x => x.DyesAndMoldVohrate);
                    var totalsumfohrate = sumFohRate.Sum();
                    oModel.TotalVohdyes = Convert.ToString(totalsumfohrate);

                    //var res = (SAPModels)result.Data;
                    //oModelMachine.ProductCode = res.ItemCode;
                    //oModelMachine.ProductName = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenSapDataDialogFGItemForMonthlyVOHTools(DialogOptions options, TrnsVoh omodel)
        {
            try
            {
                var year = oYearModel.year;
                oModel.Lyear = year;
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHToolsRateItems");
                parameters.Add("year", omodel.Lyear);
                parameters.Add("month", omodel.Lmonth);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    oModel.TrnsVohtoolsDetails = (ICollection<TrnsVohtoolsDetail>)result.Data;
                    foreach (var Line in oModel.TrnsVohtoolsDetails)
                    {
                        var VOCData = (from a in oVOCList
                                       where a.ProductCode == Line.ProductCode && a.FlgDefault == true
                                       select a).FirstOrDefault();

                        if (VOCData != null)
                        {
                            Line.ToolsVohrate = (decimal)VOCData.TrnsVoctoolsDetails.Select(x => x.Total).Sum();
                            var vocMachineAmmount = Line.ProductQuantity * Line.ToolsVohrate;
                            Line.ToolsVohamount = (decimal)vocMachineAmmount;
                        }
                        else
                        {
                            Line.ToolsVohrate = 0;
                        }

                        Line.ProductName = Line.ProductName;
                        Line.ProductCode = Line.ProductCode;
                        Line.ToolsVohrate = Line.ToolsVohrate;
                        Line.ToolsVohamount = Line.ToolsVohamount;





                    }
                    oDetailToolsList = oModel.TrnsVohtoolsDetails.ToList();
                    var sumFohRate = oDetailToolsList.Select(x => x.ToolsVohrate);
                    var totalsumfohrate = sumFohRate.Sum();
                    oModel.TotalTools = Convert.ToString(totalsumfohrate);
                    //var res = (SAPModels)result.Data;
                    //oModelMachine.ProductCode = res.ItemCode;
                    //oModelMachine.ProductName = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenSapDataDialogFGItemForMonthlyVOHGasoline(DialogOptions options, TrnsVoh omodel)
        {
            try
            {
                var year = oYearModel.year;
                oModel.Lyear = year;
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOHGasolineRateItems");
                parameters.Add("year", omodel.Lyear);
                parameters.Add("month", omodel.Lmonth);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    oModel.TrnsVohgasolineDetails = (ICollection<TrnsVohgasolineDetail>)result.Data;
                    foreach (var Line in oModel.TrnsVohgasolineDetails)
                    {
                        var VOCData = (from a in oVOCList
                                       where a.ProductCode == Line.ProductCode && a.FlgDefault == true
                                       select a).FirstOrDefault();

                        if (VOCData != null)
                        {
                            Line.GasolineVohrate = (decimal)VOCData.TrnsVocgasolineDetails.Select(x => x.Total).Sum();
                            var vocMachineAmmount = Line.ProductQuantity * Line.GasolineVohrate;
                            Line.GasolineVohamount = (decimal)vocMachineAmmount;
                        }
                        else
                        {
                            Line.GasolineVohrate = 0;
                        }

                        Line.ProductName = Line.ProductName;
                        Line.ProductCode = Line.ProductCode;
                        Line.GasolineVohrate = Line.GasolineVohrate;
                        Line.GasolineVohamount = Line.GasolineVohamount;





                    }
                    oDetailGasolineList = oModel.TrnsVohgasolineDetails.ToList();
                    var sumFohRate = oDetailGasolineList.Select(x => x.GasolineVohrate);
                    var totalsumfohrate = sumFohRate.Sum();
                    oModel.TotalVohgasoline = Convert.ToString(totalsumfohrate);
                    //var res = (SAPModels)result.Data;
                    //oModelMachine.ProductCode = res.ItemCode;
                    //oModelMachine.ProductName = res.ItemName;
                    //DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private bool FilterFunc(TrnsVohmachineDetail element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ProductCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        private bool FilterFuncLabor(TrnsVohlabourDetail element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ProductCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        private bool FilterFuncElectricity(TrnsVohelectricityDetail element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ProductCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        private bool FilterFuncDyes(TrnsVohdyesAndMoldDetail element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ProductCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        private bool FilterFuncTools(TrnsVohtoolsDetail element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ProductCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        private bool FilterFuncGasoline(TrnsVohgasolineDetail element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ProductCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }
        //private void AddEditionEventLabour(string message)
        //{
        //    editEvents.Add(message);
        //    StateHasChanged();
        //}
        private void BackupItem(object element)
        {
            elementBeforeEdit = new()
            {
                ProductCode = ((TrnsVohmachineDetail)element).ProductCode,
                ProductName = ((TrnsVohmachineDetail)element).ProductName,
                MachineVohamount = ((TrnsVohmachineDetail)element).MachineVohamount,
                MachineVohrate = ((TrnsVohmachineDetail)element).MachineVohrate,


            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((TrnsVohmachineDetail)element).ProductCode}");
        }
        private void BackupItemLabour(object element)
        {
            elementBeforeEditLabour = new()
            {
                ProductCode = ((TrnsVohlabourDetail)element).ProductCode,
                ProductName = ((TrnsVohlabourDetail)element).ProductName,
                LabourVohamount = ((TrnsVohlabourDetail)element).LabourVohamount,
                LabourVohrate = ((TrnsVohlabourDetail)element).LabourVohrate,


            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((TrnsVohlabourDetail)element).ProductCode}");
        }
        private void BackupItemElectricity(object element)
        {
            elementBeforeEditElectricity = new()
            {
                ProductCode = ((TrnsVohelectricityDetail)element).ProductCode,
                ProductName = ((TrnsVohelectricityDetail)element).ProductName,
                ElectricityVohamount = ((TrnsVohelectricityDetail)element).ElectricityVohamount,
                ElectricityVohrate = ((TrnsVohelectricityDetail)element).ElectricityVohrate,


            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((TrnsVohelectricityDetail)element).ProductCode}");
        }
        private void BackupItemDyes(object element)
        {
            elementBeforeEditDyes = new()
            {
                ProductCode = ((TrnsVohdyesAndMoldDetail)element).ProductCode,
                ProductName = ((TrnsVohdyesAndMoldDetail)element).ProductName,
                DyesAndMoldVohamount = ((TrnsVohdyesAndMoldDetail)element).DyesAndMoldVohamount,
                DyesAndMoldVohrate = ((TrnsVohdyesAndMoldDetail)element).DyesAndMoldVohrate,


            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((TrnsVohdyesAndMoldDetail)element).ProductCode}");
        }
        private void BackupItemTools(object element)
        {
            elementBeforeEditTools = new()
            {
                ProductCode = ((TrnsVohtoolsDetail)element).ProductCode,
                ProductName = ((TrnsVohtoolsDetail)element).ProductName,
                ToolsVohamount = ((TrnsVohtoolsDetail)element).ToolsVohamount,
                ToolsVohrate = ((TrnsVohtoolsDetail)element).ToolsVohrate,


            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((TrnsVohtoolsDetail)element).ProductCode}");
        }
        private void BackupItemGasoline(object element)
        {
            elementBeforeEditGasoline = new()
            {
                ProductCode = ((TrnsVohgasolineDetail)element).ProductCode,
                ProductName = ((TrnsVohgasolineDetail)element).ProductName,
                GasolineVohamount = ((TrnsVohgasolineDetail)element).GasolineVohamount,
                GasolineVohrate = ((TrnsVohgasolineDetail)element).GasolineVohrate,


            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((TrnsVohgasolineDetail)element).ProductCode}");
        }
        private void ResetItemToOriginalValues(object element)
        {
            ((TrnsVohmachineDetail)element).ProductCode = elementBeforeEdit.ProductCode;
            ((TrnsVohmachineDetail)element).ProductName = elementBeforeEdit.ProductName;
            ((TrnsVohmachineDetail)element).MachineVohamount = elementBeforeEdit.MachineVohamount;
            ((TrnsVohmachineDetail)element).MachineVohrate = elementBeforeEdit.MachineVohrate;


            AddEditionEvent($"RowEditCancel event: Editing of Element {((TrnsVohmachineDetail)element).ProductCode} cancelled");
        }
        private void ResetItemToOriginalValuesLabour(object element)
        {
            ((TrnsVohlabourDetail)element).ProductCode = elementBeforeEditLabour.ProductCode;
            ((TrnsVohlabourDetail)element).ProductName = elementBeforeEditLabour.ProductName;
            ((TrnsVohlabourDetail)element).LabourVohamount = elementBeforeEditLabour.LabourVohamount;
            ((TrnsVohlabourDetail)element).LabourVohrate = elementBeforeEditLabour.LabourVohamount;


            AddEditionEvent($"RowEditCancel event: Editing of Element {((TrnsVohlabourDetail)element).ProductCode} cancelled");
        }
        private void ResetItemToOriginalValuesElectricity(object element)
        {
            ((TrnsVohelectricityDetail)element).ProductCode = elementBeforeEditElectricity.ProductCode;
            ((TrnsVohelectricityDetail)element).ProductName = elementBeforeEditElectricity.ProductName;
            ((TrnsVohelectricityDetail)element).ElectricityVohamount = elementBeforeEditElectricity.ElectricityVohamount;
            ((TrnsVohelectricityDetail)element).ElectricityVohrate = elementBeforeEditElectricity.ElectricityVohamount;


            AddEditionEvent($"RowEditCancel event: Editing of Element {((TrnsVohelectricityDetail)element).ProductCode} cancelled");
        }
        private void ResetItemToOriginalValuesDyes(object element)
        {
            ((TrnsVohdyesAndMoldDetail)element).ProductCode = elementBeforeEditElectricity.ProductCode;
            ((TrnsVohdyesAndMoldDetail)element).ProductName = elementBeforeEditElectricity.ProductName;
            ((TrnsVohdyesAndMoldDetail)element).DyesAndMoldVohamount = elementBeforeEditDyes.DyesAndMoldVohamount;
            ((TrnsVohdyesAndMoldDetail)element).DyesAndMoldVohrate = elementBeforeEditDyes.DyesAndMoldVohamount;


            AddEditionEvent($"RowEditCancel event: Editing of Element {((TrnsVohtoolsDetail)element).ProductCode} cancelled");
        }
        private void ResetItemToOriginalValuesTools(object element)
        {
            ((TrnsVohtoolsDetail)element).ProductCode = elementBeforeEditElectricity.ProductCode;
            ((TrnsVohtoolsDetail)element).ProductName = elementBeforeEditElectricity.ProductName;
            ((TrnsVohtoolsDetail)element).ToolsVohamount = elementBeforeEditTools.ToolsVohamount;
            ((TrnsVohtoolsDetail)element).ToolsVohrate = elementBeforeEditTools.ToolsVohrate;


            AddEditionEvent($"RowEditCancel event: Editing of Element {((TrnsVohtoolsDetail)element).ProductCode} cancelled");
        }
        private void ResetItemToOriginalValuesGasoline(object element)
        {
            ((TrnsVohgasolineDetail)element).ProductCode = elementBeforeEditGasoline.ProductCode;
            ((TrnsVohgasolineDetail)element).ProductName = elementBeforeEditGasoline.ProductName;
            ((TrnsVohgasolineDetail)element).GasolineVohamount = elementBeforeEditGasoline.GasolineVohamount;
            ((TrnsVohgasolineDetail)element).GasolineVohrate = elementBeforeEditGasoline.GasolineVohrate;


            AddEditionEvent($"RowEditCancel event: Editing of Element {((TrnsVohgasolineDetail)element).ProductCode} cancelled");
        }
        private void ItemHasBeenCommitted(object element)
        {
            //oModelDetail.Clear();
            AddEditionEvent($"RowEditCommit event: Changes to Element {((TrnsVohmachineDetail)element).ProductCode} committed");
            //oModelDetail.Add((TrnsDirectMaterialDetail)element);
            //oModel.TrnsDirectMaterialDetails.Add(element);
        }
        private void ItemHasBeenCommittedLabour(object element)
        {
            //oModelDetail.Clear();
            AddEditionEvent($"RowEditCommit event: Changes to Element {((TrnsVohlabourDetail)element).ProductCode} committed");
            //oModelDetail.Add((TrnsDirectMaterialDetail)element);
            //oModel.TrnsDirectMaterialDetails.Add(element);
        }
        private void ItemHasBeenCommittedElectricity(object element)
        {
            //oModelDetail.Clear();
            AddEditionEvent($"RowEditCommit event: Changes to Element {((TrnsVohelectricityDetail)element).ProductCode} committed");
            //oModelDetail.Add((TrnsDirectMaterialDetail)element);
            //oModel.TrnsDirectMaterialDetails.Add(element);
        }
        private void ItemHasBeenCommittedDyes(object element)
        {
            //oModelDetail.Clear();
            AddEditionEvent($"RowEditCommit event: Changes to Element {((TrnsVohdyesAndMoldDetail)element).ProductCode} committed");
            //oModelDetail.Add((TrnsDirectMaterialDetail)element);
            //oModel.TrnsDirectMaterialDetails.Add(element);
        }
        private void ItemHasBeenCommittedTools(object element)
        {
            //oModelDetail.Clear();
            AddEditionEvent($"RowEditCommit event: Changes to Element {((TrnsVohtoolsDetail)element).ProductCode} committed");
            //oModelDetail.Add((TrnsDirectMaterialDetail)element);
            //oModel.TrnsDirectMaterialDetails.Add(element);
        }
        private void ItemHasBeenCommittedGasoline(object element)
        {
            //oModelDetail.Clear();
            AddEditionEvent($"RowEditCommit event: Changes to Element {((TrnsVohgasolineDetail)element).ProductCode} committed");
            //oModelDetail.Add((TrnsDirectMaterialDetail)element);
            //oModel.TrnsDirectMaterialDetails.Add(element);
        }
        private async Task OpenAddDialogLabor(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "MonthlyVohRlLaoburTab");
                parameters.Add("year", oYearModel.year);
                parameters.Add("month", oModel.Lmonth);

                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVohlabourDetail)result.Data;
                    var update = oDetailLaborList.Where(x => x.ProductCode == res.ProductCode && x.ProductName.ToLower() == res.ProductName.ToLower()).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oDetailLabor.Add(res);
                        oDetailLaborList = oDetailLabor;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogElectricity(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "MonthlyVohRlLElectricityTab");
                parameters.Add("year", oYearModel.year);
                parameters.Add("month", oModel.Lmonth);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVohelectricityDetail)result.Data;
                    var update = oDetailElecticityList.Where(x => x.ProductCode == res.ProductCode && x.ProductName.ToLower() == res.ProductName.ToLower()).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oDetailElectricity.Add(res);
                        oDetailElecticityList = oDetailElectricity;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogDyes(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "MonthlyVohRlDyesMoldTab");
                parameters.Add("year", oYearModel.year);
                parameters.Add("month", oModel.Lmonth);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVohdyesAndMoldDetail)result.Data;
                    var update = oDetailDyesList.Where(x => x.ProductCode == res.ProductCode && x.ProductName.ToLower() == res.ProductName.ToLower()).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oDetailDyes.Add(res);
                        oDetailDyesList = oDetailDyes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogTools(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "MonthlyVohRlToolsTab");
                parameters.Add("year", oYearModel.year);
                parameters.Add("month", oModel.Lmonth);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVohtoolsDetail)result.Data;
                    var update = oDetailToolsList.Where(x => x.ProductCode == res.ProductCode && x.ProductName.ToLower() == res.ProductName.ToLower()).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oDetailTools.Add(res);
                        oDetailToolsList = oDetailTools;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddDialogGasoline(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "MonthlyVohRlGasolineTab");
                parameters.Add("year", oYearModel.year);
                parameters.Add("month", oModel.Lmonth);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVohgasolineDetail)result.Data;
                    var update = oDetailGasolineList.Where(x => x.ProductCode == res.ProductCode && x.ProductName.ToLower() == res.ProductName.ToLower()).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oDetailGasoline.Add(res);
                        oDetailGasolineList = oDetailGasoline;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        //private async Task OpenAddDialogFooterLevel(DialogOptions options)
        //{
        //    try
        //    {
        //        var parameters = new DialogParameters();
        //        parameters.Add("DialogFor", "DirectMaterialFL");
        //        //parameters.Add("ProductCode", oModel.ProductCode);
        //        var dialog = Dialog.Show<DetailDialog>("", parameters, options);
        //        var result = await dialog.Result;
        //        if (!result.Cancelled)
        //        {
        //            var res = (TrnsFohdriverRatesDetail)result.Data;
        //            var update = oDetailList.Where(x => x.ProductCode == res.ProductCode && x.ProductName == res.ProductName).FirstOrDefault();
        //            if (update != null)
        //            {
        //                Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
        //            }
        //            else
        //            {
        //                oDetail.Add(res);
        //                oDetailList = oDetail;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //    }
        //}

        private async Task OpenEditDialog(DialogOptions options, MstResourceDetail oDetailPara)
        {
            //try
            //{
            //    var parameters = new DialogParameters();
            //    parameters.Add("oDetailParaTax", oDetailPara);
            //    parameters.Add("DialogFor", "ResourceMaster");
            //    var dialog = Dialog.Show<DetailDialog>("", parameters, options);
            //    var result = await dialog.Result;
            //    if (!result.Cancelled)
            //    {
            //        var res = (TrnsDirectMaterialDetail)result.Data;
            //        var update = oDetailList.Where(x => x.ItemCode == res.ItemCode && x.ItemName.ToLower() == res.ItemName.ToLower()
            //        && x.Remarks.ToLower() == res.Remarks.ToLower()
            //        && x.Uom.ToLower() == res.Uom.ToLower()
            //        && x.Currency == res.Currency
            //        && x.UnitPriceFc == res.UnitPriceFc
            //        && x.UnitPricePkr == res.UnitPricePkr
            //        && x.ChangePricePkr == res.ChangePricePkr
            //        && x.ConsQty == res.ConsQty
            //        && x.AmountPkr == res.AmountPkr
            //        && x.Lcfactor == res.Lcfactor).FirstOrDefault();
            //        if (update != null)
            //        {
            //            oDetail.Remove(update);
            //        }
            //        TrnsDirectMaterialDetail oRDDetail = new TrnsDirectMaterialDetail();
            //        oRDDetail.Id = res.Id;
            //        oRDDetail.Fkid = res.Fkid;
            //        oRDDetail.ItemCode = res.ItemCode;
            //        oRDDetail.ItemName = res.ItemName;
            //        oRDDetail.Remarks = res.Remarks;
            //        oRDDetail.Uom = res.Uom;
            //        oRDDetail.Currency = res.Currency;
            //        oRDDetail.UnitPriceFc = res.UnitPriceFc;
            //        oRDDetail.UnitPricePkr = res.UnitPricePkr;
            //        oRDDetail.ChangePricePkr = res.ChangePricePkr;
            //        oRDDetail.ConsQty = res.ConsQty;
            //        oRDDetail.AmountPkr = res.AmountPkr;
            //        oRDDetail.Lcfactor = res.Lcfactor;

            //        oDetail.Add(oRDDetail);
            //        oDetailList = oDetail.ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logs.GenerateLogs(ex);
            //}
        }

        private async Task OpenSAPDataDialog(DialogOptions options, DateTime? docDate)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("docDatePara", docDate);
                parameters.Add("DialogFor", "ExchangeRate");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    //oModelList.Currency = Convert.ToString(res.Currency);
                    //oModelList.ra = Convert.ToDecimal(res.Rate);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSapDataDialogFGItemForSalesPriceList(DialogOptions options)
        {
            //try
            //{
            //    var parameters = new DialogParameters();
            //    parameters.Add("DialogFor", "SalesItems");
            //    var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
            //    var result = await dialog.Result;
            //    if (!result.Cancelled)
            //    {
            //        var res = (SAPModels)result.Data;
            //        oModel.ProductCode = res.ItemCode;
            //        oModel.ProductDescription = res.ItemName;
            //        DialogFor = res.DialogFor;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logs.GenerateLogs(ex);
            //}
        }

        private async Task OpenSapDataDialogRMItemForItemPriceList(DialogOptions options)
        {
            //try
            //{
            //    var parameters = new DialogParameters();
            //    parameters.Add("DialogFor", "RMItemPriceList");
            //    var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
            //    var result = await dialog.Result;
            //    if (!result.Cancelled)
            //    {
            //        var res = (SAPModels)result.Data;
            //        oModel.ProductCode = res.ItemCode;
            //        oModel.ProductDescription = res.ItemName;
            //        DialogFor = res.DialogFor;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logs.GenerateLogs(ex);
            //}
        }

        private async Task OpenDialog(DialogOptions options)
        {
            //try
            //{
            //    var parameters = new DialogParameters();
            //    parameters.Add("DialogFor", "DetailMaster");
            //    var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
            //    var result = await dialog.Result;
            //    if (!result.Cancelled)
            //    {
            //        var res = (TrnsFohcostDetail)result.Data;
            //        oModel = res;
            //        //oModelCostType.Id = (int)oModel.;
            //        //oModelCostType.Description = oModel.;
            //        oDetailList = oModel.TrnsFohcostDetails.ToList();
            //        oDetail = oDetailList.ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logs.GenerateLogs(ex);
            //}
        }

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
                var res = oCostTypeList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();
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

        public async Task<IEnumerable<YearModel>> DateChange(string value)
        {
            List<string> year = new List<string>();
            var CYear = DateTime.Now.Year;
            var PYear = DateTime.Now.AddYears(-1).Year;
            var NYear = DateTime.Now.AddYears(1).Year;

            year.Add(NYear.ToString());
            year.Add(CYear.ToString());
            year.Add(PYear.ToString());
            //await Task.Delay(1);
            //if (string.IsNullOrWhiteSpace(value))
            //    return oYearModelList.Select(o => new YearModel
            //    {
            //        year = o.year,
            //        //Description = o.Description
            //    }).ToList();
            //var res = oCostTypeList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();
            //return res.Select(x => new YearModel
            //{
            //    year = x.year,
            //    //Description = x.Description
            //}).ToList();
            //return null;
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return year.Select(o => new YearModel
                    {
                        year = o.ToString(),
                        //Description = o.Description
                    }).ToList();
                var res = year.Where(x => x.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new YearModel
                {
                    year = x.ToString(),
                    //Description = x.Description
                }).ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<IEnumerable<YearModel>> DateChangeMounth(string value)
        {
            List<string> year = new List<string>();

            var PYear = DateTime.Now.AddYears(-1);

            //year.Add(CYear.ToString());
            year.Add(PYear.ToString());
            //year.Add(NYear.ToString());
            //await Task.Delay(1);
            //if (string.IsNullOrWhiteSpace(value))
            //    return oYearModelList.Select(o => new YearModel
            //    {
            //        year = o.year,
            //        //Description = o.Description
            //    }).ToList();
            //var res = oCostTypeList.Where(x => x.Description.ToUpper().Contains(value.ToUpper())).ToList();
            //return res.Select(x => new YearModel
            //{
            //    year = x.year,
            //    //Description = x.Description
            //}).ToList();
            //return null;
            try
            {
                await Task.Delay(1);
                if (string.IsNullOrWhiteSpace(value))
                    return year.Select(o => new YearModel
                    {
                        month = o.ToString(),
                        //Description = o.Description
                    }).ToList();
                var res = year.Where(x => x.ToUpper().Contains(value.ToUpper())).ToList();
                return res.Select(x => new YearModel
                {
                    month = x.ToString(),
                    //Description = x.Description
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
        private async Task GetallVOV()
        {
            try
            {
                oVOCList = await _mstVOC.GetAllData();
                var productCodeofVoc=oVOCList.Select(x=>x.ProductCode).ToList();

            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecord(string TypeOfRescr, string RescDesc, string CurrCodeSap, string CurrNameSAP, decimal? Rate, decimal? Price, decimal? Cost)
        {
            //try
            //{
            //    var remove = oDetailList.Where(x => x.TypeOfResr == TypeOfRescr && x.ResrDescription.ToLower() == RescDesc.ToLower()
            //        && x.CurrCodeSap.ToLower() == CurrCodeSap.ToLower()
            //        && x.CurrNameSap.ToLower() == CurrNameSAP.ToLower()
            //        && x.Rate == Rate
            //        && x.Price == Price
            //        && x.Cost == Cost).FirstOrDefault();
            //    if (remove != null)
            //    {
            //        oDetailList.Remove(remove);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logs.GenerateLogs(ex);
            //}
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
                        //oModel.FkcostTypeId = oModelCostType.Id;
                        //oModel.CostTypeDesc = oModelCostType.Description;
                        //For Labor
                        oModel.Lyear = oYearModel.year;
                        //oModel.Lyear=
                        oModel.TrnsVohmachineDetails = oDetailMachineList.ToList();
                        oModel.TrnsVohlabourDetails = oDetailLaborList.ToList();
                        oModel.TrnsVohelectricityDetails = oDetailElecticityList.ToList();
                        oModel.TrnsVohdyesAndMoldDetails = oDetailDyesList.ToList();
                        oModel.TrnsVohtoolsDetails = oDetailToolsList.ToList();
                        oModel.TrnsVohgasolineDetails = oDetailGasolineList.ToList();

                        if (oModel.DocDate == null && oModel.Lyear==null&&oModel.Lmonth==null)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oModel != null)
                        {
                            if (oModel.Id == 0)
                            {
                                res1 = await _mstUserProfiled.Insert(oModel);
                            }
                            else
                            {
                                res1 = await _mstUserProfiled.Update(oModel);
                            }
                        }
                        else
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (res1 != null && res1.Id == 1)
                        {
                            Snackbar.Add(res1.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                            await Task.Delay(1000);
                            Navigation.NavigateTo("/MonthlyVOHCalculation", forceLoad: true);
                        }
                        else
                        {
                            Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        //oModel. = true;
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
                Navigation.NavigateTo("/MonthlyVOHCalculation", forceLoad: true);
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
                //await DateChange();
                //oModel.FlgActive = true;
                //oModel.FlgDefaultResrMst = true;
                oModel.DocDate = DateTime.Today;
                await GetAllCostType();
                await GetAllCostDriverType();
                await GetallVOV();

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