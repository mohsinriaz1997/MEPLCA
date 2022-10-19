using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.MasterData;
using RestSharp;

namespace CA.UI.Data.MasterData
{
    public class MstResourceService : IResourceMasterData
    {
        private readonly RestClient _restClient;

        public MstResourceService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<MstResource>> GetAllData()
        {
            try
            {
                List<MstResource> oList = new List<MstResource>();

                var request = new RestRequest("MasterData/getAllResource", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstResource>>(request);

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

        public async Task<List<MstResourceDetail>> GetAllDataDyes()
        {
            try
            {
                List<MstResourceDetail> oList = new List<MstResourceDetail>();

                var request = new RestRequest("MasterData/getAllResourceDyes", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstResourceDetail>>(request);

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

        public async Task<List<MstResourceDetail>> GetAllDataTools()
        {
            try
            {
                List<MstResourceDetail> oList = new List<MstResourceDetail>();

                var request = new RestRequest("MasterData/getAllResourceTools", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstResourceDetail>>(request);

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

        public async Task<List<MstResourceDetail>> GetAllDataGasoline()
        {
            try
            {
                List<MstResourceDetail> oList = new List<MstResourceDetail>();

                var request = new RestRequest("MasterData/getAllResourceGasoline", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstResourceDetail>>(request);

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

        public async Task<List<MstResourceDetail>> GetAllDataMachine()
        {
            try
            {
                List<MstResourceDetail> oList = new List<MstResourceDetail>();

                var request = new RestRequest("MasterData/getAllResourceMachine", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstResourceDetail>>(request);

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

        public async Task<ApiResponseModel> Insert(MstResource oMstResource)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("MasterData/addResource", Method.Post);
                request.AddJsonBody(oMstResource);
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

        public async Task<ApiResponseModel> Update(MstResource oMstResource)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("MasterData/updateResource", Method.Post);
                request.AddJsonBody(oMstResource);
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