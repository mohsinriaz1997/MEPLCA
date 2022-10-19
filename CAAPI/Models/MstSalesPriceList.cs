using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstSalesPriceList
    {
        public MstSalesPriceList()
        {
            MstSalesPriceListDetails = new HashSet<MstSalesPriceListDetail>();
        }

        public int Id { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Plname { get; set; }
        public bool FlgDefult { get; set; }

        public virtual ICollection<MstSalesPriceListDetail> MstSalesPriceListDetails { get; set; }
    }
}
