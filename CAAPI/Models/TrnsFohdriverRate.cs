using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsFohdriverRate
    {
        public TrnsFohdriverRate()
        {
            TrnsFohdriverRatesDetails = new HashSet<TrnsFohdriverRatesDetail>();
        }

        public int Id { get; set; }
        public int? DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public string Lyear { get; set; }
        public string Lmonth { get; set; }
        public decimal? CostDriver { get; set; }
        public decimal? DistributionCost { get; set; }
        public decimal? TotalCostDc { get; set; }
        public decimal? TotalDriverValue { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }

        public virtual ICollection<TrnsFohdriverRatesDetail> TrnsFohdriverRatesDetails { get; set; }
    }
}
