using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApolloPayslipDist.Models;
using ApolloPayslipDist.Models.ViewModels;
using ApolloPayslipDist.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloPayslipDist.Controllers
{
    public class AccountsController : Controller
    {
        private MainDbContext db;
        private ISession Session { get; set; }
        private readonly ISecurityService securityService;
        private string email { get; set; }

        public AccountsController(IHttpContextAccessor httpContextAccessor, ISecurityService service, MainDbContext context)
        {
            Session = httpContextAccessor.HttpContext.Session;
            db = context;
            securityService = service;

            email = Session.GetString("account");
        }

        public IActionResult GotoLoginPage()
        {
            return RedirectToAction("Index", "Accounts");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Reset([Bind("AccountEmail")] Account account)
        {
            var acc = db.Account.SingleOrDefault(a => a.AccountEmail == account.AccountEmail);
            acc.AccountPassword = securityService.Encrypt("Welcome@123", acc.AccountSalt);

            db.Update(acc);
            db.SaveChanges();

            return RedirectToAction("LocalPayslipUpload", "PaySlips");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("AccountEmail,AccountPassword")] Account account)
        {
            account.AccountId = Guid.NewGuid();
            account.AccountSalt = securityService.GetUniqueString;
            account.AccountPassword = securityService.Encrypt(account.AccountPassword, account.AccountSalt);
            account.CreatedDate = DateTime.Now;

            db.Account.Add(account);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login(string email, string password)
        {
            var accountExist = db.Account.SingleOrDefault(a => a.AccountEmail == email);

            if (accountExist != null)
            {
                if (password == securityService.Decrypt(accountExist.AccountPassword, accountExist.AccountSalt))
                {
                    Session.SetString("account", accountExist.AccountEmail);
                    return RedirectToAction("LocalPayslipUpload", "Payslips");
                }
                else
                    return View(nameof(Index));
            }

            return NotFound();
        }

        public IActionResult ChangePassword(Guid id)
        {
            var account = db.Account.SingleOrDefault(a => a.AccountId == id);
            ChangePasswordViewModel model = new ChangePasswordViewModel()
            {
                AccountId = account.AccountId
            };

            ViewBag.Email = email;
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(Guid id, [Bind("AccountId, CurrentPassword", "NewPassword", "ConfirmPassword")]ChangePasswordViewModel model)
        {
            if (id != model.AccountId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var account = db.Account.SingleOrDefault(a => a.AccountId == model.AccountId);
                if (model.CurrentPassword == securityService.Decrypt(account.AccountPassword, account.AccountSalt))
                {
                    account.AccountPassword = securityService.Encrypt(model.NewPassword, account.AccountSalt);
                }
                account.UpdatedDate = DateTime.Now;

                db.Update(account);
                db.SaveChanges();
            }

            return RedirectToAction("LocalPayslipUpload", "PaySlips");
        }

        public IActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}