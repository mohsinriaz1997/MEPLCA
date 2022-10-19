using CA.API.Models;

namespace CA.UI.Interfaces.MasterData
{
    public interface IDesignationMaster
    {
        Task<List<MstDesignation>> GetAllData();

        Task<ApiResponseModel> Insert(MstDesignation oMstDesignation, string UserCode);

        Task<ApiResponseModel> Update(MstDesignation oMstDesignation, string UserCode);
    }
}