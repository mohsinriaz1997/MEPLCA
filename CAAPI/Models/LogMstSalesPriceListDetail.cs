using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class LogMstSalesPriceListDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal PerUnitSalesPrice { get; set; }
        public string LogUser { get; set; }
        public DateTime LogDate { get; set; }
        public int FksourceId { get; set; }
    }
}
