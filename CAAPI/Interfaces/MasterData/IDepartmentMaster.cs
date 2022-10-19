namespace CA.API.Interfaces.MasterData
{
    public interface IDepartmentMaster
    {
        Task<List<MstDepartment>> GetAllData();

        Task<ApiResponseModel> Insert(MstDepartment oMstDepartment, string UserCode);
        Task<ApiResponseModel> Update(MstDepartment oMstDepartment, string UserCode);
    }
}