using CA.API.Models;

namespace CA.UI.Interfaces.MasterData
{
    public interface IDepartmenMastert
    {
        Task<List<MstDepartment>> GetAllData();

        Task<ApiResponseModel> Insert(MstDepartment oMstDepartment, string UserCode);
        Task<ApiResponseModel> Update(MstDepartment oMstDepartment, string UserCode);
    }
}