using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiparisTakip.Models;
using System.Diagnostics;

namespace SiparisTakip.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}