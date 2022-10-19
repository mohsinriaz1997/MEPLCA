using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsFohratesDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public int? FkcostDriversTypeId { get; set; }
        public decimal? Rate { get; set; }
        public string FkcostDrivesTypeDescription { get; set; }

        public virtual TrnsFohrate Fk { get; set; }
    }
}
