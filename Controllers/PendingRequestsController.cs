using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisTakip.Models;

namespace SiparisTakip.Controllers
{
    public class PendingRequestsController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public PendingRequestsController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
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
            return false;
        }
        public IActionResult Index()
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "PendingRequestsController");
            }
            return View(_siparisTakipDB.Requests.Where(r=>r.userId== Convert.ToInt32(HttpContext.Session.GetString("userId")) && r.requestStatus==0 ).ToList());
        }
    }
}
