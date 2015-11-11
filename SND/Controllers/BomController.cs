using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using SND.Dac;
using SND.Models;

namespace SND.Controllers
{
    public class BomController : Controller
    {
        //
        // GET: /Bom/

        #region Category
        public ActionResult Category()
        {
            List<BomCategoryModel> model = new Dac_Bom().BomCategorySelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            return View(model);
        }
        public JsonResult BomCount(string id)
        {
            List<BomModel> model = new Dac_Bom().BomSelectById(ObjectId.Parse(id)).ToList();
            return Json(model.Count);
        }
        public ActionResult Save()
        {
            return View();
        }

        public JsonResult Insert(string name = "")
        {
            bool result = true;
            try
            {
                CompanyModel company = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
                BomCategoryModel model = new BomCategoryModel();
                model.Company = company;
                model.BomCategoryName = name;
                new Dac_Bom().BomCategorySave(model);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        }

        public JsonResult CategoryDelete(string id)
        {
            bool result = true;
            List<BomModel> model = new Dac_Bom().BomSelectById(ObjectId.Parse(id)).ToList();
            if (model.Count == 0)
            {
                new Dac_Bom().BomCategoryDelete(ObjectId.Parse(id));
            }
            else
            {
                result = false;
            }
            return Json(result);

        } 
        #endregion

        #region Bom
        public ActionResult Bom(string id = "")
        {
            List<BomModel> model = new List<BomModel>();
            if (id != "")
            {
                model = new Dac_Bom().BomSelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]), ObjectId.Parse(id)).ToList();
            }
            else
            {
                model = new Dac_Bom().BomSelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            }
            ViewBag.id = id;
            return View(model);
        }
        public ActionResult BomSave()
        {
            return View();
        }
        public JsonResult BomInsert(string unit = "", string category = "", string name = "")
        {
            bool result = true;
            try
            {
                CompanyModel company = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
                BomCategoryModel bomCategory = new Dac_Bom().BomCategorySelectById(ObjectId.Parse(category));
                BomModel model = new BomModel();
                model.Unit = unit;
                model.Company = company;
                model.BomName = name; ;
                model.BomCategory = bomCategory;
                new Dac_Bom().BomSave(model);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        }
        public JsonResult BomDelete(string id)
        {
            bool result = true;
            try
            {
                new Dac_Bom().BomDelete(ObjectId.Parse(id));
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        }

        public JsonResult GetCategory()
        {
            List<BomCategoryModel> model = new Dac_Bom().BomCategorySelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            var data = from e in model
                       select new
                       {
                           BomCategoryName = e.BomCategoryName,
                           Id = e.Id.ToString()
                       };
            return Json(data);
        }
        public JsonResult GetBomList(string catid = "")
        {
            if (catid != "")
            {
                List<BomModel> model = new Dac_Bom().BomSelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]),
                    ObjectId.Parse(catid)
                    ).ToList();
                var data = from e in model
                           select new
                           {
                               BomName = e.BomName,
                               Id = e.Id.ToString()
                           };
                return Json(data);
            }
            else
            {
                return Json("");
            }
            
        } 
        #endregion

        #region Outcompany
        public ActionResult OutCompany()
        {
            List<OutCompanyModel> model = new Dac_Bom().OutCompanySelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            return View(model);
        }
        public ActionResult OutCompanySave()
        {
            return View();
        }
        public JsonResult OutCompanyDelete(string id)
        {
            bool result = true;
            try
            {
                new Dac_Bom().OutCompanyDelete(ObjectId.Parse(id));
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        }
        public JsonResult OutCompanyInsert(string name = "", string tell = "", string addr = "")
        {
            bool result = true;
            try
            {
                CompanyModel company = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
                OutCompanyModel model = new OutCompanyModel();
                model.OutComAddr = addr;
                model.OutComName = name;
                model.OutComTell = tell; ;
                model.Company = company;
                new Dac_Bom().OutCompanySave(model);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        }
        public JsonResult GetOutCompany()
        {
            List<OutCompanyModel> model = new Dac_Bom().OutCompanySelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            var data = from e in model
                       select new
                       {
                           OutComName = e.OutComName,
                           Id = e.Id.ToString()
                       };
            return Json(data);
        }  
        #endregion

        #region BOM Sell
        public ActionResult BomSell(string catid = "", string bomId = "", string outId = "", string sdate = "", string edate = "")
        {
            ViewBag.catid = catid;
            ViewBag.bomId = bomId;
            ViewBag.outId = outId;
            ViewBag.sdate = sdate == "" ? DateTime.Now.ToShortDateString() : sdate;
            ViewBag.edate = edate == "" ? DateTime.Now.ToShortDateString() : edate;

            List<BomSellModel> list = new Dac_Bom().BomSellSelect(
                ObjectId.Parse(ConfigurationManager.AppSettings["COM"]),
                catid,
                outId,
                bomId,
                WebUtill.ConvertDatetime_S(sdate),
                WebUtill.ConvertDatetime_E(edate)
                ).ToList();
            return View(list);
        }
        public ActionResult BomSellSave()
        {

            return View();
        }
        public JsonResult BomSellInsert(string catid, string bomId, string outId, DateTime date, int price, int qty, int totalPrice)
        {
            var result = true;
            try
            {
                CompanyModel company = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
                BomModel bom = new Dac_Bom().BomSelecOnetById(ObjectId.Parse(bomId));
                OutCompanyModel outcompany = new Dac_Bom().OutCompanySelecOnetById(ObjectId.Parse(outId));

                BomSellModel model = new BomSellModel();
                model.Indate = date;
                model.OutCompany = outcompany;
                model.Price = price;
                model.Qty = qty;
                model.TotalPrice = totalPrice;
                model.Bom = bom;
                model.Company = company;

                new Dac_Bom().BomSellSave(model);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return Json(result);
        }
        public JsonResult BomSellDelete(string id)
        {
            bool result = true;
            try
            {
                new Dac_Bom().BomSellDelete(ObjectId.Parse(id));
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result);
        } 
        #endregion

    }
}
