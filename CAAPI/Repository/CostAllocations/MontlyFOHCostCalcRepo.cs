namespace CA.API.Repository.CostAllocations
{
    public class MontlyFOHCostCalcRepo : IMontlyFOHCostCalc
    {
        private WBCContext _DBContext;

        public MontlyFOHCostCalcRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<TrnsFohcost>> GetAllData()
        {
            List<TrnsFohcost> oList = new List<TrnsFohcost>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.TrnsFohcosts
                    .Include(t => t.TrnsFohcostDetails)
                    .ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(TrnsFohcost oTrnsFohcost)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.TrnsFohcosts.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oTrnsFohcost.DocNum = maxDocNum;
                    _DBContext.TrnsFohcosts.Add(oTrnsFohcost);
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

        public async Task<ApiResponseModel> Update(TrnsFohcost oTrnsFohcost)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.TrnsFohcosts.Update(oTrnsFohcost);
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