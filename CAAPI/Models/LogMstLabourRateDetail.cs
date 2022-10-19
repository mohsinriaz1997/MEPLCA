using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class LogMstLabourRateDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public string Description { get; set; }
        public decimal WageRate { get; set; }
        public int FkcostTypeId { get; set; }
        public bool FlgActive { get; set; }
        public string LogUser { get; set; }
        public DateTime LogDate { get; set; }
        public int FksourceId { get; set; }
    }
}
