using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using RestSharp;

namespace CA.UI.Data.AdministrationData
{
    public class MstStageService : IStage
    {
        private readonly RestClient _restClient;
        public MstStageService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<MstStage>> GetAllData()
        {
            try
            {
                List<MstStage> oList = new List<MstStage>();

                var request = new RestRequest("AdministrationData/getAllStage", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstStage>>(request);

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

        public async Task<ApiResponseModel> Insert(MstStage oMstStage)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/addStage", Method.Post);
                request.AddJsonBody(oMstStage);
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

        public async Task<ApiResponseModel> Update(MstStage oMstStage)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/updateStage", Method.Post);
                request.AddJsonBody(oMstStage);
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
