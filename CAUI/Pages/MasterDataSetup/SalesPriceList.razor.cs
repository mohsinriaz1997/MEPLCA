using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.MasterDataSetup
{
    public partial class SalesPriceList
    {
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }
        [Inject]
        public IUserProfile _mstUserProfile { get; set; }
        private string LoginUserCode = "";
        public string DialogFor { get; set; }
        private bool Loading = false;
        private MstSalesPriceList oModel = new MstSalesPriceList();
        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };
        //private List<<MstSalesPriceListDetail>> oDetailList = new List<MstSalesPriceListDetail>();
        private List<MstSalesPriceListDetail> oDetailList = new List<MstSalesPriceListDetail>();
        private List<MstSalesPriceListDetail> oDetail = new List<MstSalesPriceListDetail>();
        private MstSalesPriceListDetail mstSalesPriceListDetail = new MstSalesPriceListDetail();
        private SAPModels sAPModels = new SAPModels();
        public bool Basic_Switch1 { get; set; } = false;
        private DialogOptions closeButton = new DialogOptions() { CloseButton = true };

        private async Task OpenDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "SalePriceListMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstSalesPriceList)result.Data;
                    oModel = res;

                    oDetailList = oModel.MstSalesPriceListDetails.ToList();
                    oDetail = oDetailList.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Inject]
        public ISalePriceList _mstCostDrive { get; set; }

        private MudTable<MstSalesPriceListDetail> _table;

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));

        private void Cancel() => MudDialog.Cancel();

        private async Task OpenAddDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "SalePriceList");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstSalesPriceListDetail)result.Data;
                    //if (oDetailList.Where(x => x. == res.TaxCode).Count() > 0)
                    //{
                    //    Snackbar.Add("Code already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
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

        private async Task OpenSapDataDialogCustomerCode(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "Customer");
                var dialog = Dialog.Show<SAPDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    oModel.CustomerCode = res.CardCode;
                    oModel.CustomerName = res.CardName;
                    DialogFor = "SalePriceList";
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RowClickEvent(TableRowClickEventArgs<MstSalesPriceListDetail> tableRowClickEventArgs)
        {
            try
            {
                MstSalesPriceListDetail forms = new MstSalesPriceListDetail();
                forms.ProductDescription = tableRowClickEventArgs.Item.ProductDescription;
                forms.Fk = new MstSalesPriceList();
                //forms.Fk.ResrListName = "abcd";
                forms.ProductCode = tableRowClickEventArgs.Item.ProductCode;
                forms.ProductDescription = tableRowClickEventArgs.Item.ProductDescription;
                forms.Fk.MstSalesPriceListDetails = new List<MstSalesPriceListDetail>();
                forms.PerUnitSalesPrice = Convert.ToInt32(tableRowClickEventArgs.Item.PerUnitSalesPrice);
                //forms.Rate = Convert.ToInt32(tableRowClickEventArgs.Item.Rate);
                //forms.LandedFactor = Convert.ToInt32(tableRowClickEventArgs.Item.LandedFactor);
                //forms.Cost = Convert.ToInt32(tableRowClickEventArgs.Item.Cost);
                //forms.NIC = tableRowClickEventArgs.Item.NIC;
                //forms.Phone = tableRowClickEventArgs.Item.Phone;
                //forms.DriverName = tableRowClickEventArgs.Item.DriverName;
                //forms.Contractor = tableRowClickEventArgs.Item.Contractor;

                //MudDialog.Close(DialogResult.Ok<MstResourceDetail>(forms));
                var salepricelist = new DialogParameters { ["SalePriceList"] = forms };
                var dialog = Dialog.Show<DetailDialog>("", salepricelist);
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenEditDialog(DialogOptions options, MstSalesPriceListDetail oDetailPara)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("oDetailParaSales", oDetailPara);
                parameters.Add("DialogFor", "SalePriceList");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstSalesPriceListDetail)result.Data;
                    var update = oDetailList.Where(x => x.ProductCode == res.ProductCode && x.ProductDescription.ToLower() == res.ProductDescription.ToLower()
                    && x.PerUnitSalesPrice == res.PerUnitSalesPrice);
                    if (update != null)
                    {
                        oDetail.Remove((MstSalesPriceListDetail)update);
                    }
                    MstSalesPriceListDetail oRDDetail = new MstSalesPriceListDetail();
                    oRDDetail.Id = res.Id;
                    oRDDetail.Fkid = res.Fkid;
                    oRDDetail.ProductCode = res.ProductCode;
                    oRDDetail.ProductDescription = res.ProductDescription;
                    oRDDetail.PerUnitSalesPrice = res.PerUnitSalesPrice;

                    oDetail.Add(oRDDetail);
                    oDetailList = oDetail.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        public void RemoveRecord(string ProductCode, string ProductDescription, decimal PerUnitSalesPrice)
        {
            try
            {
                var remove = oDetailList.Where(x => x.ProductCode == ProductCode && x.ProductDescription == ProductDescription
                    && x.PerUnitSalesPrice == PerUnitSalesPrice).FirstOrDefault();
                if (remove != null)
                {
                    oDetailList.Remove(remove);
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
                if (!string.IsNullOrWhiteSpace(Convert.ToString(oModel.Id)))
                {
                    try
                    {
                        
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
                        oModel.CustomerName = oModel.CustomerName;
                        oModel.CustomerCode = oModel.CustomerCode;
                        oModel.Plname = oModel.Plname;
                        oModel.FlgDefult = oModel.FlgDefult;
                        oModel.MstSalesPriceListDetails = oDetail.ToList();

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

                        if (oModel != null && oModel.MstSalesPriceListDetails != null && oModel.MstSalesPriceListDetails.Count > 0)
                        {
                            if (oModel.Id == 0)
                            {
                                res = await _mstCostDrive.Insert(oModel, "manager");
                            }
                            else
                            {
                                res = await _mstCostDrive.Update(oModel, "manager");
                            }
                        }
                        else
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }

                        //}
                        if (res != null && res.Id == 1)
                        {
                            Snackbar.Add(res.Message, Severity.Success, (options) => { options.Icon = Icons.Sharp.Info; });
                            await Task.Delay(3000);
                            Navigation.NavigateTo("/SalesPriceList", forceLoad: true);
                        }
                        else
                        {
                            Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        }
                        //oModel.FlgActive = true;


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
        
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
        //        if (Session != null)
        //        {
        //            var res = await _mstUserProfile.FetchUserAuth(Session.Id);
        //            if (res.Where(x => x.MenuName == "Sales Price List" && x.UserRights != 1).ToList().Count > 0)
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
    }
}