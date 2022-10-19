﻿using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVoctoolsDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public int FkactivityTypeId { get; set; }
        public string FkactivityDescription { get; set; }
        public int? FkresourceId { get; set; }
        public string ToolsType { get; set; }
        public decimal? Cost { get; set; }
        public decimal? CycleTimeSec { get; set; }
        public decimal? NoOfWorkers { get; set; }
        public decimal? Nos { get; set; }
        public decimal? LifeUnit { get; set; }
        public decimal? Total { get; set; }

        public virtual TrnsVoc Fk { get; set; }
    }
}
