namespace CA.API.Interfaces.CostAllocations
{
    public interface IMonthlyVOHCalc
    {
        Task<List<TrnsVoh>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsVoh oTrnsVoh);

        Task<ApiResponseModel> Update(TrnsVoh oTrnsVoh);
    }
}