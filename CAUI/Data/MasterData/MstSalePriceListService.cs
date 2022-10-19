using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.MasterData;
using RestSharp;

namespace CA.UI.Data.MasterData
{
    public class MstSalePriceListService : ISalePriceList
    {
        private readonly RestClient _restClient;

        public MstSalePriceListService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<List<MstSalesPriceList>> GetAllData()
        {
            try
            {
                List<MstSalesPriceList> oList = new List<MstSalesPriceList>();

                var request = new RestRequest("MasterData/getAllSalesPriceList", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstSalesPriceList>>(request);

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

        public async Task<List<MstSalesPriceList>> GetAllDataDefault()
        {
            try
            {
                List<MstSalesPriceList> oList = new List<MstSalesPriceList>();

                var request = new RestRequest("MasterData/getAllSalesPriceListDefault", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstSalesPriceList>>(request);

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

        public async Task<ApiResponseModel> Insert(MstSalesPriceList oMstSalesPriceList, string UserCode)
        {
            {
                ApiResponseModel response = new ApiResponseModel();
                try
                {
                    var request = new RestRequest($@"MasterData/addSalesPriceList?UserCode={UserCode}", Method.Post);
                    request.AddJsonBody(oMstSalesPriceList);
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

        public async Task<ApiResponseModel> Update(MstSalesPriceList oMstSalesPriceList, string UserCode)
        {
            {
                ApiResponseModel response = new ApiResponseModel();
                try
                {
                    var request = new RestRequest($@"MasterData/updateSalesPriceList?UserCode={UserCode}", Method.Post);
                    request.AddJsonBody(oMstSalesPriceList);
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
}