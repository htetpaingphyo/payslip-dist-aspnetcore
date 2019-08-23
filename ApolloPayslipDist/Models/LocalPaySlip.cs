using System;
using System.Collections.Generic;

namespace ApolloPayslipDist.Models
{
    public partial class LocalPaySlip
    {
        public Guid LocalPayslipId { get; set; }
        public int? EmpId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public string BankDetails { get; set; }
        public decimal? BaseSalary { get; set; }
        public int? TotalWorkingDays { get; set; }
        public int? ActualWorkingDays { get; set; }
        public decimal? TotalBaseSalary { get; set; }
        public decimal? Overtime { get; set; }
        public decimal? OthersIncome { get; set; }
        public decimal? GrossTaxableIncome { get; set; }
        public decimal? Ssb { get; set; }
        public decimal? TaxPayment { get; set; }
        public decimal? OthersDeduction { get; set; }
        public decimal? TotalDeduction { get; set; }
        public decimal? NetPay { get; set; }
        public decimal? NewPerDiem { get; set; }
        public decimal? Others { get; set; }
        public decimal? Payable { get; set; }
    }
}
