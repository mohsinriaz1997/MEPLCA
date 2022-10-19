using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.MasterData;
using RestSharp;

namespace CA.UI.Data.MasterData
{
    public class MstActivityTypeService : IActivityType
    {
        private readonly RestClient _restClient;

        public MstActivityTypeService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<MstActivityType>> GetAllData()
        {
            try
            {
                List<MstActivityType> oList = new List<MstActivityType>();

                var request = new RestRequest("MasterData/getAllActivityType", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstActivityType>>(request);

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

        public async Task<ApiResponseModel> Insert(MstActivityType oMstActivityType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"MasterData/addActivityType?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstActivityType);
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

        public async Task<ApiResponseModel> Update(MstActivityType oMstActivityType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"MasterData/updateActivityType?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstActivityType);
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