using AutoMapper;

namespace CA.API.Repository.MasterData
{
    public class GroupSetupMasterRepo : IGroupSetupMaster
    {
        private WBCContext _DBContext;
        private readonly IMapper _mapper;

        public GroupSetupMasterRepo(WBCContext dBContext, IMapper mapper)
        {
            _DBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<List<MstGroupSetup>> GetAllData()
        {
            List<MstGroupSetup> oList = new List<MstGroupSetup>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstGroupSetups.ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstGroupSetup oMstGroupSetup, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstGroupSetups.Add(oMstGroupSetup);
                    _DBContext.SaveChanges();
                    var LogMstGroupSetup = _mapper.Map<LogMstGroupSetup>(oMstGroupSetup);
                    LogMstGroupSetup.Id = 0;
                    LogMstGroupSetup.LogDate = DateTime.Now;
                    LogMstGroupSetup.LogUser = UserCode;
                    _DBContext.LogMstGroupSetups.Add(LogMstGroupSetup);
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

        public async Task<ApiResponseModel> Update(MstGroupSetup oMstGroupSetup, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstGroupSetups.Update(oMstGroupSetup);
                    _DBContext.SaveChanges();
                    var LogMstGroupSetup = _mapper.Map<LogMstGroupSetup>(oMstGroupSetup);
                    LogMstGroupSetup.Id = 0;
                    LogMstGroupSetup.LogDate = DateTime.Now;
                    LogMstGroupSetup.LogUser = UserCode;
                    _DBContext.LogMstGroupSetups.Add(LogMstGroupSetup);
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