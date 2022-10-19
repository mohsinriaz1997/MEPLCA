namespace CA.API.Interfaces.MasterData
{
    public interface IResourceMasterData
    {
        Task<List<MstResource>> GetAllData();

        Task<List<MstResourceDetail>> GetAllDataDyes();

        Task<List<MstResourceDetail>> GetAllDataMachine();

        Task<List<MstResourceDetail>> GetAllDataTools();

        Task<List<MstResourceDetail>> GetAllDataGasoline();

        Task<ApiResponseModel> Insert(MstResource oMstResource);

        Task<ApiResponseModel> Update(MstResource oMstResource);
    }
}