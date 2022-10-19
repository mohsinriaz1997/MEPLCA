using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstResourceDetail
    {
        public int Id { get; set; }
        public int? Fkid { get; set; }
        public string TypeOfResr { get; set; }
        public string ResrDescription { get; set; }
        public string CurrCodeSap { get; set; }
        public string CurrNameSap { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Price { get; set; }
        public decimal? LandedFactor { get; set; }
        public decimal? Cost { get; set; }
        public bool? FlgActive { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }

        public virtual MstResource Fk { get; set; }
    }
}
