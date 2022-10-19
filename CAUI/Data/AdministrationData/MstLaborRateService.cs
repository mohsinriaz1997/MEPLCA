using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.MasterData;
using RestSharp;

namespace CA.UI.Data.AdministrationData
{
    public class MstLaborRateService : ILaborRate
    {
        private readonly RestClient _restClient;

        public MstLaborRateService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        public async Task<ApiResponseModel> Insert(MstLabourRate omstLaborRate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/addLabourRate", Method.Post);
                request.AddJsonBody(omstLaborRate);
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

        public async Task<ApiResponseModel> Update(MstLabourRate omstLaborRate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest("AdministrationData/updateLabourRate", Method.Post);
                request.AddJsonBody(omstLaborRate);
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

        public async Task<List<MstLabourRate>> GetAllData()
        {
            try
            {
                List<MstLabourRate> oList = new List<MstLabourRate>();

                var request = new RestRequest("AdministrationData/getAllLabourRate", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstLabourRate>>(request);

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

        public async Task<List<MstLabourRateDetail>> GetAllDataDetail()
        {
            try
            {
                List<MstLabourRateDetail> oList = new List<MstLabourRateDetail>();

                var request = new RestRequest("AdministrationData/getAllLabourRateDetail", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstLabourRateDetail>>(request);

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