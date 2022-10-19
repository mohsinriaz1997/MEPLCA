namespace CA.API.Repository.AdministrationData
{
    public class LabourRateRepo : ILabourRate
    {
        private WBCContext _DBContext;

        public LabourRateRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<MstLabourRate>> GetAllData()
        {
            List<MstLabourRate> oList = new List<MstLabourRate>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstLabourRates.Include(t => t.MstLabourRateDetails).ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstLabourRate oMstLabourRates)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.MstLabourRates.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oMstLabourRates.DocNum = maxDocNum;
                    _DBContext.MstLabourRates.Add(oMstLabourRates);
                    _DBContext.SaveChanges();
                    response.Id = 1;
                    response.Message = "Saved successfully";
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                response.Id = 0;
                response.Message = "Failed to save successfully";
            }
            return response;
        }

        public async Task<ApiResponseModel> Update(MstLabourRate oMstLabourRates)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstLabourRates.Update(oMstLabourRates);
                    _DBContext.SaveChanges();
                    response.Id = 1;
                    response.Message = "Saved successfully";
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                response.Id = 0;
                response.Message = "Failed to save successfully";
            }
            return response;
        }

        public async Task<List<MstLabourRateDetail>> GetAllDataDetail()
        {
            List<MstLabourRateDetail> oList = new List<MstLabourRateDetail>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstLabourRateDetails.ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }
    }
}