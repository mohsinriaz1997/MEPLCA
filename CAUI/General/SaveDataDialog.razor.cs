using CA.API.Models;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using CA.UI.Interfaces.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Xml.Linq;
using static MudBlazor.CategoryTypes;

namespace CA.UI.General
{
    public partial class SaveDataDialog
    {
        #region InjectService

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Parameter]
        public string ForAnalysis { get; set; }

        [Inject]
        public IResourceMasterData _mstRescourceMaster { get; set; }

        [Inject]
        public IItemPriceList _mstItemPriceListMaster { get; set; }

        [Inject]
        public ISalePriceList _mstSalePriceListMaster { get; set; }

        [Inject]
        public IDirectMaterial _mstDirectMaterial { get; set; }

        [Inject]
        public IMonthlyVOHCalculation _mstVOHCalculation { get; set; }
        [Inject]
        public IVOC _mstVOCCalculation { get; set; }
        [Inject]
        public IStage _mstStage { get; set; }
        [Inject]
        public IApprovalSetup _mstApproval { get; set; }

        [Inject]
        public ILaborRate _mstLaborRateListMaster { get; set; }

        [Inject]
        public ICostDriversType _mstCostDriversType { get; set; }

        [Inject]
        public IFOHRatesCalc _mstFOHRatesCalc { get; set; }
        [Inject]
        public IMonthlyFohDriverRatesCalculation _trnsFohDriverRatesCalculation { get; set; }

        #endregion InjectService

        #region Variables

        private bool Loading = false;

        private string searchString1 = "";
        private string searchString3 = "";
        private string searchString4 = "";
        private string searchString5 = "";
        private string searchString6 = "";
        private string searchString7 = "";
        private string searchString8 = "";
        private string searchString9 = "";
        private string searchString10 = "";
        private string searchString11 = "";
        private string searchString12 = "";
        private string searchString13 = "";

        private int selectedRowNumber = -1;

        private MudTable<MstResource> _table;
        private MudTable<MstItemPriceList> _table1;
        private MudTable<MstItemPriceListDetail> _tableItemPriceListDetail;
        private MudTable<MstSalesPriceList> _table2;
        private MudTable<MstSalesPriceList> _tableSales;
        private MudTable<TrnsDirectMaterial> _tableDirect;
        private MudTable<TrnsVoh> _tableVOH;
        private MudTable<TrnsVoc> _tableVOC;
        private MudTable<MstStage> _tableStage;
        private MudTable<MstApprovalSetup> _tableApproval;
        private MudTable<MstLabourRate> _table3;
        private MudTable<MstCostDriversType> _table4;
        private MudTable<TrnsFohrate> _table5;
        private MudTable<TrnsFohdriverRate> _tableFohDriver;

        private List<string> clickedEvents = new();

        private bool FilterFunc1(MstResource element) => FilterFunc(element, searchString1);

        private bool FilterFunc2(MstItemPriceList element2) => FilterFuncItem(element2, searchString1);
        private bool FilterFuncItemPriceListDetail(MstItemPriceListDetail elementItemPriceListDetail) => FilterFuncItemDetail(elementItemPriceListDetail, searchString1);

        private bool FilterFunc3(MstSalesPriceList element3) => FilterFuncSale(element3, searchString3);

        private bool FilterFuncSalesDeafult(MstSalesPriceList elementSales) => FilterFuncSaleDefault(elementSales, searchString7);

        private bool FilterFuncDirectMaterial(TrnsDirectMaterial elementDirectMaterial) => FilterFuncDirectMaterial(elementDirectMaterial, searchString8);

        private bool FilterFuncVOH(TrnsVoh elementVOH) => FilterFuncVOH(elementVOH, searchString9);
        private bool FilterFuncVOC(TrnsVoc elementVOC) => FilterFuncVOC(elementVOC, searchString10);
        private bool FilterFuncStage(MstStage elementStage) => FilterFuncStage(elementStage, searchString10);

        private bool FilterFuncStage(MstStage elementStage, string searchString10)
        {
            if (string.IsNullOrWhiteSpace(searchString11))
                return true;
            if (elementStage.StageName.Contains(searchString11, StringComparison.OrdinalIgnoreCase))
                return true;
            if (elementStage.NoOfApproval.Equals(searchString11))
                return true;
            return false;
        }
        private bool FilterFuncApproval(MstApprovalSetup elementApproval) => FilterFuncApproval(elementApproval, searchString12);

        private bool FilterFuncApproval(MstApprovalSetup elementApproval, string searchString10)
        {
            if (string.IsNullOrWhiteSpace(searchString12))
                return true;
            //if (elementApproval.id.Contains(searchString12, StringComparison.OrdinalIgnoreCase))
            //    return true;
            if (elementApproval.Name.Equals(searchString12))
                return true;
            return false;
        }

        private bool FilterFuncLabor(MstLabourRate element4) => FilterFuncLabor(element4, searchString4);

        private bool FilterFuncCostDriver(MstCostDriversType element5) => FilterFuncCostDriver(element5, searchString4);

        private bool FilterFuncTrnsFohrate(TrnsFohrate element6) => FilterFuncFohrate(element6, searchString4);
        private bool FilterFuncTrnsFohDriver(TrnsFohdriverRate element7) => FilterFuncFohrate(element7, searchString13);

        private bool FilterFuncFohrate(TrnsFohdriverRate element7, string searchString13)
        {
            if (string.IsNullOrWhiteSpace(searchString13))
                return true;

            if (element7.DocDate.Equals(searchString13))
                return true;
            if (element7.DocNum.Equals(searchString13))
                return true;
            return false;
        }

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string DialogFor { get; set; }

        private MstResource oModelResource = new MstResource();
        private List<MstResource> oListResource = new List<MstResource>();

        private MstResource oModelResourceForVOC = new MstResource();
        private List<MstResource> oListResourceForVOC = new List<MstResource>();

        private MstCostDriversType oModelCostDriverType = new MstCostDriversType();
        private List<MstCostDriversType> oListCostDriversType = new List<MstCostDriversType>();

        private TrnsFohrate oModelTrnsFohrate = new TrnsFohrate();
        private List<TrnsFohrate> oListTrnsFohrate = new List<TrnsFohrate>();
        
        private TrnsFohdriverRate oModelTrnsFohdriverRate = new TrnsFohdriverRate();
        private List<TrnsFohdriverRate> oListTrnsFohdriverRate = new List<TrnsFohdriverRate>();

        private MstSalesPriceList oModelSalesPriceList = new MstSalesPriceList();
        private List<MstSalesPriceList> oListSalesPriceList = new List<MstSalesPriceList>();

        private MstItemPriceList oModelItem = new MstItemPriceList();
        private List<MstItemPriceList> oListItem = new List<MstItemPriceList>();
        
        private MstItemPriceListDetail oModelItemDetail = new MstItemPriceListDetail();
        private List<MstItemPriceListDetail> oListItemDetail = new List<MstItemPriceListDetail>();

        private MstSalesPriceList oModelSaleItem = new MstSalesPriceList();
        private List<MstSalesPriceList> oSalesListItem = new List<MstSalesPriceList>();

        private MstSalesPriceList oModelSaleItemDefault = new MstSalesPriceList();
        private List<MstSalesPriceList> oSalesListItemDefault = new List<MstSalesPriceList>();

        private TrnsDirectMaterial oModelTrnsDirectMaterial = new TrnsDirectMaterial();
        private List<TrnsDirectMaterial> oTrnsDirectMaterial = new List<TrnsDirectMaterial>();

        private TrnsVoh oModelTrnsTrnsVoh = new TrnsVoh();
        private List<TrnsVoh> oTrnsVoh = new List<TrnsVoh>();

        private TrnsVoc oModelTrnsVoc = new TrnsVoc();
        private List<TrnsVoc> oTrnsVoc = new List<TrnsVoc>();
        
        private MstStage oModelMstStage = new MstStage();
        private List<MstStage> oMstStage = new List<MstStage>();
        
        private MstApprovalSetup oModelMstApprovalSetup = new MstApprovalSetup();
        private List<MstApprovalSetup> oMstApprovalSetup = new List<MstApprovalSetup>();

        //TrnsDirectMaterialDetail oModelTrnsDirectMaterialDetail = new TrnsDirectMaterialDetail();
        //List<TrnsDirectMaterialDetail> oTrnsDirectMaterialDetail = new List<TrnsDirectMaterialDetail>();

        private MstLabourRate oModelLaborRate = new MstLabourRate();
        private List<MstLabourRate> oLaborRate = new List<MstLabourRate>();

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

                Logs.GenerateLogs(ex.Message);
            }
        }

        private bool FilterFunc(MstResource element, string searchString1)
        {
            if (string.IsNullOrWhiteSpace(searchString1))
                return true;
            if (element.ResrListName.Contains(searchString1, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.FlgActive.Equals(searchString1))
                return true;
            return false;
        }

        private bool FilterFuncItem(MstItemPriceList element2, string searchString1)
        {
            if (string.IsNullOrWhiteSpace(searchString1))
                return true;

            if (element2.FlgDefaultPl.Equals(searchString1))
                return true;
            return false;
        }
        private bool FilterFuncItemDetail(MstItemPriceListDetail elementItemPriceListDetail, string searchString1)
        {
            if (string.IsNullOrWhiteSpace(searchString1))
                return true;

            if (elementItemPriceListDetail.AdditionalDutyValue.Equals(searchString1))
                return true;
            return false;
        }

        private bool FilterFuncSale(MstSalesPriceList element3, string searchString3)
        {
            if (string.IsNullOrWhiteSpace(searchString3))
                return true;

            if (element3.FlgDefult.Equals(searchString3))
                return true;
            return false;
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

        private bool FilterFuncVOH(TrnsVoh elementVOH, string searchString9)
        {
            if (string.IsNullOrWhiteSpace(searchString9))
                return true;

            if (elementVOH.DocNum.Equals(searchString9))
                return true;
            return false;
        }
        private bool FilterFuncVOC(TrnsVoc elementVOC, string searchString10)
        {
            if (string.IsNullOrWhiteSpace(searchString9))
                return true;

            if (elementVOC.DocNum.Equals(searchString9))
                return true;
            return false;
        }

        private bool FilterFuncLabor(MstLabourRate element4, string searchString4)
        {
            if (string.IsNullOrWhiteSpace(searchString4))
                return true;

            if (element4.DocNum.Equals(searchString4))
                return true;
            return false;
        }

        private bool FilterFuncCostDriver(MstCostDriversType element5, string searchString5)
        {
            if (string.IsNullOrWhiteSpace(searchString5))
                return true;

            if (element5.Code.Equals(searchString5))
                return true;
            if (element5.Description.Equals(searchString5))
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

        private async Task GetAllRescourceMaster()
        {
            try
            {
                oListResource = await _mstRescourceMaster.GetAllData();
                if (oListResource?.Count == 0 || oListResource == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
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

        private async Task GetAllSalePriceListMaster()
        {
            try
            {
                oSalesListItem = await _mstSalePriceListMaster.GetAllData();
                if (oSalesListItem?.Count == 0 || oSalesListItem == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllSalePriceListMasterDefault()
        {
            try
            {
                oSalesListItemDefault = await _mstSalePriceListMaster.GetAllDataDefault();
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
        private async Task GetAllDirectMaterialFA(string ForAnalysis)
        {
            try
            {
                oTrnsDirectMaterial = await _mstDirectMaterial.GetAllData(ForAnalysis);
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

        private async Task GetAllVOH()
        {
            try
            {
                oTrnsVoh = await _mstVOHCalculation.GetAllData();
                if (oTrnsVoh?.Count == 0 || oTrnsVoh == null)
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
                oTrnsVoc = await _mstVOCCalculation.GetAllData();
                if (oTrnsVoc?.Count == 0 || oTrnsVoc == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task GetAllStage()
        {
            try
            {
                oMstStage = await _mstStage.GetAllData();
                if (oMstStage?.Count == 0 || oMstStage == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task GetAllApproval()
        {
            try
            {
                oMstApprovalSetup = await _mstApproval.GetAllData();
                if (oMstApprovalSetup?.Count == 0 || oMstApprovalSetup == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllLaborRateListMaster()
        {
            try
            {
                oLaborRate = await _mstLaborRateListMaster.GetAllData();
                if (oLaborRate?.Count == 0 || oLaborRate == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllCostDriverMaster()
        {
            try
            {
                oListCostDriversType = await _mstCostDriversType.GetAllData();
                if (oListCostDriversType?.Count == 0 || oListCostDriversType == null)
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
        private async Task GetAllFohDriverMaster()
        {
            try
            {
                oListTrnsFohdriverRate = await _trnsFohDriverRatesCalculation.GetAllData();
                if (oListTrnsFohdriverRate?.Count == 0 || oListTrnsFohdriverRate == null)
                {
                    Snackbar.Add("No Record Found.", Severity.Info, (options) => { options.Icon = Icons.Sharp.Error; });
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private void Submit()
        {
            try
            {
                if (DialogFor == "ResourceMaster")
                {
                    if (oModelResource.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstResource>(oModelResource));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "ResourceMasterCopyTo")
                {
                    if (oModelResource.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstResource>(oModelResource));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "ItemPriceListMasterCopyTo")
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
                if (DialogFor == "CostDriverListMaster")
                {
                    if (oModelCostDriverType.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstCostDriversType>(oModelCostDriverType));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "LaborRateListMaster")
                {
                    if (oModelLaborRate.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstLabourRate>(oModelLaborRate));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "ItemPriceListMaster")
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
                if (DialogFor == "VOCResourceMasterData")
                {
                    if (oModelResourceForVOC.Id > 0)
                    {

                        MudDialog.Close(DialogResult.Ok<MstResource>(oModelResourceForVOC));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "ItemPriceListDetailMaster")
                {
                    if (oModelItemDetail.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstItemPriceListDetail>(oModelItemDetail));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "SalePriceListMaster")
                {
                    if (oModelSaleItem.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstSalesPriceList>(oModelSaleItem));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "SalePriceListMasterDefault")
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
                if (DialogFor == "VOHMaster")
                {
                    if (oModelTrnsTrnsVoh.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsVoh>(oModelTrnsTrnsVoh));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "VOCMaster")
                {
                    if (oModelTrnsVoc.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsVoc>(oModelTrnsVoc));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "Stage")
                {
                    if (oModelMstStage.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstStage>(oModelMstStage));
                    }
                    else
                    {
                        Snackbar.Add("Select row first", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                }
                if (DialogFor == "ApprovalSetup")
                {
                    if (oModelMstApprovalSetup.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<MstApprovalSetup>(oModelMstApprovalSetup));
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
                if (DialogFor == "FohDriverMaster")
                {
                    if (oModelTrnsFohdriverRate.Id > 0)
                    {
                        MudDialog.Close(DialogResult.Ok<TrnsFohdriverRate>(oModelTrnsFohdriverRate));
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

        private string SelectedRowClassFunc(MstResource element, int rowNumber)
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
        private string SelectedRowClassFuncItemDetail(MstItemPriceListDetail elementItemPriceListDetail, int rowNumber)
        {

            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableItemPriceListDetail.SelectedItem != null && _tableItemPriceListDetail.SelectedItem.Equals(elementItemPriceListDetail))
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

        private string SelectedRowClassFuncSale(MstSalesPriceList element3, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_table2.SelectedItem != null && _table2.SelectedItem.Equals(element3))
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

        private string SelectedRowClassFunVOH(TrnsVoh elementVOH, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableVOH.SelectedItem != null && _tableVOH.SelectedItem.Equals(elementVOH))
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
        private string SelectedRowClassFunStage(MstStage elementStage, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableStage.SelectedItem != null && _tableStage.SelectedItem.Equals(elementStage))
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
        private string SelectedRowClassFunApproval(MstApprovalSetup elementApproval, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableApproval.SelectedItem != null && _tableApproval.SelectedItem.Equals(elementApproval))
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

        private string SelectedRowClassFuncLabor(MstLabourRate element4, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_table3.SelectedItem != null && _table3.SelectedItem.Equals(element4))
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

        private string SelectedRowClassFuncCostDriver(MstCostDriversType element5, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_table4.SelectedItem != null && _table4.SelectedItem.Equals(element5))
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
        private string SelectedRowClassFuncFohRate(TrnsFohdriverRate element7, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                clickedEvents.Add("Selected Row: None");
                return string.Empty;
            }
            else if (_tableFohDriver.SelectedItem != null && _tableFohDriver.SelectedItem.Equals(element7))
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
                if (DialogFor == "ResourceMaster")
                {
                    await GetAllRescourceMaster();
                }
                if (DialogFor == "ResourceMasterCopyTo")
                {
                    await GetAllRescourceMaster();
                }
                if (DialogFor == "ItemPriceListMasterCopyTo")
                {
                    await GetAllItemPriceListMaster();
                }
                if (DialogFor == "ItemPriceListMaster")
                {
                    await GetAllItemPriceListMaster();
                }
                if (DialogFor == "DirectMaterialItemPriceList")
                {
                    await GetAllItemPriceListMaster();
                }
                if (DialogFor == "VOCResourceMasterData")
                {
                    await GetAllRescourceMaster();
                }
                if (DialogFor == "SalePriceListMaster")
                {
                    await GetAllSalePriceListMaster();
                }
                if (DialogFor == "SalePriceListMasterDefault")
                {
                    await GetAllSalePriceListMasterDefault();
                }
                if (DialogFor == "DirectMaterialMaster")
                {
                    await GetAllDirectMaterial();
                }
                //if (DialogFor == "DirectMaterialMaster")
                //{
                //    await GetAllDirectMaterialFA(ForAnalysis);
                //}
                if (DialogFor == "VOHMaster")
                {
                    await GetAllVOH();
                }
                if (DialogFor == "VOCMaster")
                {
                    await GetAllVOC();
                }
                if (DialogFor == "Stage")
                {
                    await GetAllStage();
                }
                if (DialogFor == "ApprovalSetup")
                {
                    await GetAllApproval();
                }
                if (DialogFor == "LaborRateListMaster")
                {
                    await GetAllLaborRateListMaster();
                }
                if (DialogFor == "CostDriverListMaster")
                {
                    await GetAllCostDriverMaster();
                }
                if (DialogFor == "FohRateMaster")
                {
                    await GetAllFohRateMaster();
                }
                if (DialogFor == "FohDriverMaster")
                {
                    await GetAllFohDriverMaster();
                }
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }

        private void RowClickEvent(TableRowClickEventArgs<MstResource> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventItem(TableRowClickEventArgs<MstItemPriceList> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }
        private void RowClickEventItemDetail(TableRowClickEventArgs<MstItemPriceListDetail> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }


        private void RowClickEventSale(TableRowClickEventArgs<MstSalesPriceList> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventSaleDefault(TableRowClickEventArgs<MstSalesPriceList> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventDirectMaterial(TableRowClickEventArgs<TrnsDirectMaterial> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventVOH(TableRowClickEventArgs<TrnsVoh> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }
        private void RowClickEventVOC(TableRowClickEventArgs<TrnsVoc> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }
        private void RowClickEventStage(TableRowClickEventArgs<MstStage> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }
        private void RowClickEventApproval(TableRowClickEventArgs<MstApprovalSetup> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventLabor(TableRowClickEventArgs<MstLabourRate> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventCostDriver(TableRowClickEventArgs<MstCostDriversType> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        private void RowClickEventFohRate(TableRowClickEventArgs<TrnsFohrate> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }
        private void RowClickEventFohDriver(TableRowClickEventArgs<TrnsFohdriverRate> tableRowClickEventArgs)
        {
            clickedEvents.Add("Row has been clicked");
        }

        #endregion Events
    }
}