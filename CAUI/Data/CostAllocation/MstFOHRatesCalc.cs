using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using RestSharp;

namespace CA.UI.Data.CostAllocation
{
    public class MstFOHRatesCalc : IFOHRatesCalc
    {
        private readonly RestClient _restClient;

        public MstFOHRatesCalc()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<TrnsFohrate>> GetAllData()
        {
            try
            {
                List<TrnsFohrate> oList = new List<TrnsFohrate>();

                var request = new RestRequest("CostAllocations/getAllFOHRatesCalc", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<TrnsFohrate>>(request);

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

        public async Task<ApiResponseModel> Insert(TrnsFohrate oTrnsFohrate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/addFOHRatesCalc", Method.Post);
                request.AddJsonBody(oTrnsFohrate);
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

        public async Task<ApiResponseModel> Update(TrnsFohrate oTrnsFohrate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/updateFOHRatesCalc", Method.Post);
                request.AddJsonBody(oTrnsFohrate);
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