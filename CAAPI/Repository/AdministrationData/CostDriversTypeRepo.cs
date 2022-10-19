using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CA.API.Repository.AdministrationData
{
    public class CostDriversTypeRepo : ICostDriversType
    {
        private WBCContext _DBContext;
        private readonly IMapper _mapper;

        public CostDriversTypeRepo(WBCContext DBContext, IMapper mapper)
        {
            _DBContext = DBContext;
            _mapper = mapper;
        }

        public async Task<List<MstCostDriversType>> GetAllData()
        {
            List<MstCostDriversType> oList = new List<MstCostDriversType>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstCostDriversTypes.ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstCostDriversType oMstCostDriversTypes, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    //_DBContext.MstCostDriversTypes.Add(oMstCostDriversTypes);
                    //var LogMstCostDriverType = _mapper.Map<LogMstCostDriversType>(oMstCostDriversTypes);
                    //LogMstCostDriverType.Id = 0;
                    //LogMstCostDriverType.LogDate = DateTime.Now;
                    //LogMstCostDriverType.LogUser = UserCode;
                    //_DBContext.LogMstCostDriversTypes.Add(LogMstCostDriverType);
                    //_DBContext.SaveChanges();
                    //response.Id = 1;
                    //response.Message = "Saved successfully";

                    _DBContext.MstCostDriversTypes.Add(oMstCostDriversTypes);
                    _DBContext.SaveChanges();
                    var LogMstGroupSetup = _mapper.Map<LogMstCostDriversType>(oMstCostDriversTypes);
                    LogMstGroupSetup.Id = 0;
                    LogMstGroupSetup.LogDate = DateTime.Now;
                    LogMstGroupSetup.LogUser = UserCode;
                    _DBContext.LogMstCostDriversTypes.Add(LogMstGroupSetup);
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

        public async Task<ApiResponseModel> Update(MstCostDriversType oMstCostDriversTypes, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    //_DBContext.MstCostDriversTypes.Update(oMstCostDriversTypes);
                    //_DBContext.SaveChanges();
                    //var LogMstCostDriverType = _mapper.Map<LogMstCostDriversType>(oMstCostDriversTypes);
                    //LogMstCostDriverType.Id = 0;
                    //LogMstCostDriverType.LogDate = DateTime.Now;
                    //LogMstCostDriverType.LogUser = UserCode;
                    //_DBContext.LogMstCostDriversTypes.Add(LogMstCostDriverType);
                    //response.Id = 1;
                    //response.Message = "Saved successfully";

                    _DBContext.MstCostDriversTypes.Update(oMstCostDriversTypes);
                    _DBContext.SaveChanges();
                    var LogMstGroupSetup = _mapper.Map<LogMstCostDriversType>(oMstCostDriversTypes);
                    LogMstGroupSetup.Id = 0;
                    LogMstGroupSetup.LogDate = DateTime.Now;
                    LogMstGroupSetup.LogUser = UserCode;
                    _DBContext.LogMstCostDriversTypes.Add(LogMstGroupSetup);
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