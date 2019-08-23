using ApolloPayslipDist.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace ApolloPayslipDist.Services
{
    public class FileService
    {
        MainDbContext db;

        public FileService(MainDbContext context)
        {
            db = context;
        }

        public bool SaveLocalFileData(IEnumerable<IFormFile> files)
        {
            bool isSaved = false;
            db.RemoveRange(db.LocalPaySlip);
            db.SaveChanges();

            foreach (var file in files)
            {
                // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Payslips");
                // var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                // var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                // fileInfo.Add($"{filePath}/{fileName} {file.Length}");
                // using (var stream = new MemoryStream(Path.Combine(filePath, fileName), FileMode.Create))

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);

                    try
                    {
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++)
                            {
                                LocalPaySlip slip = new LocalPaySlip
                                {
                                    LocalPayslipId = Guid.NewGuid(),
                                    EmpId = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                    Email = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                    Region = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                    BankDetails = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                    BaseSalary = decimal.Parse(worksheet.Cells[row, 6].Value.ToString().Trim()),
                                    TotalWorkingDays = int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()),
                                    ActualWorkingDays = int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim()),
                                    TotalBaseSalary = decimal.Parse(worksheet.Cells[row, 9].Value.ToString().Trim()),
                                    Overtime = decimal.Parse(worksheet.Cells[row, 10].Value.ToString().Trim()),
                                    OthersIncome = decimal.Parse(worksheet.Cells[row, 11].Value.ToString().Trim()),
                                    GrossTaxableIncome = decimal.Parse(worksheet.Cells[row, 12].Value.ToString().Trim()),
                                    Ssb = decimal.Parse(worksheet.Cells[row, 13].Value.ToString().Trim()),
                                    TaxPayment = decimal.Parse(worksheet.Cells[row, 14].Value.ToString().Trim()),
                                    OthersDeduction = decimal.Parse(worksheet.Cells[row, 15].Value.ToString().Trim()),
                                    TotalDeduction = decimal.Parse(worksheet.Cells[row, 16].Value.ToString().Trim()),
                                    NetPay = decimal.Parse(worksheet.Cells[row, 17].Value.ToString().Trim()),
                                    NewPerDiem = decimal.Parse(worksheet.Cells[row, 18].Value.ToString().Trim()),
                                    Others = decimal.Parse(worksheet.Cells[row, 19].Value.ToString().Trim()),
                                    Payable = decimal.Parse(worksheet.Cells[row, 20].Value.ToString().Trim())
                                };

                                db.LocalPaySlip.Add(slip);
                                isSaved = db.SaveChanges() > 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return isSaved;
        }

        public bool SaveExpatFileData(IEnumerable<IFormFile> files)
        {
            bool isSaved = false;
            db.RemoveRange(db.ExpatPaySlip);
            db.SaveChanges();

            List<string> fileInfo = new List<string>();

            foreach (var file in files)
            {
                // var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Payslips");
                // var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                // var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                // fileInfo.Add($"{filePath}/{fileName} {file.Length}");
                // using (var stream = new MemoryStream(Path.Combine(filePath, fileName), FileMode.Create))

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);

                    try
                    {
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++)
                            {
                                ExpatPaySlip slip = new ExpatPaySlip
                                {
                                    ExpatPaySlipId = Guid.NewGuid(),
                                    EmpId = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    Email = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    Region = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    Designation = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    MonthlySalaryUsd = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    FxRate = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    BaseSalaryMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    WorkingDays = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    ActualWorkedDays = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    TotalBaseSalary = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    HousingAllowanceMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    MedicalAllowanceMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    OtherBenefitsMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    GrossTaxableIncomeMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    SsbMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    TaxPaymentMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    ThirdPartyPaidBenefitsMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    OtherDeductionMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    TotalDeductionMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    NetPayMmk = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    NetPayUsd = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    ExpenseClaimsUsd = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                    TotalPayableUsd = decimal.Parse(worksheet.Cells[row, 1].Value.ToString().Trim())
                                };

                                db.ExpatPaySlip.Add(slip);
                                isSaved = db.SaveChanges() > 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return isSaved;
        }
    }
}
