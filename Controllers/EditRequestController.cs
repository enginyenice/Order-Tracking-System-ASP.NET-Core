using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiparisTakip.Models;
using SiparisTakip.Models.Tables;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisTakip.Controllers
{
    public class EditRequestController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment _evrimoment;

        private readonly SiparisTakipDB _siparisTakipDB;

        [Obsolete]
        public EditRequestController(SiparisTakipDB context,IHostingEnvironment evrimoment)
        {
            _siparisTakipDB = context;
            _evrimoment = evrimoment;
        }

        public bool SessionCont()
        {
            if (HttpContext != null)
            {
                if ((HttpContext.Session.GetString("userId")) != null &&
                    HttpContext.Session.GetString("userPermission") == "Administrator")
                {
                    return true;
                }
            }
            return false;
        }
        public IActionResult Index(int id)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.data = _siparisTakipDB.Departments.ToList();
            return View(_siparisTakipDB.Requests.Where(i=>i.requestId==id).Include(i=>i.user).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("requestId,requestEstimatedPrice,requestDeliveryDate,requestSupplyCompany1," +
            "requestSupplyCompany2,requestSupplyCompany3," +
            "requestStatus,requestSpecies,requestQuantity," +
            "requestDescription,requestProductFeatures,requestExpensePlace," +
            "requestProject,requestSteff,requestDepartment")] Request req)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var thisRequest = _siparisTakipDB.Requests.Where(m => m.requestId == req.requestId).FirstOrDefault();

                    thisRequest.requestDeliveryDate = req.requestDeliveryDate;
                    thisRequest.requestDepartment = req.requestDepartment;
                    thisRequest.requestDescription = req.requestDescription;
                    thisRequest.requestEstimatedPrice = req.requestEstimatedPrice;
                    thisRequest.requestExpensePlace = req.requestExpensePlace;
                    thisRequest.requestProductFeatures = req.requestProductFeatures;
                    thisRequest.requestProject = req.requestProject;
                    thisRequest.requestQuantity = req.requestQuantity;
                    thisRequest.requestSpecies = req.requestSpecies;
                    thisRequest.requestStatus = req.requestStatus;
                    thisRequest.requestSteff = req.requestSteff;
                    thisRequest.requestSupplyCompany1 = req.requestSupplyCompany1;
                    thisRequest.requestSupplyCompany2 = req.requestSupplyCompany2;
                    thisRequest.requestSupplyCompany3 = req.requestSupplyCompany3;

                    _siparisTakipDB.Update(thisRequest);
                    _siparisTakipDB.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction("Index", "AllRequests");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult EditImage(
           [Bind("requestId","ImageFile")] Request request)
        {
            try
            {
                Random rastgele = new Random();
                int sayi = rastgele.Next(5555, 25000);
                string resimler = Path.Combine(_evrimoment.WebRootPath, "images");
                string resimPath = "resimYok.jpg";
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

               
                var thisRequest = _siparisTakipDB.Requests.Where(m => m.requestId == request.requestId).FirstOrDefault();
                thisRequest.requestImage = resimPath;

                _siparisTakipDB.Update(thisRequest);
                _siparisTakipDB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return RedirectToAction("Index", "AllRequests");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult EditPDF(
          [Bind("requestId", "PDFFile")] Request request)
        {
            try
            {
                Random rastgele = new Random();
                int sayi = rastgele.Next(5555, 25000);
                string pdfler = Path.Combine(_evrimoment.WebRootPath, "pdf");
                string pdfPath = "-";
                if (request.PDFFile != null)
                {
                    string[] parcalaPath = request.PDFFile.FileName.Split(".");
                    if (parcalaPath[parcalaPath.Length - 1] == "pdf" && request.PDFFile.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(pdfler, sayi + "-" + request.PDFFile.FileName), FileMode.Create))
                        {
                            request.PDFFile.CopyTo(fileStream);
                        }
                        pdfPath = sayi + "-" + request.PDFFile.FileName;
                    }
                }


                var thisRequest = _siparisTakipDB.Requests.Where(m => m.requestId == request.requestId).FirstOrDefault();
                thisRequest.requestPDF = pdfPath;

                _siparisTakipDB.Update(thisRequest);
                var result = _siparisTakipDB.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return RedirectToAction("Index", "AllRequests");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult EditExcel(
       [Bind("requestId", "ExcelFile")] Request request)
        {
            try
            {
                Random rastgele = new Random();
                int sayi = rastgele.Next(5555, 25000);
                string Exceller = Path.Combine(_evrimoment.WebRootPath, "excel");
                string excelPath = "-";
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


                var thisRequest = _siparisTakipDB.Requests.Where(m => m.requestId == request.requestId).FirstOrDefault();
                thisRequest.requestExcel = excelPath;

                _siparisTakipDB.Update(thisRequest);
                var result = _siparisTakipDB.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return RedirectToAction("Index", "AllRequests");
        }











    }
}
