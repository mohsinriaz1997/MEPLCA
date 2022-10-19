using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVoclaborDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public int FkactivityTypeId { get; set; }
        public string FkactivityDescription { get; set; }
        public int? FklaborRateId { get; set; }
        public string LaborDescription { get; set; }
        public decimal? WageRate { get; set; }
        public decimal? CostPerSec { get; set; }
        public decimal? CycleTimeSec { get; set; }
        public decimal? NoOfWorkers { get; set; }
        public decimal? Total { get; set; }

        public virtual TrnsVoc Fk { get; set; }
    }
}
