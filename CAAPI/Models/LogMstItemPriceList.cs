using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class LogMstItemPriceList
    {
        public int Id { get; set; }
        public int DocNum { get; set; }
        public DateTime DocDate { get; set; }
        public string Plname { get; set; }
        public bool FlgDefaultPl { get; set; }
        public string PriceBase { get; set; }
        public decimal ExchangeRate { get; set; }
        public string LogUser { get; set; }
        public DateTime? LogDate { get; set; }
        public int? SourceId { get; set; }
    }
}
