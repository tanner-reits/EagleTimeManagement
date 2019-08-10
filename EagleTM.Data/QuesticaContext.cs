using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EagleTM.Data
{
    public partial class QuesticaContext : DbContext
    {
        public QuesticaContext()
        {
        }

        public QuesticaContext(DbContextOptions<QuesticaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<TblProjects> TblProjects { get; set; }
        public virtual DbSet<TblSpec> TblSpec { get; set; }
        public virtual DbSet<TblTempTimecards> TblTempTimecards { get; set; }
        public virtual DbSet<TblTimecardHeader> TblTimecardHeader { get; set; }
        public virtual DbSet<TblTimecards> TblTimecards { get; set; }
        public virtual DbSet<TlkpHourTypes> TlkpHourTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("tblEmployee");

                entity.HasIndex(e => e.EmpNumber)
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .IsUnique();

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EmpActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EmpBaseHourlyCost).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.EmpDept).HasMaxLength(50);

                entity.Property(e => e.EmpEmail).HasMaxLength(50);

                entity.Property(e => e.EmpFirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmpLastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmpNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.EmpTypeId).HasColumnName("EmpTypeID");

                entity.Property(e => e.EmployeeCustom1)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmployeeCustom2)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmployeeCustom3).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.EmployeeCustom4).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.EmployeeCustom5).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCustom6).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasDefaultValueSql("(0xED03A066A998EBC1A16E61B2610D337C95980B71816ED1D2B1)");

                entity.Property(e => e.PayGroupId).HasColumnName("PayGroupID");

                entity.Property(e => e.QbaccessCompanyId).HasColumnName("QBAccessCompanyID");

                entity.Property(e => e.QbaccessSecretCode).HasColumnName("QBAccessSecretCode");

                entity.Property(e => e.QbaccessToken).HasColumnName("QBAccessToken");

                entity.Property(e => e.RequirePasswordChange)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("UserID")
                    .HasMaxLength(30);

                entity.Property(e => e.UserName)
                    .HasMaxLength(15)
                    .HasComputedColumnSql("('TETO'+CONVERT([nvarchar](11),[EmployeeID],(0)))");
            });

            modelBuilder.Entity<TblProjects>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("tblProjects");

                entity.HasIndex(e => e.ProjectAutoId)
                    .IsUnique();

                entity.HasIndex(e => e.PsalesId);

                entity.Property(e => e.ProjectId)
                    .HasColumnName("ProjectID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Pactive).HasColumnName("PActive");

                entity.Property(e => e.Pdelivery)
                    .HasColumnName("PDelivery")
                    .HasColumnType("datetime");

                entity.Property(e => e.Pdescription)
                    .IsRequired()
                    .HasColumnName("PDescription")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PercentComplete).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.PercentCompleteDate).HasColumnType("datetime");

                entity.Property(e => e.Pfirst)
                    .HasColumnName("PFirst")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(CONVERT([datetime],CONVERT([char](8),getdate(),(112)),(0)))");

                entity.Property(e => e.Plast)
                    .HasColumnName("PLast")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(CONVERT([datetime],CONVERT([char](8),getdate(),(112)),(0)))");

                entity.Property(e => e.PmanagerId).HasColumnName("PManagerID");

                entity.Property(e => e.Pnext)
                    .HasColumnName("PNext")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(CONVERT([datetime],CONVERT([char](8),getdate(),(112)),(0)))");

                entity.Property(e => e.Ppriority).HasColumnName("PPriority");

                entity.Property(e => e.Preference).HasColumnName("PReference");

                entity.Property(e => e.ProjectAlternate).HasMaxLength(50);

                entity.Property(e => e.ProjectAutoId)
                    .HasColumnName("ProjectAutoID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProjectCustom1)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProjectCustom2)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProjectCustom3).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.ProjectCustom4).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.ProjectCustom5).HasColumnType("datetime");

                entity.Property(e => e.ProjectCustom6).HasColumnType("datetime");

                entity.Property(e => e.PsaleCurr)
                    .IsRequired()
                    .HasColumnName("PSaleCurr")
                    .HasMaxLength(3);

                entity.Property(e => e.PsaleCurrRate)
                    .HasColumnName("PSaleCurrRate")
                    .HasColumnType("decimal(20, 6)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PsalesId).HasColumnName("PSalesID");

                entity.Property(e => e.Pstatus)
                    .HasColumnName("PStatus")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Pmanager)
                    .WithMany(p => p.TblProjectsPmanager)
                    .HasForeignKey(d => d.PmanagerId)
                    .HasConstraintName("FK_tblProjects_tblEmployee_Manager");

                entity.HasOne(d => d.Psales)
                    .WithMany(p => p.TblProjectsPsales)
                    .HasForeignKey(d => d.PsalesId)
                    .HasConstraintName("FK_tblProjects_tblEmployee_SalesPerson");
            });

            modelBuilder.Entity<TblSpec>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.SpecId });

                entity.ToTable("tblSpec");

                entity.HasIndex(e => e.SpecAutoId)
                    .IsUnique();

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.SpecId).HasColumnName("SpecID");

                entity.Property(e => e.Archived).HasDefaultValueSql("((0))");

                entity.Property(e => e.BudgetAccRelease).HasColumnType("datetime");

                entity.Property(e => e.BudgetClosingRelease).HasColumnType("datetime");

                entity.Property(e => e.BudgetEngPreRelease).HasColumnType("datetime");

                entity.Property(e => e.BudgetEngRelease).HasColumnType("datetime");

                entity.Property(e => e.BudgetMfgRelease).HasColumnType("datetime");

                entity.Property(e => e.BudgetShipRelease).HasColumnType("datetime");

                entity.Property(e => e.Hierarchy).HasMaxLength(2000);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.MachineCustom1)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineCustom2)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MachineCustom3).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.MachineCustom4).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.MachineCustom5).HasColumnType("datetime");

                entity.Property(e => e.MachineCustom6).HasColumnType("datetime");

                entity.Property(e => e.MfgBegin).HasColumnType("datetime");

                entity.Property(e => e.ParentSpecAutoId).HasColumnName("ParentSpecAutoID");

                entity.Property(e => e.PercentComplete).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.PercentCompleteDate).HasColumnType("datetime");

                entity.Property(e => e.RevisedEngRelease).HasColumnType("datetime");

                entity.Property(e => e.RevisedMfgRelease).HasColumnType("datetime");

                entity.Property(e => e.Sactive).HasColumnName("SActive");

                entity.Property(e => e.SalesPrice).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.SamountInvoicedtoDate)
                    .HasColumnName("SAmountInvoicedtoDate")
                    .HasColumnType("decimal(20, 6)");

                entity.Property(e => e.Scomments)
                    .IsRequired()
                    .HasColumnName("SComments")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Sdate)
                    .HasColumnName("SDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(CONVERT([datetime],CONVERT([char](8),getdate(),(112)),0))");

                entity.Property(e => e.Sdescription)
                    .IsRequired()
                    .HasColumnName("SDescription")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SfinalBillingComplete)
                    .HasColumnName("SFinalBillingComplete")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sinternal)
                    .HasColumnName("SInternal")
                    .HasMaxLength(50);

                entity.Property(e => e.SpecAutoId)
                    .HasColumnName("SpecAutoID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Squantity)
                    .HasColumnName("SQuantity")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SrefProjectId).HasColumnName("SRefProjectID");

                entity.Property(e => e.SrefSpecId).HasColumnName("SRefSpecID");

                entity.Property(e => e.SrevDate)
                    .HasColumnName("SRevDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Srevision)
                    .HasColumnName("SRevision")
                    .HasMaxLength(25)
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EngineerNavigation)
                    .WithMany(p => p.TblSpec)
                    .HasForeignKey(d => d.Engineer)
                    .HasConstraintName("FK_tblSpec_tblEmployee");

                entity.HasOne(d => d.ParentSpecAuto)
                    .WithMany(p => p.InverseParentSpecAuto)
                    .HasPrincipalKey(p => p.SpecAutoId)
                    .HasForeignKey(d => d.ParentSpecAutoId)
                    .HasConstraintName("FK_tblSpec_tblSpec");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TblSpec)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSpec_tblProjects");
            });

            modelBuilder.Entity<TblTempTimecards>(entity =>
            {
                entity.HasKey(e => e.TempCardId)
                    .HasName("PK__tblTempT__51E0F8DEC4038228");

                entity.ToTable("tblTempTimecards");

                entity.Property(e => e.TempCardId).HasColumnName("TempCardID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EditedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.HourTime).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.SpecId).HasColumnName("SpecID");

                entity.Property(e => e.TimeDate).HasColumnType("datetime");

                entity.Property(e => e.TimeId).HasColumnName("TimeID");

                entity.Property(e => e.TimePeriodId).HasColumnName("TimePeriodID");

                entity.HasOne(d => d.HourTypeNavigation)
                    .WithMany(p => p.TblTempTimecards)
                    .HasForeignKey(d => d.HourType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblTempTi__HourT__006D4D87");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TblTempTimecards)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblTempTi__Proje__7F79294E");

                entity.HasOne(d => d.Time)
                    .WithMany(p => p.TblTempTimecards)
                    .HasForeignKey(d => d.TimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblTempTi__TimeI__7E850515");
            });

            modelBuilder.Entity<TblTimecardHeader>(entity =>
            {
                entity.HasKey(e => e.TimePeriodId);

                entity.ToTable("tblTimecardHeader");

                entity.HasIndex(e => e.PayPeriodEndDate)
                    .IsUnique();

                entity.HasIndex(e => e.PayPeriodStartDate)
                    .IsUnique();

                entity.HasIndex(e => new { e.PayPeriodStartDate, e.PayPeriodEndDate })
                    .IsUnique();

                entity.Property(e => e.TimePeriodId).HasColumnName("TimePeriodID");

                entity.Property(e => e.Amount).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.PayPeriodEndDate).HasColumnType("datetime");

                entity.Property(e => e.PayPeriodStartDate).HasColumnType("datetime");

                entity.Property(e => e.PostedDate).HasColumnType("datetime");

                entity.Property(e => e.RefDescription).HasMaxLength(50);

                entity.Property(e => e.Reference).HasMaxLength(50);
            });

            modelBuilder.Entity<TblTimecards>(entity =>
            {
                entity.HasKey(e => e.TimeId);

                entity.ToTable("tblTimecards");

                entity.HasIndex(e => e.TimeDate);

                entity.HasIndex(e => new { e.ProjectId, e.SpecId });

                entity.Property(e => e.TimeId).HasColumnName("TimeID");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmpNumber).HasMaxLength(10);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.HourClass)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Regular')");

                entity.Property(e => e.HourDrawing).HasMaxLength(50);

                entity.Property(e => e.HourFactor)
                    .HasColumnType("decimal(20, 6)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.HourRate).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.HourTime).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.NonConformanceId).HasColumnName("NonConformanceID");

                entity.Property(e => e.ProcessScheduleDetailId).HasColumnName("ProcessScheduleDetailID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.SpecId).HasColumnName("SpecID");

                entity.Property(e => e.TimeDate).HasColumnType("datetime");

                entity.Property(e => e.TimePeriodId).HasColumnName("TimePeriodID");

                entity.Property(e => e.TimecardCustom1)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TimecardCustom2)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TimecardCustom3).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.TimecardCustom4).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.TimecardCustom5).HasColumnType("datetime");

                entity.Property(e => e.TimecardCustom6).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TblTimecards)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTimecards_tblEmployee");

                entity.HasOne(d => d.HourTypeNavigation)
                    .WithMany(p => p.TblTimecards)
                    .HasForeignKey(d => d.HourType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTimecards_tlkpHourTypes");

                entity.HasOne(d => d.TimePeriod)
                    .WithMany(p => p.TblTimecards)
                    .HasForeignKey(d => d.TimePeriodId)
                    .HasConstraintName("FK_tblTimecards_tblTimecardHeader");

                entity.HasOne(d => d.TblSpec)
                    .WithMany(p => p.TblTimecards)
                    .HasForeignKey(d => new { d.ProjectId, d.SpecId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblTimecards_tblSpec");
            });

            modelBuilder.Entity<TlkpHourTypes>(entity =>
            {
                entity.HasKey(e => e.HourType);

                entity.ToTable("tlkpHourTypes");

                entity.HasIndex(e => e.HourDepartment);

                entity.Property(e => e.HourType).ValueGeneratedNever();

                entity.Property(e => e.EarningNumber).HasMaxLength(50);

                entity.Property(e => e.Exported)
                    .IsRequired()
                    .HasDefaultValueSql("(1)");

                entity.Property(e => e.Glaccount)
                    .HasColumnName("GLAccount")
                    .HasMaxLength(50);

                entity.Property(e => e.Glid).HasColumnName("GLID");

                entity.Property(e => e.HourActive)
                    .IsRequired()
                    .HasDefaultValueSql("(1)");

                entity.Property(e => e.HourClass)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.HourCost).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.HourDepartment)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HourDescription)
                    .IsRequired()
                    .HasMaxLength(75);
            });
        }
    }
}
