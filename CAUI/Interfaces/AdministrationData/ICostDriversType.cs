using CA.API.Models;

namespace CA.UI.Interfaces.AdministrationData
{
    public interface ICostDriversType
    {
        Task<List<MstCostDriversType>> GetAllData();

        Task<ApiResponseModel> Insert(MstCostDriversType oMstCostDriversType, string UserCode);

        Task<ApiResponseModel> Update(MstCostDriversType oMstCostDriversType, string UserCode);
    }
}