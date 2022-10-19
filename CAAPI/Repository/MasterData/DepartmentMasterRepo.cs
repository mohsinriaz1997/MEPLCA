using AutoMapper;

namespace CA.API.Repository.MasterData
{
    public class DepartmentMasterRepo : IDepartmentMaster
    {
        private WBCContext _DBContext;
        private readonly IMapper _mapper;

        public DepartmentMasterRepo(WBCContext dBContext, IMapper mapper)
        {
            _DBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<List<MstDepartment>> GetAllData()
        {
            List<MstDepartment> oList = new List<MstDepartment>();
            try
            {
                await Task.Run(() =>
                    {
                        oList = _DBContext.MstDepartments.ToList();
                    });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }


        public async Task<ApiResponseModel> Insert(MstDepartment oMstDepartment, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstDepartments.Add(oMstDepartment);
                    _DBContext.SaveChanges();

                    var LogMstDepartment = _mapper.Map<LogMstDepartment>(oMstDepartment);
                    LogMstDepartment.Id = 0;
                    LogMstDepartment.LogDate = DateTime.Now;
                    LogMstDepartment.LogUser = UserCode;
                    _DBContext.LogMstDepartments.Add(LogMstDepartment);
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

        public async Task<ApiResponseModel> Update(MstDepartment oMstDepartment, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstDepartments.Update(oMstDepartment);
                    _DBContext.SaveChanges();


                    var LogMstDepartment = _mapper.Map<LogMstDepartment>(oMstDepartment);
                    LogMstDepartment.Id = 0;
                    LogMstDepartment.LogDate = DateTime.Now;
                    LogMstDepartment.LogUser = UserCode;
                    _DBContext.LogMstDepartments.Add(LogMstDepartment);
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