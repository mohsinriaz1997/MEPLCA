using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsSalesQuotation
    {
        public TrnsSalesQuotation()
        {
            TrnsSalesQuotationDetails = new HashSet<TrnsSalesQuotationDetail>();
        }

        public int Id { get; set; }
        public int DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string SalesPriceList { get; set; }
        public int? FkcostTypeId { get; set; }
        public string CostTypeDesc { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }

        public virtual ICollection<TrnsSalesQuotationDetail> TrnsSalesQuotationDetails { get; set; }
    }
}
