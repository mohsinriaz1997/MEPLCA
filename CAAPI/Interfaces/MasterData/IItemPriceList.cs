namespace CA.API.Interfaces.MasterData
{
    public interface IItemPriceList
    {
        Task<List<MstItemPriceList>> GetAllData();

        Task<ApiResponseModel> Insert(MstItemPriceList oMstItemPriceList);

        Task<ApiResponseModel> Update(MstItemPriceList oMstItemPriceList);
    }
}