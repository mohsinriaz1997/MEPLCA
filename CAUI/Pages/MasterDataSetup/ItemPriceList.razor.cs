using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.MasterData;

using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.MasterDataSetup
{
    public partial class ItemPriceList
    {
        #region InjectService

        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }
        [Inject]
        public IUserProfile _mstUserProfile { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IResourceMasterData _mstUserProfiled { get; set; }

        [Inject]
        public IItemPriceList itemPriceList { get; set; }

        //[Inject]
        //public IMstElement _mstElement { get; set; }

        //[Inject]
        //public IMstLove _mstLove { get; set; }

        #endregion InjectService

        #region Variables

        private string LoginUserCode = "";
        private MudTable<MstResourceDetail> _table;
        private MudTable<MstItemPriceListDetail> _table1;
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();
        private bool Loading = false;
        private bool DisbaledCode = false;
        public IMask AlphaNumericMask = new RegexMask(@"^[a-zA-Z0-9_]*$");
        public bool Basic_Switch1 { get; set; } = false;

        private MstResource oModel = new MstResource();

        private MstItemPriceList mstItemPriceList = new MstItemPriceList();

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();

        private List<MstItemPriceListDetail> oDetailList = new List<MstItemPriceListDetail>();
        private List<MstItemPriceListDetail> oDetail = new List<MstItemPriceListDetail>();
        private MstItemPriceList mstResourceDetail = new MstItemPriceList();

        private List<MstItemPriceListDetail> oitemDetailList = new List<MstItemPriceListDetail>();

        private List<MstItemPriceListDetail> oItemDetail = new List<MstItemPriceListDetail>();
        private MstItemPriceListDetail mstItemDetail = new MstItemPriceListDetail();

        //MstElement oModel = new MstElement();
        //List<MstLove> oLoveList = new List<MstLove>();
        //private IEnumerable<MstElement> oList = new List<MstElement>();
        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        [Inject]
        public ICostType _mstCostType { get; set; }

        #endregion Variables

        #region Functions

        private void OnDateChange()
        {
            try
            {
                mstItemPriceList.DocDate = mstItemPriceList.DocDate;
            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }

            //oModel.CurrencySap = null;
            //oModel.RateSap = null;
        }
        private async Task OpenEditDialog(DialogOptions options, MstItemPriceListDetail oDetailPara)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("oDetailParaItem", oDetailPara);
                parameters.Add("DialogFor", "ItemPriceList");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstItemPriceListDetail)result.Data;
                    var update = oDetailList.Where(x => x.ItemCode == res.ItemCode && x.ItemName.ToLower() == res.ItemName.ToLower()
                    && x.AdditionalDutyValue == res.AdditionalDutyValue
                    && x.BasicPrice == res.BasicPrice
                    && x.Rate == res.Rate
                    && x.UnitPricePkr == res.UnitPricePkr
                    && x.BlanketAgreement == res.BlanketAgreement
                    && x.Hscode == res.Hscode
                    && x.FkgroupSetup1 == res.FkgroupSetup1
                    && x.FkgroupSetup2 == res.FkgroupSetup2
                    && x.FkgroupSetup3 == res.FkgroupSetup3
                    && x.FkgroupSetup4 == res.FkgroupSetup4
                    && x.BasicPrice == res.BasicPrice
                    && x.Currency == res.Currency
                    && x.InsuranceCostValue == res.InsuranceCostValue
                    && x.FreightCostValue == res.FreightCostValue
                    && x.LandingCostValue == res.LandingCostValue
                    && x.WhchargesValue == res.WhchargesValue
                    && x.YardPaymentValue == res.YardPaymentValue
                    && x.CleaningChargesValue == res.CleaningChargesValue
                    && x.ImportValue == res.ImportValue
                    && x.CustomDutyValue == res.CustomDutyValue
                    && x.RegulatoryDutyValue == res.RegulatoryDutyValue
                    && x.ExciseDutyValue == res.ExciseDutyValue
                    && x.DutiesValue == res.DutiesValue
                    && x.SalesTaxValue == res.SalesTaxValue
                    && x.IncomeTaxValue == res.IncomeTaxValue
                    && x.Value == res.Value
                    && x.Others == res.Others
                    && x.FinalItemPrice == res.FinalItemPrice);

                    //&& x.Cost == res.Cost).FirstOrDefault();
                    if (update != null)
                    {
                        oDetail.Remove((MstItemPriceListDetail)update);
                    }
                    MstItemPriceListDetail oRDDetail = new MstItemPriceListDetail();
                    oRDDetail.Id = res.Id;
                    oRDDetail.Fkid = res.Fkid;
                    oRDDetail.ItemCode = res.ItemCode;
                    oRDDetail.ItemName = res.ItemName;
                    oRDDetail.Hscode = res.Hscode;
                    oRDDetail.FkgroupSetup1 = res.FkgroupSetup1;
                    oRDDetail.FkgroupSetup2 = res.FkgroupSetup2;
                    oRDDetail.FkgroupSetup3 = res.FkgroupSetup3;
                    oRDDetail.FkgroupSetup4 = res.FkgroupSetup4;
                    oRDDetail.BlanketAgreement = res.BlanketAgreement;
                    oRDDetail.Currency = res.Currency;
                    oRDDetail.UnitPricePkr = res.UnitPricePkr;
                    oRDDetail.Rate = res.Rate;
                    oRDDetail.InsuranceCostValue = res.InsuranceCostValue;
                    oRDDetail.FreightCostValue = res.FreightCostValue;
                    oRDDetail.WhchargesValue = res.WhchargesValue;
                    oRDDetail.YardPaymentValue = res.YardPaymentValue;
                    oRDDetail.CleaningChargesValue = res.CleaningChargesValue;
                    oRDDetail.ImportValue = res.ImportValue;
                    oRDDetail.CustomDutyValue = res.CustomDutyValue;
                    oRDDetail.RegulatoryDutyValue = res.RegulatoryDutyValue;
                    oRDDetail.AdditionalDutyValue = res.AdditionalDutyValue;
                    oRDDetail.ExciseDutyValue = res.ExciseDutyValue;
                    oRDDetail.DutiesValue = res.DutiesValue;
                    oRDDetail.IncomeTaxValue = res.IncomeTaxValue;
                    oRDDetail.Value = res.Value;
                    oRDDetail.Others = res.Others;
                    oRDDetail.FinalItemPrice = res.FinalItemPrice;

                    oDetail.Add(oRDDetail);
                    oDetailList = oDetail.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RowClickEvent(TableRowClickEventArgs<MstResourceDetail> tableRowClickEventArgs)
        {
            try
            {
                MstResourceDetail forms = new MstResourceDetail();
                forms.TypeOfResr = tableRowClickEventArgs.Item.TypeOfResr;
                forms.Fk = new MstResource();
                forms.Fk.ResrListName = "abcd";
                //forms.Currency = tableRowClickEventArgs.Item.Currency;
                forms.ResrDescription = tableRowClickEventArgs.Item.ResrDescription;
                forms.Fk.MstResourceDetails = new List<MstResourceDetail>();
                forms.Price = Convert.ToInt32(tableRowClickEventArgs.Item.Price);
                forms.Rate = Convert.ToInt32(tableRowClickEventArgs.Item.Rate);
                forms.LandedFactor = Convert.ToInt32(tableRowClickEventArgs.Item.LandedFactor);
                forms.Cost = Convert.ToInt32(tableRowClickEventArgs.Item.Cost);
                //forms.NIC = tableRowClickEventArgs.Item.NIC;
                //forms.Phone = tableRowClickEventArgs.Item.Phone;
                //forms.DriverName = tableRowClickEventArgs.Item.DriverName;
                //forms.Contractor = tableRowClickEventArgs.Item.Contractor;

                //MudDialog.Close(DialogResult.Ok<MstResourceDetail>(forms));
                var ResourceMaster = new DialogParameters { ["ItemPriceList"] = forms };
                var dialog = Dialog.Show<DetailDialog>("", ResourceMaster);
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task OpenDialogCopyFrom(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "ItemPriceListMasterCopyTo");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstItemPriceList)result.Data;
                    var mst = res.MstItemPriceListDetails;
                    List<MstItemPriceListDetail> list = new List<MstItemPriceListDetail>();
                    list = mst.ToList();
                    foreach (var item in list)
                    {
                        MstItemPriceListDetail detail = new MstItemPriceListDetail();
                        detail.Id = (int)item.Id;
                        detail.ItemCode = item.ItemCode;
                        detail.ItemName = item.ItemName;
                        detail.Hscode = item.Hscode;
                        detail.FkgroupSetup1 = item.FkgroupSetup1;
                        detail.FkgroupSetup2 = item.FkgroupSetup2;
                        detail.FkgroupSetup3 = item.FkgroupSetup3;
                        detail.FkgroupSetup4 = item.FkgroupSetup4;
                        detail.BlanketAgreement = item.BlanketAgreement;
                        detail.BasicPrice = item.BasicPrice;
                        detail.Currency = item.Currency;
                        detail.Rate = item.Rate;
                        detail.UnitPricePkr = item.UnitPricePkr;
                        detail.InsuranceCostPer = item.InsuranceCostPer;
                        detail.InsuranceCostValue = item.InsuranceCostValue;
                        detail.FreightCostPer = item.FreightCostPer;
                        detail.FreightCostValue = item.FreightCostValue;
                        detail.LandingCostPer = item.LandingCostPer;
                        detail.LandingCostValue = item.LandingCostValue;
                        detail.WhchargesPer = item.WhchargesPer;
                        detail.WhchargesValue = item.WhchargesValue;
                        detail.YardPaymentPer = item.YardPaymentPer;
                        detail.YardPaymentValue = item.YardPaymentValue;
                        detail.CleaningChargesPer = item.CleaningChargesPer;
                        detail.CleaningChargesValue = item.CleaningChargesValue;
                        detail.ImportPer = item.ImportPer;
                        detail.ImportValue = item.ImportValue;
                        detail.CustomDutyPer = item.CustomDutyPer;
                        detail.CustomDutyValue = item.CustomDutyValue;
                        detail.RegulatoryDutyPer = item.RegulatoryDutyPer;
                        detail.RegulatoryDutyValue = item.RegulatoryDutyValue;
                        detail.AdditionalDutyPer = item.AdditionalDutyPer;
                        detail.AdditionalDutyValue = item.AdditionalDutyValue;
                        detail.ExciseDutyPer = item.ExciseDutyPer;
                        detail.ExciseDutyValue = item.ExciseDutyValue;
                        detail.DutiesPer = item.DutiesPer;
                        detail.DutiesValue = item.DutiesValue;
                        detail.SalesTaxPer = item.SalesTaxPer;
                        detail.SalesTaxValue = item.SalesTaxValue;
                        detail.IncomeTaxPer = item.IncomeTaxPer;
                        detail.IncomeTaxValue = item.IncomeTaxValue;
                        detail.Value = item.Value;
                        detail.Others = item.Others;
                        //detail.AdditionalDutyPer = item.AdditionalDutyPer;

                        oDetailList.Add(detail);
                    }
                    oDetailList.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        //private async Task EditDialog(DialogOptions options, MstResourceDetail mstResourceDetail)
        //{
        //    try
        //    {
        //        //SendDataToDialog sendDataToDialog = new SendDataToDialog();
        //        //sendDataToDialog.ItemCode = generalObject.ItemCode;

        //        var SendDataToDialog = new DialogParameters { ["ResourceMaster"] = mstResourceDetail };
        //        var dialog = Dialog.Show<MstResourceDetail>("", MstResourceDetail, options);
        //            var result = await dialog.Result;

        //            if (!result.Cancelled)
        //            {
        //            }
        //    }
        //    catch (Exception ex)
        //    {
        //        //UILog.GenerateLogs(ex);
        //    }

        //}

        private async Task OpenAddDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "ItemPriceList");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstItemPriceListDetail)result.Data;
                    //if (oDetailList.Where(x => x. == res.TaxCode).Count() > 0)
                    //{
                    //    Snackbar.Add("Code already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    //}
                    //else
                    //{

                    oDetailList.Add(res);

                    oitemDetailList = oDetailList;
                    //}
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecord(string ItemCode, string ItemName, string Hscode, int FkgroupSetup1, int FkgroupSetup2, int FkgroupSetup3, int FkgroupSetup4, string BlanketAgreement, decimal BasicPrice, string Currency, decimal Rate, decimal UnitPricePkr, decimal InsuranceCostPer, decimal InsuranceCostValue, decimal FreightCostPer, decimal FreightCostValue, decimal LandingCostPer, decimal LandingCostValue, decimal WhchargesPer, decimal WhchargesValue, decimal YardPaymentPer, decimal YardPaymentValue, decimal CleaningChargesPer, decimal CleaningChargesValue, decimal ImportPer, decimal ImportValue, decimal CustomDutyPer, decimal CustomDutyValue, decimal RegulatoryDutyPer, decimal RegulatoryDutyValue, decimal AdditionalDutyPer, decimal AdditionalDutyValue, decimal ExciseDutyPer, decimal ExciseDutyValue, decimal DutiesPer, decimal DutiesValue, decimal SalesTaxPer, decimal SalesTaxValue, decimal IncomeTaxPer, decimal IncomeTaxValue, decimal Value, decimal Others, decimal FinalItemPrice)
        {
            try
            {
                var remove = oDetailList.Where(x => x.ItemCode == ItemCode && x.ItemName.ToLower() == ItemName.ToLower()
                    && x.AdditionalDutyValue == AdditionalDutyValue
                    && x.BasicPrice == BasicPrice
                    && x.Rate == Rate
                    && x.UnitPricePkr == UnitPricePkr
                    && x.BlanketAgreement == BlanketAgreement
                    && x.Hscode == Hscode
                    && x.FkgroupSetup1 == FkgroupSetup1
                    && x.FkgroupSetup2 == FkgroupSetup2
                    && x.FkgroupSetup3 == FkgroupSetup3
                    && x.FkgroupSetup4 == FkgroupSetup4
                    && x.Currency == Currency
                    && x.AdditionalDutyPer == AdditionalDutyPer
                    //&& x.AdditionalDutyValue == AdditionalDutyValue
                    && x.InsuranceCostValue == InsuranceCostValue
                    && x.InsuranceCostPer == InsuranceCostPer
                    && x.FreightCostValue == FreightCostValue
                    && x.FreightCostPer == FreightCostPer
                    && x.LandingCostValue == LandingCostValue
                    && x.LandingCostPer == LandingCostPer
                    && x.WhchargesValue == WhchargesValue
                    && x.WhchargesPer == WhchargesPer
                    && x.YardPaymentValue == YardPaymentValue
                    && x.YardPaymentPer == YardPaymentPer
                    && x.CleaningChargesValue == CleaningChargesValue
                    && x.CleaningChargesPer == CleaningChargesPer
                    && x.ImportValue == ImportValue
                    && x.ImportPer == ImportPer
                    && x.CustomDutyValue == CustomDutyValue
                    && x.CustomDutyPer == CustomDutyPer
                    && x.RegulatoryDutyValue == RegulatoryDutyValue
                    && x.RegulatoryDutyPer == RegulatoryDutyPer
                    && x.ExciseDutyValue == ExciseDutyValue
                    && x.ExciseDutyPer == ExciseDutyPer
                    && x.DutiesValue == DutiesValue
                    && x.DutiesPer == DutiesPer
                    && x.SalesTaxValue == SalesTaxValue
                    && x.SalesTaxPer == SalesTaxPer
                    && x.IncomeTaxValue == IncomeTaxValue
                    && x.IncomeTaxPer == IncomeTaxPer
                    && x.Value == Value
                    && x.Others == Others
                    && x.FinalItemPrice == FinalItemPrice);
                if (remove != null)
                {
                    //oDetailList.Remove(remove);
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
                parameters.Add("DialogFor", "ItemPriceListMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstItemPriceList)result.Data;
                    mstItemPriceList = res;

                    oDetailList = mstItemPriceList.MstItemPriceListDetails.ToList(); /*mstItemPriceList.MstItemPriceListDetails.ToList();*/
                    oDetail = oDetailList.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task<ApiResponseModel> Save()
        {
            try
            {
                Loading = true;
                var res = new ApiResponseModel();
                await Task.Delay(3);
                if (!string.IsNullOrWhiteSpace(Convert.ToString(mstItemPriceList.Id)))
                {
                    try
                    {
                        Loading = true;
                        var res1 = new ApiResponseModel();
                        await Task.Delay(3);

                        //if (oDetail.Where(x => x.Id == oModel.Id).Count() > 0)
                        //{
                        //    Snackbar.Add("Code already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });

                        //}
                        //if (oUserProfileList.Where(x => x.UserName == oModelUserProfile.UserName).Count() > 0)
                        //{
                        //    Snackbar.Add("Description already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        //}
                        //else
                        //{
                        mstItemPriceList.DocDate = mstItemPriceList.DocDate;
                        mstItemPriceList.DocNum = mstItemPriceList.DocNum;
                        mstItemPriceList.ExchangeRate = mstItemPriceList.ExchangeRate;
                        mstItemPriceList.FlgDefaultPl = mstItemPriceList.FlgDefaultPl;
                        mstItemPriceList.Plname = mstItemPriceList.Plname;
                        mstItemPriceList.PriceBase = mstItemPriceList.PriceBase;
                        mstItemPriceList.MstItemPriceListDetails = oDetailList.ToList();

                        //oModel.ExchangeRate = oModel.ExchangeRate;
                        //oModel.MstResourceDetails = oDetailList.ToList();

                        //foreach (var item in oDetailList)
                        //{
                        //    item.Cost = mstResourceDetail.Cost;
                        //    item.Currency = mstResourceDetail.Currency;
                        //    item.LandedFactor = mstResourceDetail.LandedFactor;
                        //    item.FlgActive = mstResourceDetail.FlgActive;
                        //    item.Rate = mstResourceDetail.Rate;
                        //    item.Price = mstResourceDetail.Price;

                        //    oModel.MstResourceDetails.Add(item);
                        //}

                        //oModelUserProfile.FkdeptId = oModel.Id;

                        if (mstItemPriceList != null && mstItemPriceList.MstItemPriceListDetails != null && mstItemPriceList.MstItemPriceListDetails.Count > 0)
                        {
                            if (mstItemPriceList.Id == 0)
                            {
                                res1 = await itemPriceList.Insert(mstItemPriceList);
                            }
                            else
                            {
                                res1 = await itemPriceList.Update(mstItemPriceList);
                            }
                        }
                        else
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }

                        //}
                        if (res1 != null && res1.Id == 1)
                        {
                            Snackbar.Add(res1.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                            await Task.Delay(3000);
                            Navigation.NavigateTo("/ItemPriceList", forceLoad: true);
                        }
                        else
                        {
                            Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        oModel.FlgActive = true;

                        Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });

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
                await Task.Delay(3);
                Navigation.NavigateTo("/ItemPriceList", forceLoad: true);
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
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

        #endregion Functions

        #region Events
        private async Task OpenSAPDataDialog(DialogOptions options, DateTime? docDate)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "ExchangeRate");
                parameters.Add("docDatePara", docDate);
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    //oModel.CurrencySap = Convert.ToString(res.Currency);
                    mstItemPriceList.ExchangeRate = Convert.ToDecimal(res.Rate);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Loading = true;
                //oModel.Value = 0;
                //oModel.EmployeeContribution = 0;
                //oModel.EmployeeContributionMax = 0;
                //oModel.EmployerContribution = 0;
                //oModel.EmployerContributionMax = 0;
                //oModel.ApplicableAmountMax = 0;
                //oModel.FlgActive = true;
                //oModel.FlgProcessInPayroll = true;
                //oModel.FlgStandardElement = true;
                //oModel.FlgEffectOnGross = true;
                //oModel.FlgProbationApplicable = true;
                //oModel.FlgNotTaxable = false;
                //oModel.FlgEos = false;
                //oModel.FlgVariableValue = false;
                //oModel.FlgPropotionate = false;
                //oModel.StartDate = DateTime.Today;
                //oModel.EndDate = DateTime.Today;
                await GetAllCostType();
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
        //        if (Session != null)
        //        {
        //            var res = await _mstUserProfile.FetchUserAuth(Session.Id);
        //            if (res.Where(x => x.MenuName == "Item Price List" && x.UserRights != 1).ToList().Count > 0)
        //            {
        //                LoginUserCode = Session.UserCode;
        //            }
        //            else
        //            {
        //                Navigation.NavigateTo("/Login", forceLoad: true);
        //            }
        //        }
        //        else
        //        {
        //            Navigation.NavigateTo("/Login", forceLoad: true);
        //        }

        //    }
        //}

        public int rate { get; set; }
        public int price { get; set; }
        public int landedFactor { get; set; }
        public int cost { get; set; }
        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        private void OnChange()
        {
            //ValueChanged.InvokeAsync(e.Value.ToString());
            int mycost = 0;
            mycost = Convert.ToInt32((rate * price * landedFactor));
            cost = mycost;
        }

        #endregion Events
    }
}