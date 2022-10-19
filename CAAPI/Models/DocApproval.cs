using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class DocApproval
    {
        public int Id { get; set; }
        public int? FkuserId { get; set; }
        public int? FkformId { get; set; }
        public int? FkstageId { get; set; }
        public int? FkapprovalId { get; set; }
        public int? FkdocNum { get; set; }
        public string DocStatus { get; set; }
        public string Remarks { get; set; }
        public bool? FlgActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CappStamp { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UappStamp { get; set; }
        public string FkformName { get; set; }
    }
}
