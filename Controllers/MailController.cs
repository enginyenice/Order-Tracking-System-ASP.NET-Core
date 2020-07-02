using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiparisTakip.Models;
using SiparisTakip.Models.Tables;

namespace SiparisTakip.Controllers
{
    public class MailController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;
        public MailController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
        }
        public IActionResult Index()
        {
            return View(_siparisTakipDB.Settings.ToList());
        }
        public ActionResult MailUpdate(string settingEPosta,string settingPassword,string settingSmtpHost, int settingSmtpPort,bool settingSmtpSSL)
        {
            string hataMesaji = "", status = "danger";
            int settingCount = _siparisTakipDB.Settings.ToList().Count();

            if(settingEPosta != null)
            {
                if (settingCount == 0) // Mail Yok
                {
                    hataMesaji = "Mail eklendi.";
                    status = "success";
                    Setting setting = new Setting();
                    setting.settingEPosta = settingEPosta;
                    setting.settingPassword = settingPassword;
                    setting.settingSmtpHost = settingSmtpHost;
                    setting.settingSmtpPort = settingSmtpPort;
                    setting.settingSmtpSSL = settingSmtpSSL;
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
            } else
            {
                hataMesaji = "Veriler gonderilirken bir hata oluştu";
            }



            string result = hataMesaji + "|" + status;
            result = JsonConvert.SerializeObject(result);
            return new JsonResult(result);
        }
    }
}
