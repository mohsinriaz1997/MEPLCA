using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using Microsoft.AspNetCore.Components.Routing;
using RestSharp;
using static MudBlazor.CategoryTypes;
using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace CA.UI.Data.AdministrationData
{
    public class MstApprovalSetupService :  IApprovalSetup
    {
    
        private readonly RestClient _restClient;

        [Inject]
        public ILocalStorageService _localStorageService { get; set; }
        private string LoginUserCode = "";

        public MstApprovalSetupService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<MstApprovalSetup>> GetAllData()
        {
            try
            {
                List<MstApprovalSetup> oList = new List<MstApprovalSetup>();

                var request = new RestRequest("AdministrationData/getAllApprovalSetup", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstApprovalSetup>>(request);

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

        public async Task<ApiResponseModel> Insert(MstApprovalSetup oMstApprovalSetup)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/addApprovalSetup", Method.Post);
                request.AddJsonBody(oMstApprovalSetup);
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

        public async Task<ApiResponseModel> Update(MstApprovalSetup oMstApprovalSetup)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/updateApprovalSetup", Method.Post);
                request.AddJsonBody(oMstApprovalSetup);
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

        public async Task<List<MstForm>> GetApprovalDocs()
        {
            try
            {
                List<MstForm> oList = new List<MstForm>();

                var request = new RestRequest("AdministrationData/GetApprovalForm", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstForm>>(request);

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

        public async Task<List<DocApproval>> GetAlerts(int UserID, string DocStatus)
        {
            try
            {
                List<DocApproval> oList = new List<DocApproval>();

                var request = new RestRequest($"AdministrationData/getAllDocApproval?UserID={UserID}&DocStatus={DocStatus}", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<DocApproval>>(request);

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

        public async Task<ApiResponseModel> UpdateDocApproval(DocApproval oDocApproval)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/updateDocApprovalStatus", Method.Post);
                request.AddJsonBody(oDocApproval);
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
