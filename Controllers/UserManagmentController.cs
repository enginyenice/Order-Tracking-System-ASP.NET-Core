using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiparisTakip.Models;
using System;
using System.Linq;

namespace SiparisTakip.Controllers
{
    public class UserManagmentController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public UserManagmentController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
        }
        public IActionResult Index()
        {
            return View(_siparisTakipDB.Users.ToList());
        }
    }
}
