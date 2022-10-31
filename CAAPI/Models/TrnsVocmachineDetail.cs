using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVocmachineDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public int? FkactivityTypeId { get; set; }
        public string FkactivityDescription { get; set; }
        public int? FkresourceId { get; set; }
        public string MachineType { get; set; }
        public decimal? Cost { get; set; }
        public decimal? CycleTimeSec { get; set; }
        public decimal? NoOfWorkers { get; set; }
        public decimal? Nos { get; set; }
        public decimal? LifeYears { get; set; }
        public decimal? Total { get; set; }

        public virtual TrnsVoc Fk { get; set; }
    }
}
