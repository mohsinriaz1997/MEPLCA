using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.Cost_Allocation;
using RestSharp;

namespace CA.UI.Data.CostAllocation
{
    public class SalesQuotationService : ISalesQuotation
    {
        private readonly RestClient _restClient;

        public SalesQuotationService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<TrnsSalesQuotation>> GetAllData()
        {
            try
            {
                List<TrnsSalesQuotation> oList = new List<TrnsSalesQuotation>();

                var request = new RestRequest("CostAllocations/getAllSalesQuotation", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<TrnsSalesQuotation>>(request);

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

        //public async Task<ApiResponseModel> Insert(TrnsSalesQuotation oTrnsSalesQuotation)
        //{
        //    ApiResponseModel response = new ApiResponseModel();
        //    try
        //    {
        //        var request = new RestRequest("CostAllocations/addSalesQuotation", Method.Post);
        //        request.AddJsonBody(oTrnsSalesQuotation);
                
        //        var res = await _restClient.ExecuteAsync(request);
        //        if (res.IsSuccessful)
        //        {
        //            response.Id = 1;
        //            response.Message = "Saved successfully";
        //            return response;
        //        }
        //        else
        //        {
        //            response.Id = 0;
        //            response.Message = "Failed to save successfully";
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //        response.Id = 0;
        //        response.Message = "Failed to save successfully";
        //        return response;
        //    }
        //}

        public async Task<ApiResponseModel> Insert(InsertSalesQuotation oTrnsSalesQuotation)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/addSalesQuotation", Method.Post);
                request.AddJsonBody(oTrnsSalesQuotation);

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

        public async Task<ApiResponseModel> Update(InsertSalesQuotation oTrnsSalesQuotation)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("CostAllocations/updateSalesQuotation", Method.Post);
                request.AddJsonBody(oTrnsSalesQuotation);
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
