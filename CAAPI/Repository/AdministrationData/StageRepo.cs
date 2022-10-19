namespace CA.API.Repository.AdministrationData
{
    public class StageRepo : IStage
    {
        private WBCContext _DBContext;

        public StageRepo(WBCContext dBContext)
        {
            _DBContext = dBContext;
        }
        public async Task<List<MstStage>> GetAllData()
        {
            List<MstStage> oList = new List<MstStage>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstStages.Include(t => t.MstStageDetails).ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }
        public async Task<ApiResponseModel> Insert(MstStage oStage)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstStages.Add(oStage);
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
        public async Task<ApiResponseModel> Update(MstStage oStage)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    var Detail = _DBContext.MstStageDetails.Where(x => x.FkStageId == oStage.Id).ToList();
                    _DBContext.MstStageDetails.RemoveRange(Detail);
                    _DBContext.MstStages.Update(oStage);
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
