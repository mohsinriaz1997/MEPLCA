using CA.API.Models;

namespace CA.UI.Interfaces.Cost_Allocation
{
    public interface IVOC
    {
        Task<List<TrnsVoc>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsVoc oTrnsVoc);

        Task<ApiResponseModel> Update(TrnsVoc oTrnsVoc);
    }
}