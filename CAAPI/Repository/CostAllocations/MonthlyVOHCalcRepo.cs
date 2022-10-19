namespace CA.API.Repository.CostAllocations
{
    public class MonthlyVOHCalcRepo : IMonthlyVOHCalc
    {
        private WBCContext _DBContext;

        public MonthlyVOHCalcRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<TrnsVoh>> GetAllData()
        {
            List<TrnsVoh> oList = new List<TrnsVoh>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.TrnsVohs
                    .Include(t => t.TrnsVohmachineDetails)
                    .Include(t => t.TrnsVohlabourDetails)
                    .Include(t => t.TrnsVohelectricityDetails)
                    .Include(t => t.TrnsVohdyesAndMoldDetails)
                    .Include(t => t.TrnsVohgasolineDetails)
                    .Include(t => t.TrnsVohgasolineDetails)
                    .ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(TrnsVoh oTrnsVoh)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.TrnsVohs.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oTrnsVoh.DocNum = maxDocNum;
                    _DBContext.TrnsVohs.Add(oTrnsVoh);
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

        public async Task<ApiResponseModel> Update(TrnsVoh oTrnsVoh)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.TrnsVohs.Update(oTrnsVoh);
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