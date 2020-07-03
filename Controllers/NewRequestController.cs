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
            return View();
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
            "requestSupplyCompany3","requestUser,","ImageFile"
            )] Request request)
        {
            try
            {
                
                Random rastgele = new Random();
                int sayi = rastgele.Next(5555, 25000);
                string resimler = Path.Combine(_evrimoment.WebRootPath, "images");
                string resimPath = "resimYok.jpg";
                if(request.ImageFile != null)
                { 
                if (request.ImageFile.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(resimler, sayi+"-"+request.ImageFile.FileName), FileMode.Create))
                    {
                        request.ImageFile.CopyTo(fileStream);
                    }
                    resimPath = sayi+"-"+request.ImageFile.FileName;
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
                else if(allDateCount < 100)
                    alldataString = "00" + (allDateCount + 1);
                else if(allDateCount < 1000)
                    alldataString = "0" + (allDateCount + 1);
                else
                    alldataString = (allDateCount + 1).ToString();



                request.requestDeliveryDate = Convert.ToDateTime(request.date);
                request.requestImage = resimPath;
                request.userId = Int32.Parse(HttpContext.Session.GetString("userId"));
                request.requestCreateAt = DateTime.Now.ToShortDateString();
                request.requestNo = "T" + year + mount + day + "-" + alldataString;
                _siparisTakipDB.Add(request);
                _siparisTakipDB.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return RedirectToAction("Index", "Home");

        }


        }
}
