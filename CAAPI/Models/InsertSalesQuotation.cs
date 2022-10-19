namespace CA.API.Models
{
    public class InsertSalesQuotation
    {
        public List<TrnsDirectMaterial> oTrnsDirectMaterial { get; set; }

        public List<TrnsVoc> oTrnsVoc { get; set; }

        public TrnsSalesQuotation oTrnsSalesQuotation { get; set; }

    }
}
