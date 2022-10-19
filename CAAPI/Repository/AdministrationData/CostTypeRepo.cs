using AutoMapper;

namespace CA.API.Repository.AdministrationData
{
    public class CostTypeRepo : ICostType
    {
        private WBCContext _DBContext;
        private readonly IMapper _mapper;
        public CostTypeRepo(WBCContext DBContext, IMapper mapper)
        {
            _DBContext = DBContext;
            _mapper = mapper;
        }

        public async Task<List<MstCostType>> GetAllData()
        {
            List<MstCostType> oList = new List<MstCostType>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstCostTypes.ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstCostType oMstCostTypes, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstCostTypes.Add(oMstCostTypes);
                    _DBContext.SaveChanges();
                    var LogMstCostType = _mapper.Map<LogMstCostType>(oMstCostTypes);
                    LogMstCostType.Id = 0;
                    LogMstCostType.LogDate = DateTime.Now;
                    LogMstCostType.LogUser = UserCode;
                    _DBContext.LogMstCostTypes.Add(LogMstCostType);
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

        public async Task<ApiResponseModel> Update(MstCostType oMstCostTypes, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstCostTypes.Update(oMstCostTypes);
                    _DBContext.SaveChanges();
                    var LogMstCostType = _mapper.Map<LogMstCostType>(oMstCostTypes);
                    LogMstCostType.Id = 0;
                    LogMstCostType.LogDate = DateTime.Now;
                    LogMstCostType.LogUser = UserCode;
                    _DBContext.LogMstCostTypes.Add(LogMstCostType);
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