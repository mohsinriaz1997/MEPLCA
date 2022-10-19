using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstApprovalSetup
    {
        public MstApprovalSetup()
        {
            MstApprovalOriginators = new HashSet<MstApprovalOriginator>();
            MstApprovalStages = new HashSet<MstApprovalStage>();
            MstApprovalTerms = new HashSet<MstApprovalTerm>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? FlgSalesQuatation { get; set; }
        public bool? FlgMonthlyVohcalc { get; set; }
        public bool? FlgItemPriceList { get; set; }
        public bool? FlgResourceMasterData { get; set; }
        public bool? FlgMonthlyFohdriverRateCalc { get; set; }
        public bool? FlgMonthlyFohcostCalc { get; set; }
        public bool? FlgFohrateCalc { get; set; }
        public bool? FlgVariableOverheadCost { get; set; }
        public bool? FlgActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CappStamp { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UappStamp { get; set; }

        public virtual ICollection<MstApprovalOriginator> MstApprovalOriginators { get; set; }
        public virtual ICollection<MstApprovalStage> MstApprovalStages { get; set; }
        public virtual ICollection<MstApprovalTerm> MstApprovalTerms { get; set; }
    }
}
