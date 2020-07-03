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
            if (SessionCont() == false)
                return RedirectToAction("Index", "Account");
            return View(_siparisTakipDB.Users.ToList());
        }



        public  IActionResult PermissionEdit(int? id)
        {
            if (SessionCont() == false)
                return RedirectToAction("Index", "Account");
            if (id == null)
            {
                return NotFound();
            }

            var user = _siparisTakipDB.Users
                .FirstOrDefault(m => m.userId == id);
            if(user.userPermission == "Customer")
            {
                user.userPermission = "Administrator";
            } else
            {
                user.userPermission = "Customer";
            }
             _siparisTakipDB.SaveChanges();

            int SessionID = Int32.Parse(HttpContext.Session.GetString("userId"));

            if(id == SessionID)
            {
                HttpContext.Session.SetString("userPermission", user.userPermission);
            }



            return RedirectToAction("Index", "UserManagment");
        }

    }
}
