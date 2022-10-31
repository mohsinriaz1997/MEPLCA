using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CA.API.Models
{
    public partial class WBCContext : DbContext
    {
        public WBCContext()
        {
        }

        public WBCContext(DbContextOptions<WBCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DocApproval> DocApprovals { get; set; }
        public virtual DbSet<LogMstActivityType> LogMstActivityTypes { get; set; }
        public virtual DbSet<LogMstCostDriversType> LogMstCostDriversTypes { get; set; }
        public virtual DbSet<LogMstCostType> LogMstCostTypes { get; set; }
        public virtual DbSet<LogMstDepartment> LogMstDepartments { get; set; }
        public virtual DbSet<LogMstDesignation> LogMstDesignations { get; set; }
        public virtual DbSet<LogMstGroupSetup> LogMstGroupSetups { get; set; }
        public virtual DbSet<LogMstItemPriceList> LogMstItemPriceLists { get; set; }
        public virtual DbSet<LogMstItemPriceListDetail> LogMstItemPriceListDetails { get; set; }
        public virtual DbSet<LogMstLabourRate> LogMstLabourRates { get; set; }
        public virtual DbSet<LogMstLabourRateDetail> LogMstLabourRateDetails { get; set; }
        public virtual DbSet<LogMstResource> LogMstResources { get; set; }
        public virtual DbSet<LogMstResourceDetail> LogMstResourceDetails { get; set; }
        public virtual DbSet<LogMstSalesPriceList> LogMstSalesPriceLists { get; set; }
        public virtual DbSet<LogMstSalesPriceListDetail> LogMstSalesPriceListDetails { get; set; }
        public virtual DbSet<LogMstUserProfile> LogMstUserProfiles { get; set; }
        public virtual DbSet<MstActivityType> MstActivityTypes { get; set; }
        public virtual DbSet<MstApprovalOriginator> MstApprovalOriginators { get; set; }
        public virtual DbSet<MstApprovalSetup> MstApprovalSetups { get; set; }
        public virtual DbSet<MstApprovalStage> MstApprovalStages { get; set; }
        public virtual DbSet<MstApprovalTerm> MstApprovalTerms { get; set; }
        public virtual DbSet<MstCostDriversType> MstCostDriversTypes { get; set; }
        public virtual DbSet<MstCostType> MstCostTypes { get; set; }
        public virtual DbSet<MstDepartment> MstDepartments { get; set; }
        public virtual DbSet<MstDesignation> MstDesignations { get; set; }
        public virtual DbSet<MstForm> MstForms { get; set; }
        public virtual DbSet<MstGroupSetup> MstGroupSetups { get; set; }
        public virtual DbSet<MstItemPriceList> MstItemPriceLists { get; set; }
        public virtual DbSet<MstItemPriceListDetail> MstItemPriceListDetails { get; set; }
        public virtual DbSet<MstLabourRate> MstLabourRates { get; set; }
        public virtual DbSet<MstLabourRateDetail> MstLabourRateDetails { get; set; }
        public virtual DbSet<MstMenu> MstMenus { get; set; }
        public virtual DbSet<MstResource> MstResources { get; set; }
        public virtual DbSet<MstResourceDetail> MstResourceDetails { get; set; }
        public virtual DbSet<MstSalesPriceList> MstSalesPriceLists { get; set; }
        public virtual DbSet<MstSalesPriceListDetail> MstSalesPriceListDetails { get; set; }
        public virtual DbSet<MstStage> MstStages { get; set; }
        public virtual DbSet<MstStageDetail> MstStageDetails { get; set; }
        public virtual DbSet<MstUserProfile> MstUserProfiles { get; set; }
        public virtual DbSet<PasswordReset> PasswordResets { get; set; }
        public virtual DbSet<TrnsDirectMaterial> TrnsDirectMaterials { get; set; }
        public virtual DbSet<TrnsDirectMaterialDetail> TrnsDirectMaterialDetails { get; set; }
        public virtual DbSet<TrnsFohcost> TrnsFohcosts { get; set; }
        public virtual DbSet<TrnsFohcostDetail> TrnsFohcostDetails { get; set; }
        public virtual DbSet<TrnsFohdriverRate> TrnsFohdriverRates { get; set; }
        public virtual DbSet<TrnsFohdriverRatesDetail> TrnsFohdriverRatesDetails { get; set; }
        public virtual DbSet<TrnsFohrate> TrnsFohrates { get; set; }
        public virtual DbSet<TrnsFohratesDetail> TrnsFohratesDetails { get; set; }
        public virtual DbSet<TrnsSalesQuotation> TrnsSalesQuotations { get; set; }
        public virtual DbSet<TrnsSalesQuotationDetail> TrnsSalesQuotationDetails { get; set; }
        public virtual DbSet<TrnsVoc> TrnsVocs { get; set; }
        public virtual DbSet<TrnsVocactivityDetail> TrnsVocactivityDetails { get; set; }
        public virtual DbSet<TrnsVocdyesAndMoldDetail> TrnsVocdyesAndMoldDetails { get; set; }
        public virtual DbSet<TrnsVocelectricityDetail> TrnsVocelectricityDetails { get; set; }
        public virtual DbSet<TrnsVocgasolineDetail> TrnsVocgasolineDetails { get; set; }
        public virtual DbSet<TrnsVoclaborDetail> TrnsVoclaborDetails { get; set; }
        public virtual DbSet<TrnsVocmachineDetail> TrnsVocmachineDetails { get; set; }
        public virtual DbSet<TrnsVoctoolsDetail> TrnsVoctoolsDetails { get; set; }
        public virtual DbSet<TrnsVoh> TrnsVohs { get; set; }
        public virtual DbSet<TrnsVohdyesAndMoldDetail> TrnsVohdyesAndMoldDetails { get; set; }
        public virtual DbSet<TrnsVohelectricityDetail> TrnsVohelectricityDetails { get; set; }
        public virtual DbSet<TrnsVohgasolineDetail> TrnsVohgasolineDetails { get; set; }
        public virtual DbSet<TrnsVohlabourDetail> TrnsVohlabourDetails { get; set; }
        public virtual DbSet<TrnsVohmachineDetail> TrnsVohmachineDetails { get; set; }
        public virtual DbSet<TrnsVohtoolsDetail> TrnsVohtoolsDetails { get; set; }
        public virtual DbSet<UserAuthorization> UserAuthorizations { get; set; }
        public virtual DbSet<UserDataAccess> UserDataAccesses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PK-LHR-MME-131\\SQL19;Database=WBC;User ID=sa;Password=super;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocApproval>(entity =>
            {
                entity.ToTable("DocApproval");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAppStamp");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkapprovalId).HasColumnName("FKApprovalID");

                entity.Property(e => e.FkdocNum).HasColumnName("FKDocNum");

                entity.Property(e => e.FkformId).HasColumnName("FKFormID");

                entity.Property(e => e.FkformName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("FKFormName");

                entity.Property(e => e.FkstageId).HasColumnName("FKStageID");

                entity.Property(e => e.FkuserId).HasColumnName("FKUserId");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UAppStamp");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogMstActivityType>(entity =>
            {
                entity.ToTable("LogMstActivityType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");
            });

            modelBuilder.Entity<LogMstCostDriversType>(entity =>
            {
                entity.ToTable("LogMstCostDriversType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");
            });

            modelBuilder.Entity<LogMstCostType>(entity =>
            {
                entity.ToTable("LogMstCostType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");
            });

            modelBuilder.Entity<LogMstDepartment>(entity =>
            {
                entity.ToTable("LogMstDepartment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");
            });

            modelBuilder.Entity<LogMstDesignation>(entity =>
            {
                entity.ToTable("LogMstDesignation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");
            });

            modelBuilder.Entity<LogMstGroupSetup>(entity =>
            {
                entity.ToTable("LogMstGroupSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogMstItemPriceList>(entity =>
            {
                entity.ToTable("LogMstItemPriceList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FlgDefaultPl).HasColumnName("flgDefaultPL");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Plname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PLName");

                entity.Property(e => e.PriceBase)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");
            });

            modelBuilder.Entity<LogMstItemPriceListDetail>(entity =>
            {
                entity.ToTable("LogMstItemPriceListDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdditionalDutyPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.AdditionalDutyValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.BasicPrice).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.BlanketAgreement)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CleaningChargesPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CleaningChargesValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomDutyPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CustomDutyValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.DutiesPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.DutiesValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ExciseDutyPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ExciseDutyValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FinalItemPrice).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkgroupSetup1).HasColumnName("FKGroupSetup1");

                entity.Property(e => e.FkgroupSetup2).HasColumnName("FKGroupSetup2");

                entity.Property(e => e.FkgroupSetup3).HasColumnName("FKGroupSetup3");

                entity.Property(e => e.FkgroupSetup4).HasColumnName("FKGroupSetup4");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FksourceId).HasColumnName("FKSourceID");

                entity.Property(e => e.FreightCostPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FreightCostValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Hscode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HSCode");

                entity.Property(e => e.ImportPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ImportValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.IncomeTaxPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.IncomeTaxValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.InsuranceCostPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.InsuranceCostValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LandingCostPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.LandingCostValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Others).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.RegulatoryDutyPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.RegulatoryDutyValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SalesTaxPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SalesTaxValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.UnitPricePkr)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("UnitPricePKR");

                entity.Property(e => e.Value).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.WhchargesPer)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("WHChargesPer");

                entity.Property(e => e.WhchargesValue)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("WHChargesValue");

                entity.Property(e => e.YardPaymentPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.YardPaymentValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<LogMstLabourRate>(entity =>
            {
                entity.ToTable("LogMstLabourRate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.ToDate).HasColumnType("date");
            });

            modelBuilder.Entity<LogMstLabourRateDetail>(entity =>
            {
                entity.ToTable("LogMstLabourRateDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FkcostTypeId).HasColumnName("FKCostTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FksourceId).HasColumnName("FKSourceID");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WageRate).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<LogMstResource>(entity =>
            {
                entity.ToTable("LogMstResource");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkcostTypeId).HasColumnName("FKCostTypeID");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.FlgDefaultResrMst).HasColumnName("flgDefaultResrMst");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResrListName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");
            });

            modelBuilder.Entity<LogMstResourceDetail>(entity =>
            {
                entity.ToTable("LogMstResourceDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FksourceId).HasColumnName("FKSourceID");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LandedFactor).HasColumnType("numeric(5, 0)");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ResrDescription)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeOfResr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogMstSalesPriceList>(entity =>
            {
                entity.ToTable("LogMstSalesPriceList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FlgDefult).HasColumnName("flgDefult");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Plname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PLName");

                entity.Property(e => e.SourceId).HasColumnName("SourceID");
            });

            modelBuilder.Entity<LogMstSalesPriceListDetail>(entity =>
            {
                entity.ToTable("LogMstSalesPriceListDetail");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FksourceId).HasColumnName("FKSourceID");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PerUnitSalesPrice).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogMstUserProfile>(entity =>
            {
                entity.ToTable("LogMstUserProfile");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.FkdeptId).HasColumnName("FKDeptID");

                entity.Property(e => e.FkdesignationId).HasColumnName("FKDesignationID");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.FlgSuper).HasColumnName("flgSuper");

                entity.Property(e => e.LogDate).HasColumnType("datetime");

                entity.Property(e => e.LogUser)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).HasColumnName("SourceID");

                entity.Property(e => e.UserCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstActivityType>(entity =>
            {
                entity.ToTable("MstActivityType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");
            });

            modelBuilder.Entity<MstApprovalOriginator>(entity =>
            {
                entity.ToTable("MstApprovalOriginator");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkApprovalId).HasColumnName("FkApprovalID");

                entity.Property(e => e.OriginatorId).HasColumnName("OriginatorID");

                entity.HasOne(d => d.FkApproval)
                    .WithMany(p => p.MstApprovalOriginators)
                    .HasForeignKey(d => d.FkApprovalId)
                    .HasConstraintName("FK_MstApprovalOriginator_MstApprovalSetup");
            });

            modelBuilder.Entity<MstApprovalSetup>(entity =>
            {
                entity.ToTable("MstApprovalSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAppStamp");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.FlgFohrateCalc).HasColumnName("flgFOHRateCalc");

                entity.Property(e => e.FlgItemPriceList).HasColumnName("flgItemPriceList");

                entity.Property(e => e.FlgMonthlyFohcostCalc).HasColumnName("flgMonthlyFOHCostCalc");

                entity.Property(e => e.FlgMonthlyFohdriverRateCalc).HasColumnName("flgMonthlyFOHDriverRateCalc");

                entity.Property(e => e.FlgMonthlyVohcalc).HasColumnName("flgMonthlyVOHCalc");

                entity.Property(e => e.FlgResourceMasterData).HasColumnName("flgResourceMasterData");

                entity.Property(e => e.FlgSalesQuatation).HasColumnName("flgSalesQuatation");

                entity.Property(e => e.FlgVariableOverheadCost).HasColumnName("flgVariableOverheadCost");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UAppStamp");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MstApprovalStage>(entity =>
            {
                entity.ToTable("MstApprovalStage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkapprovalId).HasColumnName("FKApprovalID");

                entity.Property(e => e.FkstageId).HasColumnName("FKStageID");

                entity.Property(e => e.FkstageName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("FKStageName");

                entity.HasOne(d => d.Fkapproval)
                    .WithMany(p => p.MstApprovalStages)
                    .HasForeignKey(d => d.FkapprovalId)
                    .HasConstraintName("FK_MstApprovalStage_MstApprovalSetup");
            });

            modelBuilder.Entity<MstApprovalTerm>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkapprovalId).HasColumnName("FKApprovalID");

                entity.Property(e => e.TermQuery)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fkapproval)
                    .WithMany(p => p.MstApprovalTerms)
                    .HasForeignKey(d => d.FkapprovalId)
                    .HasConstraintName("FK_MstApprovalTerms_MstApprovalSetup");
            });

            modelBuilder.Entity<MstCostDriversType>(entity =>
            {
                entity.ToTable("MstCostDriversType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");
            });

            modelBuilder.Entity<MstCostType>(entity =>
            {
                entity.ToTable("MstCostType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");
            });

            modelBuilder.Entity<MstDepartment>(entity =>
            {
                entity.ToTable("MstDepartment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");
            });

            modelBuilder.Entity<MstDesignation>(entity =>
            {
                entity.ToTable("MstDesignation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");
            });

            modelBuilder.Entity<MstForm>(entity =>
            {
                entity.ToTable("MstForm");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAppStamp");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FlgApprovalSetup).HasColumnName("flgApprovalSetup");

                entity.Property(e => e.FlgDataAccess).HasColumnName("flgDataAccess");

                entity.Property(e => e.FormName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UAppStamp");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MstGroupSetup>(entity =>
            {
                entity.ToTable("MstGroupSetup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstItemPriceList>(entity =>
            {
                entity.ToTable("MstItemPriceList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FlgDefaultPl).HasColumnName("flgDefaultPL");

                entity.Property(e => e.Plname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PLName");

                entity.Property(e => e.PriceBase)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstItemPriceListDetail>(entity =>
            {
                entity.ToTable("MstItemPriceListDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdditionalDutyPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.AdditionalDutyValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.BasicPrice).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.BlanketAgreement)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CleaningChargesPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CleaningChargesValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomDutyPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CustomDutyValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.DutiesPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.DutiesValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ExciseDutyPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ExciseDutyValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FinalItemPrice).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkgroupSetup1).HasColumnName("FKGroupSetup1");

                entity.Property(e => e.FkgroupSetup2).HasColumnName("FKGroupSetup2");

                entity.Property(e => e.FkgroupSetup3).HasColumnName("FKGroupSetup3");

                entity.Property(e => e.FkgroupSetup4).HasColumnName("FKGroupSetup4");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FreightCostPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FreightCostValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Hscode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HSCode");

                entity.Property(e => e.ImportPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ImportValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.IncomeTaxPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.IncomeTaxValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.InsuranceCostPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.InsuranceCostValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LandingCostPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.LandingCostValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Others).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.RegulatoryDutyPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.RegulatoryDutyValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SalesTaxPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SalesTaxValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.UnitPricePkr)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("UnitPricePKR");

                entity.Property(e => e.Value).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.WhchargesPer)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("WHChargesPer");

                entity.Property(e => e.WhchargesValue)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("WHChargesValue");

                entity.Property(e => e.YardPaymentPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.YardPaymentValue).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.MstItemPriceListDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MstItemPriceListDetail_MstItemPriceList");
            });

            modelBuilder.Entity<MstLabourRate>(entity =>
            {
                entity.ToTable("MstLabourRate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.ToDate).HasColumnType("date");
            });

            modelBuilder.Entity<MstLabourRateDetail>(entity =>
            {
                entity.ToTable("MstLabourRateDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FkcostTypeDescription)
                    .HasMaxLength(50)
                    .HasColumnName("FKCostTypeDescription");

                entity.Property(e => e.FkcostTypeId).HasColumnName("FKCostTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.WageRate).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.MstLabourRateDetails)
                    .HasForeignKey(d => d.Fkid)
                    .HasConstraintName("FK_MstLaborRateDetail_MstLaborRate");
            });

            modelBuilder.Entity<MstMenu>(entity =>
            {
                entity.ToTable("MstMenu");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAppStamp");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Icon)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.MenuLink)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UAppStamp");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MstResource>(entity =>
            {
                entity.ToTable("MstResource");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CappStamp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CAppStamp");

                entity.Property(e => e.CostTypeDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencySap)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CurrencySAP");

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkcostTypeId).HasColumnName("FKCostTypeID");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.FlgDefaultResrMst).HasColumnName("flgDefaultResrMst");

                entity.Property(e => e.RateSap)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("RateSAP");

                entity.Property(e => e.ResrListName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UAppStamp");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MstResourceDetail>(entity =>
            {
                entity.ToTable("MstResourceDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CurrCodeSap)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CurrCodeSAP");

                entity.Property(e => e.CurrNameSap)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CurrNameSAP");

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.LandedFactor).HasColumnType("numeric(5, 0)");

                entity.Property(e => e.Price).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ResrDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TypeOfResr)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.MstResourceDetails)
                    .HasForeignKey(d => d.Fkid)
                    .HasConstraintName("FK_MstResourceDetail_MstResource");
            });

            modelBuilder.Entity<MstSalesPriceList>(entity =>
            {
                entity.ToTable("MstSalesPriceList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FlgDefult).HasColumnName("flgDefult");

                entity.Property(e => e.Plname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PLName");
            });

            modelBuilder.Entity<MstSalesPriceListDetail>(entity =>
            {
                entity.ToTable("MstSalesPriceListDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.PerUnitSalesPrice).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.MstSalesPriceListDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MstSalesPriceListDetail_MstSalesPriceList");
            });

            modelBuilder.Entity<MstStage>(entity =>
            {
                entity.ToTable("MstStage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAppStamp");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StageName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UAppStamp");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MstStageDetail>(entity =>
            {
                entity.ToTable("MstStageDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkStageId).HasColumnName("FkStageID");

                entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

                entity.Property(e => e.FlgType).HasColumnName("flgType");

                entity.HasOne(d => d.FkStage)
                    .WithMany(p => p.MstStageDetails)
                    .HasForeignKey(d => d.FkStageId)
                    .HasConstraintName("FK_MstStageDetail_MstStage");
            });

            modelBuilder.Entity<MstUserProfile>(entity =>
            {
                entity.ToTable("MstUserProfile");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.EncryptKey)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FkdeptId).HasColumnName("FKDeptID");

                entity.Property(e => e.FkdesignationId).HasColumnName("FKDesignationID");

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.FlgSuper).HasColumnName("flgSuper");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TokenValue)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UserCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PasswordReset>(entity =>
            {
                entity.ToTable("PasswordReset");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.EncryptKey).HasMaxLength(250);

                entity.Property(e => e.FlgActive).HasColumnName("flgActive");

                entity.Property(e => e.UserCode).HasMaxLength(250);
            });

            modelBuilder.Entity<TrnsDirectMaterial>(entity =>
            {
                entity.ToTable("TrnsDirectMaterial");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CostTypeDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyEr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CurrencyER");

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkcostTypeId).HasColumnName("FKCostTypeID");

                entity.Property(e => e.FkitemPriceListDocNum).HasColumnName("FKItemPriceListDocNum");

                entity.Property(e => e.ForAnalysis)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDept)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TotalImportCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.TotalLocalCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.TotalMaterialCost).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<TrnsDirectMaterialDetail>(entity =>
            {
                entity.ToTable("TrnsDirectMaterialDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountPkr)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("AmountPKR");

                entity.Property(e => e.ChangeConsQty).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ChangePricePkr)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("ChangePricePKR");

                entity.Property(e => e.ConsQty).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Lcfactor)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("LCFactor");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPriceFc)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("UnitPriceFC");

                entity.Property(e => e.UnitPricePkr)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("UnitPricePKR");

                entity.Property(e => e.Uom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UOM");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsDirectMaterialDetails)
                    .HasForeignKey(d => d.Fkid)
                    .HasConstraintName("FK_TrnsDirectMaterialDetail_TrnsDirectMaterial");
            });

            modelBuilder.Entity<TrnsFohcost>(entity =>
            {
                entity.ToTable("TrnsFOHCost");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lmonth)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LMonth");

                entity.Property(e => e.Lyear)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LYear");

                entity.Property(e => e.TotalCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.TotalFoh)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("TotalFOH");

                entity.Property(e => e.TotalVoh)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("TotalVOH");
            });

            modelBuilder.Entity<TrnsFohcostDetail>(entity =>
            {
                entity.ToTable("TrnsFOHCostDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AcctCode)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.AcctDescription)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CostDriver)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FkcostDriverTypeId).HasColumnName("FKCostDriverTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.Fohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("FOHAmount");

                entity.Property(e => e.Vohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("VOHAmount");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsFohcostDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsFOHCostDetail_TrnsFOHCost");
            });

            modelBuilder.Entity<TrnsFohdriverRate>(entity =>
            {
                entity.ToTable("TrnsFOHDriverRates");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CostDriver).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.DistributionCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lmonth)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LMonth");

                entity.Property(e => e.Lyear)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LYear");

                entity.Property(e => e.TotalCostDc)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("TotalCostDC");

                entity.Property(e => e.TotalDriverValue).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<TrnsFohdriverRatesDetail>(entity =>
            {
                entity.ToTable("TrnsFOHDriverRatesDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DriverValue).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FohdistributionCost)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("FOHDistributionCost");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsFohdriverRatesDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsFOHDriverRatesDetail_TrnsFOHDriverRates");
            });

            modelBuilder.Entity<TrnsFohrate>(entity =>
            {
                entity.ToTable("TrnsFOHRates");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkcostTypeId).HasColumnName("FKCostTypeID");

                entity.Property(e => e.Fohrate)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("FOHRate");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrnsFohratesDetail>(entity =>
            {
                entity.ToTable("TrnsFOHRatesDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkcostDriversTypeId).HasColumnName("FKCostDriversTypeID");

                entity.Property(e => e.FkcostDrivesTypeDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FKCostDrivesTypeDescription");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsFohratesDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsFOHRatesDetail_TrnsFOHRates");
            });

            modelBuilder.Entity<TrnsSalesQuotation>(entity =>
            {
                entity.ToTable("TrnsSalesQuotation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CostTypeDesc)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SalesPriceList)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrnsSalesQuotationDetail>(entity =>
            {
                entity.ToTable("TrnsSalesQuotationDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiesAndMolds)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Electricity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkdirectMaterialDocNum).HasColumnName("FKDirectMaterialDocNum");

                entity.Property(e => e.FkdirectMaterialId).HasColumnName("FKDirectMaterialID");

                entity.Property(e => e.FkfohdocNum).HasColumnName("FKFOHDocNum");

                entity.Property(e => e.Fkfohid).HasColumnName("FKFOHID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FkvohdocNum).HasColumnName("FKVOHDocNum");

                entity.Property(e => e.Fkvohid).HasColumnName("FKVOHID");

                entity.Property(e => e.Fohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("FOHAmount");

                entity.Property(e => e.FohratePer)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("FOHRatePer");

                entity.Property(e => e.Gasoline)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImportCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Labour)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocalCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Machine)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Margin).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.MarginPer).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.OtherFoh)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("OtherFOH");

                entity.Property(e => e.Others)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Packing)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDepartment)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Rmanalysis)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RMAnalysis");

                entity.Property(e => e.SellingPrice).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Tools)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalDirectCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.TotalFoh)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("TotalFOH");

                entity.Property(e => e.TotalRmcost)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("TotalRMCost");

                entity.Property(e => e.TotalUnitCost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.TotalVohcost)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("TotalVOHCost");

                entity.Property(e => e.Transportation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vohanalysis)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VOHAnalysis");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsSalesQuotationDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsSalesQuatationDetail_TrnsSalesQuotation");
            });

            modelBuilder.Entity<TrnsVoc>(entity =>
            {
                entity.ToTable("TrnsVOC");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CostTypeDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkcostTypeId).HasColumnName("FKCostTypeID");

                entity.Property(e => e.FkresourceDocNum).HasColumnName("FKResourceDocNum");

                entity.Property(e => e.FlgDefault).HasColumnName("flgDefault");

                entity.Property(e => e.ForAnalysis)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MonthlyVolume).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.NoOfShift).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.PerDayShiftHrs).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.WorkingDays).HasColumnType("numeric(18, 6)");
            });

            modelBuilder.Entity<TrnsVocactivityDetail>(entity =>
            {
                entity.ToTable("TrnsVOCActivityDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActualTimeCycle).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CycleTimeSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkactivityDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FKActivityDescription");

                entity.Property(e => e.FkactivityTypeId).HasColumnName("FKActivityTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.IncFactor).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVocactivityDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOCActivityDetail_TrnsVOC");
            });

            modelBuilder.Entity<TrnsVocdyesAndMoldDetail>(entity =>
            {
                entity.ToTable("TrnsVOCDyesAndMoldDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CycleTimeSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.DyesAndMold)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkactivityDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FKActivityDescription");

                entity.Property(e => e.FkactivityTypeId).HasColumnName("FKActivityTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FkresourceId).HasColumnName("FKResourceID");

                entity.Property(e => e.LifeUnit).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.NoOfWorkers).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Nos).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVocdyesAndMoldDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOCDyesAndMoldDetail_TrnsVOC");
            });

            modelBuilder.Entity<TrnsVocelectricityDetail>(entity =>
            {
                entity.ToTable("TrnsVOCElectricityDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActualKwperHrs)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("ActualKWPerHrs");

                entity.Property(e => e.CostPerSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CycleTimeSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkactivityDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FKActivityDescription");

                entity.Property(e => e.FkactivityTypeId).HasColumnName("FKActivityTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FkresourceId).HasColumnName("FKResourceID");

                entity.Property(e => e.IncFactor).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.KwperHrs)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("KWPerHrs");

                entity.Property(e => e.MachineType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NoOfMachine).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.NoOfWorkers).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.RatePerUnit).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVocelectricityDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOCElectricityDetail_TrnsVOC");
            });

            modelBuilder.Entity<TrnsVocgasolineDetail>(entity =>
            {
                entity.ToTable("TrnsVOCGasolineDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CycleTimeSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkactivityDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FKActivityDescription");

                entity.Property(e => e.FkactivityTypeId).HasColumnName("FKActivityTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FkresourceId).HasColumnName("FKResourceID");

                entity.Property(e => e.GasolineType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LifeUnit).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.NoOfWorkers).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Nos).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVocgasolineDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOCGasolineDetail_TrnsVOC");
            });

            modelBuilder.Entity<TrnsVoclaborDetail>(entity =>
            {
                entity.ToTable("TrnsVOCLaborDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CostPerSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CycleTimeSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkactivityDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FKActivityDescription");

                entity.Property(e => e.FkactivityTypeId).HasColumnName("FKActivityTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FklaborRateId).HasColumnName("FKLaborRateID");

                entity.Property(e => e.LaborDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NoOfWorkers).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.WageRate).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVoclaborDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOCLaborDetail_TrnsVOC");
            });

            modelBuilder.Entity<TrnsVocmachineDetail>(entity =>
            {
                entity.ToTable("TrnsVOCMachineDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CycleTimeSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkactivityDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FKActivityDescription");

                entity.Property(e => e.FkactivityTypeId).HasColumnName("FKActivityTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FkresourceId).HasColumnName("FKResourceID");

                entity.Property(e => e.LifeYears).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.MachineType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NoOfWorkers).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Nos).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVocmachineDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOCMachineDetail_TrnsVOC");
            });

            modelBuilder.Entity<TrnsVoctoolsDetail>(entity =>
            {
                entity.ToTable("TrnsVOCToolsDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.CycleTimeSec).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.FkactivityDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FKActivityDescription");

                entity.Property(e => e.FkactivityTypeId).HasColumnName("FKActivityTypeID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.FkresourceId).HasColumnName("FKResourceID");

                entity.Property(e => e.LifeUnit).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.NoOfWorkers).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.Nos).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.ToolsType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("numeric(18, 6)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVoctoolsDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOCToolsDetail_TrnsVOC");
            });

            modelBuilder.Entity<TrnsVoh>(entity =>
            {
                entity.ToTable("TrnsVOH");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DocApprovalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocDate).HasColumnType("date");

                entity.Property(e => e.DocStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lmonth)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LMonth");

                entity.Property(e => e.Lyear)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LYear");

                entity.Property(e => e.TotalTools).HasMaxLength(50);

                entity.Property(e => e.TotalVohElectriccity).HasMaxLength(50);

                entity.Property(e => e.TotalVohdyes)
                    .HasMaxLength(50)
                    .HasColumnName("TotalVOHDyes");

                entity.Property(e => e.TotalVohgasoline)
                    .HasMaxLength(50)
                    .HasColumnName("TotalVOHGasoline");

                entity.Property(e => e.TotalVohlabor)
                    .HasMaxLength(50)
                    .HasColumnName("TotalVOHLabor");

                entity.Property(e => e.TotalVohmachine)
                    .HasMaxLength(50)
                    .HasColumnName("TotalVOHMachine");
            });

            modelBuilder.Entity<TrnsVohdyesAndMoldDetail>(entity =>
            {
                entity.ToTable("TrnsVOHDyesAndMoldDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DyesAndMoldVohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("DyesAndMoldVOHAmount");

                entity.Property(e => e.DyesAndMoldVohrate)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("DyesAndMoldVOHRate");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductQuantity).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVohdyesAndMoldDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOHDyesAndMoldDetail_TrnsVOH");
            });

            modelBuilder.Entity<TrnsVohelectricityDetail>(entity =>
            {
                entity.ToTable("TrnsVOHElectricityDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ElectricityVohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("ElectricityVOHAmount");

                entity.Property(e => e.ElectricityVohrate)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("ElectricityVOHRate");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductQuantity).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVohelectricityDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOHElectricityDetail_TrnsVOH");
            });

            modelBuilder.Entity<TrnsVohgasolineDetail>(entity =>
            {
                entity.ToTable("TrnsVOHGasolineDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.GasolineVohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("GasolineVOHAmount");

                entity.Property(e => e.GasolineVohrate)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("GasolineVOHRate");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductQuantity).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVohgasolineDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOHGasolineDetail_TrnsVOH");
            });

            modelBuilder.Entity<TrnsVohlabourDetail>(entity =>
            {
                entity.ToTable("TrnsVOHLabourDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.LabourVohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("LabourVOHAmount");

                entity.Property(e => e.LabourVohrate)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("LabourVOHRate");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductQuantity).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVohlabourDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOHLabourDetail_TrnsVOH");
            });

            modelBuilder.Entity<TrnsVohmachineDetail>(entity =>
            {
                entity.ToTable("TrnsVOHMachineDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.MachineVohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("MachineVOHAmount");

                entity.Property(e => e.MachineVohrate)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("MachineVOHRate");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductQuantity).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVohmachineDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOHMachineDetail_TrnsVOH");
            });

            modelBuilder.Entity<TrnsVohtoolsDetail>(entity =>
            {
                entity.ToTable("TrnsVOHToolsDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fkid).HasColumnName("FKID");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductQuantity).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ToolsVohamount)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("ToolsVOHAmount");

                entity.Property(e => e.ToolsVohrate)
                    .HasColumnType("numeric(18, 6)")
                    .HasColumnName("ToolsVOHRate");

                entity.HasOne(d => d.Fk)
                    .WithMany(p => p.TrnsVohtoolsDetails)
                    .HasForeignKey(d => d.Fkid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrnsVOHToolsDetail_TrnsVOH");
            });

            modelBuilder.Entity<UserAuthorization>(entity =>
            {
                entity.ToTable("UserAuthorization");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAppStamp");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FkmenuId).HasColumnName("FKMenuID");

                entity.Property(e => e.FkuserId).HasColumnName("FKUserID");

                entity.Property(e => e.MenuLink)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UappStamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UAppStamp");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserDataAccess>(entity =>
            {
                entity.ToTable("UserDataAccess");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FkCostId).HasColumnName("FkCostID");

                entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

                entity.Property(e => e.FlgAccess).HasColumnName("flgAccess");

                entity.Property(e => e.FormName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
