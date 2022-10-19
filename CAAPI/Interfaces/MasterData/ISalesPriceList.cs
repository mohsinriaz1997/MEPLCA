namespace CA.API.Interfaces.MasterData
{
    public interface ISalesPriceList
    {
        Task<List<MstSalesPriceList>> GetAllData();

        Task<List<MstSalesPriceList>> GetAllDataDefault();

        Task<ApiResponseModel> Insert(MstSalesPriceList oMstSalesPriceList, string UserCode);

        Task<ApiResponseModel> Update(MstSalesPriceList oMstSalesPriceList, string UserCode);
    }
}