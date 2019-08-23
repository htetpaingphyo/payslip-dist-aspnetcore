using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApolloPayslipDist.Models
{
    public partial class MainDbContext : DbContext
    {
        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<ExpatPaySlip> ExpatPaySlip { get; set; }
        public virtual DbSet<LocalPaySlip> LocalPaySlip { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId).ValueGeneratedNever();

                entity.Property(e => e.AccountEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountPassword)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AccountSalt)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExpatPaySlip>(entity =>
            {
                entity.Property(e => e.ExpatPaySlipId).ValueGeneratedNever();

                entity.Property(e => e.BaseSalaryMmk)
                    .HasColumnName("BaseSalary_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Designation).HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpenseClaimsUsd)
                    .HasColumnName("ExpenseClaims_USD")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FxRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GrossTaxableIncomeMmk)
                    .HasColumnName("GrossTaxableIncome_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HousingAllowanceMmk)
                    .HasColumnName("HousingAllowance_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MedicalAllowanceMmk)
                    .HasColumnName("MedicalAllowance_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MonthlySalaryUsd)
                    .HasColumnName("MonthlySalary_USD")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NetPayMmk)
                    .HasColumnName("NetPay_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetPayUsd)
                    .HasColumnName("NetPay_USD")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OtherBenefitsMmk)
                    .HasColumnName("OtherBenefits_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OtherDeductionMmk)
                    .HasColumnName("OtherDeduction_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Region).HasMaxLength(50);

                entity.Property(e => e.SsbMmk)
                    .HasColumnName("SSB_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TaxPaymentMmk)
                    .HasColumnName("TaxPayment_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ThirdPartyPaidBenefitsMmk)
                    .HasColumnName("ThirdPartyPaidBenefits_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalBaseSalary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalDeductionMmk)
                    .HasColumnName("TotalDeduction_MMK")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPayableUsd)
                    .HasColumnName("TotalPayable_USD")
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<LocalPaySlip>(entity =>
            {
                entity.Property(e => e.LocalPayslipId).ValueGeneratedNever();

                entity.Property(e => e.BankDetails)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BaseSalary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GrossTaxableIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NetPay).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NewPerDiem).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Others).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OthersDeduction).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OthersIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Overtime).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Payable).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ssb)
                    .HasColumnName("SSB")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TaxPayment).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalBaseSalary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalDeduction).HasColumnType("decimal(18, 2)");
            });
        }
    }
}
