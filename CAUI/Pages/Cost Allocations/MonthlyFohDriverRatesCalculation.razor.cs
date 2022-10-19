using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.Cost_Allocations
{
    public partial class MonthlyFohDriverRatesCalculation
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
        public IMonthlyFohDriverRatesCalculation _mstUserProfiled { get; set; }

        [Inject]
        public ICostDriversType _mstCostDriverType { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }
        [Parameter]
        public string year { get; set; }
        [Parameter]
        public string month { get; set; }

        [Parameter]
        public string DialogFor { get; set; }

        #endregion InjectService

        #region Variables
        public string searchString;
        private bool Loading = false;

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();
        private YearModel oYearModel = new YearModel();
        private List<YearModel> oYearModelList = new List<YearModel>();
        private TrnsFohdriverRate oModel = new TrnsFohdriverRate();
        private TrnsFohdriverRatesDetail oModelList = new TrnsFohdriverRatesDetail();
        private List<TrnsFohdriverRatesDetail> oDetailList = new List<TrnsFohdriverRatesDetail>();
        private HashSet<TrnsFohdriverRatesDetail> selectedItems1 = new HashSet<TrnsFohdriverRatesDetail>();
        private List<TrnsFohdriverRatesDetail> oDetail = new List<TrnsFohdriverRatesDetail>();

        private MstCostType oModelCostType = new MstCostType();
        private TrnsFohdriverRatesDetail elementBeforeEdit;
        private List<string> editEvents = new();
        private bool blockSwitch = false;

        private List<MstCostType> oCostTypeList = new List<MstCostType>();

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
            try
            {
            oModel.DocDate = oModel.DocDate;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex); ;
            }
            //oModel.CurrencySap = null;
            //oModel.RateSap = null;
        }
        private async Task OpenAddDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "MonthlyFohDriverRatesCalculationRl");

                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohdriverRatesDetail)result.Data;
                    var update = oDetailList.Where(x => x.ProductCode == res.ProductCode && x.ProductName.ToLower() == res.ProductName.ToLower()).FirstOrDefault();
                    //if (update != null)
                    //{
                    //    Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    //}
                    //else
                    //{
                    oDetail.Add(res);
                    oDetailList = oDetail;
                    //}
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenAddDialogGrid(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "FOHDriverRateItems");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                //oModel.Lyear = year;
                if (!result.Cancelled)
                {
                    oModel.TrnsFohdriverRatesDetails = (ICollection<TrnsFohdriverRatesDetail>)result.Data;
                    foreach (var Line in oModel.TrnsFohdriverRatesDetails)
                    {
                        //User already select item master and its detaail


                        //var itemDetail = (from a in oModel.TrnsVohmachineDetails

                        //                  select a).FirstOrDefault();

                        Line.ProductName = Line.ProductName;
                        Line.ProductCode = Line.ProductCode;
                        //Line.MachineVohrate = Line.MachineVohrate;
                        //Line.MachineVohamount = Line.MachineVohamount;





                    }
                    oDetailList = oModel.TrnsFohdriverRatesDetails.ToList();
                    var sumOfDriverValue = oDetailList.Select(x => x.DriverValue);
                    var totalsumAmmountField = sumOfDriverValue.Sum();
                    oModel.TotalDriverValue = Convert.ToDecimal(totalsumAmmountField);

                    //var sumOfFOHDC = oDetailList.Select(x => x.Vohamount);
                    //var totalsumvoh = sumOfFOHDC.Sum();
                    //oModel.tot = Convert.ToDecimal(totalsumvoh);
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
        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }
        private void BackupItem(object element)
        {
            elementBeforeEdit = new()
            {
                ProductCode = ((TrnsFohdriverRatesDetail)element).ProductCode,
                ProductName = ((TrnsFohdriverRatesDetail)element).ProductName,
                FohdistributionCost = ((TrnsFohdriverRatesDetail)element).FohdistributionCost,
                DriverValue = ((TrnsFohdriverRatesDetail)element).DriverValue,


            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((TrnsFohdriverRatesDetail)element).ProductCode}");
        }
        private void ItemHasBeenCommitted(object element)
        {
            //oModelDetail.Clear();
            AddEditionEvent($"RowEditCommit event: Changes to Element {((TrnsFohdriverRatesDetail)element).ProductCode} committed");
            //oModelDetail.Add((TrnsDirectMaterialDetail)element);
            //oModel.TrnsDirectMaterialDetails.Add(element);
        }
        private bool FilterFunc(TrnsFohdriverRatesDetail element)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ProductCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
        private void ResetItemToOriginalValues(object element)
        {
            ((TrnsFohdriverRatesDetail)element).ProductCode = elementBeforeEdit.ProductCode;
            ((TrnsFohdriverRatesDetail)element).ProductName = elementBeforeEdit.ProductName;
            ((TrnsFohdriverRatesDetail)element).DriverValue = elementBeforeEdit.DriverValue;
            ((TrnsFohdriverRatesDetail)element).FohdistributionCost = elementBeforeEdit.FohdistributionCost;


            AddEditionEvent($"RowEditCancel event: Editing of Element {((TrnsFohdriverRatesDetail)element).ProductCode} cancelled");
        }

        private async Task OpenAddDialogFooterLevel(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "DirectMaterialFL");
                //parameters.Add("ProductCode", oModel.ProductCode);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohdriverRatesDetail)result.Data;
                    var update = oDetailList.Where(x => x.ProductCode == res.ProductCode && x.ProductName == res.ProductName).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oDetail.Add(res);
                        oDetailList = oDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenEditDialog(DialogOptions options, TrnsFohdriverRatesDetail oDetailFohdriverRatesDetail)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("oDetailFohdriverRatesDetail", oDetailFohdriverRatesDetail);
                parameters.Add("DialogFor", "MonthlyFohDriverRatesCalculationRl");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohdriverRatesDetail)result.Data;
                    var update = oDetailList.Where(x => x.ProductCode == res.ProductCode && x.ProductName.ToLower() == res.ProductName.ToLower()
                    && x.DriverValue == res.DriverValue

                    && x.FohdistributionCost == res.FohdistributionCost).FirstOrDefault();
                    if (update != null)
                    {
                        oDetail.Remove(update);
                    }
                    TrnsFohdriverRatesDetail oRDDetail = new TrnsFohdriverRatesDetail();
                    oRDDetail.Id = res.Id;
                    oRDDetail.Fkid = res.Fkid;
                    oRDDetail.ProductCode = res.ProductCode;
                    oRDDetail.ProductName = res.ProductName;
                    oRDDetail.DriverValue = res.DriverValue;
                    oRDDetail.FohdistributionCost = res.FohdistributionCost;

                    oDetail.Add(oRDDetail);
                    oDetailList = oDetail.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
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
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "FohDriverMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohdriverRate)result.Data;
                    oModel = res;
                    //oModelCostType.Id = (int)oModel.;
                    //oModelCostType.Description = oModel.;
                    oDetailList = oModel.TrnsFohdriverRatesDetails.ToList();
                    oDetail = oDetailList.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
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
                        oModel.TrnsFohdriverRatesDetails = oDetailList.ToList();
                        if (oModel.DocDate == null)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oModel != null && oModel.TrnsFohdriverRatesDetails != null && oModel.TrnsFohdriverRatesDetails.Count > 0)
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
                            Navigation.NavigateTo("/MonthlyFohDriverRatesCalculation", forceLoad: true);
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
                Navigation.NavigateTo("/ResourceMasterData", forceLoad: true);
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
                //oModel.FlgActive = true;
                //oModel.FlgDefaultResrMst = true;
                oModel.DocDate = DateTime.Today;
                await GetAllCostType();
                await GetAllCostDriverType();
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