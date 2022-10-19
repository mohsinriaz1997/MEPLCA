namespace CA.API.Interfaces.CostAllocations
{
    public interface IFOHRatesCalc
    {
        Task<List<TrnsFohrate>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsFohrate oTrnsFohrate);

        Task<ApiResponseModel> Update(TrnsFohrate oTrnsFohrate);
    }
}