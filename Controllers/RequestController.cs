using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SiparisTakip.Models;
using SiparisTakip.Models.Tables;

namespace SiparisTakip.Controllers
{
    public class RequestController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;
        public RequestController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
        }
        public IActionResult Index()
        {
            return View(_siparisTakipDB.Requests.ToList());
        }

        [HttpPost]
        public ActionResult RequestStatusEdit(string data,int status)
        {
            if(data != null)
            {
                data = data.Replace("res[]=", "-");
                string[] id = data.Split('-');
                for (int i = 1; i < id.Length; i++)
                {


                    Request BilgiDegistir = _siparisTakipDB.Requests.SingleOrDefault(k => k.requestId == Int32.Parse(id[i]));
                    BilgiDegistir.requestStatus = status;
                    _siparisTakipDB.SaveChanges();

                }
            }
            return new JsonResult("success");
        }
        [HttpPost]
        public IActionResult RequestInfo(int id)
        {
            Request getRequest = _siparisTakipDB.Requests.Include(m => m.User).First(n => n.requestId == id);
            string getAllData = JsonConvert.SerializeObject(getRequest);
            return new JsonResult(getAllData);

        }
    }
}

