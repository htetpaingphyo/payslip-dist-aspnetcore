using System;
using System.Collections.Generic;

namespace ApolloPayslipDist.Models
{
    public partial class ExpatPaySlip
    {
        public Guid ExpatPaySlipId { get; set; }
        public int? EmpId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public string Designation { get; set; }
        public decimal? MonthlySalaryUsd { get; set; }
        public decimal? FxRate { get; set; }
        public decimal? BaseSalaryMmk { get; set; }
        public int? WorkingDays { get; set; }
        public int? ActualWorkedDays { get; set; }
        public decimal? TotalBaseSalary { get; set; }
        public decimal? HousingAllowanceMmk { get; set; }
        public decimal? MedicalAllowanceMmk { get; set; }
        public decimal? OtherBenefitsMmk { get; set; }
        public decimal? GrossTaxableIncomeMmk { get; set; }
        public decimal? SsbMmk { get; set; }
        public decimal? TaxPaymentMmk { get; set; }
        public decimal? ThirdPartyPaidBenefitsMmk { get; set; }
        public decimal? OtherDeductionMmk { get; set; }
        public decimal? TotalDeductionMmk { get; set; }
        public decimal? NetPayMmk { get; set; }
        public decimal? NetPayUsd { get; set; }
        public decimal? ExpenseClaimsUsd { get; set; }
        public decimal? TotalPayableUsd { get; set; }
    }
}
