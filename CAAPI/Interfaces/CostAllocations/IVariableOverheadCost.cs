namespace CA.API.Interfaces.CostAllocations
{
    public interface IVariableOverheadCost
    {
        Task<List<TrnsVoc>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsVoc oTrnsVoc);

        Task<ApiResponseModel> Update(TrnsVoc oTrnsVoc);
    }
}