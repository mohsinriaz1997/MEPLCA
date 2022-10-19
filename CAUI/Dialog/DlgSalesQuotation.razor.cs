using CA.API.Models;
using CA.UI.Data.CostAllocation;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using CA.UI.Interfaces.MasterData;
using CA.UI.Interfaces.SAPData;
using CA.UI.Pages.Cost_Allocations;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using MudBlazor;

namespace CA.UI.Dialog
{
    public partial class DlgSalesQuotation
    {
        #region Inject Services

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public ISAPData _SAPService { get; set; }

        [Inject]
        public ISalePriceList _mstSalePriceListMaster { get; set; }

        [Inject]
        public IDirectMaterial _mstDirectMaterial { get; set; }

        [Inject]
        public IVOC _mstVOC { get; set; }


        [Inject]
        public IFOHRatesCalc _mstFOHRatesCalc { get; set; }

        [Inject]
        public ISalesQuotation _SalesQuotation { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        #endregion

        #region Parameter

        [Parameter]
        public string DialogFor { get; set; }

        [Parameter]
        public string ProductCode { get; set; }

        [Parameter]
        public MstSalesPriceList oModelSalesPriceList { get; set; }

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        #endregion

        #region Variables

        private bool Loading = false;

        private string searchString1 = "";
        private string searchString6 = "";
        private string searchString7 = "";
        private string searchString8 = "";
        private string searchString9 = "";
        private string searchStringSQ = "";

        private MudTable<SAPModels> _table;
        private MudTable<MstSalesPriceList> _tableSales;
        private MudTable<TrnsDirectMaterial> _tableDirect;
        private MudTable<TrnsVoc> _tableVOC;
        private MudTable<TrnsFohrate> _table5;
        private MudTable<TrnsSalesQuotation> _tableSQ;

        private TrnsSalesQuotationDetail mstSalesQuatationDetail = new TrnsSalesQuotationDetail();
        private TrnsSalesQuotation oModelSQ = new TrnsSalesQuotation();
        private List<TrnsSalesQuotation> oListSalesQuotation = new List<TrnsSalesQuotation>();

        private SAPModels oSAPModels = new SAPModels();
        private List<SAPModels> SAPModelsList = new List<SAPModels>();

        private MstSalesPriceList oModelSaleItemDefault = new MstSalesPriceList();
        private List<MstSalesPriceList> oSalesListItemDefault = new List<MstSalesPriceList>();

        private TrnsDirectMaterial oModelTrnsDirectMaterial = new TrnsDirectMaterial();
        private List<TrnsDirectMaterial> oTrnsDirectMaterial = new List<TrnsDirectMaterial>();

        private TrnsVoc oModelVOC = new TrnsVoc();
        private List<TrnsVoc> oTrnsVOCList = new List<TrnsVoc>();

        private TrnsFohrate oModelTrnsFohrate = new TrnsFohrate();
        private List<TrnsFohrate> oListTrnsFohrate = new List<TrnsFohrate>();

        private HashSet<SAPModels> selectedItems1 = new HashSet<SAPModels>();
        private int selectedRowNumber = -1;

        private List<string> clickedEvents = new();

        private void Cancel() => MudDialog.Cancel();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        private bool FilterFunc1(SAPModels element) => FilterFunc(element, searchString1);

        private bool FilterFuncSalesDeafult(MstSalesPriceList elementSales) => FilterFuncSaleDefault(elementSales, searchString7);

        private bool FilterFuncDirectMaterial(TrnsDirectMaterial elementDirectMaterial) => FilterFuncDirectMaterial(elementDirectMaterial, searchString8);

        private bool FilterFuncVOC(TrnsVoc elementVOC) => FilterFuncVOC(elementVOC, searchString9);

        private bool FilterFuncTrnsFohrate(TrnsFohrate element6) => FilterFuncFohrate(element6, searchString6);

        private bool FilterFuncSQ(TrnsSalesQuotation element) => FilterFuncSQ(element, searchStringSQ);

        #endregion

        #region Functions

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

        private string SelectedRowClassFuncSaleDefault(MstSalesPriceList elementSales, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableSales.SelectedItem != null && _tableSales.SelectedItem.Equals(elementSales))
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

        private string SelectedRowClassFuncFohRate(TrnsFohrate element6, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_table5.SelectedItem != null && _table5.SelectedItem.Equals(element6))
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

        private string SelectedRowClassFuncSQ(TrnsSalesQuotation element, int rowNumber)
        {

            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableSQ.SelectedItem != null && _tableSQ.SelectedItem.Equals(element))
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

        private bool FilterFuncSaleDefault(MstSalesPriceList elementSales, string searchString7)
        {
            if (string.IsNullOrWhiteSpace(searchString7))
                return true;

            if (elementSales.FlgDefult.Equals(searchString7))
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

        private bool FilterFuncVOC(TrnsVoc elementVOC, string searchString9)
        {
            if (string.IsNullOrWhiteSpace(searchString9))
                return true;

            if (elementVOC.DocNum.Equals(searchString9))
                return true;
            return false;
        }

        private bool FilterFuncFohrate(TrnsFohrate element6, string searchString6)
        {
            if (string.IsNullOrWhiteSpace(searchString6))
                return true;

            if (element6.ProductCode.Equals(searchString6))
                return true;
            if (element6.ProductName.Equals(searchString6))
                return true;
            if (element6.DocName.Equals(searchString6))
                return true;
            if (element6.DocNum.Equals(searchString6))
                return true;
            return false;
        }

        private bool FilterFuncSQ(TrnsSalesQuotation element, string searchStringSQ)
        {
            if (string.IsNullOrWhiteSpace(searchStringSQ))
                return true;
            if (element.CustomerCode.Contains(searchStringSQ, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.CustomerName.Equals(searchStringSQ))
                return true;
            return false;
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

        private async Task OpenSapDataDialogFinishGoodsProduct(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "FinishGoodsProduct");
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    mstSalesQuatationDetail.ProductCode = res.ItemCode;
                    mstSalesQuatationDetail.ProductDescription = res.ItemName;
                    mstSalesQuatationDetail.ProductDepartment = res.U_Item_Department;

                    var sellingPrice = oModelSalesPriceList.MstSalesPriceListDetails.Where(x => x.ProductCode == mstSalesQuatationDetail.ProductCode).FirstOrDefault();
                    if (sellingPrice == null)
                        mstSalesQuatationDetail.SellingPrice = 0;
                    else
                        mstSalesQuatationDetail.SellingPrice = DataConversion.ValueRound2(sellingPrice.PerUnitSalesPrice);
                    DialogFor = "SalesQuotation";
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
                if (mstSalesQuatationDetail.ProductCode == null)
                {
                    Snackbar.Add("Select Product.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "DirectMaterialMaster");
                parameters.Add("ProductCode", mstSalesQuatationDetail.ProductCode);
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsDirectMaterial)result.Data;
                    mstSalesQuatationDetail.FkdirectMaterialDocNum = (int)res.DocNum;
                    mstSalesQuatationDetail.FkdirectMaterialId = res.Id;
                    mstSalesQuatationDetail.ImportCost = DataConversion.ValueRound2((decimal)res.TotalImportCost);
                    mstSalesQuatationDetail.LocalCost = DataConversion.ValueRound2((decimal)res.TotalLocalCost);
                    mstSalesQuatationDetail.TotalRmcost = DataConversion.ValueRound2((decimal)res.TotalMaterialCost);
                    DialogFor = "SalesQuotation";
                    oModelTrnsDirectMaterial = res;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogVOH(DialogOptions options)
        {
            try
            {
                if (mstSalesQuatationDetail.ProductCode == null)
                {
                    Snackbar.Add("Select Product.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                if (mstSalesQuatationDetail.FkdirectMaterialDocNum == 0)
                {
                    Snackbar.Add("First Select D.M.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "VOCMaster");
                parameters.Add("ProductCode", mstSalesQuatationDetail.ProductCode);
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsVoc)result.Data;
                    mstSalesQuatationDetail.FkvohdocNum = (int)res.DocNum;
                    mstSalesQuatationDetail.Fkvohid = res.Id;
                    var machine = res.TrnsVocmachineDetails.Where(x => x.Fkid == res.Id).Select(x => new { x.MachineType, x.Total }).FirstOrDefault();
                    mstSalesQuatationDetail.Machine = machine.MachineType;
                    decimal machineTotal = DataConversion.ValueRound2(machine.Total);
                    var Labor = res.TrnsVoclaborDetails.Where(x => x.Fkid == res.Id).Select(x => new { x.LaborDescription, x.Total }).FirstOrDefault();
                    mstSalesQuatationDetail.Labour = Labor.LaborDescription;
                    decimal LaborTotal = DataConversion.ValueRound2((decimal)Labor.Total);
                    var Electricity = res.TrnsVocelectricityDetails.Where(x => x.Fkid == res.Id).Select(x => new { x.MachineType, x.Total }).FirstOrDefault();
                    mstSalesQuatationDetail.Electricity = Electricity.MachineType;
                    decimal ElectricityTotal = DataConversion.ValueRound2((decimal)Electricity.Total);
                    var DyesAndMold = res.TrnsVocdyesAndMoldDetails.Where(x => x.Fkid == res.Id).Select(x => new { x.DyesAndMold, x.Total }).FirstOrDefault();
                    mstSalesQuatationDetail.DiesAndMolds = DyesAndMold.DyesAndMold;
                    decimal DyesAndMoldTotal = DataConversion.ValueRound2((decimal)DyesAndMold.Total);
                    var Tools = res.TrnsVoctoolsDetails.Where(x => x.Fkid == res.Id).Select(x => new { x.ToolsType, x.Total }).FirstOrDefault();
                    mstSalesQuatationDetail.Tools = Tools.ToolsType;
                    decimal ToolsTotal = DataConversion.ValueRound2((decimal)Tools.Total);
                    var Gasoline = res.TrnsVocgasolineDetails.Where(x => x.Fkid == res.Id).Select(x => new { x.GasolineType, x.Total }).FirstOrDefault();
                    mstSalesQuatationDetail.Gasoline = Gasoline.GasolineType;
                    decimal GasolineTotal = DataConversion.ValueRound2((decimal)Gasoline.Total);
                    mstSalesQuatationDetail.TotalVohcost = LaborTotal + ElectricityTotal + DyesAndMoldTotal + ToolsTotal + GasolineTotal;
                    DialogFor = "SalesQuotation";
                    oModelVOC = res;
                }

            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogFohRate(DialogOptions options)
        {
            try
            {
                if (mstSalesQuatationDetail.ProductCode == null)
                {
                    Snackbar.Add("Select Product.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                if (mstSalesQuatationDetail.FkvohdocNum == 0)
                {
                    Snackbar.Add("First Select V.O.H.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "FohRateMaster");
                parameters.Add("ProductCode", mstSalesQuatationDetail.ProductCode);
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsFohrate)result.Data;
                    mstSalesQuatationDetail.FkfohdocNum = res.DocNum;
                    mstSalesQuatationDetail.Fkfohid = res.Id;
                    mstSalesQuatationDetail.TotalDirectCost = DataConversion.ValueRound2(mstSalesQuatationDetail.TotalVohcost) + DataConversion.ValueRound2(mstSalesQuatationDetail.TotalRmcost);
                    mstSalesQuatationDetail.FohratePer = DataConversion.ValueRound2((decimal)res.Fohrate);
                    mstSalesQuatationDetail.Fohamount = DataConversion.ValueRound2(mstSalesQuatationDetail.FohratePer) * DataConversion.ValueRound2(mstSalesQuatationDetail.TotalDirectCost);
                    mstSalesQuatationDetail.TotalFoh = DataConversion.ValueRound2(mstSalesQuatationDetail.Fohamount);
                    mstSalesQuatationDetail.TotalUnitCost = DataConversion.ValueRound2(mstSalesQuatationDetail.TotalRmcost) + DataConversion.ValueRound2(mstSalesQuatationDetail.TotalVohcost) + DataConversion.ValueRound2(mstSalesQuatationDetail.TotalFoh);
                    mstSalesQuatationDetail.Margin = mstSalesQuatationDetail.SellingPrice - DataConversion.ValueRound2(mstSalesQuatationDetail.TotalUnitCost);
                    mstSalesQuatationDetail.MarginPer = (mstSalesQuatationDetail.Margin / mstSalesQuatationDetail.SellingPrice) * 100;
                    DialogFor = "SalesQuotation";
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task<List<SAPModels>> GetCustomer()
        {
            SAPModelsList.Clear();
            SAPModelsList = await _SAPService.GetCustomerFromSAP();
            return SAPModelsList;
        }

        private async Task GetAllSalePriceListMasterDefault()
        {
            try
            {
                oSalesListItemDefault = await _mstSalePriceListMaster.GetAllData();
                if (oSalesListItemDefault?.Count == 0 || oSalesListItemDefault == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task<List<SAPModels>> GetItems()
        {
            SAPModelsList.Clear();
            string Clause = "ItmsGrpCod = '101'";
            SAPModelsList = await _SAPService.GetItemsFromSAP(Clause);
            return SAPModelsList;
        }

        private async Task GetAllDirectMaterial()
        {
            try
            {
                oTrnsDirectMaterial = await _mstDirectMaterial.GetAllData();
                oTrnsDirectMaterial = oTrnsDirectMaterial.Where(x => x.ProductCode == ProductCode).ToList();
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

        private async Task GetAllVOC()
        {
            try
            {
                oTrnsVOCList = await _mstVOC.GetAllData();
                oTrnsVOCList = oTrnsVOCList.Where(x => x.ProductCode == ProductCode).ToList();
                if (oTrnsVOCList?.Count == 0 || oTrnsVOCList == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllFohRateMaster()
        {
            try
            {
                oListTrnsFohrate = await _mstFOHRatesCalc.GetAllData();
                oListTrnsFohrate = oListTrnsFohrate.Where(x => x.ProductCode == ProductCode).ToList();
                if (oListTrnsFohrate?.Count == 0 || oListTrnsFohrate == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllSalesQuotaion()
        {
            try
            {
                oListSalesQuotation = await _SalesQuotation.GetAllData();
                if (oListSalesQuotation?.Count == 0 || oListSalesQuotation == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
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
                if (DialogFor == "SalePriceList")
                {
                    if (oModelSaleItemDefault.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstSalesPriceList>(oModelSaleItemDefault));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "FinishGoodsProduct")
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
                if (DialogFor == "VOCMaster")
                {
                    if (oModelVOC.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsVoc>(oModelVOC));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "FohRateMaster")
                {
                    if (oModelTrnsFohrate.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsFohrate>(oModelTrnsFohrate));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "SalesQuotation")
                {
                    if (mstSalesQuatationDetail.ProductCode != null && mstSalesQuatationDetail.FkdirectMaterialDocNum != 0 && mstSalesQuatationDetail.FkvohdocNum != 0
                        && mstSalesQuatationDetail.FkfohdocNum != 0)
                    {
                        //MudDialog.Close(DialogResult.Ok<TrnsSalesQuotationDetail>(mstSalesQuatationDetail));
                        vmSalesQuotationTransferData ovmData = new vmSalesQuotationTransferData();
                        ovmData.ModelSalesQuotationDetail = mstSalesQuatationDetail;
                        ovmData.ModelDirectMaterial = oModelTrnsDirectMaterial;
                        ovmData.ModelVOH = oModelVOC;
                        MudDialog.Close(DialogResult.Ok<vmSalesQuotationTransferData>(ovmData));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "SaveDataSalesQuotation")
                {
                    if (oModelSQ.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsSalesQuotation>(oModelSQ));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "SaveDataSalesQuotationCopyForm")
                {
                    if (oModelSQ.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsSalesQuotation>(oModelSQ));
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

        private async Task RedirectDirectMaterial(int DMDocNum, DialogOptions options)
        {
            try
            {
                options.FullScreen = true;
                var parameters = new DialogParameters();
                parameters.Add("DocNum", DMDocNum);
                var dialog = Dialog.Show<DirectMaterial>("",parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    oModelTrnsDirectMaterial = (TrnsDirectMaterial)result.Data;
                    MudDialog.Close(DialogResult.Ok<TrnsDirectMaterial>(oModelTrnsDirectMaterial));
                }

            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task RedirectVOC(int VOCDocNum, DialogOptions options)
        {
            try
            {
                options.FullScreen = true;
                var parameters = new DialogParameters();
                parameters.Add("DocNum", VOCDocNum);
                var dialog = Dialog.Show<VariableOverheadCost>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    oModelVOC = (TrnsVoc)result.Data;
                    MudDialog.Close(DialogResult.Ok<TrnsVoc>(oModelVOC));
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
                if (DialogFor == "Customer")
                {
                    await GetCustomer();
                }
                if (DialogFor == "SalePriceList")
                {
                    await GetAllSalePriceListMasterDefault();
                }
                if (DialogFor == "FinishGoodsProduct")
                {
                    await GetItems();
                }
                if (DialogFor == "DirectMaterialMaster")
                {
                    await GetAllDirectMaterial();
                }
                if (DialogFor == "VOCMaster")
                {
                    await GetAllVOC();
                }
                if (DialogFor == "FohRateMaster")
                {
                    await GetAllFohRateMaster();
                }
                if (DialogFor == "SaveDataSalesQuotation")
                {
                    await GetAllSalesQuotaion();
                }
                if (DialogFor == "SaveDataSalesQuotationCopyForm")
                {
                    await GetAllSalesQuotaion();
                }
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
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

        private void RowClickEventSaleDefault(TableRowClickEventArgs<MstSalesPriceList> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventDirectMaterial(TableRowClickEventArgs<TrnsDirectMaterial> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventVOC(TableRowClickEventArgs<TrnsVoc> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventFohRate(TableRowClickEventArgs<TrnsFohrate> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventSQ(TableRowClickEventArgs<TrnsSalesQuotation> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        #endregion
    }
}

public class vmSalesQuotationTransferData
{
    public TrnsSalesQuotationDetail ModelSalesQuotationDetail { get; set; }
    public TrnsDirectMaterial ModelDirectMaterial { get; set; }
    public TrnsVoc ModelVOH { get; set; }
}