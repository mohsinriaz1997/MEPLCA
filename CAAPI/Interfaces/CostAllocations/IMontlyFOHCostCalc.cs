namespace CA.API.Interfaces.CostAllocations
{
    public interface IMontlyFOHCostCalc
    {
        Task<List<TrnsFohcost>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsFohcost oTrnsFohcost);

        Task<ApiResponseModel> Update(TrnsFohcost oTrnsFohcost);
    }
}