namespace CA.API.Interfaces.AdministrationData
{
    public interface ILabourRate
    {
        Task<List<MstLabourRate>> GetAllData();

        Task<ApiResponseModel> Insert(MstLabourRate oMstLabourRate);

        Task<ApiResponseModel> Update(MstLabourRate oMstLabourRatee);

        Task<List<MstLabourRateDetail>> GetAllDataDetail();
    }
}