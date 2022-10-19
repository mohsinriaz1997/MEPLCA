using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.MasterData;
using RestSharp;

namespace CA.UI.Data.MasterData
{
    public class MstDesignationService : IDesignationMaster
    {
        private readonly RestClient _restClient;

        public MstDesignationService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<MstDesignation>> GetAllData()
        {
            try
            {
                List<MstDesignation> oList = new List<MstDesignation>();

                var request = new RestRequest("MasterData/getAllDesignation", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstDesignation>>(request);

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

        public async Task<ApiResponseModel> Insert(MstDesignation oMstDesignation, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"MasterData/addDesignation?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstDesignation);
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

        public async Task<ApiResponseModel> Update(MstDesignation oMstDesignation, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"MasterData/updateDesignation?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstDesignation);
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