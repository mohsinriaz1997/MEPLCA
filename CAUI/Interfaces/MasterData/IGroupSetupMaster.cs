using CA.API.Models;

namespace CA.UI.Interfaces.MasterData
{
    public interface IGroupSetupMaster
    {
        Task<List<MstGroupSetup>> GetAllData();

        Task<ApiResponseModel> Insert(MstGroupSetup oMstGroupSetup, string UserCode);

        Task<ApiResponseModel> Update(MstGroupSetup oMstGroupSetup, string UserCode);
    }
}