using CA.API.Models;

namespace CA.UI.Interfaces.Cost_Allocation
{
    public interface IFOHRatesCalc
    {
        Task<List<TrnsFohrate>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsFohrate oTrnsFohrate);

        Task<ApiResponseModel> Update(TrnsFohrate oTrnsFohrate);
    }
}