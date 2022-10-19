using Microsoft.AspNetCore.Mvc;

namespace CA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostAllocationsController : ControllerBase
    {
        private IDirectMaterial _trnsDirectMaterial;
        private IVariableOverheadCost _trnsVoc;
        private IMonthlyVOHCalc _trnsVoh;
        private IFOHRatesCalc _trnsFohrate;
        private IMontlyFOHCostCalc _trnsFohcost;
        private IMonthlyFOHDriverRatesCalc _trnsFohdriverRate;
        private ISalesQuotation _trnsSalesQuotation;

        public CostAllocationsController(IDirectMaterial trnsDirectMaterial, IVariableOverheadCost trnsVoc, IMonthlyVOHCalc trnsVoh, IFOHRatesCalc trnsFohrate, IMontlyFOHCostCalc trnsFohcost, IMonthlyFOHDriverRatesCalc trnsFohdriverRate, ISalesQuotation trnsSalesQuotation)
        {
            _trnsDirectMaterial = trnsDirectMaterial;
            _trnsVoc = trnsVoc;
            _trnsVoh = trnsVoh;
            _trnsFohrate = trnsFohrate;
            _trnsFohcost = trnsFohcost;
            _trnsFohdriverRate = trnsFohdriverRate;
            _trnsSalesQuotation = trnsSalesQuotation;
        }

        #region DirectMaterial

        [Route("getAllDirectMaterial")]
        [HttpGet]
        public async Task<IActionResult> GetAllDirectMaterial()
        {
            List<TrnsDirectMaterial> oTrnsDirectMaterial = new List<TrnsDirectMaterial>();
            try
            {
                oTrnsDirectMaterial = await _trnsDirectMaterial.GetAllData();
                if (oTrnsDirectMaterial == null)
                {
                    return BadRequest(oTrnsDirectMaterial);
                }
                else
                {
                    return Ok(oTrnsDirectMaterial);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }
        [Route("getAllDirectMaterialForAnalysis")]
        [HttpGet]
        public async Task<IActionResult> GetAllDirectMaterial(string ForAnalysis)
        {
            List<TrnsDirectMaterial> oTrnsDirectMaterial = new List<TrnsDirectMaterial>();
            try
            {
                oTrnsDirectMaterial = await _trnsDirectMaterial.GetAllData(ForAnalysis);
                if (oTrnsDirectMaterial == null)
                {
                    return BadRequest(oTrnsDirectMaterial);
                }
                else
                {
                    return Ok(oTrnsDirectMaterial);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }
        [Route("getAllDirectMaterialForDocNum")]
        [HttpGet]
        public async Task<IActionResult> GetAllDirectMaterialDocNum(int DocNum)
        {
            List<TrnsDirectMaterial> oTrnsDirectMaterial = new List<TrnsDirectMaterial>();
            try
            {
                oTrnsDirectMaterial = await _trnsDirectMaterial.GetAllDataItem(DocNum);
                if (oTrnsDirectMaterial == null)
                {
                    return BadRequest(oTrnsDirectMaterial);
                }
                else
                {
                    return Ok(oTrnsDirectMaterial);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addDirectMaterial")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TrnsDirectMaterial pTrnsDirectMaterial)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsDirectMaterial.Insert(pTrnsDirectMaterial);
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

        [Route("updateDirectMaterial")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TrnsDirectMaterial pTrnsDirectMaterial)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsDirectMaterial.Update(pTrnsDirectMaterial);
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

        #endregion DirectMaterial

        #region Variable Overhead Cost

        [Route("getAllVariableOverheadCost")]
        [HttpGet]
        public async Task<IActionResult> GetAllVariableOverheadCost()
        {
            List<TrnsVoc> oTrnsVoc = new List<TrnsVoc>();
            try
            {
                oTrnsVoc = await _trnsVoc.GetAllData();
                if (oTrnsVoc == null)
                {
                    return BadRequest(oTrnsVoc);
                }
                else
                {
                    return Ok(oTrnsVoc);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addVariableOverheadCost")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TrnsVoc pTrnsVoc)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsVoc.Insert(pTrnsVoc);
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

        [Route("updateVariableOverheadCost")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TrnsVoc pTrnsVoc)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsVoc.Update(pTrnsVoc);
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

        #endregion Variable Overhead Cost

        #region Monthly VOH Calculation

        [Route("getAllMonthlyVOHCalc")]
        [HttpGet]
        public async Task<IActionResult> GetAllMonthlyVOHCalculation()
        {
            List<TrnsVoh> oTrnsVoh = new List<TrnsVoh>();
            try
            {
                oTrnsVoh = await _trnsVoh.GetAllData();
                if (oTrnsVoh == null)
                {
                    return BadRequest(oTrnsVoh);
                }
                else
                {
                    return Ok(oTrnsVoh);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addMonthlyVOHCalc")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TrnsVoh pTrnsVoh)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsVoh.Insert(pTrnsVoh);
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

        [Route("updateMonthlyVOHCalc")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TrnsVoh pTrnsVoh)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsVoh.Update(pTrnsVoh);
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

        #endregion Monthly VOH Calculation

        #region FOH Rates Calculation

        [Route("getAllFOHRatesCalc")]
        [HttpGet]
        public async Task<IActionResult> GetAllFOHRatesCalculation()
        {
            List<TrnsFohrate> oTrnsFohrate = new List<TrnsFohrate>();
            try
            {
                oTrnsFohrate = await _trnsFohrate.GetAllData();
                if (oTrnsFohrate == null)
                {
                    return BadRequest(oTrnsFohrate);
                }
                else
                {
                    return Ok(oTrnsFohrate);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addFOHRatesCalc")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TrnsFohrate pTrnsFohrate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsFohrate.Insert(pTrnsFohrate);
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

        [Route("updateFOHRatesCalc")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TrnsFohrate pTrnsFohrate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsFohrate.Update(pTrnsFohrate);
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

        #endregion FOH Rates Calculation

        #region Montly FOH Cost Calculation

        [Route("getAllMontlyFOHCostCalc")]
        [HttpGet]
        public async Task<IActionResult> GetAllMontlyFOHCostCalculation()
        {
            List<TrnsFohcost> oTrnsFohcost = new List<TrnsFohcost>();
            try
            {
                oTrnsFohcost = await _trnsFohcost.GetAllData();
                if (oTrnsFohcost == null)
                {
                    return BadRequest(oTrnsFohcost);
                }
                else
                {
                    return Ok(oTrnsFohcost);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addMontlyFOHCostCalc")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TrnsFohcost pTrnsFohcost)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsFohcost.Insert(pTrnsFohcost);
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

        [Route("updateMontlyFOHCostCalc")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TrnsFohcost pTrnsFohcost)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsFohcost.Update(pTrnsFohcost);
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

        #endregion Montly FOH Cost Calculation

        #region Montly FOH Driver Cost Calculation

        [Route("getAllMontlyFOHDriverCostCalc")]
        [HttpGet]
        public async Task<IActionResult> GetAllMontlyFOHDriverCost()
        {
            List<TrnsFohdriverRate> oTrnsFohdriverRate = new List<TrnsFohdriverRate>();
            try
            {
                oTrnsFohdriverRate = await _trnsFohdriverRate.GetAllData();
                if (oTrnsFohdriverRate == null)
                {
                    return BadRequest(oTrnsFohdriverRate);
                }
                else
                {
                    return Ok(oTrnsFohdriverRate);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addMontlyFOHDriverCostCalc")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TrnsFohdriverRate pTrnsFohdriverRate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsFohdriverRate.Insert(pTrnsFohdriverRate);
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

        [Route("updateMontlyFOHDriverCostCalc")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] TrnsFohdriverRate pTrnsFohdriverRate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsFohdriverRate.Update(pTrnsFohdriverRate);
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

        #endregion Montly FOH Driver Cost Calculation

        #region Sales Quotation

        [Route("getAllSalesQuotation")]
        [HttpGet]
        public async Task<IActionResult> GetAllSalesQuotation()
        {
            List<TrnsSalesQuotation> oTrnsSalesQuotation = new List<TrnsSalesQuotation>();
            try
            {
                oTrnsSalesQuotation = await _trnsSalesQuotation.GetAllData();
                if (oTrnsSalesQuotation == null)
                {
                    return BadRequest(oTrnsSalesQuotation);
                }
                else
                {
                    return Ok(oTrnsSalesQuotation);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        //[Route("addSalesQuotation")]
        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] TrnsSalesQuotation pTrnsSalesQuotation)
        //{
        //    ApiResponseModel response = new ApiResponseModel();
        //    try
        //    {
        //        response = await _trnsSalesQuotation.Insert(pTrnsSalesQuotation);
        //        if (response == null)
        //        {
        //            return BadRequest(response);
        //        }
        //        else
        //        {
        //            return Ok(response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //        return BadRequest("Something went wrong.");
        //    }
        //}

        [Route("addSalesQuotation")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InsertSalesQuotation pTrnsSalesQuotation)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsSalesQuotation.Insert(pTrnsSalesQuotation);
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

        [Route("updateSalesQuotation")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] InsertSalesQuotation pTrnsSalesQuotation)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _trnsSalesQuotation.Update(pTrnsSalesQuotation);
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

        #endregion Sales Quotation
    }
}