using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiparisTakip.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisTakip.Controllers
{
    public class EditRequestController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public EditRequestController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
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

            return View(_siparisTakipDB.Requests.Where(i=>i.requestId==id).Include(i=>i.user).FirstOrDefault());
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int id, [Bind("requestId,requestImage,requestEstimatedPrice,requestDeliveryDate,requestSupplyCompany1,requestSupplyCompany2,requestSupplyCompany3,requestStatus,requestCreateAt,requestSpecies,requestQuantity,requestDescription,requestProductFeatures,requestExpensePlace,requestProject,requestSteff,requestDepartment,requestNo")] SiparisTakip.Models.Tables.Request req)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != req.requestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _siparisTakipDB.Update(req);
                    _siparisTakipDB.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction("Index", "AllRequests");
            }
            return View();
        }


     
    }
}
