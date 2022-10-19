using CA.API.Models;

namespace CA.UI.Interfaces.MasterData
{
    public interface IResourceMasterData
    {
        Task<List<MstResource>> GetAllData();

        Task<List<MstResourceDetail>> GetAllDataDyes();

        Task<List<MstResourceDetail>> GetAllDataTools();

        Task<List<MstResourceDetail>> GetAllDataGasoline();

        Task<List<MstResourceDetail>> GetAllDataMachine();

        Task<ApiResponseModel> Insert(MstResource oMstResource);

        Task<ApiResponseModel> Update(MstResource oMstResource);
    }
}