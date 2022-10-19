using Microsoft.AspNetCore.Mvc;

namespace CA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private IActivityType _mstActivityType;
        private IDepartmentMaster _mstDepartment;
        private IDesignationMaster _mstDesignation;
        private IGroupSetupMaster _mstGroupSetup;
        private IItemPriceList _mstItemPriceList;
        private IResourceMasterData _mstResouce;
        private ISalesPriceList _mstSalesPriceList;

        public MasterDataController(IActivityType mstActivityType, IDepartmentMaster mstDepartment, IDesignationMaster mstDesignation
            , IGroupSetupMaster mstGroupSetup, IItemPriceList mstItemPriceList, IResourceMasterData mstResouce, ISalesPriceList mstSalesPriceList)
        {
            _mstActivityType = mstActivityType;
            _mstDepartment = mstDepartment;
            _mstDesignation = mstDesignation;
            _mstGroupSetup = mstGroupSetup;
            _mstItemPriceList = mstItemPriceList;
            _mstResouce = mstResouce;
            _mstSalesPriceList = mstSalesPriceList;
        }

        #region ActivityType

        [Route("getAllActivityType")]
        [HttpGet]
        public async Task<IActionResult> GetAllActivityType()
        {
            List<MstActivityType> oMstActivityType = new List<MstActivityType>();
            try
            {
                oMstActivityType = await _mstActivityType.GetAllData();
                if (oMstActivityType == null)
                {
                    return BadRequest(oMstActivityType);
                }
                else
                {
                    return Ok(oMstActivityType);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addActivityType")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstActivityType pMstActivityType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstActivityType.Insert(pMstActivityType, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("updateActivityType")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstActivityType pMstActivityType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstActivityType.Update(pMstActivityType, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion ActivityType

        #region Department

        [Route("getAllDepartment")]
        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            List<MstDepartment> oMstDepartment = new List<MstDepartment>();
            try
            {
                oMstDepartment = await _mstDepartment.GetAllData();
                if (oMstDepartment == null)
                {
                    return BadRequest(oMstDepartment);
                }
                else
                {
                    return Ok(oMstDepartment);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addDepartment")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstDepartment pMstDepartment, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstDepartment.Insert(pMstDepartment, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("updateDepartment")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstDepartment pMstDepartment, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstDepartment.Update(pMstDepartment, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion Department

        #region Designation

        [Route("getAllDesignation")]
        [HttpGet]
        public async Task<IActionResult> GetAllDesignation()
        {
            List<MstDesignation> oMstDesignation = new List<MstDesignation>();
            try
            {
                oMstDesignation = await _mstDesignation.GetAllData();
                if (oMstDesignation == null)
                {
                    return BadRequest(oMstDesignation);
                }
                else
                {
                    return Ok(oMstDesignation);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addDesignation")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstDesignation pMstDesignation, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstDesignation.Insert(pMstDesignation, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("updateDesignation")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstDesignation pMstDesignation, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstDesignation.Update(pMstDesignation, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion Designation

        #region GroupSetup

        [Route("getAllGroupSetup")]
        [HttpGet]
        public async Task<IActionResult> GetAllGroupSetup()
        {
            List<MstGroupSetup> oMstGroupSetup = new List<MstGroupSetup>();
            try
            {
                oMstGroupSetup = await _mstGroupSetup.GetAllData();
                if (oMstGroupSetup == null)
                {
                    return BadRequest(oMstGroupSetup);
                }
                else
                {
                    return Ok(oMstGroupSetup);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addGroupSetup")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstGroupSetup pMstGroupSetup, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstGroupSetup.Insert(pMstGroupSetup, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("updatepGroupSetup")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstGroupSetup pMstGroupSetup, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstGroupSetup.Update(pMstGroupSetup, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion GroupSetup

        #region ItemPriceList

        [Route("getAllItemPriceList")]
        [HttpGet]
        public async Task<IActionResult> GetAllItemPriceList()
        {
            List<MstItemPriceList> oMstItemPriceList = new List<MstItemPriceList>();
            try
            {
                oMstItemPriceList = await _mstItemPriceList.GetAllData();
                if (oMstItemPriceList == null)
                {
                    return BadRequest(oMstItemPriceList);
                }
                else
                {
                    return Ok(oMstItemPriceList);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addItemPriceList")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstItemPriceList pMstItemPriceList)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstItemPriceList.Insert(pMstItemPriceList);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("updateItemPriceList")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstItemPriceList pMstItemPriceList)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstItemPriceList.Update(pMstItemPriceList);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion ItemPriceList

        #region ResouceMasterData

        [Route("getAllResource")]
        [HttpGet]
        public async Task<IActionResult> GetAllResouce()
        {
            List<MstResource> oMstResource = new List<MstResource>();
            try
            {
                oMstResource = await _mstResouce.GetAllData();
                if (oMstResource == null)
                {
                    return BadRequest(oMstResource);
                }
                else
                {
                    return Ok(oMstResource);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("getAllResourceDyes")]
        [HttpGet]
        public async Task<IActionResult> GetAllResouceDyes()
        {
            List<MstResourceDetail> oMstResourceDetail = new List<MstResourceDetail>();
            try
            {
                oMstResourceDetail = await _mstResouce.GetAllDataDyes();
                if (oMstResourceDetail == null)
                {
                    return BadRequest(oMstResourceDetail);
                }
                else
                {
                    return Ok(oMstResourceDetail);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("getAllResourceMachine")]
        [HttpGet]
        public async Task<IActionResult> GetAllResouceMachine()
        {
            List<MstResourceDetail> oMstResourceDetail = new List<MstResourceDetail>();
            try
            {
                oMstResourceDetail = await _mstResouce.GetAllDataMachine();
                if (oMstResourceDetail == null)
                {
                    return BadRequest(oMstResourceDetail);
                }
                else
                {
                    return Ok(oMstResourceDetail);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("getAllResourceTools")]
        [HttpGet]
        public async Task<IActionResult> GetAllResouceTools()
        {
            List<MstResourceDetail> oMstResourceDetail = new List<MstResourceDetail>();
            try
            {
                oMstResourceDetail = await _mstResouce.GetAllDataTools();
                if (oMstResourceDetail == null)
                {
                    return BadRequest(oMstResourceDetail);
                }
                else
                {
                    return Ok(oMstResourceDetail);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("getAllResourceGasoline")]
        [HttpGet]
        public async Task<IActionResult> GetAllResouceGasoline()
        {
            List<MstResourceDetail> oMstResourceDetail = new List<MstResourceDetail>();
            try
            {
                oMstResourceDetail = await _mstResouce.GetAllDataGasoline();
                if (oMstResourceDetail == null)
                {
                    return BadRequest(oMstResourceDetail);
                }
                else
                {
                    return Ok(oMstResourceDetail);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addResource")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstResource pMstResource)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstResouce.Insert(pMstResource);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("updateResource")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstResource pMstResource)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstResouce.Update(pMstResource);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion ResouceMasterData

        #region SalesPriceList

        [Route("getAllSalesPriceList")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesPriceList()
        {
            List<MstSalesPriceList> oMstSalesPriceList = new List<MstSalesPriceList>();
            try
            {
                oMstSalesPriceList = await _mstSalesPriceList.GetAllData();
                if (oMstSalesPriceList == null)
                {
                    return BadRequest(oMstSalesPriceList);
                }
                else
                {
                    return Ok(oMstSalesPriceList);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("getAllSalesPriceListDefault")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesPriceListDefault()
        {
            List<MstSalesPriceList> oMstSalesPriceList = new List<MstSalesPriceList>();
            try
            {
                oMstSalesPriceList = await _mstSalesPriceList.GetAllDataDefault();
                if (oMstSalesPriceList == null)
                {
                    return BadRequest(oMstSalesPriceList);
                }
                else
                {
                    return Ok(oMstSalesPriceList);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addSalesPriceList")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstSalesPriceList pMstSalesPriceList, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstSalesPriceList.Insert(pMstSalesPriceList, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("updateSalesPriceList")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstSalesPriceList pMstSalesPriceList, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstSalesPriceList.Update(pMstSalesPriceList, UserCode);
                if (response == null)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion SalesPriceList
    }
}