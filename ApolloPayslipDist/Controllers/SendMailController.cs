using System;
using System.Collections.Generic;
using System.Linq;
using ApolloPayslipDist.Models;
using ApolloPayslipDist.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace ApolloPayslipDist.Controllers
{
    public class SendMailController : Controller
    {
        MainDbContext db;
        private ISession Session { get; set; }
        public string email { get; set; }
        ISecurityService service;


        public SendMailController(MainDbContext context, IHttpContextAccessor httpContextAccessor, ISecurityService securityService)
        {
            db = context;
            Session = httpContextAccessor.HttpContext.Session;
            service = securityService;
            email = Session.GetString("account");
        }

        [HttpPost]
        public JsonResult SendLocal(IEnumerable<string> emails)
        {
            try
            {
                var sender = db.Account.SingleOrDefault(a => a.AccountEmail == email);

                foreach (var email in emails)
                {
                    var rcpt = db.LocalPaySlip.SingleOrDefault(l => l.Email == email);

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(sender.AccountEmail));
                    message.To.Add(new MailboxAddress(email));
                    message.Subject = $"Pay Slip for {DateTime.Now.ToString("MMM-yyyy")}";
                    // message.Subject = $"Pay Slip for June 2019";
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = "<style>" +
                        "table { border-collapse: collapse; } " +
                        "th, td { border: 1px solid #ddd; padding: 3px; font-family: Calibri; font-size: 10.5pt; } " +
                        ".emp-info { background: lightyellow; } " +
                        ".income, .payable {background: lightgreen; } " +
                        ".deduction { background: red; }" +
                        "</style>" +
                        "<table>" +
                        "<thead>" +
                        "<tr>" +
                        "<th class='emp-info'>No.</th>" +
                        "<th class='emp-info'>Name</th>" +
                        "<th class='emp-info'>Email</th>" +
                        "<th class='emp-info'>Region</th>" +
                        "<th class='emp-info'>Bank Details</th>" +
                        "<th class='income'>Base Salary</th>" +
                        "<th class='income'>Total Working Days</th>" +
                        "<th class='income'>Actual Working Days</th>" +
                        "<th class='income'>Total Base Salary</th>" +
                        "<th class='income'>Overtime</th>" +
                        "<th class='income'>Others Income</th>" +
                        "<th class='income'>Gross Taxable Income</th>" +
                        "<th class='deduction'>SSB</th>" +
                        "<th class='deduction'>Tax Payment</th>" +
                        "<th class='deduction'>Others Deduction</th>" +
                        "<th class='deduction'>Total Deduction</th>" +
                        "<th class='payable'>Net Pay</th>" +
                        "<th class='payable'>New Per Diem</th>" +
                        "<th class='payable'>Others</th>" +
                        "<th class='payable'>Payable</th>" +
                        "</tr>" +
                        "</thead>" +
                        "<tbody>" +
                        "<tr>" +
                        $"<td>{rcpt.EmpId}</td>" +
                        $"<td>{rcpt.Name}</td>" +
                        $"<td>{rcpt.EmpId}</td>" +
                        $"<td>{rcpt.Region}</td>" +
                        $"<td>{rcpt.BankDetails}</td>" +
                        $"<td>{rcpt.BaseSalary}</td>" +
                        $"<td>{rcpt.TotalWorkingDays}</td>" +
                        $"<td>{rcpt.ActualWorkingDays}</td>" +
                        $"<td>{rcpt.TotalBaseSalary}</td>" +
                        $"<td>{rcpt.Overtime}</td>" +
                        $"<td>{rcpt.OthersIncome}</td>" +
                        $"<td>{rcpt.GrossTaxableIncome}0</td>" +
                        $"<td>{rcpt.Ssb}</td>" +
                        $"<td>{rcpt.TaxPayment}</td>" +
                        $"<td>{rcpt.OthersDeduction}</td>" +
                        $"<td>{rcpt.TotalDeduction}</td>" +
                        $"<td>{rcpt.NetPay}</td>" +
                        $"<td>{rcpt.NewPerDiem}</td>" +
                        $"<td>{rcpt.Others}</td>" +
                        $"<td>{rcpt.Payable}</td>"
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.office365.com", 587, false);
                        client.Authenticate(sender.AccountEmail, service.Decrypt(sender.AccountPassword, sender.AccountSalt));
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            return Json(emails);
        }

        [HttpPost]
        public JsonResult SendExpat(IEnumerable<string> emails)
        {
            try
            {
                var sender = db.Account.SingleOrDefault(a => a.AccountEmail == email);

                foreach (var email in emails)
                {
                    var rcpt = db.ExpatPaySlip.SingleOrDefault(l => l.Email == email);

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(sender.AccountEmail));
                    message.To.Add(new MailboxAddress(email));
                    message.Subject = $"Pay Slip for {DateTime.Now.ToString("MMM-yyyy")}";
                    // message.Subject = $"Pay Slip for June 2019";
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = "<style>" +
                        "table { border-collapse: collapse; } " +
                        "th, td { border: 1px solid #ddd; padding: 3px; font-family: Calibri; font-size: 10.5pt; } " +
                        ".emp-info { background: lightyellow; } " +
                        ".income, .payable {background: lightgreen; } " +
                        ".deduction { background: red; }" +
                        "</style>" +
                        "<table>" +
                        "<thead>" +
                        "<tr>" +
                        "<th class='emp-info'>No.</th>" +
                        "<th class='emp-info'>Name</th>" +
                        "<th class='emp-info'>Email</th>" +
                        "<th class='emp-info'>Region</th>" +
                        "<th class='emp-info'>Designation</th>" +
                        "<th class='emp-info'>MonthlySalary_USD</th>" +
                        "<th class='income'>FxRate</th>" +
                        "<th class='income'>BaseSalary_MMK</th>" +
                        "<th class='income'>WorkingDays</th>" +
                        "<th class='income'>ActualWorkedDays</th>" +
                        "<th class='income'>TotalBaseSalary</th>" +
                        "<th class='income'>HousingAllowance_MMK</th>" +
                        "<th class='income'>MedicalAllowance_MMK</th>" +
                        "<th class='deduction'>OtherBenefits_MMK</th>" +
                        "<th class='deduction'>GrossTaxableIncome_MMK</th>" +
                        "<th class='deduction'>SSB_MMK</th>" +
                        "<th class='deduction'>TaxPayment_MMK</th>" +
                        "<th class='payable'>ThirdPartyPaidBenefits_MMK</th>" +
                        "<th class='payable'>OtherDeduction_MMK</th>" +
                        "<th class='payable'>TotalDeduction_MMK</th>" +
                        "<th class='payable'>NetPay_MMK</th>" +
                        "<th class='payable'>NetPay_USD</th>" +
                        "<th class='payable'>ExpenseClaims_USD</th>" +
                        "<th class='payable'>TotalPayable_USD</th>" +
                        "</tr>" +
                        "</thead>" +
                        "<tbody>" +
                        "<tr>" +
                        $"<td>{rcpt.EmpId}</td>" +
                        $"<td>{rcpt.Name}</td>" +
                        $"<td>{rcpt.EmpId}</td>" +
                        $"<td>{rcpt.Region}</td>" +
                        $"<td>{rcpt.Designation}</td>" +
                        $"<td>{rcpt.MonthlySalaryUsd}</td>" +
                        $"<td>{rcpt.FxRate}</td>" +
                        $"<td>{rcpt.BaseSalaryMmk}</td>" +
                        $"<td>{rcpt.WorkingDays}</td>" +
                        $"<td>{rcpt.ActualWorkedDays}</td>" +
                        $"<td>{rcpt.TotalBaseSalary}</td>" +
                        $"<td>{rcpt.HousingAllowanceMmk}</td>" +
                        $"<td>{rcpt.MedicalAllowanceMmk}</td>" +
                        $"<td>{rcpt.OtherBenefitsMmk}</td>" +
                        $"<td>{rcpt.GrossTaxableIncomeMmk}</td>" +
                        $"<td>{rcpt.SsbMmk}</td>" +
                        $"<td>{rcpt.TaxPaymentMmk}</td>" +
                        $"<td>{rcpt.ThirdPartyPaidBenefitsMmk}</td>" +
                        $"<td>{rcpt.OtherDeductionMmk}</td>" +
                        $"<td>{rcpt.TotalDeductionMmk}</td>" +
                        $"<td>{rcpt.NetPayMmk}</td>" +
                        $"<td>{rcpt.NetPayUsd}</td>" +
                        $"<td>{rcpt.ExpenseClaimsUsd}</td>" +
                        $"<td>{rcpt.TotalPayableUsd}</td>"
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.office365.com", 587, false);
                        client.Authenticate(sender.AccountEmail, service.Decrypt(sender.AccountPassword, sender.AccountSalt));
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            return Json(emails);
        }
    }
}