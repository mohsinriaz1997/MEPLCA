using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.Cost_Allocations
{
    public partial class MonthlyFohCostCalculation
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
        public IFOHCostCalc _mstUserProfiled { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }

        [Parameter]
        public string DialogFor { get; set; }
        [Parameter]
        public string year { get; set; }
        [Parameter]
        public string month { get; set; }

        #endregion InjectService

        #region Variables

        private bool Loading = false;

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();

        private TrnsFohcost oModel = new TrnsFohcost();
        private TrnsFohcostDetail oModelList = new TrnsFohcostDetail();
        private List<TrnsFohcostDetail> oDetailList = new List<TrnsFohcostDetail>();
        private List<TrnsFohcostDetail> oDetail = new List<TrnsFohcostDetail>();

        private YearModel oYearModel = new YearModel();
        private List<YearModel> oYearModelList = new List<YearModel>();

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();

        private MstSalesPriceList oModelSalesPriceList = new MstSalesPriceList();
        private List<MstSalesPriceListDetail> oDetailSalesPriceList = new List<MstSalesPriceListDetail>();
        private MstSalesPriceListDetail mstSalesPriceListDetail = new MstSalesPriceListDetail();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        #endregion Variables

        #region Functions

        private void OnDateChange()
        {
            try
            {
                oModel.DocDate = oModel.DocDate;
            }
            catch (Exception ex )
            {

                Logs.GenerateLogs(ex);
            }
            
            //oModel.CurrencySap = null;
            //oModel.RateSap = null;
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
        private async Task OpenAddDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "MonthlyFohCostRl");
                parameters.Add("year", oYearModel.year);
                parameters.Add("month", oModel.Lmonth);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohcostDetail)result.Data;
                    var update = oDetailList.Where(x => x.AcctCode == res.AcctCode && x.AcctDescription.ToLower() == res.AcctDescription.ToLower()).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oDetail.Add(res);
                        oDetailList = oDetail;
                        var sumOfAmmountField = oDetailList.Select(x => x.Amount);
                        var totalsumAmmountField = sumOfAmmountField.Sum();
                        oModel.TotalCost = Convert.ToDecimal(totalsumAmmountField);

                        var sumOfvoh = oDetailList.Select(x => x.Vohamount);
                        var totalsumvoh = sumOfvoh.Sum();
                        oModel.TotalVoh = Convert.ToDecimal(totalsumvoh);

                        var sumOffoh = oDetailList.Select(x => x.Fohamount);
                        var totalsumfoh = sumOfvoh.Sum();
                        oModel.TotalFoh = Convert.ToDecimal(totalsumfoh);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
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
                    var res = (TrnsFohcostDetail)result.Data;
                    var update = oDetailList.Where(x => x.AcctCode == res.AcctCode && x.AcctDescription == res.AcctDescription).FirstOrDefault();
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

        private async Task OpenEditDialog(DialogOptions options, TrnsFohcostDetail oDetailPara)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("oDetailFohCost", oDetailPara);
                parameters.Add("DialogFor", "MonthlyFohCostRl");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohcostDetail)result.Data;
                    var update = oDetailList.Where(x => x.AcctCode == res.AcctCode 
                    
                    && x.Vohamount == res.Vohamount
                    && x.Fohamount == res.Fohamount).FirstOrDefault();
                    if (update != null)
                    {
                        oDetail.Remove(update);
                    }
                    TrnsFohcostDetail oRDDetail = new TrnsFohcostDetail();
                    oRDDetail.Id = res.Id;
                    oRDDetail.Fkid = res.Fkid;
                    oRDDetail.AcctCode = res.AcctCode;
                    oRDDetail.AcctDescription = res.AcctDescription;
                    oRDDetail.CostDriver = res.CostDriver;
                    oRDDetail.Amount = res.Amount;
                    oRDDetail.Vohamount = res.Vohamount;
                    oRDDetail.Fohamount = res.Fohamount;


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
                        oModel.TrnsFohcostDetails = oDetailList.ToList();
                        if (oModel.DocDate == null)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oModel != null && oModel.TrnsFohcostDetails != null && oModel.TrnsFohcostDetails.Count > 0)
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
                            Navigation.NavigateTo("/MonthlyFohCostCalculation", forceLoad: true);
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
                Navigation.NavigateTo("/MonthlyFohCostCalculation", forceLoad: true);
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