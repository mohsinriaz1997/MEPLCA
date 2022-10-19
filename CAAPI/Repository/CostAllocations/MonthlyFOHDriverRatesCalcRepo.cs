namespace CA.API.Repository.CostAllocations
{
    public class MonthlyFOHDriverRatesCalcRepo : IMonthlyFOHDriverRatesCalc
    {
        private WBCContext _DBContext;

        public MonthlyFOHDriverRatesCalcRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<TrnsFohdriverRate>> GetAllData()
        {
            List<TrnsFohdriverRate> oList = new List<TrnsFohdriverRate>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.TrnsFohdriverRates
                    .Include(t => t.TrnsFohdriverRatesDetails)
                    .ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(TrnsFohdriverRate oTrnsFohdriverRate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.TrnsFohdriverRates.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oTrnsFohdriverRate.DocNum = maxDocNum;
                    _DBContext.TrnsFohdriverRates.Add(oTrnsFohdriverRate);
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

        public async Task<ApiResponseModel> Update(TrnsFohdriverRate oTrnsFohdriverRate)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.TrnsFohdriverRates.Update(oTrnsFohdriverRate);
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