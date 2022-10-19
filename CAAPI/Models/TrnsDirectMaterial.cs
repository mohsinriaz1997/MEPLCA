using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsDirectMaterial
    {
        public TrnsDirectMaterial()
        {
            TrnsDirectMaterialDetails = new HashSet<TrnsDirectMaterialDetail>();
        }

        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string ProductDept { get; set; }
        public DateTime? DocDate { get; set; }
        public string DocName { get; set; }
        public int? FkcostTypeId { get; set; }
        public string CostTypeDesc { get; set; }
        public string CurrencyEr { get; set; }
        public decimal? ExchangeRate { get; set; }
        public int? FkitemPriceListDocNum { get; set; }
        public string ForAnalysis { get; set; }
        public decimal? TotalMaterialCost { get; set; }
        public decimal? TotalImportCost { get; set; }
        public decimal? TotalLocalCost { get; set; }
        public int? DocNum { get; set; }
        public bool? FlgAnalysis { get; set; }
        public int? FkSqdocNum { get; set; }

        public virtual ICollection<TrnsDirectMaterialDetail> TrnsDirectMaterialDetails { get; set; }
    }
}
