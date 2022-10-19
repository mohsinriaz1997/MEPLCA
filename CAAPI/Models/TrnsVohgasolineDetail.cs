using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVohgasolineDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? GasolineVohrate { get; set; }
        public decimal? GasolineVohamount { get; set; }

        public virtual TrnsVoh Fk { get; set; }
    }
}
