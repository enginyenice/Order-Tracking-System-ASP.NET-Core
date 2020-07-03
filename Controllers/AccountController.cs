using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiparisTakip.Models;
using System;
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

        public bool SessionCont()
        {
            if (HttpContext != null)
            {
                if ((HttpContext.Session.GetString("userId")) == null)
                {
                    return true;
                }
            }
            return false; ;
        }

        public IActionResult Index()
        {
            if (SessionCont() == false)
                return RedirectToAction("Index", "Home");
            return View();
        }

        #region LoginPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(string userMail, string userPassword)
        {
            if (SessionCont() == false)
                return RedirectToAction("Index", "Home");
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
            if (SessionCont() == false)
                return RedirectToAction("Index", "Home");
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

        #region EditProfile

        public IActionResult EditProfile()
        {
            if (SessionCont() == true)
                return RedirectToAction("Index", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProfile(string userMail, string userPassword)
        {
            return RedirectToAction(nameof(AccountController.Index), "Account");
        }

        #endregion EditProfile

        #region EditUser

        public ActionResult EditUser(string userName, string userSurname)
        {
            string hataMesaji, status = "danger";
            if (userName != null && userSurname != null)
            {
                if (userName.Length > 3 && userSurname.Length > 3)
                {
                    int userId = Int32.Parse(HttpContext.Session.GetString("userId"));
                    User BilgiDegistir = _siparisTakipDB.Users.SingleOrDefault(k => k.userId == userId);
                    BilgiDegistir.userName = userName;
                    BilgiDegistir.userSurname = userSurname;
                    _siparisTakipDB.SaveChanges();
                    HttpContext.Session.SetString("userName", userName);
                    HttpContext.Session.SetString("userSurname", userSurname);
                    hataMesaji = "Profil güncellendi";
                    status = "success";
                }
                else
                {
                    hataMesaji = "Ad ve Soyad en az 3 karakter olmalıdır";
                    status = "danger";
                }
            }
            else
            {
                hataMesaji = "Ad ve Soyad en az 3 karakter olmalıdır";
                status = "danger";
            }

            string result = hataMesaji + "|" + status;
            result = JsonConvert.SerializeObject(result);
            return new JsonResult(result);
        }

        #endregion EditUser

        #region EditPassword

        public ActionResult EditPassword(string password, string newpassword, string newpassword2)
        {
            string hataMesaji, status = "danger";

            if (password != null && newpassword != null && newpassword2 != null)
            {
                if (newpassword.Length > 3)
                {
                    if (newpassword == newpassword2)
                    {
                        int userId = Int32.Parse(HttpContext.Session.GetString("userId"));
                        User BilgiDegistir = _siparisTakipDB.Users.SingleOrDefault(k => k.userId == userId);
                        if (BilgiDegistir.userPassword == password)
                        {
                            BilgiDegistir.userPassword = newpassword;
                            _siparisTakipDB.SaveChanges();
                            hataMesaji = "Şifre başarıyla değiştirildi.";
                            status = "success";
                        }
                        else
                        {
                            hataMesaji = "Geçerli şifre hatalıdır.";
                        }
                    }
                    else
                    {
                        hataMesaji = "Parolalar eşleşmiyor..";
                    }
                }
                else
                {
                    hataMesaji = "Şifre en az 3 karakter olmalıdır.";
                }
            }
            else
            {
                hataMesaji = "Şifre en az 3 karakter olmalıdır.";
            }
            string result = hataMesaji + "|" + status;
            result = JsonConvert.SerializeObject(result);
            return new JsonResult(result);
        }

        #endregion EditPassword

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