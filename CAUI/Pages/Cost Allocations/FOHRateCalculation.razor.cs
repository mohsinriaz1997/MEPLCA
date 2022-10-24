using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.Cost_Allocations
{
    public partial class FOHRateCalculation
    {
        #region InjectService

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IUserProfile _mstUserProfile { get; set; }
        [Inject]
        public IFOHRatesCalc _mstUserProfiled { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }

        [Inject]
        public ICostDriversType _mstCostDriverType { get; set; }

        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        [Parameter]
        public string DialogFor { get; set; }

        #endregion InjectService

        #region Variables

        private bool Loading = false;

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();

        private TrnsFohrate oModel = new TrnsFohrate();
        private TrnsFohratesDetail oModelList = new TrnsFohratesDetail();
        private List<TrnsFohratesDetail> oDetailList = new List<TrnsFohratesDetail>();
        private List<TrnsFohratesDetail> oDetail = new List<TrnsFohratesDetail>();

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();

        private MstCostDriversType oModelCostDriversType = new MstCostDriversType();
        private List<MstCostDriversType> oCostDriversTypeList = new List<MstCostDriversType>();

        private MstSalesPriceList oModelSalesPriceList = new MstSalesPriceList();
        private List<MstSalesPriceListDetail> oDetailSalesPriceList = new List<MstSalesPriceListDetail>();
        private MstSalesPriceListDetail mstSalesPriceListDetail = new MstSalesPriceListDetail();


        private UserDataAccess oModelMstUserProfile = new UserDataAccess();
        private List<UserDataAccess> oMstUserProfileList = new List<UserDataAccess>();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };



        private string LoginUserCode = "";

        #endregion Variables

        #region Functions

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
                parameters.Add("DialogFor", "FOHRateCalculationRl");
                //parameters.Add("ProductCode", oModel.ProductCode);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohratesDetail)result.Data;
                    var update = oDetailList.Where(x => x.FkcostDriversTypeId == res.FkcostDriversTypeId && x.Rate == res.Rate && x.FkcostDrivesTypeDescription==res.FkcostDrivesTypeDescription).FirstOrDefault();
                    if (update != null)
                    {
                        Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    else
                    {
                        oDetail.Add(res);
                        oDetailList = oDetail;
                        var sumFohRate=oDetailList.Select(x=>x.Rate);
                        var totalsumfohrate=sumFohRate.Sum();
                        oModel.Fohrate= Convert.ToDecimal(totalsumfohrate);
                        //oModel.Fohrate= oModel.TrnsFohratesDetails.Sum(Convert.ToDecimal( sumFohRate));
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
                    var res = (TrnsFohratesDetail)result.Data;
                    var update = oDetailList.Where(x => x.FkcostDriversTypeId == res.FkcostDriversTypeId && x.Rate == res.Rate).FirstOrDefault();
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

        private async Task OpenEditDialog(DialogOptions options, TrnsFohratesDetail oDetailPara)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("oDetailFohRate", oDetailPara);
                parameters.Add("DialogFor", "FOHRateCalculationRl");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;

                if (!result.Cancelled)
                {
                    var res = (TrnsFohratesDetail)result.Data;
                    var res1 = (TrnsFohrate)result.Data;
                    var update = oDetailList.Where(x => x.FkcostDriversTypeId == res.FkcostDriversTypeId && x.Rate == res.Rate).FirstOrDefault();

                    if (update != null)
                    {
                        oDetail.Remove(update);
                    }
                    TrnsFohratesDetail oRDDetail = new TrnsFohratesDetail();
                    oRDDetail.Id = res.Id;
                    oRDDetail.Fkid = res.Fkid;
                    oRDDetail.FkcostDriversTypeId = res.FkcostDriversTypeId;
                    oRDDetail.Rate = res.Rate;
                    //res1.Fohrate = res.Rate;
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
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "SalesItems");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
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

        private async Task OpenSapDataDialogRMItemForItemPriceList(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "RMItemPriceList");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
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

        private async Task OpenDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "FohRateMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohrate)result.Data;
                    oModel = res;
                    oModelCostType.Id = (int)oModel.FkcostTypeId;
                    //oModelCostType.Description = oModel.;
                    oDetailList = oModel.TrnsFohratesDetails.ToList();
                    oDetail = oDetailList.ToList();
                }
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

        private async Task GetAllCostType()
        {
            try
            {

                oMstUserProfileList = await _mstUserProfile.GetAllFormAndCostTypesFOHRate(LoginUserCode);
                //oCostTypeList = await _mstCostType.GetAllData();
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
                        oModel.FkcostTypeId = oModelCostType.Id;
                        //oModel.CostTypeDesc = oModelCostType.Description;
                        oModel.TrnsFohratesDetails = oDetailList.ToList();
                        if (oModel.DocDate == null)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oModel != null && oModel.TrnsFohratesDetails != null && oModel.TrnsFohratesDetails.Count > 0)
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
                            Navigation.NavigateTo("/FOHRateCalculation", forceLoad: true);
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
                Navigation.NavigateTo("/FOHRateCalculation", forceLoad: true);
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
                    oModel.DocDate = DateTime.Today;
                    await GetAllCostType();
                    await GetAllCostDriverType();
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