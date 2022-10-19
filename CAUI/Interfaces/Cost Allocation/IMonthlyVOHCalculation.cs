using CA.API.Models;

namespace CA.UI.Interfaces.Cost_Allocation
{
    public interface IMonthlyVOHCalculation
    {
        Task<List<TrnsVoh>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsVoh oTrnsVoh);

        Task<ApiResponseModel> Update(TrnsVoh oTrnsVoh);
    }
}