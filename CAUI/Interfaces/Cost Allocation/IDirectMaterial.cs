using CA.API.Models;

namespace CA.UI.Interfaces.Cost_Allocation
{
    public interface IDirectMaterial
    {
        Task<List<TrnsDirectMaterial>> GetAllData();
        Task<List<TrnsDirectMaterial>> GetAllData(string ForAnalysis);
        Task<List<TrnsDirectMaterial>> GetAllDataItem(int DocNum);
        Task<ApiResponseModel> Insert(TrnsDirectMaterial oTrnsDirectMaterial);
        Task<ApiResponseModel> Update(TrnsDirectMaterial oTrnsDirectMaterial);
    }
}