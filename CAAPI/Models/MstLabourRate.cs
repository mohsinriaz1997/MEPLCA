using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstLabourRate
    {
        public MstLabourRate()
        {
            MstLabourRateDetails = new HashSet<MstLabourRateDetail>();
        }

        public int Id { get; set; }
        public int? DocNum { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public virtual ICollection<MstLabourRateDetail> MstLabourRateDetails { get; set; }
    }
}
