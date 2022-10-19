namespace CA.API.Repository.MasterData
{
    public class ItemPriceListRepo : IItemPriceList
    {
        private WBCContext _DBContext;

        public ItemPriceListRepo(WBCContext dBContext)
        {
            _DBContext = dBContext;
        }

        public async Task<List<MstItemPriceList>> GetAllData()
        {
            List<MstItemPriceList> oList = new List<MstItemPriceList>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstItemPriceLists.Include(t => t.MstItemPriceListDetails).ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstItemPriceList oMstItemPriceList)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.MstItemPriceLists.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oMstItemPriceList.DocNum = maxDocNum;
                    _DBContext.MstItemPriceLists.Add(oMstItemPriceList);
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

        public async Task<ApiResponseModel> Update(MstItemPriceList oMstItemPriceList)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstItemPriceLists.Update(oMstItemPriceList);
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