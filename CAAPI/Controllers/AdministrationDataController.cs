using CA.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationDataController : ControllerBase
    {
        private IUserProfile _mstUserProfile;
        private ICostType _mstCostType;
        private ICostDriversType _mstCostDriversType;
        private ILabourRate _mstLobourRate;
        private IStage _mstStage;
        private IApprovalSetup _mstApprovalSetup;

        public AdministrationDataController(IUserProfile mstUserProfile, ICostType mstCostType, ICostDriversType mstCostDriversType, ILabourRate mstLobourRate, IStage mstStage, IApprovalSetup mstApprovalSetup)
        {
            _mstUserProfile = mstUserProfile;
            _mstCostType = mstCostType;
            _mstCostDriversType = mstCostDriversType;
            _mstLobourRate = mstLobourRate;
            _mstStage = mstStage;
            _mstApprovalSetup = mstApprovalSetup;
        }

        #region MstUserProfile

        [Route("getAllUserProfile")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserProfile()
        {
            List<MstUserProfile> oMstUserProfile = new List<MstUserProfile>();
            try
            {
                oMstUserProfile = await _mstUserProfile.GetAllData();
                if (oMstUserProfile == null)
                {
                    return BadRequest(oMstUserProfile);
                }
                else
                {
                    return Ok(oMstUserProfile);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addUserProfile")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstUserProfile pMstUserProfile)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstUserProfile.Insert(pMstUserProfile);
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

        [Route("updateUserProfile")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstUserProfile pMstUserProfile)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstUserProfile.Update(pMstUserProfile);
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

        [Route("validateLogin")]
        [HttpGet]
        public async Task<IActionResult> ValidateLogin([FromBody] MstUserProfile pUser)
        {
            MstUserProfile oUser = new MstUserProfile();
            try
            {
                oUser = await _mstUserProfile.ValidateLogin(pUser.UserCode, pUser.Password);
                if (oUser == null)
                {
                    return BadRequest(oUser);
                }
                else
                {
                    return Ok(oUser);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("getAllMenu")]
        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            List<MstMenu> oMstMenu = new List<MstMenu>();
            try
            {
                oMstMenu = await _mstUserProfile.GetAllMenu();
                if (oMstMenu == null)
                {
                    return BadRequest(oMstMenu);
                }
                else
                {
                    return Ok(oMstMenu);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("getUserAuthorization")]
        [HttpGet]
        public async Task<IActionResult> FetchUserAuth(int userID)
        {
            List<UserAuthorization> oUserAuthorization = new List<UserAuthorization>();
            try
            {
                oUserAuthorization = await _mstUserProfile.FetchUserAuth(userID);
                if (oUserAuthorization == null)
                {
                    return BadRequest(oUserAuthorization);
                }
                else
                {
                    return Ok(oUserAuthorization);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addUserAuth")]
        [HttpPost]
        public async Task<IActionResult> AddUserAuth([FromBody] List<UserAuthorization> pUserAuthorization)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstUserProfile.AddUserAuthorization(pUserAuthorization);
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

        [Route("generateOTP")]
        [HttpGet]
        public async Task<IActionResult> GenerateOTP([FromBody] MstUserProfile pMstUser)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstUserProfile.GenerateOTP(pMstUser);
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

        [Route("authenticateOTP")]
        [HttpGet]
        public async Task<IActionResult> AuthenticateOTP([FromBody] PasswordReset pPasswordReset)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstUserProfile.AuthenticateOTP(pPasswordReset);
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
        [Route("changePassword")]
        [HttpGet]
        public async Task<IActionResult> ChangePassword([FromBody] MstUserProfile pMstUser)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstUserProfile.ChangePassword(pMstUser);
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

        [Route("getAllFormAndCostType")]
        [HttpGet]
        public async Task<IActionResult> GetAllFormCostType(int UserID)
        {
            List<UserDataAccess> oUserDataAccess = new List<UserDataAccess>();
            try
            {
                oUserDataAccess = await _mstUserProfile.GetAllFormAndCostType(UserID);
                if (oUserDataAccess == null)
                {
                    return BadRequest(oUserDataAccess);
                }
                else
                {
                    return Ok(oUserDataAccess);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }
        [Route("GetAllFormAndCostTypesResource")]
        [HttpGet]
        public async Task<IActionResult> GetAllFormCostTypes(string UserID)
        {
            List<UserDataAccess> oUserDataAccess = new List<UserDataAccess>();
            try
            {
                oUserDataAccess = await _mstUserProfile.GetAllFormAndCostTypesResource(UserID);
                if (oUserDataAccess == null)
                {
                    return BadRequest(oUserDataAccess);
                }
                else
                {
                    return Ok(oUserDataAccess);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }
        [Route("GetAllFormAndCostTypesFOHRate")]
        [HttpGet]
        public async Task<IActionResult> GetAllFormAndCostTypesFOHRate(string UserID)
        {
            List<UserDataAccess> oUserDataAccess = new List<UserDataAccess>();
            try
            {
                oUserDataAccess = await _mstUserProfile.GetAllFormAndCostTypesFOHRate(UserID);
                if (oUserDataAccess == null)
                {
                    return BadRequest(oUserDataAccess);
                }
                else
                {
                    return Ok(oUserDataAccess);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }


        [Route("addUserDataAccess")]
        [HttpPost]
        public async Task<IActionResult> AddDataAccess([FromBody] List<UserDataAccess> pUserDataAccess)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstUserProfile.AddUserDataAccess(pUserDataAccess);
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

        #endregion
        #region MstCostType

        [Route("getAllCostType")]
        [HttpGet]
        public async Task<IActionResult> GetAllCostType()
        {
            List<MstCostType> oMstCostType = new List<MstCostType>();
            try
            {
                oMstCostType = await _mstCostType.GetAllData();
                if (oMstCostType == null)
                {
                    return BadRequest(oMstCostType);
                }
                else
                {
                    return Ok(oMstCostType);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addCostType")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstCostType pMstCostType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstCostType.Insert(pMstCostType, UserCode);
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

        [Route("updateCostType")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstCostType pMstCostType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstCostType.Update(pMstCostType, UserCode);
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

        #endregion MstCostType

        #region MstCostDriversType

        [Route("getAllCostDriversType")]
        [HttpGet]
        public async Task<IActionResult> GetAllCostDriversType()
        {
            List<MstCostDriversType> oMstCostType = new List<MstCostDriversType>();
            try
            {
                oMstCostType = await _mstCostDriversType.GetAllData();
                if (oMstCostType == null)
                {
                    return BadRequest(oMstCostType);
                }
                else
                {
                    return Ok(oMstCostType);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addCostDriversType")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstCostDriversType pMstCostDriversType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstCostDriversType.Insert(pMstCostDriversType, UserCode);
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

        [Route("updateCostDriversType")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstCostDriversType pMstCostDriversType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstCostDriversType.Update(pMstCostDriversType, UserCode);
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

        #endregion MstCostDriversType

        #region MstLabourRate

        [Route("getAllLabourRate")]
        [HttpGet]
        public async Task<IActionResult> GetAllLabourRate()
        {
            List<MstLabourRate> oMstLabourRate = new List<MstLabourRate>();
            try
            {
                oMstLabourRate = await _mstLobourRate.GetAllData();
                if (oMstLabourRate == null)
                {
                    return BadRequest(oMstLabourRate);
                }
                else
                {
                    return Ok(oMstLabourRate);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addLabourRate")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstLabourRate pMstLabourRate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstLobourRate.Insert(pMstLabourRate);
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

        [Route("updateLabourRate")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstLabourRate pMstLabourRate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstLobourRate.Update(pMstLabourRate);
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

        [Route("getAllLabourRateDetail")]
        [HttpGet]
        public async Task<IActionResult> getAllLabourRateDetail()
        {
            List<MstLabourRateDetail> oMstLabourRate = new List<MstLabourRateDetail>();
            try
            {
                oMstLabourRate = await _mstLobourRate.GetAllDataDetail();
                if (oMstLabourRate == null)
                {
                    return BadRequest(oMstLabourRate);
                }
                else
                {
                    return Ok(oMstLabourRate);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion MstLabourRate

        #region MstStage

        [Route("getAllStage")]
        [HttpGet]
        public async Task<IActionResult> GetAllStage()
        {
            List<MstStage> oMstStage = new List<MstStage>();
            try
            {
                oMstStage = await _mstStage.GetAllData();
                if (oMstStage == null)
                {
                    return BadRequest(oMstStage);
                }
                else
                {
                    return Ok(oMstStage);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addStage")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstStage pMstStage)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstStage.Insert(pMstStage);
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

        [Route("updateStage")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstStage pMstStage)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstStage.Update(pMstStage);
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

        #endregion

        #region MstApprovalSetup

        [Route("getAllApprovalSetup")]
        [HttpGet]
        public async Task<IActionResult> GetAllApproval()
        {
            List<MstApprovalSetup> oMstApprovalSetup = new List<MstApprovalSetup>();
            try
            {
                oMstApprovalSetup = await _mstApprovalSetup.GetAllData();
                if (oMstApprovalSetup == null)
                {
                    return BadRequest(oMstApprovalSetup);
                }
                else
                {
                    return Ok(oMstApprovalSetup);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("addApprovalSetup")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MstApprovalSetup pMstApprovalSetup)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstApprovalSetup.Insert(pMstApprovalSetup);
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

        [Route("updateApprovalSetup")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] MstApprovalSetup pMstApprovalSetup)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                response = await _mstApprovalSetup.Update(pMstApprovalSetup);
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

        [Route("GetApprovalForm")]
        [HttpGet]
        public async Task<IActionResult> GetApprovalForm()
        {
            List<MstForm> oMstForm = new List<MstForm>();
            try
            {
                oMstForm = await _mstApprovalSetup.GetApprovalDocs();
                if (oMstForm == null)
                {
                    return BadRequest(oMstForm);
                }
                else
                {
                    return Ok(oMstForm);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("getAllDocApproval")]
        [HttpGet]
        public async Task<IActionResult> GetAlerts(int UserID, string DocStatus)
        {
            List<DocApproval> oDocApproval = new List<DocApproval>();
            try
            {
                oDocApproval = await _mstApprovalSetup.GetAlerts(UserID, DocStatus);
                if (oDocApproval == null)
                {
                    return BadRequest(oDocApproval);
                }
                else
                {
                    return Ok(oDocApproval);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        [Route("updateDocApprovalStatus")]
        [HttpPost]
        public async Task<IActionResult> UpdateDocApprovalStatus([FromBody] DocApproval pDocApproval)
        {
            DocApproval oDocApproval = new DocApproval();
            try
            {
                oDocApproval = await _mstApprovalSetup.UpdateDocApprStatus(pDocApproval);
                if (oDocApproval == null)
                {
                    return BadRequest(oDocApproval);
                }
                else
                {
                    return Ok(oDocApproval);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return BadRequest("Something went wrong.");
            }
        }

        #endregion
    }
}