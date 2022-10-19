using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVoc
    {
        public TrnsVoc()
        {
            TrnsVocactivityDetails = new HashSet<TrnsVocactivityDetail>();
            TrnsVocdyesAndMoldDetails = new HashSet<TrnsVocdyesAndMoldDetail>();
            TrnsVocelectricityDetails = new HashSet<TrnsVocelectricityDetail>();
            TrnsVocgasolineDetails = new HashSet<TrnsVocgasolineDetail>();
            TrnsVoclaborDetails = new HashSet<TrnsVoclaborDetail>();
            TrnsVocmachineDetails = new HashSet<TrnsVocmachineDetail>();
            TrnsVoctoolsDetails = new HashSet<TrnsVoctoolsDetail>();
        }

        public int Id { get; set; }
        public int? DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public string DocName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? PerDayShiftHrs { get; set; }
        public decimal? WorkingDays { get; set; }
        public decimal? NoOfShift { get; set; }
        public decimal? MonthlyVolume { get; set; }
        public int? FkresourceDocNum { get; set; }
        public bool? FlgDefault { get; set; }
        public int? FkcostTypeId { get; set; }
        public string CostTypeDesc { get; set; }
        public string ForAnalysis { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }
        public bool? FlgAnalysis { get; set; }
        public int? FkSqdocNum { get; set; }

        public virtual ICollection<TrnsVocactivityDetail> TrnsVocactivityDetails { get; set; }
        public virtual ICollection<TrnsVocdyesAndMoldDetail> TrnsVocdyesAndMoldDetails { get; set; }
        public virtual ICollection<TrnsVocelectricityDetail> TrnsVocelectricityDetails { get; set; }
        public virtual ICollection<TrnsVocgasolineDetail> TrnsVocgasolineDetails { get; set; }
        public virtual ICollection<TrnsVoclaborDetail> TrnsVoclaborDetails { get; set; }
        public virtual ICollection<TrnsVocmachineDetail> TrnsVocmachineDetails { get; set; }
        public virtual ICollection<TrnsVoctoolsDetail> TrnsVoctoolsDetails { get; set; }
    }
}
