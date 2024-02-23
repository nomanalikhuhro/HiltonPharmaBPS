using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PASSForm_BPS.Models;

public partial class PassDbContext : DbContext
{
    public PassDbContext()
    {
    }

    public PassDbContext(DbContextOptions<PassDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Approver> Approvers { get; set; }

    public virtual DbSet<BpsRequest> BpsRequests { get; set; }

    public virtual DbSet<BpsSalesrecord> BpsSalesrecords { get; set; }

    public virtual DbSet<Chemist> Chemists { get; set; }

    public virtual DbSet<DisterMapping> DisterMappings { get; set; }

    public virtual DbSet<DisMacMapping> DisMacMappings { get; set; }

    public virtual DbSet<Distributer> Distributers { get; set; }

    public virtual DbSet<Exceptionlog> Exceptionlogs { get; set; }

    public virtual DbSet<Hcpdetail> Hcpdetails { get; set; }

    public virtual DbSet<Hcphospital> Hcphospitals { get; set; }

    public virtual DbSet<Hcprequest> Hcprequests { get; set; }

    public virtual DbSet<Hospitaldetail> Hospitaldetails { get; set; }

    public virtual DbSet<Hspreqteam> Hspreqteams { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<MacChemMapping> MacChemMappings { get; set; }

    public virtual DbSet<Macrobrick> Macrobricks { get; set; }

    public virtual DbSet<MasterApproverHierarchy> MasterApproverHierarchies { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Passcategory> Passcategories { get; set; }

    public virtual DbSet<Passubcategory> Passubcategories { get; set; }

    public virtual DbSet<Pathtable> Pathtables { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Roleaccess> Roleaccesses { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Tblhcpterritorymapping> Tblhcpterritorymappings { get; set; }

    public virtual DbSet<Tblteamsmapping> Tblteamsmappings { get; set; }

    public virtual DbSet<Tblterritory> Tblterritories { get; set; }

    public virtual DbSet<Tblterritorymapping> Tblterritorymappings { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TerbrickMapping> TerbrickMappings { get; set; }

    public virtual DbSet<Uploadedfile> Uploadedfiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }
    public virtual DbSet<wf_activityinstance> Wf_Activityinstances { get; set; }
    public virtual DbSet<OutPutParameter> OutPutParameters { get; set; }
    public virtual DbSet<OutputParaMetersSales> OutputParaMetersSales { get; set; }
    public virtual DbSet<tblproduct> Tblproducts { get; set; }
    public virtual DbSet<wf_instance> Wf_Instances { get; set; }
    public virtual DbSet<wf_workflow> Wf_Workflows { get; set; }
    public virtual DbSet<wf_workflowactivity> Wf_Workflowactivities { get; set; }
    public virtual DbSet<wf_worklist> Wf_Worklists { get; set; }
    public virtual DbSet<wf_comments> Wf_Comments { get; set; }
    public virtual DbSet<wf_uploadfilespath> Wf_Uploadfilespaths { get; set; }



    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //    => optionsBuilder.UseMySQL("Server=localhost;port=3307;user=root;Database=pass_db;convert zero datetime=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Approver>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PRIMARY");

            entity.ToTable("approvers");

            entity.HasIndex(e => e.EmpId, "fk_empid");

            entity.Property(e => e.RecordId)
                .HasColumnType("int(11)")
                .HasColumnName("Record_ID");
            entity.Property(e => e.ApproverOrder)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("Approver_Order");
            entity.Property(e => e.EmpId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.UserRole)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("User_Role");

            entity.HasOne(d => d.Emp).WithMany(p => p.Approvers)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_empid");
        });

        modelBuilder.Entity<BpsRequest>(entity =>
        {
            entity.HasKey(e => e.BpsRecordId).HasName("PRIMARY");

            entity.ToTable("bps_request");

            entity.HasIndex(e => e.DistributerCode, "DistributerCode");

            entity.HasIndex(e => e.TrackingId, "TrackingID");

            entity.HasIndex(e => e.TrackingId, "TrackingID_idx");

            entity.HasIndex(e => e.StatusId, "fk12_status123_id");

            entity.HasIndex(e => e.MacroBrickCode, "fk_macrecbrick_code");

            entity.Property(e => e.BpsRecordId)
                .HasColumnType("int(11)")
                .HasColumnName("BPS_Record_ID");
            entity.Property(e => e.ApproverOrder)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("Approver_Order");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentApproverEmpId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("Current_Approver_Emp_ID");
            entity.Property(e => e.DistributerCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.FromDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date");
            entity.Property(e => e.Hcpreqid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("HCPREQID");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.MacroBrickCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("Status_ID");
            entity.Property(e => e.ToDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date");
            entity.Property(e => e.TrackingId)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("TrackingID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.PONumber)
    .HasMaxLength(1000)
    .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ActivityStatus)
.HasMaxLength(100)
.HasDefaultValueSql("'NULL'");
            entity.HasOne(d => d.DistributerCodeNavigation).WithMany(p => p.BpsRequests)
                .HasForeignKey(d => d.DistributerCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("DistributerCode");

            entity.HasOne(d => d.MacroBrickCodeNavigation).WithMany(p => p.BpsRequests)
                .HasForeignKey(d => d.MacroBrickCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_macrecbrick_code");

            entity.HasOne(d => d.Status).WithMany(p => p.BpsRequests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk12_status123_id");

            entity.Property(e => e.Comments)
 .HasDefaultValueSql("'NULL'")
    .HasColumnType("LONGTEXT");

        });

        modelBuilder.Entity<BpsSalesrecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PRIMARY");

            entity.ToTable("bps_salesrecord");

            entity.HasIndex(e => e.BpsRecordId, "fk_bpssale_code");

            entity.HasIndex(e => e.ChemistCode, "fk_chem_code");

            entity.Property(e => e.RecordId)
                .HasColumnType("int(11)")
                .HasColumnName("Record_ID");
            entity.Property(e => e.ActualDiscount)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.BpsRecordId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("BPS_Record_ID");
            entity.Property(e => e.ChemistCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("Chemist_Code");
            entity.Property(e => e.Contribution)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CostCenter)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Month)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PackCode)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PostFromdate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("Post_Fromdate");
            entity.Property(e => e.PostTodate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("Post_Todate");
            entity.Property(e => e.PostTotalSal)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PreFromdate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("Pre_Fromdate");
            entity.Property(e => e.PreTodate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("Pre_Todate");
            entity.Property(e => e.PreTotalSal)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Roi)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ROI");
            entity.Property(e => e.SalesSku)
                .HasPrecision(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("Sales_Sku");
            entity.Property(e => e.SalesType)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.SalesValue)
                .HasPrecision(10)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("Sales_Value");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Year)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");



            entity.Property(e => e.DiscountPercentagePre)
    .HasMaxLength(200)
    .HasDefaultValueSql("'NULL'")
    .HasColumnName("DiscountPercentagePre");

            entity.Property(e => e.DiscountPercentagePost)
.HasMaxLength(200)
.HasDefaultValueSql("'NULL'")
.HasColumnName("DiscountPercentagePost");

            entity.Property(e => e.TotalWithoutDiscount)
.HasMaxLength(200)
.HasDefaultValueSql("'NULL'")
.HasColumnName("TotalWithoutDiscount");
            entity.Property(e => e.TotalWithDiscount)
.HasMaxLength(200)
.HasDefaultValueSql("'NULL'")
.HasColumnName("TotalWithDiscount");


            entity.Property(e => e.TotalRoiPercentage)
.HasMaxLength(200)
.HasDefaultValueSql("'NULL'")
.HasColumnName("TotalRoiPercentage");

            entity.Property(e => e.BPSPercentage)
.HasMaxLength(200)
.HasDefaultValueSql("'NULL'")
.HasColumnName("BPSPercentage");
        });

        modelBuilder.Entity<Chemist>(entity =>
        {
            entity.HasKey(e => e.ChemistCode).HasName("PRIMARY");

            entity.ToTable("chemist");

            entity.Property(e => e.ChemistCode)
                .HasMaxLength(200)
                .HasColumnName("Chemist_Code");
            entity.Property(e => e.ChemistName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("isActive");
        });

        modelBuilder.Entity<DisterMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dister_mapping");

            entity.HasIndex(e => e.DistributerCode, "fk_distcode");

            entity.HasIndex(e => e.TerritoryCode, "fk_tercode");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.DistributerCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TerritoryCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.DistributerCodeNavigation).WithMany(p => p.DisterMappings)
                .HasForeignKey(d => d.DistributerCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_distcode");

            entity.HasOne(d => d.TerritoryCodeNavigation).WithMany(p => p.DisterMappings)
                .HasForeignKey(d => d.TerritoryCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_tercode");
        });

        modelBuilder.Entity<DisMacMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dismac_mapping");

            entity.Property(e => e.DistributorCode)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'");

            entity.Property(e => e.MacrobrickCode)
    .HasMaxLength(45)
    .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Distributer>(entity =>
        {
            entity.HasKey(e => e.DistributerCode).HasName("PRIMARY");

            entity.ToTable("distributer");

            entity.Property(e => e.DistributerCode).HasMaxLength(200);
            entity.Property(e => e.DistrubiterName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Exceptionlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("exceptionlogs");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Exception).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LogEvent)
                .HasMaxLength(64)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Hcpdetail>(entity =>
        {
            entity.HasKey(e => e.Hcpid).HasName("PRIMARY");

            entity.ToTable("hcpdetails");

            entity.Property(e => e.Hcpid)
                .HasColumnType("int(11)")
                .HasColumnName("HCPID");
            entity.Property(e => e.City)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Hcpname)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("HCPName");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Pmcnum)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("PMCNum");
            entity.Property(e => e.Speciality)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Hcphospital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("hcphospitals");

            entity.HasIndex(e => e.Hcpid, "fk_hcpid");

            entity.HasIndex(e => e.HospId, "fk_hospid");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Hcpid)
                .HasColumnType("int(11)")
                .HasColumnName("HCPID");
            entity.Property(e => e.HospId)
                .HasColumnType("int(11)")
                .HasColumnName("HospID");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Hcp).WithMany(p => p.Hcphospitals)
                .HasForeignKey(d => d.Hcpid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_hcpid");

            entity.HasOne(d => d.Hosp).WithMany(p => p.Hcphospitals)
                .HasForeignKey(d => d.HospId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_hospid");
        });

        modelBuilder.Entity<Hcprequest>(entity =>
        {
            entity.HasKey(e => new { e.Hcpreqid, e.TrackingId }).HasName("PRIMARY");

            entity.ToTable("hcprequest");

            entity.HasIndex(e => e.PasscategoryCode, "fk_cat_id");

            entity.HasIndex(e => e.Hcpid, "fk_hcp_id");

            entity.HasIndex(e => e.HospId, "fk_hosp_id");

            entity.Property(e => e.Hcpreqid)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("HCPREQID");
            entity.Property(e => e.TrackingId)
                .HasMaxLength(200)
                .HasColumnName("TrackingID");
            entity.Property(e => e.Asmcode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ASMCode");
            entity.Property(e => e.BaseArea)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.BeneficiaryName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Comments)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CostCenter)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentApproverEmpId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("Current_Approver_EMP_ID");
            entity.Property(e => e.DescriptionOfPlan)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.EstimatedSupport)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Hcpid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("HCPID");
            entity.Property(e => e.HospId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("HospID");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Ntnnumber)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("NTNNumber");
            entity.Property(e => e.PasscategoryCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("PASSCategoryCode");
            entity.Property(e => e.PassubCategoryCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("PASSubCategoryCode");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PendingTo)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PrevPlan)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("Status_ID");
            entity.Property(e => e.TeamId)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Tmcode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("TMCode");
            entity.Property(e => e.TmempNo)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("TMEmpNo");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.VendorDetails)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Hospitaldetail>(entity =>
        {
            entity.HasKey(e => e.HospId).HasName("PRIMARY");

            entity.ToTable("hospitaldetails");

            entity.Property(e => e.HospId)
                .HasColumnType("int(11)")
                .HasColumnName("HospID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.HospitalAddress)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.HospitalCity)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.HospitalName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Hspreqteam>(entity =>
        {
            entity.HasKey(e => e.HcpreqTeamId).HasName("PRIMARY");

            entity.ToTable("hspreqteam");

            entity.HasIndex(e => e.TeamCode, "FK_TeamCode1");

            entity.Property(e => e.HcpreqTeamId)
                .HasColumnType("int(11)")
                .HasColumnName("HCPREQ_Team_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Hcpreqid)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("HCPREQID");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TeamCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");

            entity.HasOne(d => d.TeamCodeNavigation).WithMany(p => p.Hspreqteams)
                .HasForeignKey(d => d.TeamCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TeamCode1");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.ApproverLogId).HasName("PRIMARY");

            entity.ToTable("logs");

            entity.Property(e => e.ApproverLogId)
                .HasColumnType("int(11)")
                .HasColumnName("Approver_Log_ID");
            entity.Property(e => e.ActivityBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("Activity_By");
            entity.Property(e => e.ActivityOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("Activity_On");
            entity.Property(e => e.ApprovalActivity)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("Approval_Activity");
            entity.Property(e => e.ApproverLog)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("Approver_Log");
            entity.Property(e => e.UserRole)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("User_Role");
        });

        modelBuilder.Entity<MacChemMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mac_chem_mapping");

            entity.HasIndex(e => e.ChemistCode, "fk_chemid");

            entity.HasIndex(e => e.MacroBrickCode, "fk_macid");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.ChemistCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("Chemist_Code");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("isActive");
            entity.Property(e => e.MacroBrickCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.ChemistCodeNavigation).WithMany(p => p.MacChemMappings)
                .HasForeignKey(d => d.ChemistCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_chemid");

            entity.HasOne(d => d.MacroBrickCodeNavigation).WithMany(p => p.MacChemMappings)
                .HasForeignKey(d => d.MacroBrickCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_macid");
        });

        modelBuilder.Entity<Macrobrick>(entity =>
        {
            entity.HasKey(e => e.MacroBrickCode).HasName("PRIMARY");

            entity.ToTable("macrobricks");

            entity.Property(e => e.MacroBrickCode).HasMaxLength(200);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.MacroBrickName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<MasterApproverHierarchy>(entity =>
        {
            entity.HasKey(e => e.HierarchyId).HasName("PRIMARY");

            entity.ToTable("master_approver_hierarchy");

            entity.Property(e => e.HierarchyId)
                .HasColumnType("int(11)")
                .HasColumnName("Hierarchy_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Roles)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.UserRole)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("User_Role");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PRIMARY");

            entity.ToTable("pages");

            entity.Property(e => e.PageId)
                .HasColumnType("int(11)")
                .HasColumnName("PageID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PageName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PagePath)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Passcategory>(entity =>
        {
            entity.HasKey(e => e.PasscategoryCode).HasName("PRIMARY");

            entity.ToTable("passcategory");

            entity.Property(e => e.PasscategoryCode)
                .HasMaxLength(200)
                .HasColumnName("PASSCategoryCode");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PasscategoryType)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("PASSCategoryType");
        });

        modelBuilder.Entity<Passubcategory>(entity =>
        {
            entity.HasKey(e => e.PassubCategoryCode).HasName("PRIMARY");

            entity.ToTable("passubcategory");

            entity.HasIndex(e => e.PasscategoryCode, "fk_catcode");

            entity.Property(e => e.PassubCategoryCode)
                .HasMaxLength(200)
                .HasColumnName("PASSubCategoryCode");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PasscategoryCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("PASSCategoryCode");
            entity.Property(e => e.PassubCategoryType)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("PASSubCategoryType");
        });

        modelBuilder.Entity<Pathtable>(entity =>
        {
            entity.HasKey(e => e.Pathid).HasName("PRIMARY");

            entity.ToTable("pathtable");

            entity.Property(e => e.Pathid)
                .HasColumnType("int(11)")
                .HasColumnName("pathid");
            entity.Property(e => e.Pathdescription)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("pathdescription");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionCode).HasName("PRIMARY");

            entity.ToTable("regions");

            entity.HasIndex(e => e.ZoneCode, "fk_zonecode");

            entity.Property(e => e.RegionCode).HasMaxLength(200);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("isActive");
            entity.Property(e => e.RegionName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.ZoneCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.ZoneCodeNavigation).WithMany(p => p.Regions)
                .HasForeignKey(d => d.ZoneCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_zonecode");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("RoleID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.RoleName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Roleaccess>(entity =>
        {
            entity.HasKey(e => e.RoleAccessId).HasName("PRIMARY");

            entity.ToTable("roleaccess");

            entity.HasIndex(e => e.PageId, "fk_pageid");

            entity.HasIndex(e => e.RoleId, "fk_roleid1");

            entity.Property(e => e.RoleAccessId)
                .HasColumnType("int(11)")
                .HasColumnName("RoleAccessID");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsInsert).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsUpdate).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.IsView).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PageId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("PageID");
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("RoleID");

            entity.HasOne(d => d.Page).WithMany(p => p.Roleaccesses)
                .HasForeignKey(d => d.PageId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pageid");

            entity.HasOne(d => d.Role).WithMany(p => p.Roleaccesses)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_roleid1");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PRIMARY");

            entity.ToTable("status");

            entity.Property(e => e.StatusId)
                .HasColumnType("int(11)")
                .HasColumnName("Status_ID");
            entity.Property(e => e.StatusType)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Tblhcpterritorymapping>(entity =>
        {
            entity.HasKey(e => e.HcpterritoryMappingId).HasName("PRIMARY");

            entity.ToTable("tblhcpterritorymappings");

            entity.HasIndex(e => e.Hcpid, "FK_hcpdetails");

            entity.HasIndex(e => e.TerritoryCode, "FK_terhcpmap");

            entity.Property(e => e.HcpterritoryMappingId)
                .HasColumnType("int(11)")
                .HasColumnName("HCPTerritoryMappingID");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Hcpid)
                .HasColumnType("int(11)")
                .HasColumnName("HCPID");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)");
            entity.Property(e => e.LastModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.LastModifiedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.TerritoryCode).HasMaxLength(20);

            entity.HasOne(d => d.Hcp).WithMany(p => p.Tblhcpterritorymappings)
                .HasForeignKey(d => d.Hcpid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_12_59");

            entity.HasOne(d => d.TerritoryCodeNavigation).WithMany(p => p.Tblhcpterritorymappings)
                .HasForeignKey(d => d.TerritoryCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_12_89");
        });

        modelBuilder.Entity<Tblteamsmapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tblteamsmappings");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.EmpId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.IsDeletedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IsDeletedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.TeamCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Tblterritory>(entity =>
        {
            entity.HasKey(e => e.TerritoryCode).HasName("PRIMARY");

            entity.ToTable("tblterritories");

            entity.Property(e => e.TerritoryCode).HasMaxLength(20);
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");
            entity.Property(e => e.LastModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.LastModifiedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Tblterritorymapping>(entity =>
        {
            entity.HasKey(e => e.TerritoryMappingId).HasName("PRIMARY");

            entity.ToTable("tblterritorymappings");

            entity.HasIndex(e => e.EmpId, "FK_emp");

            entity.HasIndex(e => e.RoleId, "FK_roleids");

            entity.HasIndex(e => e.TerritoryCode, "FK_termap");

            entity.Property(e => e.TerritoryMappingId)
                .HasColumnType("int(11)")
                .HasColumnName("TerritoryMappingID");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.EmpId)
                .HasColumnType("int(11)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");
            entity.Property(e => e.LastModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.LastModifiedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("RoleID");
            entity.Property(e => e.TeamCode)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TerritoryCode).HasMaxLength(20);

            entity.HasOne(d => d.Emp).WithMany(p => p.Tblterritorymappings)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_12_10");

            entity.HasOne(d => d.Role).WithMany(p => p.Tblterritorymappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_12_17");

            entity.HasOne(d => d.TerritoryCodeNavigation).WithMany(p => p.Tblterritorymappings)
                .HasForeignKey(d => d.TerritoryCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_12_8");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamCode).HasName("PRIMARY");

            entity.ToTable("teams");

            entity.Property(e => e.TeamCode).HasMaxLength(200);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)");
            entity.Property(e => e.TeamName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TerbrickMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("terbrick_mapping");

            entity.HasIndex(e => e.MacroBrickCode, "fk_brick_code");

            entity.HasIndex(e => e.TerritoryCode, "fk_ter_code");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.MacroBrickCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TerritoryCode)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MacroBrickCodeNavigation).WithMany(p => p.TerbrickMappings)
                .HasForeignKey(d => d.MacroBrickCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_brick_code");

            entity.HasOne(d => d.TerritoryCodeNavigation).WithMany(p => p.TerbrickMappings)
                .HasForeignKey(d => d.TerritoryCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ter_code");
        });

        modelBuilder.Entity<Uploadedfile>(entity =>
        {
            entity.HasKey(e => e.Fileid).HasName("PRIMARY");

            entity.ToTable("uploadedfile");

            entity.Property(e => e.Fileid)
                .HasColumnType("int(11)")
                .HasColumnName("fileid");
            entity.Property(e => e.TrackingId)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.filename)
                .HasMaxLength(1000)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.filepath)
                .HasMaxLength(500)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedOn)
    .HasDefaultValueSql("'NULL'")
    .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("int")
                .HasColumnName("IsDeleted");
            entity.Property(e => e.IsDeletedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeletedBy)
                .HasColumnType("int")
                .HasColumnName("IsDeletedBy");
            entity.Property(e => e.HCPREQID)
                .HasColumnType("int")
                .HasColumnName("HCPREQID"); 
            entity.Property(e => e.BPSID)
                .HasColumnType("int")
                .HasColumnName("BPSID");
            entity.Property(e => e.Filetype)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'"); 
            entity.Property(e => e.CategoryCode)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ActivityBy)
                .HasColumnType("int")
                .HasColumnName("ActivityBy"); 
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.RoleId, "fk_roleid");

            entity.Property(e => e.EmpId)
                .HasColumnType("int(11)")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LoginId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("LoginID");
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("RoleID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("User_Email");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("User_Name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ReportingTo)
    .HasDefaultValueSql("'NULL'")
    .HasColumnType("int(25)")
    .HasColumnName("ReportingTo");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_roleid");
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.ZoneCode).HasName("PRIMARY");

            entity.ToTable("zones");

            entity.Property(e => e.ZoneCode).HasMaxLength(200);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("isActive");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.ZoneName)
                .HasMaxLength(200)
                .HasDefaultValueSql("'NULL'");
        });


        modelBuilder.Entity<OutPutParameter>(entity =>
        {
            entity.HasKey(e => e.BPS_Record_ID).HasName("PRIMARY");

            entity.ToTable("Bpsrequest");

        });
        
        modelBuilder.Entity<OutputParaMetersSales>(entity =>
        {
            entity.HasKey(e => e.Record_ID).HasName("PRIMARY");

            entity.ToTable("Bpssalesrecord");

        });



        modelBuilder.Entity<tblproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tblproduct");

           
            entity.Property(e => e.TeamCode)
                .HasMaxLength(45)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TeamName)
              .HasMaxLength(45)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PackCode)
              .HasMaxLength(250)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Description)
              .HasMaxLength(500)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ProductName)
              .HasMaxLength(500)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UnitPrice)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DECIMAL(18,2)");
            entity.Property(e => e.IsDeleted)
             .HasDefaultValueSql("'0'")
                .HasColumnType("BIT(1)");
            entity.Property(e => e.DeletedOn)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");
            entity.Property(e => e.DeletedBy)
            .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.CreatedOn)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");
            entity.Property(e => e.CreatedBy)
            .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");

        });

        modelBuilder.Entity<wf_activityinstance>(entity =>
        {
            entity.HasKey(e => e.WFActivityInstanceId).HasName("PRIMARY");

            entity.ToTable("wf_activityinstance");


            entity.Property(e => e.WFInstanceId)
            .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.WorkflowActivityId)
            .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.Status)
            .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.StartDate)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");
            entity.Property(e => e.FinishDate)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");
           
        });

        modelBuilder.Entity<wf_instance>(entity =>
        {
            entity.HasKey(e => e.WFInstanceId).HasName("PRIMARY");

            entity.ToTable("wf_instance");


            entity.Property(e => e.WorkflowId)
            .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.HcpReqId)
            .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.TrackingID)
              .HasMaxLength(100)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Status)
            .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.CreatedBy)
.HasDefaultValueSql("'NULL'")
    .HasColumnType("INT");
            entity.Property(e => e.CreatedOn)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");
            entity.Property(e => e.BpsId)
              .HasMaxLength(255)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.StartDate)
 .HasDefaultValueSql("'NULL'")
    .HasColumnType("DATETIME");
            entity.Property(e => e.FinishDate)
 .HasDefaultValueSql("'NULL'")
    .HasColumnType("DATETIME");

        });

        modelBuilder.Entity<wf_workflow>(entity =>
        {
            entity.HasKey(e => e.WorkfFowId).HasName("PRIMARY");

            entity.ToTable("wf_workflow");


            entity.Property(e => e.WorkFlowName)
              .HasMaxLength(500)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Createdby)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.Updateby)
                 .HasDefaultValueSql("'NULL'")
    .HasColumnType("INT");
            entity.Property(e => e.Updatedon)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");
           

        });

        modelBuilder.Entity<wf_workflowactivity>(entity =>
        {
            entity.HasKey(e => e.WorkflowActivityId).HasName("PRIMARY");

            entity.ToTable("wf_workflowactivity");

            entity.Property(e => e.WorkflowId)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.WorkflowActivityName)
              .HasMaxLength(250)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ActionRule)
              .HasMaxLength(5000)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Craetedby)
                 .HasDefaultValueSql("'NULL'")
    .HasColumnType("INT");
            entity.Property(e => e.Createdon)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");
        });

        modelBuilder.Entity<wf_worklist>(entity =>
        {
            entity.HasKey(e => e.WFWorklistId).HasName("PRIMARY");

            entity.ToTable("wf_worklist");

            entity.Property(e => e.WFActivityInstanceId)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.Destination)
              .HasMaxLength(45)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Action)
              .HasMaxLength(45)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ActionBy)
                 .HasDefaultValueSql("'NULL'")
    .HasColumnType("INT");
            entity.Property(e => e.Status)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.StartDate)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");
            entity.Property(e => e.FinishDate)
 .HasDefaultValueSql("'NULL'")
    .HasColumnType("DATETIME");
        });

        modelBuilder.Entity<wf_comments>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("wf_comments");

            entity.Property(e => e.WFWorklistId)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.Comments)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("LONGTEXT");
            entity.Property(e => e.UploadFilePath)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("LONGTEXT");
            entity.Property(e => e.Createdby)
                 .HasDefaultValueSql("'NULL'")
    .HasColumnType("INT");
            entity.Property(e => e.TrackingId)
              .HasMaxLength(100)
              .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Createdon)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("DATETIME");

        });

        modelBuilder.Entity<wf_uploadfilespath>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("wf_uploadfilespath");

            entity.Property(e => e.WFWorklistId)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("INT");
            entity.Property(e => e.UploadFilePath)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("LONGTEXT");
            entity.Property(e => e.FileName)
             .HasDefaultValueSql("'NULL'")
                .HasColumnType("LONGTEXT");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
