using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class TrnsSalesQuatationDetail
    {
        public int Id { get; set; }
        public int Fkid { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string ProductDepartment { get; set; }
        public int FkdirectMaterialDocNum { get; set; }
        public int FkdirectMaterialId { get; set; }
        public int Fkvohid { get; set; }
        public int FkvohdocNum { get; set; }
        public int Fkfohid { get; set; }
        public int FkfohdocNum { get; set; }
        public string Rmanalysis { get; set; }
        public decimal ImportCost { get; set; }
        public decimal LocalCost { get; set; }
        public decimal TotalRmcost { get; set; }
        public string Vohanalysis { get; set; }
        public string Machine { get; set; }
        public string Labour { get; set; }
        public string Electricity { get; set; }
        public string DiesAndMolds { get; set; }
        public string Tools { get; set; }
        public string Gasoline { get; set; }
        public string Packing { get; set; }
        public string Transportation { get; set; }
        public string Others { get; set; }
        public decimal TotalVohcost { get; set; }
        public decimal TotalDirectCost { get; set; }
        public decimal FohratePer { get; set; }
        public decimal Fohamount { get; set; }
        public decimal OtherFoh { get; set; }
        public decimal TotalFoh { get; set; }
        public decimal TotalUnitCost { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Margin { get; set; }
        public decimal MarginPer { get; set; }

        public virtual TrnsSalesQuotation Fk { get; set; }
    }
}
