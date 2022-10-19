using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CA.UI.Pages.MasterDataSetup
{
    public partial class LaborRate
    {
        #region InjectService

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IDialogService Dialog { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public ILaborRate _mstUserProfiled { get; set; }

        [Inject]
        public ICostType _mstCostType { get; set; }

        #endregion InjectService

        #region Variables

        private bool Loading = false;

        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; } = new MudDialogInstance();

        private MstLabourRate oModel = new MstLabourRate();
        private List<MstLabourRateDetail> oDetailList = new List<MstLabourRateDetail>();
        private List<MstLabourRateDetail> oDetail = new List<MstLabourRateDetail>();

        private MstCostType oModelCostType = new MstCostType();
        private List<MstCostType> oCostTypeList = new List<MstCostType>();

        private DialogOptions FullView = new DialogOptions() { MaxWidth = MaxWidth.ExtraExtraLarge, FullWidth = true, CloseButton = true, DisableBackdropClick = true, CloseOnEscapeKey = true };

        #endregion Variables

        #region Functions

        private void OnFromDateChange()
        {
            try
            {
            oModel.FromDate = oModel.FromDate;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
            //oModel.ToDate = null;
        }

        private void OnToDateChange()
        {
            try
            {
            oModel.ToDate = oModel.ToDate;

            }
            catch (Exception ex)
            {

                Logs.GenerateLogs(ex);
            }
            //oModel.FromDate = null;
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

                        oModel.MstLabourRateDetails = oDetailList.ToList();
                        if (oModel.FromDate == null)
                        {
                            Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        if (oModel != null && oModel.MstLabourRateDetails != null && oModel.MstLabourRateDetails.Count > 0)
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
                            Navigation.NavigateTo("/LaborRate", forceLoad: true);
                        }
                        else
                        {
                            Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                            Loading = false;
                            return null;
                        }
                        //oModel.FlgActive = true;
                        Loading = false;
                        return res1;
                    }
                    //}
                    catch (Exception ex)
                    {
                        Logs.GenerateLogs(ex);
                        Loading = false;
                        return null;
                    }
                }
                //else
                //{
                //    Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                //}
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

        private async Task OpenAddDialog(DialogOptions options)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("DialogFor", "LaborRate");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstLabourRateDetail)result.Data;
                    var update = oDetailList.Where(x => x.Description == res.Description && x.WageRate == res.WageRate).FirstOrDefault();
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
            catch (Exception)
            {
                throw;
            }
        }

        private async void Reset()
        {
            try
            {
                Loading = true;
                await Task.Delay(1);
                Navigation.NavigateTo("/ResourceMasterData", forceLoad: true);
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }

        private async Task OpenEditDialog(DialogOptions options, MstLabourRateDetail oDetailPara)
        {
            try
            {
                var parameters = new DialogParameters();
                parameters.Add("oDetailLaborRate", oDetailPara);
                parameters.Add("DialogFor", "LaborRate");
                var dialog = Dialog.Show<DetailDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstLabourRateDetail)result.Data;
                    var update = oDetailList.Where(x => x.Description == res.Description && x.FlgActive == res.FlgActive
                    && x.FkcostTypeId == res.FkcostTypeId
                    && x.WageRate == res.WageRate).FirstOrDefault();
                    if (update != null)
                    {
                        oDetail.Remove(update);
                    }
                    MstLabourRateDetail oRDDetail = new MstLabourRateDetail();
                    oRDDetail.Id = res.Id;
                    oRDDetail.Fkid = res.Fkid;
                    oRDDetail.Description = res.Description;
                    oRDDetail.WageRate = res.WageRate;
                    oRDDetail.FkcostTypeId = res.FkcostTypeId;
                    oRDDetail.FlgActive = res.FlgActive;
                    oDetail.Add(oRDDetail);
                    oDetailList = oDetail.ToList();
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
                parameters.Add("DialogFor", "LaborRateListMaster");
                var dialog = Dialog.Show<SaveDataDialog>("", parameters, options);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    var res = (MstLabourRate)result.Data;
                    oModel = res;
                    oDetailList = oModel.MstLabourRateDetails.ToList();
                    oDetail = oDetailList.ToList();
                }
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
                Loading = true;
                oModel.DocNum = oModel.DocNum;

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