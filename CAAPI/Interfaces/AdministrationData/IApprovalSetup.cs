using CA.API.Models;

namespace CA.API.Interfaces.AdministrationData
{
    public interface IApprovalSetup
    {
        Task<List<MstApprovalSetup>> GetAllData();
        Task<ApiResponseModel> Insert(MstApprovalSetup oMstApprovalSetup);
        Task<ApiResponseModel> Update(MstApprovalSetup oMstApprovalSetup);
        Task<List<MstForm>> GetApprovalDocs();
        int InsertDocApproval(int OriginatorID, int DocNum, string FLG, int FormCode, string FoamName,string UserCode);
        Task<List<DocApproval>> GetAlerts(int UserID, string DocStatus);
        Task<DocApproval> UpdateDocApprStatus(DocApproval oDocApproval);
    }
}
