using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SiparisTakip.Models;

namespace SiparisTakip.Controllers
{
    public class TalepController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;
        public TalepController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
        }
        public IActionResult Index()
        {
            return View(_siparisTakipDB.Requests.ToList());
        }

        public IActionResult RequestStatusEdit([FromBody] JObject jobject)
        {
            //throw new Exception("stop");
            return new JsonResult(jobject);
        }
    }
}
