using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsDirectMaterialDetail
    {
        public int Id { get; set; }
        public int? Fkid { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal? ConsQty { get; set; }
        public string Uom { get; set; }
        public string Currency { get; set; }
        public decimal? UnitPriceFc { get; set; }
        public decimal? Lcfactor { get; set; }
        public decimal? UnitPricePkr { get; set; }
        public decimal? AmountPkr { get; set; }
        public decimal? ChangeConsQty { get; set; }
        public decimal? ChangePricePkr { get; set; }
        public string Remarks { get; set; }

        public virtual TrnsDirectMaterial Fk { get; set; }
    }
}
