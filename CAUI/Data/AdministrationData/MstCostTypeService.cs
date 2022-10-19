using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using RestSharp;

namespace CA.UI.Data.AdministrationData
{
    public class MstCostTypeService : ICostType
    {
        private readonly RestClient _restClient;

        public MstCostTypeService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<ApiResponseModel> Insert(MstCostType oMstCostType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"AdministrationData/addCostType?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstCostType);
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

        public async Task<ApiResponseModel> Update(MstCostType oMstCostType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"AdministrationData/updateCostType?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstCostType);
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

        public async Task<List<MstCostType>> GetAllData()
        {
            try
            {
                List<MstCostType> oList = new List<MstCostType>();

                var request = new RestRequest("AdministrationData/getAllCostType", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstCostType>>(request);

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
    }
}