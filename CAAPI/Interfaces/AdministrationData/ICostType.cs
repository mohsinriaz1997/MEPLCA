namespace CA.API.Interfaces.AdministrationData
{
    public interface ICostType
    {
        Task<List<MstCostType>> GetAllData();

        Task<ApiResponseModel> Insert(MstCostType oMstCostType, string UserCode);

        Task<ApiResponseModel> Update(MstCostType oMstCostType, string UserCode);
    }
}