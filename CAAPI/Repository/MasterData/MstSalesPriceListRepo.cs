using AutoMapper;

namespace CA.API.Repository.MasterData
{
    public class MstSalesPriceListRepo : ISalesPriceList
    {
        private WBCContext _DBContext;
        private readonly IMapper _mapper;

        public MstSalesPriceListRepo(WBCContext dBContext, IMapper mapper)
        {
            _DBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<List<MstSalesPriceList>> GetAllData()
        {
            List<MstSalesPriceList> oList = new List<MstSalesPriceList>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstSalesPriceLists.Include(c => c.MstSalesPriceListDetails).ToList();
                    //foreach (var item in oList)
                    //{
                    //    var detail = (from a in _DBContext.MstSalesPriceListDetails
                    //                  where a.Fkid == item.Id
                    //                  select a).ToList();
                    //}
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<List<MstSalesPriceList>> GetAllDataDefault()
        {
            List<MstSalesPriceList> oList = new List<MstSalesPriceList>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstSalesPriceLists.Include(c => c.MstSalesPriceListDetails).Where(x => x.FlgDefult == true).ToList();
                    //foreach (var item in oList)
                    //{
                    //    var detail = (from a in _DBContext.MstSalesPriceListDetails
                    //                  where a.Fkid == item.Id
                    //                  select a).ToList();
                    //}
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstSalesPriceList oMstSalesPriceList, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstSalesPriceLists.Add(oMstSalesPriceList);
                    _DBContext.SaveChanges();
                    var LogMstSalesPriceList = _mapper.Map<LogMstSalesPriceList>(oMstSalesPriceList);
                    LogMstSalesPriceList.Id = 0;
                    LogMstSalesPriceList.LogDate = DateTime.Now;
                    LogMstSalesPriceList.LogUser = UserCode;
                    _DBContext.LogMstSalesPriceLists.Add(LogMstSalesPriceList);
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

        public async Task<ApiResponseModel> Update(MstSalesPriceList oMstSalesPriceList, string UserCode)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstSalesPriceLists.Update(oMstSalesPriceList);
                    _DBContext.SaveChanges();
                    var LogMstSalesPriceList = _mapper.Map<LogMstSalesPriceList>(oMstSalesPriceList);
                    LogMstSalesPriceList.Id = 0;
                    LogMstSalesPriceList.LogDate = DateTime.Now;
                    LogMstSalesPriceList.LogUser = UserCode;
                    _DBContext.LogMstSalesPriceLists.Add(LogMstSalesPriceList);
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