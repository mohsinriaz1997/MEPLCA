using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsFohcostDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public int? FkcostDriverTypeId { get; set; }
        public string CostDriver { get; set; }
        public string AcctCode { get; set; }
        public string AcctDescription { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Vohamount { get; set; }
        public decimal? Fohamount { get; set; }

        public virtual TrnsFohcost Fk { get; set; }
    }
}
