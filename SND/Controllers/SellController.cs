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
    public class SellController : Controller
    {
        //
        // GET: /Sell/

        public ActionResult Index(string cate = "", string prod = "", string sdate = "", string edate = "")
        {
            ViewBag.cate = cate;
            ViewBag.prod = prod;
            ViewBag.sdate = sdate == "" ? DateTime.Now.ToShortDateString() : sdate;
            ViewBag.edate = edate == "" ? DateTime.Now.ToShortDateString() : edate;


            //ViewBag.Count = countList.Count;

            List<ProductSellModel> list = new Dac_Product().ProductSellSelect(cate, prod, 
                WebUtill.ConvertDatetime_S(sdate),
                WebUtill.ConvertDatetime_E(edate)
                ).ToList();
            return View(list);
        }

        public ActionResult Sell(string sdate = "", string edate = "")
        {
            ViewBag.sdate = sdate == "" ? DateTime.Now.ToShortDateString() : sdate;
            ViewBag.edate = edate == "" ? DateTime.Now.ToShortDateString() : edate;
            List<ProductSellMasterModel> list = new Dac_Product().ProductSellMasterSelect(
                ObjectId.Parse(ConfigurationManager.AppSettings["COM"]), 
                WebUtill.ConvertDatetime_S(sdate),
                WebUtill.ConvertDatetime_E(edate)
                ).ToList();

            return View(list);
        }

        public JsonResult SellDetail(string orno = "")
        {
            List<ProductSellModel> list = new Dac_Product().ProductSellSelectOrNo(orno).ToList();
            var data = from e in list
                       select new
                       {
                           name = e.Product.ProductNm,
                           price = e.Price,
                           category = e.Product.ProductCategory.ProductCategoryNm,
                           sale = e.DiscountYn == "Y"
                       };
            data.OrderByDescending(x => x.category);
            return Json(data);
        }

    }
}
