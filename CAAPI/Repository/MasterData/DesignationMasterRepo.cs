using AutoMapper;
using CA.API.Models;

namespace CA.API.Repository.MasterData
{
    public class DesignationMasterRepo : IDesignationMaster
    {
        private WBCContext _DBContext;
        private readonly IMapper _mapper;

        public DesignationMasterRepo(WBCContext dBContext, IMapper mapper)
        {
            _DBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<List<MstDesignation>> GetAllData()
        {
            List<MstDesignation> oList = new List<MstDesignation>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstDesignations.ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstDesignation oMstDesignation, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstDesignations.Add(oMstDesignation);
                    _DBContext.SaveChanges();

                    var LogMstDesignation = _mapper.Map<LogMstDesignation>(oMstDesignation);
                    LogMstDesignation.Id = 0;
                    LogMstDesignation.LogDate = DateTime.Now;
                    LogMstDesignation.LogUser = UserCode;
                    _DBContext.LogMstDesignations.Add(LogMstDesignation);
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

        public async Task<ApiResponseModel> Update(MstDesignation oMstDesignation, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    //_DBContext.MstDesignations.Update(oMstDesignation);
                    //_DBContext.SaveChanges();

                    //var LogMstDesignation = _mapper.Map<LogMstDesignation>(oMstDesignation);
                    //LogMstDesignation.Id = 0;
                    //LogMstDesignation.LogDate = DateTime.Now;
                    //LogMstDesignation.LogUser = UserCode;
                    //_DBContext.LogMstDesignations.Add(LogMstDesignation);
                    //response.Id = 1;
                    //response.Message = "Saved successfully";

                    _DBContext.MstDesignations.Update(oMstDesignation);
                    _DBContext.SaveChanges();
                    var LogMstCostType = _mapper.Map<LogMstDesignation>(oMstDesignation);
                    LogMstCostType.Id = 0;
                    LogMstCostType.LogDate = DateTime.Now;
                    LogMstCostType.LogUser = UserCode;
                    _DBContext.LogMstDesignations.Add(LogMstCostType);
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