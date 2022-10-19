using CA.API.Models;
using CA.UI.Dialog;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using CA.UI.Interfaces.MasterData;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace CA.UI.Pages.Cost_Allocations
{
    public partial class SalesQuotation
    {
        #region InjectService

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IVOC _mstVOC { get; set; }

        [Inject]
        public ISalesQuotation _mstSalesQuoatation { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }

        [Inject]
        public IDirectMaterial _directMaterialService { get; set; }


        #endregion InjectService

        #region Parameter

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();

        [Parameter]
        public string DialogFor { get; set; }

        #endregion

        #region Variables

        private bool Loading = false;

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();

        private TrnsSalesQuotation oModel = new TrnsSalesQuotation();
        private InsertSalesQuotation InsertoModel = new InsertSalesQuotation();
        private List<TrnsSalesQuotationDetail> oSalesQuatationDetailList = new List<TrnsSalesQuotationDetail>();

        private TrnsDirectMaterial oModelDM = new TrnsDirectMaterial();
        private List<TrnsDirectMaterial> oModelDMList = new List<TrnsDirectMaterial>();

        private TrnsVoc oModelVOC = new TrnsVoc();
        private List<TrnsVoc> oModelVOCList = new List<TrnsVoc>();

        private MstSalesPriceList oModelSalesPriceList = new MstSalesPriceList();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        #endregion Variables

        #region Functions

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

        private async Task OpenSapDataDialogCustomer(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "Customer");
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    oModel.CustomerCode = res.CardCode;
                    oModel.CustomerName = res.CardName;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSalesPriceListDialog(DialogOptions options)
        {
            try
            {
                if (oModel.CustomerCode == null)
                {
                    Snackbar.Add("Select Customer.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "SalePriceList");
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstSalesPriceList)result.Data;
                    oModel.SalesPriceList = Convert.ToString(res.Id);
                    oModelSalesPriceList = res;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenAddRowDialog(DialogOptions options)
        {
            try
            {
                if (oModel.SalesPriceList == null)
                {
                    Snackbar.Add("Select Sales Price List.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "SalesQuotation");
                parameters.Add("oModelSalesPriceList", oModelSalesPriceList);
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (vmSalesQuotationTransferData)result.Data;
                    //var res = (TrnsSalesQuotationDetail)result.Data;
                    //var update = oSalesQuatationDetailList.Where(x => x.ProductCode == res.ModelSalesQuotationDetail.ProductCode).FirstOrDefault();
                    //if (update != null)
                    //{
                    //    Snackbar.Add("Record already exits.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    //}
                    //else
                    //{
                    if (res.ModelDirectMaterial.ForAnalysis == "Yes")
                        res.ModelSalesQuotationDetail.Rmanalysis = "Yes";
                    else
                        res.ModelSalesQuotationDetail.Rmanalysis = "No";
                    if (res.ModelVOH.ForAnalysis == "Yes")
                        res.ModelSalesQuotationDetail.Vohanalysis = "Yes";
                    else
                        res.ModelSalesQuotationDetail.Vohanalysis = "No";
                    oModelDMList.Add(res.ModelDirectMaterial);
                    oModelVOCList.Add(res.ModelVOH);
                    //oModelVOC = res.ModelVOH;
                    oSalesQuatationDetailList.Add(res.ModelSalesQuotationDetail);
                    //}
                }
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
                parameters.Add("DialogFor", "SaveDataSalesQuotationCopyForm");
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled) if (!result.Cancelled)
                    {
                        var res = (TrnsSalesQuotation)result.Data;
                        var mst = res.TrnsSalesQuotationDetails;
                        List<TrnsSalesQuotationDetail> list = new List<TrnsSalesQuotationDetail>();
                        list = mst.ToList();
                        foreach (var item in list)
                        {
                            TrnsSalesQuotationDetail detail = new TrnsSalesQuotationDetail();
                            detail.Id = (int)item.Id;
                            detail.Fkid = item.Fkid;
                            detail.ProductCode = item.ProductCode;
                            detail.ProductDescription = item.ProductDescription;
                            detail.ProductDepartment = item.ProductDepartment;
                            detail.FkdirectMaterialDocNum = Convert.ToInt32(item.FkdirectMaterialDocNum);
                            detail.FkdirectMaterialId = item.FkdirectMaterialId;
                            detail.Fkvohid = item.Fkvohid;
                            detail.FkvohdocNum = Convert.ToInt32(item.FkvohdocNum);
                            detail.Fkfohid = item.Fkfohid;
                            detail.FkfohdocNum = Convert.ToInt32(item.FkfohdocNum);
                            detail.Rmanalysis = item.Rmanalysis;
                            detail.ImportCost = item.ImportCost;
                            detail.LocalCost = item.LocalCost;
                            detail.TotalRmcost = item.TotalRmcost;
                            detail.Vohanalysis = item.Vohanalysis.ToString();
                            detail.Machine = item.Machine.ToString();
                            detail.Labour = item.Labour.ToString();
                            detail.Electricity = item.Electricity.ToString();
                            detail.DiesAndMolds = item.DiesAndMolds.ToString();
                            detail.Tools = item.Tools.ToString();
                            detail.Gasoline = item.Gasoline.ToString();
                            detail.Packing = item.Packing.ToString();
                            detail.Transportation = item.Transportation.ToString();
                            detail.Others = item.Others.ToString();
                            detail.TotalVohcost = item.TotalVohcost;
                            detail.FohratePer = item.FohratePer;
                            detail.Fohamount = item.Fohamount;
                            detail.OtherFoh = item.OtherFoh;
                            detail.TotalFoh = item.TotalFoh;
                            detail.TotalUnitCost = item.TotalUnitCost;
                            detail.SellingPrice = item.SellingPrice;
                            detail.Margin = item.Margin;
                            detail.MarginPer = item.MarginPer;
                            oSalesQuatationDetailList.Add(detail);
                        }
                        oSalesQuatationDetailList.ToList();
                    }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "SaveDataSalesQuotation");
                var dialog = Dialog.Show<DlgSalesQuotation>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsSalesQuotation)result.Data;
                    oModel = res;
                    oModelCostType.Id = (int)oModel.FkcostTypeId;
                    oModelCostType.Description = oModel.CostTypeDesc;
                    oSalesQuatationDetailList = oModel.TrnsSalesQuotationDetails.ToList();

                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecord(string ProductCode)
        {
            try
            {
                var remove = oSalesQuatationDetailList.Where(x => x.ProductCode == ProductCode).FirstOrDefault();
                if (remove != null)
                {
                    oSalesQuatationDetailList.Remove(remove);
                    oModelDM = null;
                    oModelVOC = null;
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
                        Loading = true;
                        var res1 = new ApiResponseModel();
                        var resDM = new ApiResponseModel();
                        var resVOC = new ApiResponseModel();
                        await Task.Delay(1);
                        oModel.FkcostTypeId = oModelCostType.Id;
                        oModel.CostTypeDesc = oModelCostType.Description;
                        oModel.TrnsSalesQuotationDetails = oSalesQuatationDetailList.ToList();
                        if (oModel.CustomerCode == null || oModelCostType.Id == 0)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oSalesQuatationDetailList.Count == 0)
                        {
                            Snackbar.Add("Please select row level detail", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oModel != null && oModel.TrnsSalesQuotationDetails.Count > 0)
                        {
                            if (oModel.Id == 0)
                            {
                                //if (oModelDM.ForAnalysis == "Yes")
                                //{
                                //    resDM = await _directMaterialService.Insert(oModelDM);
                                //}
                                //if (oModelVOC.ForAnalysis == "Yes")
                                //{
                                //    resVOC = await _mstVOC.Insert(oModelVOC);
                                //}
                                //res1 = await _mstSalesQuoatation.Insert(oModel);
                                InsertoModel.oTrnsSalesQuotation = oModel;
                                InsertoModel.oTrnsDirectMaterial = oModelDMList;
                                InsertoModel.oTrnsVoc = oModelVOCList;
                                res1 = await _mstSalesQuoatation.Insert(InsertoModel);
                            }
                            else
                            {
                                InsertoModel.oTrnsSalesQuotation = oModel;
                                InsertoModel.oTrnsDirectMaterial = oModelDMList;
                                InsertoModel.oTrnsVoc = oModelVOCList;
                                res1 = await _mstSalesQuoatation.Update(InsertoModel);
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
                            Navigation.NavigateTo("/SalesQuotation", forceLoad: true);
                        }
                        else
                        {
                            Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
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
                Navigation.NavigateTo("/SalesQuotation", forceLoad: true);
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