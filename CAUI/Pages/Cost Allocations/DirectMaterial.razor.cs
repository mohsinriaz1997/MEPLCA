using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.Dialog;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using CA.UI.Interfaces.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.Cost_Allocations
{
    public partial class DirectMaterial
    {
        #region InjectService

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDirectMaterial _directMaterialService { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }

        [Inject]
        public IDirectMaterial _mstDirectMaterial { get; set; }
        [Inject]
        public IUserProfile _mstUserProfile { get; set; }
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }

        #endregion InjectService

        #region Parameter

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();

        [Parameter]
        public int DocNum { get; set; }

        [Parameter]
        public string DialogFor { get; set; }

        #endregion

        #region Variables

        private bool Loading = false;

        private TrnsDirectMaterial oModel = new TrnsDirectMaterial();
        private List<TrnsDirectMaterialDetail> oDetailList = new List<TrnsDirectMaterialDetail>();

        private MstItemPriceList oItemPriceList = new MstItemPriceList();

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };


        private UserDataAccess oModelMstUserProfile = new UserDataAccess();
        private List<UserDataAccess> oMstUserProfileList = new List<UserDataAccess>();
        private HashSet<TrnsDirectMaterialDetail> selectedItems1 = new HashSet<TrnsDirectMaterialDetail>();

        private string searchString = "";

        private bool ForAnalysis = false;
        private string LoginUserCode = "";

        #endregion Variables

        #region Functions

        private void OnDateChange()
        {
            try
            {
                oModel.CurrencyEr = null;
                oModel.ExchangeRate = null;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllCostType()
        {
            try
            {
                oMstUserProfileList = await _mstUserProfile.GetAllFormAndCostTypesVariableOverHeadCost(LoginUserCode);
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


        private async Task OpenSAPDataDialogForBOMProduct(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                //parameters.Add("DialogFor", "RawMaterialItemPriceList");
                parameters.Add("DialogFor", "DirectMaterialProduct");
                var dialog = Dialog.Show<DlgDirectMaterial>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    oModel.ProductCode = res.Code;
                    oModel.ProductDescription = res.Name;
                    oModel.ProductDept = res.U_Dept;
                    DialogFor = res.DialogFor;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSAPDataDialogForExhangeRate(DialogOptions options, DateTime? docDate)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("docDatePara", docDate);
                parameters.Add("DialogFor", "ExchangeRate");
                var dialog = Dialog.Show<DlgDirectMaterial>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (SAPModels)result.Data;
                    oModel.ExchangeRate = Convert.ToDecimal(res.Rate);
                    oModel.CurrencyEr = res.Currency;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogForItemPriceList(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "DirectMaterialItemPriceList");
                var dialog = Dialog.Show<DlgDirectMaterial>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    if (result.Data != null)
                    {
                        oItemPriceList = (MstItemPriceList)result.Data;
                        oModel.FkitemPriceListDocNum = oItemPriceList.DocNum;
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSAPDataDialogForItemLevel(DialogOptions options)
        {
            try
            {
                if (oModel.ProductCode == null)
                {
                    Snackbar.Add("First Select Product Code.", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    return;
                }
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "DirectMaterialItem");
                parameters.Add("ProductCode", oModel.ProductCode);
                var dialog = Dialog.Show<DlgDirectMaterial>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    oModel.TrnsDirectMaterialDetails = (ICollection<TrnsDirectMaterialDetail>)result.Data;
                    foreach (var Line in oModel.TrnsDirectMaterialDetails)
                    {
                        var itemDetail = (from a in oItemPriceList.MstItemPriceListDetails
                                          where a.ItemCode == Line.ItemCode
                                          select a).FirstOrDefault();
                        if (itemDetail != null)
                        {
                            Line.Currency = itemDetail.Currency;
                            Line.UnitPriceFc = itemDetail.UnitPricePkr;
                            Line.Lcfactor = itemDetail.LandingCostValue;
                            Line.UnitPricePkr = itemDetail.UnitPricePkr;
                        }
                        else
                        {
                            Line.Currency = "-";
                            Line.UnitPriceFc = 0;
                            Line.Lcfactor = 0;
                            Line.UnitPricePkr = 0;
                        }
                        Line.ChangePricePkr = Line.UnitPricePkr;
                        Line.AmountPkr = Convert.ToDecimal(Line.ConsQty * Line.UnitPricePkr);
                        if (Line.UnitPriceFc > 0)
                        {
                            Line.ChangePricePkr = ((oModel.ExchangeRate * Line.UnitPriceFc) + Line.Lcfactor);
                        }
                    }
                    oDetailList = oModel.TrnsDirectMaterialDetails.ToList();
                    oModel.TotalMaterialCost = oModel.TrnsDirectMaterialDetails.Sum(x => x.UnitPricePkr);
                    oModel.TotalImportCost = oModel.TrnsDirectMaterialDetails.Sum(x => x.UnitPriceFc);
                    oModel.TotalLocalCost = Convert.ToDecimal(oModel.TotalMaterialCost) - Convert.ToDecimal(oModel.TotalImportCost);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task OpenSaveDataDialogForDirectMaterial(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "DirectMaterialMaster");
                var dialog = Dialog.Show<DlgDirectMaterial>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (TrnsDirectMaterial)result.Data;
                    oModel = res;
                    oModelCostType.Id = (int)oModel.FkcostTypeId;
                    oModelCostType.Description = oModel.CostTypeDesc;
                    if (oModel.ForAnalysis == "Yes")
                        ForAnalysis = true;
                    else
                        ForAnalysis = false;
                    oDetailList = oModel.TrnsDirectMaterialDetails.ToList();
                    oDetailList = oDetailList.ToList();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void RemoveRecord(string ItemCode, string UOM)
        {
            try
            {
                var remove = oDetailList.Where(x => x.ItemCode == ItemCode && x.Uom.ToLower() == UOM.ToLower()).FirstOrDefault();
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
                int i = oDetailList.Count;
                if (!string.IsNullOrWhiteSpace(Convert.ToString(oModel.Id)))
                {
                    try
                    {
                        Loading = true;
                        var res1 = new ApiResponseModel();
                        await Task.Delay(1);
                        if (oModel.ProductCode == null || oModel.DocName == null || oModelCostType.Id == 0 || oModel.ExchangeRate == null)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oDetailList.Count == 0)
                        {
                            Snackbar.Add("Please select row level detail", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        oModel.FkcostTypeId = oModelCostType.Id;
                        oModel.CostTypeDesc = oModelCostType.Description;
                        oModel.TrnsDirectMaterialDetails = oDetailList.ToList();
                        if (ForAnalysis)
                            oModel.ForAnalysis = "Yes";
                        else
                            oModel.ForAnalysis = "No";
                        if (oModel != null)
                        {
                            if (oModel.Id == 0)
                            {
                                res1 = await _directMaterialService.Insert(oModel);
                            }
                            else
                            {
                                res1 = await _directMaterialService.Update(oModel);
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
                            Navigation.NavigateTo("/DirectMaterial", forceLoad: true);
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
                Navigation.NavigateTo("/DirectMaterial", forceLoad: true);
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }

        private bool FilterFunc(TrnsDirectMaterialDetail element)
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

        private async Task GetAllDirectMaterialWithDocNum(int DocNum)
        {
            try
            {
                var result = await _directMaterialService.GetAllDataItem(DocNum);
                foreach (var item in result)
                {
                    oModel = item;
                }
                oModelCostType.Id = (int)oModel.FkcostTypeId;
                oDetailList = oModel.TrnsDirectMaterialDetails.ToList();
                oDetailList = oDetailList.ToList();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        #endregion Functions

        #region Events

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (DocNum > 0)
                {
                    Loading = true;
                    var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
                    if (Session != null)
                    {
                        //oModel.FlgActive = true;
                        //oModel.FlgDefaultResrMst = true;
                        LoginUserCode = Session.UserCode;
                        await GetAllCostType();
                        await GetAllDirectMaterialWithDocNum(DocNum);
                    }
                    Loading = false;
                }
                else
                {
                    Loading = true;
                    oModel.DocDate = DateTime.Today;
                    await GetAllCostType();
                    Loading = false;
                }
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