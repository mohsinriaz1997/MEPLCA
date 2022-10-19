using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsFohrate
    {
        public TrnsFohrate()
        {
            TrnsFohratesDetails = new HashSet<TrnsFohratesDetail>();
        }

        public int Id { get; set; }
        public int DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public string DocName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int? FkcostTypeId { get; set; }
        public decimal? Fohrate { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }

        public virtual ICollection<TrnsFohratesDetail> TrnsFohratesDetails { get; set; }
    }
}
