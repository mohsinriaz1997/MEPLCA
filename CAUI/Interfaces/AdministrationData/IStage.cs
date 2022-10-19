using CA.API.Models;

namespace CA.UI.Interfaces.AdministrationData
{
    public interface IStage
    {
        Task<List<MstStage>> GetAllData();
        Task<ApiResponseModel> Insert(MstStage oMstStage);
        Task<ApiResponseModel> Update(MstStage oMstStage);
    }
}


