using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.Authentication;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;

namespace CA.UI.Data.AdministrationData
{
    public class MstUserProfileService : IUserProfile
    {
        private readonly RestClient _restClient;
        private readonly AuthenticationStateProvider oAuth;
        private ILocalStorageService oStorageService;

        public MstUserProfileService(ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _restClient = new RestClient(Settings.APIBaseURL);
            oStorageService = localStorageService;
            oAuth = authenticationStateProvider;
        }


        public async Task<ApiResponseModel> Insert(MstUserProfile oMstUserProfile)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/addUserProfile", Method.Post);
                request.AddJsonBody(oMstUserProfile);
                var res = await _restClient.ExecuteAsync(request);
                if (res.IsSuccessful)
                {
                    response.Id = 1;
                    response.Message = "Saved successfully";
                    return response;
                }
                else
                {
                    response.Id = 0;
                    response.Message = "Failed to save successfully";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                response.Id = 0;
                response.Message = "Failed to save successfully";
                return response;
            }
        }
        public async Task<ApiResponseModel> Update(MstUserProfile oMstUserProfile)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/updateCostType", Method.Post);
                request.AddJsonBody(oMstUserProfile);
                var res = await _restClient.ExecuteAsync(request);
                if (res.IsSuccessful)
                {
                    response.Id = 1;
                    response.Message = "Saved successfully";
                    return response;
                }
                else
                {
                    response.Id = 0;
                    response.Message = "Failed to save successfully";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                response.Id = 0;
                response.Message = "Failed to save successfully";
                return response;
            }
        }
        public async Task<List<MstUserProfile>> GetAllData()
        {
            try
            {
                List<MstUserProfile> oList = new List<MstUserProfile>();

                var request = new RestRequest("AdministrationData/getAllUserProfile", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstUserProfile>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<MstUserProfile> ValidateLogin(MstUserProfile mstUser)
        {
            try
            {
                var request = new RestRequest("AdministrationData/validateLogin", Method.Get) { RequestFormat = DataFormat.Json };
                request.AddBody(mstUser);
                var response = await _restClient.ExecuteAsync<MstUserProfile>(request);

                if (response.IsSuccessful)
                {
                    await oStorageService.SetItemAsync("User", response.Data);
                    ((AuthStateProvider)oAuth).NotifyUserAuthentication(response.Data.TokenValue);
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await oStorageService.RemoveItemAsync("User");
                ((AuthStateProvider)oAuth).NotifyUserLogout();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<List<MstMenu>> GetAllMenu()
        {
            try
            {
                List<MstMenu> oList = new List<MstMenu>();

                var request = new RestRequest("AdministrationData/getAllMenu", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstMenu>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<List<UserAuthorization>> FetchUserAuth(int userID)
        {
            try
            {
                List<UserAuthorization> oList = new List<UserAuthorization>();

                var request = new RestRequest($"AdministrationData/getUserAuthorization?userID={userID}", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<UserAuthorization>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<ApiResponseModel> AddUserAuthorization(List<UserAuthorization> oUserAuthorization)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/addUserAuth", Method.Post);
                request.AddJsonBody(oUserAuthorization);
                var res = await _restClient.ExecuteAsync(request);
                if (res.IsSuccessful)
                {
                    response.Id = 1;
                    response.Message = "Saved successfully";
                    return response;
                }
                else
                {
                    response.Id = 0;
                    response.Message = "Failed to save successfully";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                response.Id = 0;
                response.Message = "Failed to save successfully";
                return response;
            }
        }

        public async Task<ApiResponseModel> GenerateOTP(MstUserProfile oMstUser)
        {
            try
            {
                var request = new RestRequest("AdministrationData/generateOTP", Method.Get) { RequestFormat = DataFormat.Json };
                request.AddBody(oMstUser);
                var response = await _restClient.ExecuteAsync<ApiResponseModel>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<ApiResponseModel> AuthenticateOTP(PasswordReset oPasswordReset)
        {
            try
            {
                var request = new RestRequest("AdministrationData/authenticateOTP", Method.Get) { RequestFormat = DataFormat.Json };
                request.AddBody(oPasswordReset);
                var response = await _restClient.ExecuteAsync<ApiResponseModel>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<ApiResponseModel> ChangePassword(MstUserProfile oMstUser)
        {
            try
            {
                var request = new RestRequest("AdministrationData/changePassword", Method.Get) { RequestFormat = DataFormat.Json };
                request.AddBody(oMstUser);
                var response = await _restClient.ExecuteAsync<ApiResponseModel>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<List<UserDataAccess>> GetAllFormAndCostType(int UserID)
        {
            try
            {
                List<UserDataAccess> oList = new List<UserDataAccess>();

                var request = new RestRequest($"AdministrationData/getAllFormAndCostType?userID={UserID}", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<UserDataAccess>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        public async Task<List<UserDataAccess>> GetAllFormAndCostTypesResource(string UserID)
        {
            try
            {
                List<UserDataAccess> oList = new List<UserDataAccess>();

                var request = new RestRequest($"AdministrationData/GetAllFormAndCostTypesResource?userID={UserID}", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<UserDataAccess>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        public async Task<List<UserDataAccess>> GetAllFormAndCostTypesFOHRate(string UserID)
        {
            try
            {
                List<UserDataAccess> oList = new List<UserDataAccess>();

                var request = new RestRequest($"AdministrationData/GetAllFormAndCostTypesFOHRate?userID={UserID}", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<UserDataAccess>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        public async Task<List<UserDataAccess>> GetAllFormAndCostTypesVariableOverHeadCost(string UserID)
        {
            try
            {
                List<UserDataAccess> oList = new List<UserDataAccess>();

                var request = new RestRequest($"AdministrationData/GetAllFormAndCostTypesVariableOverHeadCost?userID={UserID}", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<UserDataAccess>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }

        public async Task<ApiResponseModel> AddUserDataAccess(List<UserDataAccess> oUserDataAccess)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/addUserDataAccess", Method.Post);
                request.AddJsonBody(oUserDataAccess);
                var res = await _restClient.ExecuteAsync(request);
                if (res.IsSuccessful)
                {
                    response.Id = 1;
                    response.Message = "Saved successfully";
                    return response;
                }
                else
                {
                    response.Id = 0;
                    response.Message = "Failed to save successfully";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                response.Id = 0;
                response.Message = "Failed to save successfully";
                return response;
            }
        }


    }
}