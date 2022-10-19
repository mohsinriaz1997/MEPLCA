using CA.API.Models;

namespace CA.UI.Interfaces.Cost_Allocation
{
    public interface ISalesQuotation
    {
        Task<List<TrnsSalesQuotation>> GetAllData();

        //Task<ApiResponseModel> Insert(TrnsSalesQuotation oTrnsSalesQuotation);
        Task<ApiResponseModel> Insert(InsertSalesQuotation oTrnsSalesQuotation);

        Task<ApiResponseModel> Update(InsertSalesQuotation oTrnsSalesQuotation);
    }
}
