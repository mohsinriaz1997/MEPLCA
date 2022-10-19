using CA.API.Models;

namespace CA.UI.Interfaces.AdministrationData
{
    public interface IApprovalSetup
    {
        Task<List<MstApprovalSetup>> GetAllData();
        Task<ApiResponseModel> Insert(MstApprovalSetup oMstApprovalSetup);
        Task<ApiResponseModel> Update(MstApprovalSetup oMstApprovalSetup);
        Task<List<MstForm>> GetApprovalDocs();
        Task<List<DocApproval>> GetAlerts(int UserID, string DocStatus);
        Task<ApiResponseModel> UpdateDocApproval(DocApproval oDocApproval);
    }
}
