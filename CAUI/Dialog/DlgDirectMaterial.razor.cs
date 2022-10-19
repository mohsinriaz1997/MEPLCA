using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using CA.UI.Interfaces.MasterData;
using CA.UI.Interfaces.SAPData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Dialog
{
    public partial class DlgDirectMaterial
    {
        #region InjectService

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public ISAPData _SAPService { get; set; }

        [Inject]
        public IItemPriceList _mstItemPriceListMaster { get; set; }

        [Inject]
        public IDirectMaterial _mstDirectMaterial { get; set; }

        #endregion InjectService

        #region Parameter

        [Parameter]
        public string DialogFor { get; set; }

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public DateTime? DocDatePara { get; set; }

        [Parameter]
        public string ProductCode { get; set; }

        #endregion

        #region Varibale 

        private bool Loading = false;

        private MudTable<SAPModels> _table;

        private MudTable<MstItemPriceList> _table1;

        private MudTable<TrnsDirectMaterial> _tableDirect;

        private MudTable<MstSalesPriceList> _tableSales;

        private string searchString1 = "";

        private string searchString8 = "";

        private int selectedRowNumber = -1;

        private bool FilterFunc1(SAPModels element) => FilterFunc(element, searchString1);

        private bool FilterFunc2(MstItemPriceList element2) => FilterFuncItem(element2, searchString1);

        private bool FilterFuncBom(SAPModels element) => FilterFuncBomItem(element, searchString1);

        private bool FilterFuncDirectMaterial(TrnsDirectMaterial elementDirectMaterial) => FilterFuncDirectMaterial(elementDirectMaterial, searchString8);

        private List<string> clickedEvents = new();

        private HashSet<SAPModels> selectedItems1 = new HashSet<SAPModels>();

        private SAPModels oSAPModels = new SAPModels();

        private List<SAPModels> SAPModelsList = new List<SAPModels>();

        private List<MstItemPriceList> oListItem = new List<MstItemPriceList>();

        private MstItemPriceList oModelItem = new MstItemPriceList();

        private TrnsDirectMaterial oModelTrnsDirectMaterial = new TrnsDirectMaterial();

        private List<TrnsDirectMaterial> oTrnsDirectMaterial = new List<TrnsDirectMaterial>();

        private List<TrnsDirectMaterialDetail> trnsDirectMaterialDetails = new List<TrnsDirectMaterialDetail>();
        private void Cancel() => MudDialog.Cancel();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

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

        private bool FilterFuncItem(MstItemPriceList element2, string searchString1)
        {
            if (string.IsNullOrWhiteSpace(searchString1))
                return true;

            if (element2.FlgDefaultPl.Equals(searchString1))
                return true;
            return false;
        }

        private bool FilterFuncDirectMaterial(TrnsDirectMaterial elementDirectMaterial, string searchString8)
        {
            if (string.IsNullOrWhiteSpace(searchString8))
                return true;

            if (elementDirectMaterial.DocName.Equals(searchString8))
                return true;
            return false;
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

        private string SelectedRowClassFuncItem(MstItemPriceList element, int rowNumber)
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

        private string SelectedRowClassFuncDirectMaterial(TrnsDirectMaterial elementDirectMaterial, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableDirect.SelectedItem != null && _tableDirect.SelectedItem.Equals(elementDirectMaterial))
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

        private async Task GetAllItemPriceListMaster()
        {
            try
            {
                oListItem = await _mstItemPriceListMaster.GetAllData();
                if (oListItem?.Count == 0 || oListItem == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllDirectMaterial()
        {
            try
            {
                oTrnsDirectMaterial = await _mstDirectMaterial.GetAllData();
                if (oTrnsDirectMaterial?.Count == 0 || oTrnsDirectMaterial == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task<List<SAPModels>> GetBomItems(string ProductCode)
        {
            SAPModelsList.Clear();
            SAPModelsList = await _SAPService.GetBomItemsFromSAP(ProductCode);
            return SAPModelsList;
        }

        private async Task<List<SAPModels>> GetProduct()
        {
            try
            {
                SAPModelsList.Clear();
                SAPModelsList = await _SAPService.GetProductFromSap();
                return SAPModelsList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return SAPModelsList = null;
            }
        }

        private async Task<List<SAPModels>> GetExchangeRate(string DocDate)
        {
            try
            {
                SAPModelsList.Clear();
                SAPModelsList = await _SAPService.GetExchangeRateFromSAP(DocDate);
                return SAPModelsList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return SAPModelsList = null;
            }
        }

        private async void Submit()
        {
            try
            {
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
                if (DialogFor == "DirectMaterialItemPriceList")
                {
                    if (oModelItem.Id > 0)
                    {

                        MudDialog.Close(DialogResult.Ok<MstItemPriceList>(oModelItem));
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
                if (DialogFor == "DirectMaterialMaster")
                {
                    if (oModelTrnsDirectMaterial.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsDirectMaterial>(oModelTrnsDirectMaterial));
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

        #endregion

        #region Events

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Loading = true;
                if (DialogFor == "DirectMaterialProduct")
                {
                    await GetProduct();
                }
                if (DialogFor == "ExchangeRate")
                {
                    if (DocDatePara != null)
                    {
                        await GetExchangeRate(Convert.ToString(DocDatePara));
                    }
                }
                if (DialogFor == "DirectMaterialItemPriceList")
                {
                    await GetAllItemPriceListMaster();
                }
                if (DialogFor == "DirectMaterialItem")
                {
                    if (ProductCode != null)
                    {
                        await GetBomItems(ProductCode);
                    }
                }
                if (DialogFor == "DirectMaterialMaster")
                {
                    await GetAllDirectMaterial();
                }

                Loading = false;
            }
            catch (Exception ex)
            {

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

        private void RowClickEventItem(TableRowClickEventArgs<MstItemPriceList> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventDirectMaterial(TableRowClickEventArgs<TrnsDirectMaterial> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        #endregion
    }
}
