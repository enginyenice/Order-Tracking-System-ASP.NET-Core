using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisTakip.Models;
using System;
using System.Linq;

namespace SiparisTakip.Controllers
{
    public class ConfirmedRequestsController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public ConfirmedRequestsController(SiparisTakipDB context)
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
                return RedirectToAction("Index", "Account");
            }
            return View(_siparisTakipDB.Requests.Where(r => r.userId == Convert.ToInt32(HttpContext.Session.GetString("userId")) && r.requestStatus == 1).ToList());
        }
    }
}