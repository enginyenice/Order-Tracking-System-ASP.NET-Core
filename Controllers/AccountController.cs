using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisTakip.Models;
using System.Linq;

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

        #region LoginPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(string userMail, string userPassword)
        {
            int result = 0;
            result = _siparisTakipDB.Users.Where(u => u.userMail == userMail && u.userPassword == userPassword).Count();
            if (result > 0)
            {
                var userProfile = _siparisTakipDB.Users.Select(User => User).Where(u => u.userMail == userMail && u.userPassword == userPassword).FirstOrDefault();

                int SessionUserId = userProfile.userId;
                string SessionUserMail = userProfile.userMail;
                string SessionUserName = userProfile.userName;
                string SessionUserSurname = userProfile.userSurname;
                string SessionUserPermission = userProfile.userPermission;

                HttpContext.Session.SetString("userId", SessionUserId.ToString());
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

        #endregion LoginPost

        #region RegisterPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPost(string userMail, string REuserMail, string userName, string userSurname, string userPassword, string REuserPassword, string password)
        {
            if (REuserMail == userMail)
            {
                if (REuserPassword == userPassword)
                {
                    int result = 0;
                    result = _siparisTakipDB.Users.Where(u => u.userMail == userMail && u.userPassword == userPassword).Count();
                    if (result == 0)
                    {
                        User newUser = new User
                        {
                            userMail = userMail,
                            userName = userName,
                            userSurname = userSurname,
                            userPassword = userPassword,
                            userPermission = "Customer"
                        };
                        _siparisTakipDB.Add(newUser);
                        _siparisTakipDB.SaveChanges();
                        TempData["Info"] = "Hesap Başarıyla Oluşturuldu";
                        TempData["InfoType"] = "success";
                    
                    }
                    else
                    {
                        TempData["Info"] = "Bu Mail Hesabı Zaten Kayıtlı";
                        TempData["InfoType"] = "danger";
                    }
                }
                else
                {
                    TempData["Info"] = "Şifreler Uyuşmuyor";
                    TempData["InfoType"] = "danger";
                }
            }
            else
            {
                TempData["Info"] = "E-Postalar Uyuşmuyor";
                TempData["InfoType"] = "danger";
            }
             return RedirectToAction(nameof(AccountController.Index), "Account");
        }

        #endregion RegisterPost

        public IActionResult DeleteSession()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userMail");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("userSurname");
            HttpContext.Session.Remove("userPermission");

            return RedirectToAction(nameof(AccountController.Index), "Account");
        }
    }
}