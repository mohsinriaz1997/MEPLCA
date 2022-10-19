namespace CA.API.Repository.CostAllocations
{
    public class FOHRatesCalcRepo : IFOHRatesCalc
    {
        private WBCContext _DBContext;

        public FOHRatesCalcRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<TrnsFohrate>> GetAllData()
        {
            List<TrnsFohrate> oList = new List<TrnsFohrate>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.TrnsFohrates
                    .Include(t => t.TrnsFohratesDetails)
                    .ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(TrnsFohrate oTrnsFohrate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.TrnsFohrates.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oTrnsFohrate.DocNum = maxDocNum;
                    _DBContext.TrnsFohrates.Add(oTrnsFohrate);
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

        public async Task<ApiResponseModel> Update(TrnsFohrate oTrnsFohrate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.TrnsFohrates.Update(oTrnsFohrate);
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
    }
}