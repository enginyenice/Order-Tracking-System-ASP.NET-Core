using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiparisTakip.Models;
using SiparisTakip.Models.Tables;
using System.Linq;

namespace SiparisTakip.Controllers
{
    public class MailController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public MailController(SiparisTakipDB context)
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
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_siparisTakipDB.Settings.ToList());
        }

        public ActionResult MailUpdate(string settingEPosta, string settingPassword, string settingSmtpHost, int settingSmtpPort, bool settingSmtpSSL)
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }

            string hataMesaji = "", status = "danger";
            int settingCount = _siparisTakipDB.Settings.ToList().Count();

            if (settingEPosta != null)
            {
                if (settingCount == 0) // Mail Yok
                {
                    hataMesaji = "Mail eklendi.";
                    status = "success";
                    Setting setting = new Setting
                    {
                        settingEPosta = settingEPosta,
                        settingPassword = settingPassword,
                        settingSmtpHost = settingSmtpHost,
                        settingSmtpPort = settingSmtpPort,
                        settingSmtpSSL = settingSmtpSSL
                    };
                    _siparisTakipDB.Add(setting);
                    _siparisTakipDB.SaveChanges();
                }
                else // Mail Yok
                {
                    hataMesaji = "Mail düzenlendi.";
                    status = "success";
                    Setting setting = _siparisTakipDB.Settings.SingleOrDefault();
                    setting.settingEPosta = settingEPosta;
                    setting.settingPassword = settingPassword;
                    setting.settingSmtpHost = settingSmtpHost;
                    setting.settingSmtpPort = settingSmtpPort;
                    setting.settingSmtpSSL = settingSmtpSSL;
                    _siparisTakipDB.SaveChanges();
                }
            }
            else
            {
                hataMesaji = "Veriler gonderilirken bir hata oluştu";
            }

            string result = hataMesaji + "|" + status;
            result = JsonConvert.SerializeObject(result);
            return new JsonResult(result);
        }
    }
}