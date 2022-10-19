using CA.API.Models;

namespace CA.UI.Interfaces.MasterData
{
    public interface IItemPriceList
    {
        Task<List<MstItemPriceList>> GetAllData();

        Task<ApiResponseModel> Insert(MstItemPriceList oMstItemPriceList);

        Task<ApiResponseModel> Update(MstItemPriceList oMstItemPriceList);
    }
}