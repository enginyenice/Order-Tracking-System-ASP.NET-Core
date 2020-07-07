using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SiparisTakip.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisTakip.Controllers
{
    public class AllRequestsController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public AllRequestsController(SiparisTakipDB context)
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

        public IActionResult Index()
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_siparisTakipDB.Requests.Include(r => r.user).ToList());
        }

        public ActionResult Delete(int id,string requestDeleteDescription)
        {
            string result = "error";
            var request = _siparisTakipDB.Requests
                .FirstOrDefault(m => m.requestId == id);
            if (request == null)
            {
                result = "error";
                result = JsonConvert.SerializeObject(result);
                return new JsonResult(result);
            }

            request.requestStatus = 2;
            request.requestDeleteDescription = requestDeleteDescription;
            _siparisTakipDB.Update(request);
            _siparisTakipDB.SaveChanges();
            result = "success";
            result = JsonConvert.SerializeObject(result);
            return new JsonResult(result);
        }


        public ActionResult BringBack(int id)
        {
            string result = "error";
            var request = _siparisTakipDB.Requests
                .FirstOrDefault(m => m.requestId == id);
            if (request == null)
            {
                result = "error";
                result = JsonConvert.SerializeObject(result);
                return new JsonResult(result);
            }

            request.requestStatus = 0;
            request.requestDeleteDescription = "";
            _siparisTakipDB.Update(request);
            _siparisTakipDB.SaveChanges();
            result = "success";
            result = JsonConvert.SerializeObject(result);
            return new JsonResult(result);
        }
    }
}