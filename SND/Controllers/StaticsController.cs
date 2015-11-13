﻿using System;
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

        public ActionResult Product(string yyyy = "2015")
        {
            yyyy = yyyy == "" ? DateTime.Now.Year.ToString() : yyyy;
            ViewBag.yyyy = yyyy;
            DateTime sdate = DateTime.Parse(yyyy + "-01-01 00:00:00");
            DateTime edate = DateTime.Parse(sdate.AddYears(1).AddDays(-1).ToShortDateString() + " 23:59:59");

            List<ProductSellModel> list = new Dac_Product().ProductSellSelect(
               "", "",
                sdate,
                edate
                ).ToList();
            List<StaticProductMasterModel> modelList = new List<StaticProductMasterModel>();
            if (list != null)
            {
                //1.달별
                for (int i = 1; i <= 12; i++)
                {
                    StaticProductMasterModel modelMaster = new StaticProductMasterModel();

                    DateTime ssdate = DateTime.Parse(yyyy + "-" + i.ToString() + "-01 00:00:00");
                    DateTime eedate = DateTime.Parse(ssdate.AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59");

                    modelMaster.yyyy = ssdate.Year.ToString();
                    modelMaster.mm = i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString();

                    List<ProductSellModel> prList = list.Where(x => x.Indate >= ssdate && x.Indate <= eedate).ToList();
                    
                    //2. 상품별
                    List<ProductModel> productModel = new Dac_Product().ProductSelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
                    List<StaticsProductProModel> modelProductList = new List<StaticsProductProModel>();
                    foreach(var productModelData in productModel)
                    {
                        StaticsProductProModel modelProduct = new StaticsProductProModel();
                        modelProduct.productName = productModelData.ProductNm;
                        modelProduct.Id = productModelData.Id;
                        //3. Detail
                        int totalPrice = 0;
                        int totalQty = 0; 
                        foreach(var detailData in prList)
                        {
                            if(productModelData.Id == detailData.Product.Id)
                            {
                                totalPrice += detailData.Price;
                                totalQty++;
                            }
                        }
                        modelProduct.qty = totalQty;
                        modelProduct.price = totalPrice;

                        modelProductList.Add(modelProduct);
                    }
                    modelMaster.ProductList = modelProductList;
                    modelList.Add(modelMaster);
                }

            }

            return View(modelList);
        }

        public ActionResult Category()
        {
            return View();
        }

       

    }
}
