using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstItemPriceList
    {
        public MstItemPriceList()
        {
            MstItemPriceListDetails = new HashSet<MstItemPriceListDetail>();
        }

        public int Id { get; set; }
        public int DocNum { get; set; }
        public DateTime? DocDate { get; set; }
        public string Plname { get; set; }
        public bool FlgDefaultPl { get; set; }
        public string PriceBase { get; set; }
        public decimal ExchangeRate { get; set; }
        public string DocStatus { get; set; }
        public string DocApprovalStatus { get; set; }

        public virtual ICollection<MstItemPriceListDetail> MstItemPriceListDetails { get; set; }
    }
}
