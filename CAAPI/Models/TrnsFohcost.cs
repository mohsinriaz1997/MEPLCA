using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsFohcost
    {
        public TrnsFohcost()
        {
            TrnsFohcostDetails = new HashSet<TrnsFohcostDetail>();
        }

        public int Id { get; set; }
        public int? DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public string Lyear { get; set; }
        public string Lmonth { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? TotalVoh { get; set; }
        public decimal? TotalFoh { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }

        public virtual ICollection<TrnsFohcostDetail> TrnsFohcostDetails { get; set; }
    }
}
