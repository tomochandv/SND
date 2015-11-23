using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using MongoDB.Bson;
using SND.Dac;
using SND.Models;

namespace SND.Controllers
{
    public class SystemController : Controller
    {
        //
        // GET: /System/

        public ActionResult Index(string id = "")
        {
            var model = new Dac_User().UserSelect(id).ToList();
            ViewBag.id = id;
            return View(model);
        }

        public JsonResult GetCompany()
        {
            List<CompanyModel> company = new Dac_Company().CompanyInfo().ToList();
            var data = from e in company
                       select new
                       {
                           Name = e.ComName,
                           Id = e.Id.ToString()
                       };
            return Json(data);
        }

        public ActionResult UserSave()
        {
            return View();
        }

        public JsonResult UserInsert(string company = "",  string pwd = "", string name = "", string email = "", string grade = "", string tell = "")
        {
            bool result = true;
            try
            {
                UserGrade enumgrade = new UserGrade();
                if(grade == "0")
                {
                    enumgrade = UserGrade.Civil;
                }
                if (grade == "1")
                {
                    enumgrade = UserGrade.Night;
                }
                if (grade == "2")
                {
                    enumgrade = UserGrade.King;
                }
                CompanyModel companyModel = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
                LoginModel model = new LoginModel();
                model.name = name;
                model.company = companyModel;
                model.password = pwd;
                model.email = email;
                model.tell = tell;
                model.grade = enumgrade;
                new Dac_User().UserSave(model);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        }

        public JsonResult UserDelete(string id)
        {
            bool result = true;
            try
            {
                new Dac_User().UserDelete(ObjectId.Parse(id));
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        }

        public JsonResult Email()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("aka.tomochan@gmail.com", "tomochan");
            mail.To.Add(new MailAddress("dj_tomochan@naver.com"));
            mail.Subject = "Test 입니다.";
            mail.IsBodyHtml = true;
            mail.Body = "<h1>Test</h1>";
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            SmtpClient client = new SmtpClient("smtp-pulse.com");
            client.Credentials = new System.Net.NetworkCredential("aka.tomochan@gmail.com", "WdnL32A3dWB");
            client.Port = 2525;
            client.Send(mail);
            return Json("");
        }
    }
}
