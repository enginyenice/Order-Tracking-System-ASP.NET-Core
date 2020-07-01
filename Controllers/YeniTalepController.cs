using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SiparisTakip.Controllers
{
    public class YeniTalepController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
