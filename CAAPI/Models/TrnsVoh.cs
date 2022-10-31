using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsVoh
    {
        public TrnsVoh()
        {
            TrnsVohdyesAndMoldDetails = new HashSet<TrnsVohdyesAndMoldDetail>();
            TrnsVohelectricityDetails = new HashSet<TrnsVohelectricityDetail>();
            TrnsVohgasolineDetails = new HashSet<TrnsVohgasolineDetail>();
            TrnsVohlabourDetails = new HashSet<TrnsVohlabourDetail>();
            TrnsVohmachineDetails = new HashSet<TrnsVohmachineDetail>();
            TrnsVohtoolsDetails = new HashSet<TrnsVohtoolsDetail>();
        }

        public int Id { get; set; }
        public int DocNum { get; set; }
        public DateTime DocDate { get; set; }
        public string Lyear { get; set; }
        public string Lmonth { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }
        public string TotalVohmachine { get; set; }
        public string TotalVohgasoline { get; set; }
        public string TotalVohdyes { get; set; }
        public string TotalVohElectriccity { get; set; }
        public string TotalTools { get; set; }
        public string TotalVohlabor { get; set; }

        public virtual ICollection<TrnsVohdyesAndMoldDetail> TrnsVohdyesAndMoldDetails { get; set; }
        public virtual ICollection<TrnsVohelectricityDetail> TrnsVohelectricityDetails { get; set; }
        public virtual ICollection<TrnsVohgasolineDetail> TrnsVohgasolineDetails { get; set; }
        public virtual ICollection<TrnsVohlabourDetail> TrnsVohlabourDetails { get; set; }
        public virtual ICollection<TrnsVohmachineDetail> TrnsVohmachineDetails { get; set; }
        public virtual ICollection<TrnsVohtoolsDetail> TrnsVohtoolsDetails { get; set; }
    }
}
