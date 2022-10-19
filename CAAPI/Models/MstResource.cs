using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstResource
    {
        public MstResource()
        {
            MstResourceDetails = new HashSet<MstResourceDetail>();
        }

        public int Id { get; set; }
        public int? DocNum { get; set; }
        public string ResrListName { get; set; }
        public DateTime? DocDate { get; set; }
        public string CurrencySap { get; set; }
        public decimal? RateSap { get; set; }
        public int? FkcostTypeId { get; set; }
        public string CostTypeDesc { get; set; }
        public bool? FlgDefaultResrMst { get; set; }
        public bool? FlgActive { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CappStamp { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UappStamp { get; set; }

        public virtual ICollection<MstResourceDetail> MstResourceDetails { get; set; }
    }
}
