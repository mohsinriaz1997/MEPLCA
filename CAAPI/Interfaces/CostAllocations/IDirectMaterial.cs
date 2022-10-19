namespace CA.API.Interfaces.CostAllocations
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