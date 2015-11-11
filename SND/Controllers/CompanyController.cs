using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Bson;
using SND.Dac;
using SND.Lib;
using SND.Models;

namespace SND.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            List<CompanyModel> list = new Dac_Company().CompanyInfo().ToList();
           return View(list);
        }

        public ActionResult Save(string id = "")
        {
            CompanyModel model = new CompanyModel();
            if (id != "")
            {
                model = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(id));
            }
            return View(model);
        }

        public JsonResult GetList(string name)
        {
            List<CompanyModel> list = new Dac_Company().CompanyInfo(name).ToList();
            return Json(list);
        }

        public JsonResult Insert(string addr = "", string tell = "", string name = "",
            string ceoTell = "", string ceoName = "", string id = "")
        {
            bool result = true;
            try
            {
                if (id == "")
                {
                    CompanyModel model = new CompanyModel();
                    model.ComAddr = addr;
                    model.ComCeoName = ceoName;
                    model.ComCeoTell = ceoTell;
                    model.ComName = name;
                    model.ComTell = tell;

                    new Dac_Company().CompanySave(model);
                }
                else
                {
                    CompanyModel model = new CompanyModel();
                    model.ComAddr = addr;
                    model.ComCeoName = ceoName;
                    model.ComCeoTell = ceoTell;
                    model.ComName = name;
                    model.ComTell = tell;
                    model.Id = ObjectId.Parse(id);
                    new Dac_Company().CompanySave(model);
                }
            }
            catch(Exception ex)
            {
                result = false;
            }
            return Json(result);
        }
        public JsonResult Delete(string id = "")
        {
            bool result = true;
            try
            {
                new Dac_Company().CompanyDelete(ObjectId.Parse(id));
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        }
        

    }
}
