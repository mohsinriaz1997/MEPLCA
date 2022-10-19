using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using RestSharp;

namespace CA.UI.Data.AdministrationData
{
    public class MstCostDriveService : ICostDriversType
    {
        private readonly RestClient _restClient;

        public MstCostDriveService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<ApiResponseModel> Insert(MstCostDriversType oMstCostDriveType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"AdministrationData/addCostDriversType?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstCostDriveType);
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

        public async Task<ApiResponseModel> Update(MstCostDriversType oMstCostDriveType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"AdministrationData/updateCostDriversType?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstCostDriveType);
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

        public async Task<List<MstCostDriversType>> GetAllData()
        {
            try
            {
                List<MstCostDriversType> oList = new List<MstCostDriversType>();

                var request = new RestRequest("AdministrationData/getAllCostDriversType", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstCostDriversType>>(request);

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