using CA.API.Models;

namespace CA.UI.Interfaces.Cost_Allocation
{
    public interface IFOHCostCalc
    {
        Task<List<TrnsFohcost>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsFohcost oTrnsFohrate);

        Task<ApiResponseModel> Update(TrnsFohcost oTrnsFohrate);
    }
}