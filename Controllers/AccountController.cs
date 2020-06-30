using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiparisTakip.Models;

namespace SiparisTakip.Controllers
{
    public class AccountController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public AccountController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
        }




        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(string userMail,string userPassword)
        {
            int result = 0;
            result = _siparisTakipDB.Users.Where(u => u.userMail == userMail && u.userPassword == userPassword).Count();
            if(result > 0)
            {

                var userProfile = _siparisTakipDB.Users.Select(User => User).Where(u => u.userMail == userMail && u.userPassword == userPassword).FirstOrDefault();

                string SessionUserMail = userProfile.userMail;
                string SessionUserName = userProfile.userName;
                string SessionUserSurname = userProfile.userSurname;
                string SessionUserPermission = userProfile.userPermission;

                HttpContext.Session.SetString("userMail", SessionUserMail);
                HttpContext.Session.SetString("userName", SessionUserName);
                HttpContext.Session.SetString("userSurname", SessionUserSurname);
                HttpContext.Session.SetString("userPermission", SessionUserPermission);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                TempData["Info"] = "E-mail or password is incorrect";
                TempData["InfoType"] = "warning";
                return RedirectToAction(nameof(AccountController.Index), "Account");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPost(string userMail,string userName, string userSurname ,string userPassword, string password)
        {
            int result = 0;
            result = _siparisTakipDB.Users.Where(u => u.userMail == userMail && u.userPassword == userPassword).Count();
            if(result == 0)
            {
                User newUser = new User();
                newUser.userMail         = userMail;
                newUser.userName         = userName;
                newUser.userSurname      = userSurname;
                newUser.userPassword     = userPassword;
                newUser.userPermission   = "Customer";
                _siparisTakipDB.Add(newUser);
                _siparisTakipDB.SaveChanges();
                TempData["Info"] = "Your account has been created";
                TempData["InfoType"] = "success";
            } else
            {
                TempData["Info"] = "E-mail address is registered in the system.";
                TempData["InfoType"] = "danger";
            }
            return RedirectToAction(nameof(AccountController.Index), "Account");
        }
    }
}
