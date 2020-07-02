using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiparisTakip.Models;

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
            return View(_siparisTakipDB.Requests.Include(r=>r.user).ToList());
        }
    }
}
