using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstStage
    {
        public MstStage()
        {
            MstStageDetails = new HashSet<MstStageDetail>();
        }

        public int Id { get; set; }
        public string StageName { get; set; }
        public int? NoOfApproval { get; set; }
        public int? NoOfRejection { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CappStamp { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UappStamp { get; set; }

        public virtual ICollection<MstStageDetail> MstStageDetails { get; set; }
    }
}
