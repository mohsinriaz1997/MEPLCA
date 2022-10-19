using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstApprovalTerm
    {
        public int Id { get; set; }
        public int? FkapprovalId { get; set; }
        public bool? Always { get; set; }
        public string TermQuery { get; set; }

        public virtual MstApprovalSetup Fkapproval { get; set; }
    }
}
