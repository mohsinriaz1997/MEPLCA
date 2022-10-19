using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using RestSharp;

namespace CA.UI.Data.CostAllocation
{
    public class MstMonthlyFohDriverRatesCalculationService : IMonthlyFohDriverRatesCalculation
    {
        private readonly RestClient _restClient;

        public MstMonthlyFohDriverRatesCalculationService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<TrnsFohdriverRate>> GetAllData()
        {
            try
            {
                List<TrnsFohdriverRate> oList = new List<TrnsFohdriverRate>();

                var request = new RestRequest("CostAllocations/getAllMontlyFOHDriverCostCalc", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<TrnsFohdriverRate>>(request);

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

        public async Task<ApiResponseModel> Insert(TrnsFohdriverRate oTrnsFohrate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/addMontlyFOHDriverCostCalc", Method.Post);
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

        public async Task<ApiResponseModel> Update(TrnsFohdriverRate oTrnsFohrate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/updateMontlyFOHDriverCostCalc", Method.Post);
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