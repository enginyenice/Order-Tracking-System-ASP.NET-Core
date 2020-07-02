﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SiparisTakip.Models;
using SiparisTakip.Models.Tables;
using System.Net.Mail;
using System.Net;

namespace SiparisTakip.Controllers
{
    public class RequestController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;
        public RequestController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
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
            return View(_siparisTakipDB.Requests.OrderByDescending(m => m.requestId).Take(20));
        }

        [HttpPost]
        public ActionResult RequestStatusEdit(string data,int status)
        {
            string message = "success";
            if (data != null)
            {
                
                data = data.Replace("res[]=", "-");
                data = data.Replace("&", "");
                string[] id = data.Split('-');
                int lastRequestId = -1;
                if (status == 1)
                {
                    for(int i = 1;i< id.Length;i++)
                    {
                        Request GelenData = _siparisTakipDB.Requests.Where(m => m.requestId == Int32.Parse(id[i])).SingleOrDefault();
                        if (GelenData.requestStatus == 0)
                        {
                            lastRequestId = GelenData.requestId;
                            break;
                        }
                    }

                    if(lastRequestId != -1)
                    {
                        //message = lastRequestId + "Mail gidecek";
                        Request newCheckStatus = _siparisTakipDB.Requests
                            .Include(m => m.user)
                            .Where(m => m.requestId == lastRequestId).SingleOrDefault();
                        sendMail(newCheckStatus);
                    }
                    
                }
                
                
                for (int i = 1; i < id.Length; i++)
                {
                    Request BilgiDegistir = _siparisTakipDB.Requests.SingleOrDefault(k => k.requestId == Int32.Parse(id[i]));
                    BilgiDegistir.requestStatus = status;
                    _siparisTakipDB.SaveChanges();

                }
            }
            return new JsonResult(message);
        }

        public ActionResult RequestInfo(int id)
        {
            if (SessionCont() == false)
                return RedirectToAction("Index", "Account");
            Request getRequest = _siparisTakipDB.Requests
                .Include(m => m.user)
                .First(m => m.requestId == id);


            string getData = JsonConvert.SerializeObject(getRequest);
            return new JsonResult(getData);
        }


        public void sendMail(Request request)
        {
            MailMessage mail = new MailMessage(); //yeni bir mail nesnesi Oluşturuldu.
            mail.IsBodyHtml = true; //mail içeriğinde html etiketleri kullanılsın mı?
            mail.To.Add(request.user.userMail); //Kime mail gönderilecek.

            //mail kimden geliyor, hangi ifade görünsün?

            Setting setting = _siparisTakipDB.Settings.SingleOrDefault();


            mail.From = new MailAddress(request.user.userMail, "Talebiniz Onaylandı" , System.Text.Encoding.UTF8);
            mail.Subject = "Talebiniz Onaylandı";//mailin konusu




            //mailin içeriği.. Bu alan isteğe göre genişletilip daraltılabilir.
            string text = "";
            if (request.requestStatus == 0)
                text = "<span style='color: #17a2b8'><strong>Onay Bekliyor</strong></span>";
                    else

                 text = "<span style='color: #28a745'><strong>Onaylandı</strong></span>";
            mail.Body = "<div style='color:black'>"+
            "<p><strong> Talep No: </strong> <span id = 'requestDeliveryDate' >" + request.requestId + "</span ></p>" +
            "<p><strong> Talep Tarihi: </strong> <span id = 'requestDeliveryDate' >" + request.requestDeliveryDate + "</span ></p>" +
            "<p><strong> Talep Eden Departman: </strong><span id = 'requestDepartment' >" + request.requestDepartment + "</span ></p>" +
            "<p><strong> Talep Eden Personel: </strong><span id = 'requestSteff' >" + request.requestSteff + "</span ></p>" +
            "<p><strong> Açıklama: </strong><span id = 'requestDescription' >" + request.requestDescription + "</span ></p>" +
            "<p><strong> Tahmini Fiyat: </strong><span id = 'requestEstimatedPrice' >" + request.requestEstimatedPrice + "</span ></p>" +
            "<p><strong> Gider Yeri: </strong><span id = 'requestExpensePlace' > " + request.requestExpensePlace + " </span ></p>" +
            "<p><strong> Ürün Özellikleri: </strong><span id = 'requestProductFeatures' > " + request.requestProductFeatures + " </span ></p>" +
            "<p><strong> Proje: </strong><span id = 'requestProject' > " + request.requestProject + " </span ></p>" +
            "<p><strong> Miktar: </strong><span id = 'requestQuantity' > " + request.requestQuantity + " </span ></p>" +
            "<p><strong> Durum: </strong><span id = 'requestStatus' > " + text + " </span ></p>" +
            "<p><strong> Cinsi: </strong><span id = 'requestSpecies' >" + request.requestSpecies + "</span ></p>" +
            "<p><strong> Tedarik Firma 1: </strong><span id = 'requestSupplyCompany1' >" + request.requestSupplyCompany1 + "</span ></p>" +
            "<p><strong> Tedarik Firma 2: </strong><span id = 'requestSupplyCompany2' > " + request.requestSupplyCompany2 + " </span ></p>" +
            "<p><strong> Tedarik Firma 3: </strong><span id = 'requestSupplyCompany3' > " + request.requestSupplyCompany3 + " </span ></p>" +
            "<p><strong> Müşteri Adı Soyadı: </strong><span id = 'userName' > " + request.user.userName +" "+ request.user.userSurname +" </span ></p>"+
            "</div>";
            mail.IsBodyHtml = true;
            SmtpClient smp = new SmtpClient();

            //mailin gönderileceği adres ve şifresi
            smp.Credentials = new NetworkCredential(setting.settingEPosta, setting.settingPassword);
            smp.Port = setting.settingSmtpPort;
            smp.Host = setting.settingSmtpHost;//gmail üzerinden gönderiliyor.
            smp.EnableSsl = setting.settingSmtpSSL;
            try
            {
                smp.Send(mail);//mail isimli mail gönderiliyor.
            }
            catch (Exception)
            {

                
            }
        }
    }

}

