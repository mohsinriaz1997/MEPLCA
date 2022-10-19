using CA.API.Models;

namespace CA.UI.Interfaces.MasterData
{
    public interface ISalePriceList
    {
        Task<List<MstSalesPriceList>> GetAllData();

        Task<List<MstSalesPriceList>> GetAllDataDefault();

        Task<ApiResponseModel> Insert(MstSalesPriceList oMstSalesPriceList, string UserCode);

        Task<ApiResponseModel> Update(MstSalesPriceList oMstSalesPriceList, string UserCode);
    }
}