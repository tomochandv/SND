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
    public class StaticsController : Controller
    {
        //
        // GET: /Statics/

        public ActionResult Price(string yyyy = "")
        {
            yyyy = yyyy == "" ? DateTime.Now.Year.ToString() : yyyy;
            ViewBag.yyyy = yyyy;
            DateTime sdate = DateTime.Parse(yyyy+"-01-01 00:00:00");
            DateTime edate = DateTime.Parse(sdate.AddYears(1).AddDays(-1).ToShortDateString() + " 23:59:59");

            List<ProductSellMasterModel> list = new Dac_Product().ProductSellMasterSelect(
                ObjectId.Parse(ConfigurationManager.AppSettings["COM"]),
                sdate,
                edate
                ).ToList();

            List<StaticsModel> model = new List<StaticsModel>();
            if(list != null)
            {
                for (int i = 1; i <= 12; i++ )
                {
                    DateTime ssdate = DateTime.Parse(yyyy + "-" + i.ToString() + "-01 00:00:00");
                    DateTime eedate = DateTime.Parse(ssdate.AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59");
                    int totalParice = 0;
                    StaticsModel smodel = new StaticsModel();
                    smodel.yyyy = ssdate.Year.ToString();
                    smodel.mm = i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString();
                    
                    List<ProductSellMasterModel> priceList = list.Where(x => x.Indate >= ssdate && x.Indate <= eedate).ToList();
                    smodel.count = priceList.Count;
                    if(priceList != null)
                    {
                        foreach(var dataPrice in priceList)
                        {
                            totalParice += dataPrice.TotalPrice;
                        }
                    }
                    smodel.price = totalParice;
                    model.Add(smodel);
                }
            }
            return View(model);
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }

       

    }
}
