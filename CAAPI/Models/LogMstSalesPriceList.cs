using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class LogMstSalesPriceList
    {
        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Plname { get; set; }
        public bool FlgDefult { get; set; }
        public string LogUser { get; set; }
        public DateTime LogDate { get; set; }
        public int SourceId { get; set; }
    }
}
