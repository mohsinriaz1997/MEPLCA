using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.MasterData;
using RestSharp;

namespace CA.UI.Data.MasterData
{
    public class MstDepartmentService : IDepartmenMastert
    {
        #region Variable

        private readonly RestClient _restClient;

        #endregion Variable

        #region Events

        public MstDepartmentService()
        {
            _restClient = new RestClient(Settings.APIBaseURL);
        }

        #endregion Events

        #region Function

        public async Task<List<MstDepartment>> GetAllData()
        {
            try
            {
                List<MstDepartment> oList = new List<MstDepartment>();

                var request = new RestRequest("MasterData/getAllDepartment", Method.Get) { RequestFormat = DataFormat.Json };

                var response = await _restClient.ExecuteAsync<List<MstDepartment>>(request);

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

        public async Task<ApiResponseModel> Insert(MstDepartment oMstDepartment, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"MasterData/addDepartment?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstDepartment);
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
        public async Task<ApiResponseModel> Update(MstDepartment oMstDepartment, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                var request = new RestRequest($@"MasterData/updateDepartment?UserCode={UserCode}", Method.Post);
                request.AddJsonBody(oMstDepartment);
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

        #endregion Function
    }
}