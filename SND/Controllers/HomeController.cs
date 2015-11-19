using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        public JsonResult Save(List<string> id, List<string> price, List<string> qty, string pay)
        {
            bool result = false;
            try
            {

                if (id.Count > 0)
                {
                    string orderNo = DateTime.Now.AddMonths(-1).ToString("yyyyMMddHHmmss");
                    DateTime now = DateTime.Now;

                    int total = 0;
                    for (int i = 0; i < id.Count; i++)
                    {
                        total += WebUtill.ConvertInt(price[i]) * WebUtill.ConvertInt(qty[i]);
                    }

                    ProductSellMasterModel master = new ProductSellMasterModel();
                    master.Company = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
                    master.Indate = now;
                    master.OrderNo = orderNo;
                    master.PayType = pay;
                    master.TotalPrice = total;
                    new Dac_Product().ProductSellMasterSave(master);

                    for (int i = 0; i < id.Count; i++)
                    {
                        for(int j=0; j < WebUtill.ConvertInt(qty[i]); j++)
                        {
                            ProductSellModel data = new ProductSellModel();
                            data.OrderNo = orderNo;
                            data.Product = new Dac_Product().ProductView(ObjectId.Parse(id[i]));
                            data.Price = WebUtill.ConvertInt(price[i]);
                            data.DiscountYn = "O";
                            data.PayType = pay;
                            data.Indate = now;
                            data.TotalPrice = total;
                            new Dac_Product().ProductSellSave(data);
                            result = true;
                        }
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
