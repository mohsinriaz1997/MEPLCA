using AutoMapper;

namespace CA.API.Repository.MasterData
{
    public class ActivityTypeRepo : IActivityType
    {
        private WBCContext _DBContext;
        private readonly IMapper _mapper;

        public ActivityTypeRepo(WBCContext DBContext, IMapper mapper)
        {
            _DBContext = DBContext;
            _mapper = mapper;
        }

        public async Task<List<MstActivityType>> GetAllData()
        {
            List<MstActivityType> oList = new List<MstActivityType>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstActivityTypes.ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstActivityType oMstActivityType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstActivityTypes.Add(oMstActivityType);
                    _DBContext.SaveChanges();
                    var LogMstActivityType = _mapper.Map<LogMstActivityType>(oMstActivityType);
                    LogMstActivityType.Id = 0;
                    LogMstActivityType.LogDate = DateTime.Now;
                    LogMstActivityType.LogUser = UserCode;
                    _DBContext.LogMstActivityTypes.Add(LogMstActivityType);
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

        public async Task<ApiResponseModel> Update(MstActivityType oMstActivityType, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstActivityTypes.Update(oMstActivityType);
                    _DBContext.SaveChanges();
                    var LogMstActivityType = _mapper.Map<LogMstActivityType>(oMstActivityType);
                    LogMstActivityType.Id = 0;
                    LogMstActivityType.LogDate = DateTime.Now;
                    LogMstActivityType.LogUser = UserCode;
                    _DBContext.LogMstActivityTypes.Add(LogMstActivityType);
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