using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PASSForm_BPS.Models;

public partial class TestSalesDbContext : DbContext
{
    public TestSalesDbContext()
    {
    }

    public TestSalesDbContext(DbContextOptions<TestSalesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DsrHiltonDailySalesTeamToChemist202223> DsrHiltonDailySalesTeamToChemist202223s { get; set; }

    public virtual DbSet<MacroMicroBrickMaping> MacroMicroBrickMapings { get; set; }
    public virtual DbSet<PreeAccordionModel> PreeAccordionModels { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=192.168.10.6;Database=TestSalesDB;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DsrHiltonDailySalesTeamToChemist202223>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DSR_HiltonDailySales_TeamToChemist2022-23");

            //entity.Property(e => e.ChemistName).HasMaxLength(200);
            //entity.Property(e => e.ClientCode).HasMaxLength(50);
            //entity.Property(e => e.ClientType).HasMaxLength(50);
            // entity.Property(e => e.Date).HasColumnType("smalldatetime");
            entity.Property(e => e.Description).HasMaxLength(50);
            //entity.Property(e => e.DistributorCode).HasMaxLength(50);
            //entity.Property(e => e.DistributorName).HasMaxLength(200);
            //entity.Property(e => e.LastUpdateDate)
            //    .HasDefaultValueSql("(getdate())")
            //    .HasColumnType("datetime");
            //entity.Property(e => e.MicroBrickCode).HasMaxLength(50);
            //entity.Property(e => e.MicroBrickName).HasMaxLength(100);
            entity.Property(e => e.PackCode).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(50);
            //entity.Property(e => e.RegionCode).HasMaxLength(50);
            //entity.Property(e => e.RegionName).HasMaxLength(50);
            //entity.Property(e => e.SalesUnits)
            //    .HasColumnType("numeric(18, 2)")
            //    .HasColumnName("Sales-Units");
            //entity.Property(e => e.SalesValueDp)
            //    .HasColumnType("numeric(18, 2)")
            //    .HasColumnName("Sales-ValueDP");
            //entity.Property(e => e.SalesValueNp)
            //    .HasColumnType("numeric(18, 2)")
            //    .HasColumnName("Sales-ValueNP");
            //entity.Property(e => e.SalesValueTp)
            //    .HasColumnType("numeric(18, 2)")
            //    .HasColumnName("Sales-ValueTP");
            //entity.Property(e => e.TeamCode).HasMaxLength(50);
            //entity.Property(e => e.TeamName).HasMaxLength(50);
            //entity.Property(e => e.TerritoryCode).HasMaxLength(50);
            //entity.Property(e => e.TerritoryName).HasMaxLength(50);
            //entity.Property(e => e.ZoneCode).HasMaxLength(50);
            //entity.Property(e => e.ZoneName).HasMaxLength(50);
        });





        modelBuilder.Entity<MacroMicroBrickMaping>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MacroMicroBrickMaping");

            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.DesigCode).HasMaxLength(50);
            entity.Property(e => e.DistCode)
                .HasMaxLength(50)
                .HasColumnName("Dist.Code");
            entity.Property(e => e.DistributorName).HasMaxLength(250);
            entity.Property(e => e.DsrdesigName)
                .HasMaxLength(50)
                .HasColumnName("DSRDesigName");
            entity.Property(e => e.MajorBrickName).HasMaxLength(200);
            entity.Property(e => e.MinorBrickCode).HasMaxLength(50);
            entity.Property(e => e.MinorBrickName).HasMaxLength(200);
            entity.Property(e => e.Mreptcode)
                .HasMaxLength(100)
                .HasColumnName("MREPTcode");
            entity.Property(e => e.Person).HasMaxLength(250);
            entity.Property(e => e.RegionCode).HasMaxLength(50);
            entity.Property(e => e.RegionName).HasMaxLength(150);
            entity.Property(e => e.Share).HasMaxLength(50);
            entity.Property(e => e.ZoneCode).HasMaxLength(50);
            entity.Property(e => e.ZoneName).HasMaxLength(150);
        });
        modelBuilder.Entity<PreeAccordionModel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PreeAccordionModel");




            //entity.Property(e => e.PackCode).HasMaxLength(50);
            //entity.Property(e => e.ProductName).HasMaxLength(50);
            //entity.Property(e => e.SalesUnits)
            //    .HasColumnType("numeric(18, 2)")
            //    .HasColumnName("Sales-Units");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
