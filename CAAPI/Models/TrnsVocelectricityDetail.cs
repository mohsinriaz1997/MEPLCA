using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVocelectricityDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public int FkactivityTypeId { get; set; }
        public string FkactivityDescription { get; set; }
        public int? FkresourceId { get; set; }
        public string MachineType { get; set; }
        public decimal? NoOfWorkers { get; set; }
        public decimal? ActualKwperHrs { get; set; }
        public decimal? IncFactor { get; set; }
        public decimal? KwperHrs { get; set; }
        public decimal? RatePerUnit { get; set; }
        public decimal? CostPerSec { get; set; }
        public decimal? CycleTimeSec { get; set; }
        public decimal? Total { get; set; }
        public decimal? NoOfMachine { get; set; }

        public virtual TrnsVoc Fk { get; set; }
    }
}
