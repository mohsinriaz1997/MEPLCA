using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class LogMstItemPriceListDetail
    {
        public int Id { get; set; }
        public int? Fkid { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Hscode { get; set; }
        public int? FkgroupSetup1 { get; set; }
        public int? FkgroupSetup2 { get; set; }
        public int? FkgroupSetup3 { get; set; }
        public int? FkgroupSetup4 { get; set; }
        public string BlanketAgreement { get; set; }
        public decimal? BasicPrice { get; set; }
        public string Currency { get; set; }
        public decimal? Rate { get; set; }
        public decimal? UnitPricePkr { get; set; }
        public decimal? InsuranceCostPer { get; set; }
        public decimal? InsuranceCostValue { get; set; }
        public decimal? FreightCostPer { get; set; }
        public decimal? FreightCostValue { get; set; }
        public decimal? LandingCostPer { get; set; }
        public decimal? LandingCostValue { get; set; }
        public decimal? WhchargesPer { get; set; }
        public decimal? WhchargesValue { get; set; }
        public decimal? YardPaymentPer { get; set; }
        public decimal? YardPaymentValue { get; set; }
        public decimal? CleaningChargesPer { get; set; }
        public decimal? CleaningChargesValue { get; set; }
        public decimal? ImportPer { get; set; }
        public decimal? ImportValue { get; set; }
        public decimal? CustomDutyPer { get; set; }
        public decimal? CustomDutyValue { get; set; }
        public decimal? RegulatoryDutyPer { get; set; }
        public decimal? RegulatoryDutyValue { get; set; }
        public decimal? AdditionalDutyPer { get; set; }
        public decimal? AdditionalDutyValue { get; set; }
        public decimal ExciseDutyPer { get; set; }
        public decimal ExciseDutyValue { get; set; }
        public decimal? DutiesPer { get; set; }
        public decimal? DutiesValue { get; set; }
        public decimal? SalesTaxPer { get; set; }
        public decimal? SalesTaxValue { get; set; }
        public decimal? IncomeTaxPer { get; set; }
        public decimal? IncomeTaxValue { get; set; }
        public decimal? Value { get; set; }
        public decimal? Others { get; set; }
        public decimal? FinalItemPrice { get; set; }
        public string LogUser { get; set; }
        public DateTime? LogDate { get; set; }
        public int? FksourceId { get; set; }
    }
}
