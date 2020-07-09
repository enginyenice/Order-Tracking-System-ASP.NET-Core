using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiparisTakip.Models;
using SiparisTakip.Models.Tables;
using System;
using System.IO;
using System.Linq;

namespace SiparisTakip.Controllers
{
    public class NewRequestController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment _evrimoment;

        private readonly SiparisTakipDB _siparisTakipDB;

        [Obsolete]
        public NewRequestController(SiparisTakipDB context, IHostingEnvironment evrimoment)
        {
            _siparisTakipDB = context;
            _evrimoment = evrimoment;
        }

        public bool SessionCont()
        {
            if (HttpContext != null)
            {
                if ((HttpContext.Session.GetString("userId")) != null)
                {
                    return true;
                }
            }
            return false; ;
        }

        public IActionResult Index()
        {
            if (SessionCont() == false)
                return RedirectToAction("Index", "Account");
            return View(_siparisTakipDB.Departments.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult CreateRequest(
            [Bind("requestDepartment", "requestSteff",
            "requestProject","requestExpensePlace",
            "requestProductFeatures",
            "requestDescription","requestQuantity",
            "requestSpecies","requestEstimatedPrice",
            "date","requestSupplyCompany1",
            "requestSupplyCompany1","requestSupplyCompany2",
            "requestSupplyCompany3","requestUser,",
            "ImageFile","PDFFile","ExcelFile"

            )] Request request)
        {
            try
            {
                string resimPath = "resimYok.jpg";
                string excelPath = "-";
                string pdfPath = "-";

                Random rastgele = new Random();
                int sayi = rastgele.Next(5555, 25000);
                string resimler = Path.Combine(_evrimoment.WebRootPath, "images");
                string pdfler = Path.Combine(_evrimoment.WebRootPath, "pdf");
                string Exceller = Path.Combine(_evrimoment.WebRootPath, "excel");
                if(request.PDFFile != null)
                {
                    string[] parcalaPath = request.PDFFile.FileName.Split(".");
                    if(parcalaPath[parcalaPath.Length - 1] == "pdf" && request.PDFFile.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(pdfler, sayi + "-" + request.PDFFile.FileName), FileMode.Create))
                        {
                            request.PDFFile.CopyTo(fileStream);
                        }
                        pdfPath = sayi + "-" + request.PDFFile.FileName;
                    }
                }

                if (request.ExcelFile != null)
                {
                    string[] parcalaPath = request.ExcelFile.FileName.Split(".");
                    if (parcalaPath[parcalaPath.Length - 1] == "xls" || parcalaPath[parcalaPath.Length - 1] == "xlsx" && request.ExcelFile.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(Exceller, sayi + "-" + request.ExcelFile.FileName), FileMode.Create))
                        {
                            request.ExcelFile.CopyTo(fileStream);
                        }
                        excelPath = sayi + "-" + request.ExcelFile.FileName;
                    }
                }




                if (request.ImageFile != null)
                {
                    if (request.ImageFile.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(resimler, sayi + "-" + request.ImageFile.FileName), FileMode.Create))
                        {
                            request.ImageFile.CopyTo(fileStream);
                        }
                        resimPath = sayi + "-" + request.ImageFile.FileName;
                    }
                }

                string nowDate = DateTime.Now.ToShortDateString();
                int allDateCount = _siparisTakipDB.Requests.Where(n => n.requestCreateAt == nowDate).Count();

                string year = DateTime.Now.Year.ToString();
                string mount = DateTime.Now.Month.ToString();
                int dayInt = Int32.Parse(DateTime.Now.Day.ToString());
                string alldataString = "";
                string day = dayInt.ToString();
                if (dayInt < 10)
                {
                    day = "0" + dayInt;
                }

                if (allDateCount < 10)
                    alldataString = "000" + (allDateCount + 1);
                else if (allDateCount < 100)
                    alldataString = "00" + (allDateCount + 1);
                else if (allDateCount < 1000)
                    alldataString = "0" + (allDateCount + 1);
                else
                    alldataString = (allDateCount + 1).ToString();

                request.requestDeliveryDate = Convert.ToDateTime(request.date);
                request.requestImage = resimPath;
                request.requestPDF = pdfPath;
                request.requestExcel = excelPath;
                request.userId = Int32.Parse(HttpContext.Session.GetString("userId"));
                request.requestCreateAt = DateTime.Now.ToShortDateString();
                request.requestNo = "T" + year + mount + day + "-" + alldataString;
                request.requestDeleteDescription = "";
                request.user = _siparisTakipDB.Users.Where(m => m.userId == request.userId).FirstOrDefault();
                _siparisTakipDB.Add(request);
                _siparisTakipDB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "PendingRequests");
        }
    }
}