using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SiparisTakip.Controllers
{
    public class PendingRequestsController : Controller
    {
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
    }
}
