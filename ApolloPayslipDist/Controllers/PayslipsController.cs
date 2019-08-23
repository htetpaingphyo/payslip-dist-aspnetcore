using System.Collections.Generic;
using System.Linq;
using ApolloPayslipDist.Models;
using ApolloPayslipDist.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApolloPayslipDist.Controllers
{
    public class PayslipsController : Controller
    {
        MainDbContext db;
        private ISession Session { get; set; }
        public string email { get; set; }

        public PayslipsController(IHttpContextAccessor httpContextAccessor, MainDbContext context)
        {
            db = context;
            Session = httpContextAccessor.HttpContext.Session;
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

        public IActionResult LocalPayslipUpload()
        {
            if (string.IsNullOrEmpty(email))
                return GotoLoginPage();
            else
            {
                ViewBag.Email = email;
                ViewBag.UserId = db.Account.SingleOrDefault(a => a.AccountEmail == email).AccountId;
                return View();
            }
        }

        public IActionResult ExpatPayslipUpload()
        {
            if (string.IsNullOrEmpty(email))
                return GotoLoginPage();
            else
            {
                ViewBag.UserId = db.Account.SingleOrDefault(a => a.AccountEmail == email).AccountId;
                ViewBag.Email = email;
                return View();
            }
        }

        public IActionResult SubmitLocalPayslip(IEnumerable<IFormFile> files)
        {
            IEnumerable<string> fileInfo = new List<string>();
            FileService service = new FileService(db);

            if (service.SaveLocalFileData(files))
                return RedirectToAction(nameof(LocalPayslipResult));
            else
                return View(nameof(LocalPayslipUpload));
        }

        public IActionResult SubmiExpatPayslip(IEnumerable<IFormFile> files)
        {
            IEnumerable<string> fileInfo = new List<string>();
            FileService service = new FileService(db);

            if (service.SaveExpatFileData(files))
                return RedirectToAction(nameof(ExpatPayslipResult));
            else
                return View(nameof(ExpatPayslipUpload));
        }

        public IActionResult LocalPayslipResult()
        {
            if (string.IsNullOrEmpty(email))
                return GotoLoginPage();
            else
            {
                ViewBag.UserId = db.Account.SingleOrDefault(a => a.AccountEmail == email).AccountId;
                ViewBag.Email = email;
                return View();
            }
        }

        public IActionResult ReadLocalPayslipResult([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetLocalPaySlips().ToDataSourceResult(request));
        }

        public IActionResult ExpatPayslipResult()
        {
            if (string.IsNullOrEmpty(email))
                return GotoLoginPage();
            else
            {
                ViewBag.UserId = db.Account.SingleOrDefault(a => a.AccountEmail == email).AccountId;
                ViewBag.Email = email;
                return View();
            }   
        }

        public IActionResult ReadExpatPayslipResult([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetExpatPaySlips().ToDataSourceResult(request));
        }

        private IQueryable<LocalPaySlip> GetLocalPaySlips()
        {
            return db.LocalPaySlip.AsQueryable();
        }

        private IQueryable<ExpatPaySlip> GetExpatPaySlips()
        {
            return db.ExpatPaySlip.AsQueryable();
        }
    }
}