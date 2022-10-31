using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVohlabourDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? LabourVohrate { get; set; }
        public decimal? LabourVohamount { get; set; }
        public decimal? ProductQuantity { get; set; }

        public virtual TrnsVoh Fk { get; set; }
    }
}
