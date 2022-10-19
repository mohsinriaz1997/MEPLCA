using CA.API.Models;

namespace CA.UI.Interfaces.Cost_Allocation
{
    public interface IMonthlyFohDriverRatesCalculation
    {
        Task<List<TrnsFohdriverRate>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsFohdriverRate oTrnsFohdriverRate);

        Task<ApiResponseModel> Update(TrnsFohdriverRate oTrnsFohdriverRate);
    }
}