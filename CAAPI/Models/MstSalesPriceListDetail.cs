using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstSalesPriceListDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal PerUnitSalesPrice { get; set; }

        public virtual MstSalesPriceList Fk { get; set; }
    }
}
