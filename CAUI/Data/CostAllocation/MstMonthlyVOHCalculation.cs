using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using RestSharp;

namespace CA.UI.Data.CostAllocation
{
    public class MstMonthlyVOHCalculation : IMonthlyVOHCalculation
    {
        private readonly RestClient _restClient;

        public MstMonthlyVOHCalculation()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<TrnsVoh>> GetAllData()
        {
            try
            {
                List<TrnsVoh> oList = new List<TrnsVoh>();

                var request = new RestRequest("CostAllocations/getAllMonthlyVOHCalc", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<TrnsVoh>>(request);

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

        public async Task<ApiResponseModel> Insert(TrnsVoh oTrnsVoh)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/addMonthlyVOHCalc", Method.Post);
                request.AddJsonBody(oTrnsVoh);
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

        public async Task<ApiResponseModel> Update(TrnsVoh oTrnsVoh)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/updateMonthlyVOHCalc", Method.Post);
                request.AddJsonBody(oTrnsVoh);
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