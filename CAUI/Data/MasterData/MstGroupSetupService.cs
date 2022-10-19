using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.MasterData;
using RestSharp;

namespace CA.UI.Data.MasterData
{
    public class MstGroupSetupService : IGroupSetupMaster
    {
        private readonly RestClient _restClient;

        public MstGroupSetupService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<MstGroupSetup>> GetAllData()
        {
            try
            {
                List<MstGroupSetup> oList = new List<MstGroupSetup>();

                var request = new RestRequest("MasterData/getAllGroupSetup", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstGroupSetup>>(request);

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

        public async Task<ApiResponseModel> Insert(MstGroupSetup oMstGroupSetup, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"MasterData/addGroupSetup?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstGroupSetup);
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

        public async Task<ApiResponseModel> Update(MstGroupSetup oMstGroupSetup, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"MasterData/updatepGroupSetup?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstGroupSetup);
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