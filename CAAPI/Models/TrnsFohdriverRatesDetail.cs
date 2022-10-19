using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsFohdriverRatesDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal? DriverValue { get; set; }
        public decimal? FohdistributionCost { get; set; }

        public virtual TrnsFohdriverRate Fk { get; set; }
    }
}
