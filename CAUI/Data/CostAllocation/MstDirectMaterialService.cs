using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using RestSharp;

namespace CA.UI.Data.CostAllocation
{
    public class MstDirectMaterialService : IDirectMaterial
    {
        private readonly RestClient _restClient;

        public MstDirectMaterialService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<TrnsDirectMaterial>> GetAllData()
        {
            try
            {
                List<TrnsDirectMaterial> oList = new List<TrnsDirectMaterial>();

                var request = new RestRequest("CostAllocations/getAllDirectMaterial", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<TrnsDirectMaterial>>(request);

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
        public async Task<List<TrnsDirectMaterial>> GetAllData(string ForAnalysis)
        {
            try
            {
                List<TrnsDirectMaterial> oList = new List<TrnsDirectMaterial>();

                var request = new RestRequest("CostAllocations/getAllDirectMaterialForAnalysis", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<TrnsDirectMaterial>>(request);

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
        public async Task<List<TrnsDirectMaterial>> GetAllDataItem(int DocNum)
        {
            try
            {
                //CostAllocations / getAllDirectMaterialForDocNum
                List<TrnsDirectMaterial> oList = new List<TrnsDirectMaterial>();

                var request = new RestRequest($"CostAllocations/getAllDirectMaterialForDocNum?DocNum={DocNum}", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<TrnsDirectMaterial>>(request);

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

        public async Task<ApiResponseModel> Insert(TrnsDirectMaterial oTrnsDirectMaterial)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/addDirectMaterial", Method.Post);
                request.AddJsonBody(oTrnsDirectMaterial);
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

        public async Task<ApiResponseModel> Update(TrnsDirectMaterial oTrnsDirectMaterial)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/updateDirectMaterial", Method.Post);
                request.AddJsonBody(oTrnsDirectMaterial);
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