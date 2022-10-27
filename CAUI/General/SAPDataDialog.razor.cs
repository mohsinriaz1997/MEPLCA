using CA.API.Models;
using CA.UI.Interfaces.SAPData;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace CA.UI.General
{
    public partial class SAPDataDialog
    {
        #region InjectService

        [Inject]
        public ISAPData _SAPService { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        #endregion InjectService

        #region Variables

        private bool Loading = false;

        private string searchString1 = "";
        private string searchStringAccount = "";
        private string searchStringCustomer = "";

        private int selectedRowNumber = -1;

        private List<string> clickedEvents = new();
        private HashSet<SAPModels> selectedItems1 = new HashSet<SAPModels>();
        private List<TrnsDirectMaterialDetail> trnsDirectMaterialDetails= new List<TrnsDirectMaterialDetail>();
        private List<TrnsFohdriverRatesDetail> TrnsFohdriverRatesDetail = new List<TrnsFohdriverRatesDetail>();
        private List<TrnsVohmachineDetail> TrnsVohmachineDetail = new List<TrnsVohmachineDetail>();
        private List<TrnsVohlabourDetail> TrnsVohlabourDetail = new List<TrnsVohlabourDetail>();
        private List<TrnsVohelectricityDetail> TrnsVohelectricityDetail = new List<TrnsVohelectricityDetail>();
        private List<TrnsVohdyesAndMoldDetail> TrnsVohdyesAndMoldDetail = new List<TrnsVohdyesAndMoldDetail>();
        private List<TrnsVohtoolsDetail> TrnsVohtoolsDetail = new List<TrnsVohtoolsDetail>();
        private List<TrnsVohgasolineDetail> TrnsVohgasolineDetail = new List<TrnsVohgasolineDetail>();
        //private List<string> clickedEvents = new();
        private List<string> _events = new();


        private MudTable<SAPModels> _table;
        private MudTable<SAPModels> _tableVohMachine;
        private MudTable<SAPModels> _tableVohLabor;
        private MudTable<SAPModels> _tableVohElectricity;
        private MudTable<SAPModels> _tableVohDyes;
        private MudTable<SAPModels> _tableTools;
        private MudTable<SAPModels> _tableGasoline;

        private bool FilterFunc1(SAPModels element) => FilterFunc(element, searchString1);
        private bool FilterFuncCustomer(SAPModels elementCustomer) => FilterFuncCustomer(elementCustomer, searchStringCustomer);

        private bool FilterFuncBom(SAPModels element) => FilterFuncBomItem(element, searchString1);
        private bool FilterFuncAccount(SAPModels elementAccount) => FilterFuncBomAccount(elementAccount, searchStringAccount);
        private bool FilterFuncVohMachine(SAPModels element) => FilterFuncVohMachine(element, searchString1);
        private bool FilterFuncVohLabor(SAPModels element) => FilterFuncVohLabor(element, searchString1);
        private bool FilterFuncVohElectricity(SAPModels element) => FilterFuncVohElectricity(element, searchString1);
        private bool FilterFuncVohDyes(SAPModels element) => FilterFuncVohDyes(element, searchString1);
        private bool FilterFuncVohTools(SAPModels element) => FilterFuncVohTools(element, searchString1);
        private bool FilterFuncVohGasoline(SAPModels element) => FilterFuncVohGasoline(element, searchString1);

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public DateTime? DocDatePara { get; set; }

        [Parameter]
        public string ProductCode { get; set; }


        [Parameter]
        public TrnsDirectMaterialDetail oDMDetail { get; set; } = new TrnsDirectMaterialDetail(); 
        private List<TrnsDirectMaterialDetail> oDMDetailList = new List<TrnsDirectMaterialDetail>();
        [Parameter]
        public string year { get; set; }

        [Parameter]
        public string month { get; set; }

        [Parameter]
        public string DialogFor { get; set; }

        private SAPModels oSAPModels = new SAPModels();
        private List<SAPModels> SAPModelsList = new List<SAPModels>();

        private void Cancel() => MudDialog.Cancel();

        #endregion Variables

        #region Functions

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
        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };
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
        private bool FilterFuncCustomer(SAPModels elementCustomer, string searchStringCustomer)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchStringCustomer))
                    return true;
                if (elementCustomer.CardCode.Contains(searchStringCustomer, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (elementCustomer.CardName.Contains(searchStringCustomer, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }
        private bool FilterFuncVohLabor(SAPModels element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.ItemCodeVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemQuantityVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemNameVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
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
        private bool FilterFuncBomAccount(SAPModels elementAccount, string searchStringAccount)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchStringAccount))
                    return true;
                if (elementAccount.AcctCode.Contains(searchStringAccount, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (elementAccount.AcctName.Contains(searchStringAccount, StringComparison.OrdinalIgnoreCase))
                    return true;
                
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }
        private bool FilterFuncVohMachine(SAPModels element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.ItemCodeVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemQuantityVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemNameVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }
        private bool FilterFuncVohElectricity(SAPModels element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.ItemCodeVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemQuantityVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemNameVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }
        private bool FilterFuncVohDyes(SAPModels element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.ItemCodeVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemQuantityVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemNameVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }
        private bool FilterFuncVohTools(SAPModels element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.ItemCodeVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemQuantityVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemNameVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }
        private bool FilterFuncVohGasoline(SAPModels element, string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.ItemCodeVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemQuantityVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (element.ItemNameVOH.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return false;
            }
        }

        private async Task OpenAddDialog(DialogOptions options,string ProductCode)
        {
            try
            {
                var parameters = new DialogParameters();
                //parameters.Add("DialogFor", "BOMItem");
                parameters.Add("DialogFor", "DirectMaterialRL");
                parameters.Add("ProductCode", ProductCode);
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsDirectMaterialDetail)result.Data;
                    //var update = oDetailList.Where(x => x.ItemCode == res.ItemCode && x.ItemName.ToLower() == res.ItemName.ToLower()).FirstOrDefault();
                    //if (update != null)
                    //{
                    //    Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    //}
                    //else
                    //{
                    //    oDetail.Add(res);
                    //    oDetailList = oDetail;
                    //}
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task<List<SAPModels>> GetExchangeRate(string DocDate)
        {
            
            SAPModelsList.Clear();
            SAPModelsList = await _SAPService.GetExchangeRateFromSAP(DocDate);
            return SAPModelsList;
        }
        private async Task<List<SAPModels>> GetBlanketAgreement(string ProductCode)
        {
            SAPModelsList.Clear();
            SAPModelsList = await _SAPService.GetBlanketAgreementFromSAP(ProductCode);
            return SAPModelsList;
        }
        private async Task<List<SAPModels>> GetCurrency()
        {
            SAPModelsList.Clear();
            SAPModelsList = await _SAPService.GetCurrencyFromSAP();
            return SAPModelsList;
        }

        private async Task<List<SAPModels>> GetBomItems(string ProductCode)
        {
            SAPModelsList.Clear();
            SAPModelsList = await _SAPService.GetBomItemsFromSAP(ProductCode);
            return SAPModelsList;
        }

        private async Task<List<SAPModels>> GetProduct()
        {
            SAPModelsList.Clear();
            SAPModelsList = await _SAPService.GetProductFromSap();
            return SAPModelsList;
        }

        private async Task<List<SAPModels>> GetCustomer()
        {
            SAPModelsList.Clear();
            SAPModelsList = await _SAPService.GetCustomerFromSAP();
            return SAPModelsList;
        }

        private async Task<List<SAPModels>> GetItems()
        {
            SAPModelsList.Clear();
            string Clause = "ItmsGrpCod = '101'";
            SAPModelsList = await _SAPService.GetItemsFromSAP(Clause);
            return SAPModelsList;
        }
        private async Task<List<SAPModels>> GetRawItems()
        {
            SAPModelsList.Clear();
            //string Clause = "ItmsGrpCod = '101'";
            SAPModelsList = await _SAPService.GetAllItemsFromSAP();
            return SAPModelsList;
        }

        private async Task<List<SAPModels>> GetItemsVOH(string year, string month)
        {
            SAPModelsList.Clear();
            string Clause = "'105'";
            //string year = "2007";
            //string month = "4";
            SAPModelsList = await _SAPService.GetItemsVOHFromSAP(Clause, year, month);
            return SAPModelsList;
        }
       
        //private IEnumerable<SAPModels> elements = new List<SAPModels>();

        private async Task<List<SAPModels>> GetAccounts()
        {
            SAPModelsList.Clear();
            string Clause = "Acttype = 'E'";
            SAPModelsList = await _SAPService.GetAccountsFromSAP(Clause);
            return SAPModelsList;
        }

        private async Task<List<SAPModels>> GetRMItemsForSalesPriceList()
        {
            SAPModelsList.Clear();
            string Clause = "ItmsGrpCod = '105'";
            SAPModelsList = await _SAPService.GetItemsFromSAP(Clause);
            return SAPModelsList;
        }
        

        private async Task<List<SAPModels>> GetFinishedGoodsItem()
        {
            SAPModelsList.Clear();
            string Clause = "ItmsGrpCod = '105'";
            SAPModelsList = await _SAPService.GetItemsFromSAP(Clause);
            return SAPModelsList;
        }

        private async void Submit()
        {
            try
            {
                if (DialogFor == "ExchangeRate")
                {
                    if (oSAPModels.Rate > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "Currency")
                {
                    if (oSAPModels.CurrName != "")
                    {
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "BomItemCode")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        var bomItemcode= SAPModelsList.Select(x => x.BOMItemCode);
                        var bomItemUOM= SAPModelsList.Select(x => x.BOMUOM);
                        var bomItemQuantity = SAPModelsList.Select(x => x.BOMQuantity);
                        foreach (var item in selectedItems1)
                        {
                            TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                            s.ItemCode = item.BOMItemCode;
                            s.ItemName = item.BOMItemName;
                            s.ConsQty = Convert.ToDecimal( item.BOMQuantity);
                            s.Uom = item.BOMUOM;
                            trnsDirectMaterialDetails.Add(s);
                        }
                        //var a = trnsDirectMaterialDetails;
                        //oDML = SAPModelsList.ToString();
                        //oSAPModels =(SAPModelsList);
                        //oSAPModels.ItemCode.ToString();
                        //var c = (SAPModels)selectedItems1.Select(x => x.BOMItemCode);
                        MudDialog.Close(DialogResult.Ok<List<TrnsDirectMaterialDetail>>(trnsDirectMaterialDetails));
                        //MudDialog.Close(DialogResult.Ok<TrnsDirectMaterialDetail>(oDMDetail));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "BlanketAgreement")
                {
                    if (oSAPModels.BOMItemName != "")
                    {
                        selectedItems1.ToList();

                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "DirectMaterialItem")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        foreach (var item in selectedItems1)
                        {
                            TrnsDirectMaterialDetail otrnsDirectMaterialDetail = new TrnsDirectMaterialDetail();
                            otrnsDirectMaterialDetail.ItemCode = item.BOMItemCode;
                            otrnsDirectMaterialDetail.ItemName = item.BOMItemName;
                            otrnsDirectMaterialDetail.ConsQty = Convert.ToDecimal(item.BOMQuantity);
                            otrnsDirectMaterialDetail.Uom = item.BOMUOM;
                            otrnsDirectMaterialDetail.ChangeConsQty = Convert.ToDecimal(item.BOMQuantity);
                            trnsDirectMaterialDetails.Add(otrnsDirectMaterialDetail);
                        }
                        MudDialog.Close(DialogResult.Ok<List<TrnsDirectMaterialDetail>>(trnsDirectMaterialDetails));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "Product")
                {
                    if (oSAPModels.BOMItemName != "")
                    {
                        selectedItems1.ToList();

                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "DirectMaterialProduct")
                {
                    if (oSAPModels.BOMItemName != "")
                    {
                        selectedItems1.ToList();

                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "Customer")
                {
                    if (oSAPModels.CardCode != "")
                    {
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "SalesItems")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        //oSAPModels.DialogFor = "SalePriceList";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "SalesQuotation")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        oSAPModels.DialogFor = "SalesQuotation";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "FOHDriverRateItems")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        //var bomItemcode = SAPModelsList.Select(x => x.BOMItemCode);
                        //var bomItemUOM = SAPModelsList.Select(x => x.BOMUOM);
                        //var bomItemQuantity = SAPModelsList.Select(x => x.BOMQuantity);
                        foreach (var item in selectedItems1)
                        {
                            TrnsFohdriverRatesDetail s = new TrnsFohdriverRatesDetail();
                            s.ProductCode = item.ItemCode;
                            s.ProductName = item.ItemName;
                            //s.ConsQty = Convert.ToDecimal(item.BOMQuantity);
                            //s.Uom = item.BOMUOM;
                            TrnsFohdriverRatesDetail.Add(s);
                        }
                        //var a = trnsDirectMaterialDetails;
                        //oDML = SAPModelsList.ToString();
                        //oSAPModels =(SAPModelsList);
                        //oSAPModels.ItemCode.ToString();
                        //var c = (SAPModels)selectedItems1.Select(x => x.BOMItemCode);
                        MudDialog.Close(DialogResult.Ok<List<TrnsFohdriverRatesDetail>>(TrnsFohdriverRatesDetail));
                        //MudDialog.Close(DialogResult.Ok<TrnsDirectMaterialDetail>(oDMDetail));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOHMachineRateItems")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        var ItemcodeVoh = SAPModelsList.Select(x => x.ItemCodeVOH);
                        var ItemNameVOH = SAPModelsList.Select(x => x.ItemNameVOH);
                        var ItemQuantityVOH = SAPModelsList.Select(x => x.ItemQuantityVOH);
                        foreach (var item in selectedItems1)
                        {
                            TrnsVohmachineDetail s = new TrnsVohmachineDetail();
                            s.ProductCode = item.ItemCodeVOH;
                            s.ProductName = item.ItemNameVOH;
                            //s.p = Convert.ToDecimal(item.ItemQuantityVOH);
                            //s.Uom = item.BOMUOM;
                            TrnsVohmachineDetail.Add(s);
                        }
                        //oSAPModels.DialogFor = "MonthlyVohRlMachineTab";
                        MudDialog.Close(DialogResult.Ok<List<TrnsVohmachineDetail>>(TrnsVohmachineDetail));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOHLaborRateItems")
                {

                    if (oSAPModels.ItemCode != "")
                    {
                        var ItemcodeVoh = SAPModelsList.Select(x => x.ItemCodeVOH);
                        var ItemNameVOH = SAPModelsList.Select(x => x.ItemNameVOH);
                        var ItemQuantityVOH = SAPModelsList.Select(x => x.ItemQuantityVOH);
                        foreach (var item in selectedItems1)
                        {
                            TrnsVohlabourDetail s = new TrnsVohlabourDetail();
                            s.ProductCode = item.ItemCodeVOH;
                            s.ProductName = item.ItemNameVOH;
                            //s.p = Convert.ToDecimal(item.ItemQuantityVOH);
                            //s.Uom = item.BOMUOM;
                            TrnsVohlabourDetail.Add(s);
                        }
                        //oSAPModels.DialogFor = "MonthlyVohRlMachineTab";
                        MudDialog.Close(DialogResult.Ok<List<TrnsVohlabourDetail>>(TrnsVohlabourDetail));

                        //SAPModels.DialogFor = "MonthlyVohRlLaoburTab";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOHElectricityRateItems")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        var ItemcodeVoh = SAPModelsList.Select(x => x.ItemCodeVOH);
                        var ItemNameVOH = SAPModelsList.Select(x => x.ItemNameVOH);
                        var ItemQuantityVOH = SAPModelsList.Select(x => x.ItemQuantityVOH);
                        foreach (var item in selectedItems1)
                        {
                            TrnsVohelectricityDetail s = new TrnsVohelectricityDetail();
                            s.ProductCode = item.ItemCodeVOH;
                            s.ProductName = item.ItemNameVOH;
                            //s.p = Convert.ToDecimal(item.ItemQuantityVOH);
                            //s.Uom = item.BOMUOM;
                            TrnsVohelectricityDetail.Add(s);
                        }
                        //oSAPModels.DialogFor = "MonthlyVohRlMachineTab";
                        MudDialog.Close(DialogResult.Ok<List<TrnsVohelectricityDetail>>(TrnsVohelectricityDetail));

                        //SAPModels.DialogFor = "MonthlyVohRlLaoburTab";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOHDyesRateItems")
                {
                    if (oSAPModels.ItemCode != "") if (oSAPModels.ItemCode != "")
                        {
                            var ItemcodeVoh = SAPModelsList.Select(x => x.ItemCodeVOH);
                            var ItemNameVOH = SAPModelsList.Select(x => x.ItemNameVOH);
                            var ItemQuantityVOH = SAPModelsList.Select(x => x.ItemQuantityVOH);
                            foreach (var item in selectedItems1)
                            {
                                TrnsVohdyesAndMoldDetail s = new TrnsVohdyesAndMoldDetail();
                                s.ProductCode = item.ItemCodeVOH;
                                s.ProductName = item.ItemNameVOH;
                                //s.p = Convert.ToDecimal(item.ItemQuantityVOH);
                                //s.Uom = item.BOMUOM;
                                TrnsVohdyesAndMoldDetail.Add(s);
                            }
                            //oSAPModels.DialogFor = "MonthlyVohRlMachineTab";
                            MudDialog.Close(DialogResult.Ok<List<TrnsVohdyesAndMoldDetail>>(TrnsVohdyesAndMoldDetail));

                            //SAPModels.DialogFor = "MonthlyVohRlLaoburTab";
                            MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                        }
                        else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOHToolsRateItems")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        var ItemcodeVoh = SAPModelsList.Select(x => x.ItemCodeVOH);
                        var ItemNameVOH = SAPModelsList.Select(x => x.ItemNameVOH);
                        var ItemQuantityVOH = SAPModelsList.Select(x => x.ItemQuantityVOH);
                        foreach (var item in selectedItems1)
                        {
                            TrnsVohtoolsDetail s = new TrnsVohtoolsDetail();
                            s.ProductCode = item.ItemCodeVOH;
                            s.ProductName = item.ItemNameVOH;
                            //s.p = Convert.ToDecimal(item.ItemQuantityVOH);
                            //s.Uom = item.BOMUOM;
                            TrnsVohtoolsDetail.Add(s);
                        }
                        //oSAPModels.DialogFor = "MonthlyVohRlMachineTab";
                        MudDialog.Close(DialogResult.Ok<List<TrnsVohtoolsDetail>>(TrnsVohtoolsDetail));

                        //SAPModels.DialogFor = "MonthlyVohRlLaoburTab";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOHGasolineRateItems")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        var ItemcodeVoh = SAPModelsList.Select(x => x.ItemCodeVOH);
                        var ItemNameVOH = SAPModelsList.Select(x => x.ItemNameVOH);
                        var ItemQuantityVOH = SAPModelsList.Select(x => x.ItemQuantityVOH);
                        foreach (var item in selectedItems1)
                        {
                            TrnsVohgasolineDetail s = new TrnsVohgasolineDetail();
                            s.ProductCode = item.ItemCodeVOH;
                            s.ProductName = item.ItemNameVOH;
                            //s.p = Convert.ToDecimal(item.ItemQuantityVOH);
                            //s.Uom = item.BOMUOM;
                            TrnsVohgasolineDetail.Add(s);
                        }
                        //oSAPModels.DialogFor = "MonthlyVohRlMachineTab";
                        MudDialog.Close(DialogResult.Ok<List<TrnsVohgasolineDetail>>(TrnsVohgasolineDetail));

                        //SAPModels.DialogFor = "MonthlyVohRlLaoburTab";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "RMItemPriceList")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        oSAPModels.DialogFor = "ItemPriceList";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
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
                if (DialogFor == "RawMaterialItemPriceList")
                {
                    if (oSAPModels.ItemCode != "")
                    {
                        oSAPModels.DialogFor = "ItemPriceList";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }

                if (DialogFor == "AccountExpenseList")
                {
                    if (oSAPModels.AcctCode != "")
                    {
                        //oSAPModels.DialogFor = "AccountCodeList";
                        MudDialog.Close(DialogResult.Ok<SAPModels>(oSAPModels));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
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
        private string SelectedRowClassFuncAccount(SAPModels elementAccount, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_table.SelectedItem != null && _table.SelectedItem.Equals(elementAccount))
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
        private string SelectedRowClassFuncVohMachine(SAPModels element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableVohMachine.SelectedItem != null && _tableVohMachine.SelectedItem.Equals(element))
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
        private string SelectedRowClassFuncVohLabor(SAPModels element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableVohLabor.SelectedItem != null && _tableVohLabor.SelectedItem.Equals(element))
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
        private string SelectedRowClassFuncVohElectricity(SAPModels element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableVohElectricity.SelectedItem != null && _tableVohElectricity.SelectedItem.Equals(element))
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
        private string SelectedRowClassFuncVohDyes(SAPModels element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableVohDyes.SelectedItem != null && _tableVohDyes.SelectedItem.Equals(element))
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
        private string SelectedRowClassFuncVohTools(SAPModels element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableTools.SelectedItem != null && _tableTools.SelectedItem.Equals(element))
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
        private string SelectedRowClassFuncVohGasoline(SAPModels element, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableGasoline.SelectedItem != null && _tableGasoline.SelectedItem.Equals(element))
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

        #endregion Functions

        #region Events

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Loading = true;
                if (DialogFor == "ExchangeRate")
                {
                    if (DocDatePara != null)
                    {
                        await GetExchangeRate(Convert.ToString(DocDatePara));
                    }
                }
                if (DialogFor == "AccountExpenseList")
                {
                    await GetAccounts();
                }
                if (DialogFor == "Currency")
                {
                    await GetCurrency();
                }
                if (DialogFor == "BlanketAgreement")
                {
                    if (ProductCode != null)
                    {
                        await GetBlanketAgreement(ProductCode);
                    }
                }
                if (DialogFor == "BomItemCode")
                {
                    if (ProductCode != null)
                    {
                        await GetBomItems(ProductCode);
                    }
                }
                if (DialogFor == "DirectMaterialItem")
                {
                    if (ProductCode != null)
                    {
                        await GetBomItems(ProductCode);
                    }
                }
                if (DialogFor == "Product")
                {
                    await GetProduct();
                }
                if (DialogFor == "DirectMaterialProduct")
                {
                    await GetProduct();
                }
                if (DialogFor == "FOHDriverRateItems")
                {
                    await GetItems();
                }
                if (DialogFor == "VOHMachineRateItems")
                {
                    if (year != null && month != null)
                    {
                        await GetItemsVOH(year, month);
                    }
                }
                if (DialogFor == "VOHLaborRateItems")
                {
                    if (year != null && month != null)
                    {
                        await GetItemsVOH(year, month);
                    }
                }
                if (DialogFor == "VOHElectricityRateItems")
                {
                    if (year != null && month != null)
                    {
                        await GetItemsVOH(year, month);
                    }
                }
                if (DialogFor == "VOHDyesRateItems")
                {
                    if (year != null && month != null)
                    {
                        await GetItemsVOH(year, month);
                    }
                }
                if (DialogFor == "VOHToolsRateItems")
                {
                    if (year != null && month != null)
                    {
                        await GetItemsVOH(year, month);
                    }
                }
                if (DialogFor == "VOHGasolineRateItems")
                {
                    if (year != null && month != null)
                    {
                        await GetItemsVOH(year, month);
                    }
                }
                if (DialogFor == "Customer")
                {
                    await GetCustomer();
                }
                if (DialogFor == "SalesItems")
                {
                    await GetItems();
                }
                if (DialogFor == "SalesQuotation")
                {
                    await GetItems();
                }
                if (DialogFor == "RMItemPriceList")
                {
                    await GetRMItemsForSalesPriceList();
                    //await GetRawItems();
                }
                if (DialogFor == "VOCFinishedGoodsItem")
                {
                    await GetFinishedGoodsItem();
                    //await GetRawItems();
                }
                if (DialogFor == "RawMaterialItemPriceList")
                {
                    //await GetRMItemsForSalesPriceList();
                    await GetRawItems();
                }
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
                //foreach (var item in selectedItems1)
                //{
                //    TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                //    s.ItemCode = item.ItemCode;
                //    s.ItemName = item.ItemName;
                //    trnsDirectMaterialDetails.Add(s);
                //}

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }
        private void RowClickEventAccount(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                selectedItems1.Add(oSAPModels);
                //foreach (var item in selectedItems1)
                //{
                //    TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                //    s.ItemCode = item.ItemCode;
                //    s.ItemName = item.ItemName;
                //    trnsDirectMaterialDetails.Add(s);
                //}

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }
        private void RowClickEventVohMachine(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                selectedItems1.Add(oSAPModels);
                //foreach (var item in selectedItems1)
                //{
                //    TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                //    s.ItemCode = item.ItemCode;
                //    s.ItemName = item.ItemName;
                //    trnsDirectMaterialDetails.Add(s);
                //}

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }
        private void RowClickEventVohLabor(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                selectedItems1.Add(oSAPModels);
                //foreach (var item in selectedItems1)
                //{
                //    TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                //    s.ItemCode = item.ItemCode;
                //    s.ItemName = item.ItemName;
                //    trnsDirectMaterialDetails.Add(s);
                //}

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }
        private void RowClickEventVohElectricity(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                selectedItems1.Add(oSAPModels);
                //foreach (var item in selectedItems1)
                //{
                //    TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                //    s.ItemCode = item.ItemCode;
                //    s.ItemName = item.ItemName;
                //    trnsDirectMaterialDetails.Add(s);
                //}

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }
        private void RowClickEventVohDyes(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                selectedItems1.Add(oSAPModels);
                //foreach (var item in selectedItems1)
                //{
                //    TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                //    s.ItemCode = item.ItemCode;
                //    s.ItemName = item.ItemName;
                //    trnsDirectMaterialDetails.Add(s);
                //}

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }
        private void RowClickEventVohTools(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                selectedItems1.Add(oSAPModels);
                //foreach (var item in selectedItems1)
                //{
                //    TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                //    s.ItemCode = item.ItemCode;
                //    s.ItemName = item.ItemName;
                //    trnsDirectMaterialDetails.Add(s);
                //}

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }
        private void RowClickEventVohGasoline(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        {
            try
            {
                selectedItems1.Add(oSAPModels);
                //foreach (var item in selectedItems1)
                //{
                //    TrnsDirectMaterialDetail s = new TrnsDirectMaterialDetail();
                //    s.ItemCode = item.ItemCode;
                //    s.ItemName = item.ItemName;
                //    trnsDirectMaterialDetails.Add(s);
                //}

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex.Message);
            }
        }
        //private void SelectedItemsChanged(HashSet<SAPModels> items)
        //{
        //    try
        //    {
        //        _events.Insert(0, $"Data = {System.Text.Json.JsonSerializer.Serialize(items)}");
        //    }
        //    catch (Exception ex)
        //    {

        //        Logs.GenerateLogs(ex.Message);
        //    }
        //    //_events.Insert(0, $"Event = SelectedItemsChanged, Data = {System.Text.Json.JsonSerializer.Serialize(items)}");
        //}

        //public void RowClickEvent(TableRowClickEventArgs<SAPModels> tableRowClickEventArgs)
        //{
        //    try
        //    {
        //        SAPModels forms = new SAPModels();
        //        if (DialogFor == "ExchangeRate")
        //        {
        //            forms.Currency = tableRowClickEventArgs.Item.Currency;
        //            forms.Rate = tableRowClickEventArgs.Item.Rate;
        //        }
        //        if (DialogFor == "Currency")
        //        {
        //            forms.CurrCode = tableRowClickEventArgs.Item.CurrCode;
        //            forms.CurrName = tableRowClickEventArgs.Item.CurrName;
        //        }
        //        MudDialog.Close(DialogResult.Ok<SAPModels>(forms));
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex.Message);
        //    }
        //}

        #endregion Events
    }
}