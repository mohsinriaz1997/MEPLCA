using CA.API.Models;

namespace CA.UI.Interfaces.MasterData
{
    public interface ILaborRate
    {
        Task<List<MstLabourRate>> GetAllData();

        Task<ApiResponseModel> Insert(MstLabourRate oMstLabourRate);

        Task<ApiResponseModel> Update(MstLabourRate oMstLabourRatee);

        Task<List<MstLabourRateDetail>> GetAllDataDetail();
    }
}