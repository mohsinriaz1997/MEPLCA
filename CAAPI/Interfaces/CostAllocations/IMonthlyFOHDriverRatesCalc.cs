namespace CA.API.Interfaces.CostAllocations
{
    public interface IMonthlyFOHDriverRatesCalc
    {
        Task<List<TrnsFohdriverRate>> GetAllData();

        Task<ApiResponseModel> Insert(TrnsFohdriverRate oTrnsFohdriverRate);

        Task<ApiResponseModel> Update(TrnsFohdriverRate oTrnsFohdriverRate);
    }
}