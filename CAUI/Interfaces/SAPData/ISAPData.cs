using CA.API.Models;

namespace CA.UI.Interfaces.SAPData
{
    public interface ISAPData
    {
        Task<List<SAPModels>> GetExchangeRateFromSAP(string DocDate);
        Task<List<SAPModels>> GetExpenseAccountAmmountFromSAP(string year, string month, string AccCode);
        

        Task<List<SAPModels>> GetCurrencyFromSAP();

        Task<List<SAPModels>> GetProductFromSap();

        Task<List<SAPModels>> GetCustomerFromSAP();

        Task<List<SAPModels>> GetItemsFromSAP(string clause);
        Task<List<SAPModels>> GetAllItemsFromSAP();
        Task<List<SAPModels>> GetBlanketAgreementFromSAP(string ProductCode);

        Task<List<SAPModels>> GetItemsVOHFromSAP(string clause, string year, string month);

        Task<List<SAPModels>> GetAccountsFromSAP(string Clause);

        Task<List<SAPModels>> GetBomItemsFromSAP(string ProductCode);
    }
}