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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProduct(string id = "")
        {
            List<ProductModel> model = new List<ProductModel>();
            if (id == "")
            {
                model = new Dac_Product().ProductSelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            }
            else
            {
                model = new Dac_Product().ProductSelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]), ObjectId.Parse(id)).ToList();
            }

            var data = from e in model
                       select new
                       {
                           name = e.ProductNm,
                           Id = e.Id.ToString(),
                           price = e.Price,
                           sale = e.PriceSale
                       };

            return Json(data);
        }

        public JsonResult Save(string id = "", string price = "", string type = "", string pay = "")
        {
            bool result = false;
            try
            {
                if (id != "")
                {
                    string orderNo = DateTime.Now.AddMonths(-1).ToString("yyyyMMddHHmmss");
                    DateTime now = DateTime.Now.AddMonths(-1);
                    string[] arrId = id.Split(',');
                    string[] arrPrice = price.Split(',');
                    string[] arrType = type.Split(',');
                    int total = 0;
                    for (int i = 0; i < arrId.Length; i++)
                    {
                        total += WebUtill.ConvertInt(arrPrice[i]);
                    }

                    ProductSellMasterModel master = new ProductSellMasterModel();
                    master.Company = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
                    master.Indate = now;
                    master.OrderNo = orderNo;
                    master.PayType = pay;
                    master.TotalPrice = total;
                    new Dac_Product().ProductSellMasterSave(master);
                    for (int i = 0; i < arrId.Length; i++)
                    {
                        ProductSellModel data = new ProductSellModel();
                        data.OrderNo = orderNo; 
                        data.Product = new Dac_Product().ProductView(ObjectId.Parse(arrId[i]));
                        data.Price = WebUtill.ConvertInt(arrPrice[i]);
                        data.DiscountYn = arrType[i];
                        data.PayType = pay;
                        data.Indate = DateTime.Now;
                        data.TotalPrice = total;
                        new Dac_Product().ProductSellSave(data);
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }
            }
            catch(Exception ex)
            {
                result = false;
            }
            return Json(result);
        }

    }


}
