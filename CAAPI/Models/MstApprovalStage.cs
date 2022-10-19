using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstApprovalStage
    {
        public int Id { get; set; }
        public int? FkstageId { get; set; }
        public string FkstageName { get; set; }
        public int? FkapprovalId { get; set; }
        public int? ApprovalPriority { get; set; }

        public virtual MstApprovalSetup Fkapproval { get; set; }
    }
}
