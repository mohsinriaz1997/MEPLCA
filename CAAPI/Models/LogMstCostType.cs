using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class LogMstCostType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool FlgActive { get; set; }
        public string LogUser { get; set; }
        public DateTime LogDate { get; set; }
        public int SourceId { get; set; }
    }
}
