using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVohtoolsDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? ToolsVohrate { get; set; }
        public decimal? ToolsVohamount { get; set; }

        public virtual TrnsVoh Fk { get; set; }
    }
}
