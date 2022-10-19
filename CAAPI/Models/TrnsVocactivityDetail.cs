using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVocactivityDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public int? FkactivityTypeId { get; set; }
        public string FkactivityDescription { get; set; }
        public decimal? ActualTimeCycle { get; set; }
        public decimal? IncFactor { get; set; }
        public decimal? CycleTimeSec { get; set; }

        public virtual TrnsVoc Fk { get; set; }
    }
}
