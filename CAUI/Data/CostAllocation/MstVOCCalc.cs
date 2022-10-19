using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using RestSharp;

namespace CA.UI.Data.CostAllocation
{
    public class MstVOCCalc : IVOC
    {
        private readonly RestClient _restClient;

        public MstVOCCalc()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<TrnsVoc>> GetAllData()
        {
            try
            {
                List<TrnsVoc> oList = new List<TrnsVoc>();

                var request = new RestRequest("CostAllocations/getAllVariableOverheadCost", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<TrnsVoc>>(request);

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

        public async Task<ApiResponseModel> Insert(TrnsVoc oTrnsVoc)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/addVariableOverheadCost", Method.Post);
                request.AddJsonBody(oTrnsVoc);
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

        public async Task<ApiResponseModel> Update(TrnsVoc oTrnsVoc)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/updateVariableOverheadCost", Method.Post);
                request.AddJsonBody(oTrnsVoc);
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