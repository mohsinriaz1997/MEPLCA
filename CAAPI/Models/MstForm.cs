using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstForm
    {
        public int Id { get; set; }
        public int? FormCode { get; set; }
        public string FormName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CappStamp { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UappStamp { get; set; }
        public bool? FlgDataAccess { get; set; }
        public bool? FlgApprovalSetup { get; set; }
    }
}
