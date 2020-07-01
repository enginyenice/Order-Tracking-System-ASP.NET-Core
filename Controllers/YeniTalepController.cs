using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiparisTakip.Models;
using SiparisTakip.Models.Tables;

namespace SiparisTakip.Controllers
{

    public class YeniTalepController : Controller
    {
        private readonly IHostingEnvironment _evrimoment;
        private readonly SiparisTakipDB _siparisTakipDB;
        public YeniTalepController(SiparisTakipDB context, IHostingEnvironment evrimoment)
        {
            _siparisTakipDB = context;
            _evrimoment = evrimoment;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRequest(
            [Bind("requestDepartment", "requestSteff",
            "requestProject","requestProject",
            "requestExpensePlace","requestProductFeatures",
            "requestDescription","requestQuantity",
            "requestSpecies","requestEstimatedPrice",
            "date","requestSupplyCompany1",
            "requestSupplyCompany1","requestSupplyCompany2",
            "requestSupplyCompany3","requestUser,","ImageFile"
            )] Request request)
        {
            try
            {
                string resimler = Path.Combine(_evrimoment.WebRootPath, "images");
                if (request.ImageFile.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(resimler, request.ImageFile.FileName), FileMode.Create))
                    {
                        request.ImageFile.CopyToAsync(fileStream);
                    }
                }


                request.requestDeliveryDate = Convert.ToDateTime(request.date);
                request.requestImage = request.ImageFile.FileName;
                request.userId = Int32.Parse(HttpContext.Session.GetString("userId"));
                _siparisTakipDB.Add(request);
                _siparisTakipDB.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }
            return RedirectToAction("Index", "Account");















            return View();
        }


        }
}
