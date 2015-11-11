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
    public class PrController : Controller
    {
        #region Category
        public ActionResult Category()
        {
            List<ProductCategoryModel> model = new Dac_Product().ProductCategorySelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            return View(model);
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
                ProductCategoryModel model = new ProductCategoryModel();
                model.Company = company;
                model.ProductCategoryNm = name;
                new Dac_Product().ProductCategorySave(model);
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
            List<ProductModel> model = new Dac_Product().ProductSelectById(ObjectId.Parse(id)).ToList();
            if (model.Count == 0)
            {
                new Dac_Product().ProductCategoryDelete(ObjectId.Parse(id));
            }
            else
            {
                result = false;
            }
            return Json(result);

        }
        public JsonResult ProductCount(string id)
        {
            List<ProductModel> model = new Dac_Product().ProductSelectById(ObjectId.Parse(id)).ToList();
            return Json(model.Count);
        }
        #endregion

        public ActionResult Product(string id = "")
        {
            List<ProductModel> model = new List<ProductModel>();
            ViewBag.id = id;
            if(id == "")
            {
                model = new Dac_Product().ProductSelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            }
            else
            {
                model = new Dac_Product().ProductSelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]), ObjectId.Parse(id)).ToList();
            }
            
            return View(model);
        }

        public ActionResult Views(string id = "")
        {
            ViewBag.id = id;
            ProductModel product = new Dac_Product().ProductView(ObjectId.Parse(id));
            return View(product);
        }

        public ActionResult ProductSave()
        {
            return View();
        }
        public RedirectResult ProductInsert(string selCategory = "", string txtName = "", int txtPrice = 0, int txtSalePrice = 0, string txtDesc = "")
        {
            HttpFileCollectionBase files = Request.Files;
            List<string> img = new List<string>();
            ProductModel model = new ProductModel();
            CompanyModel company = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
            ProductCategoryModel category = new Dac_Product().ProductCategorySelectById(ObjectId.Parse(selCategory));
            model.Company = company;
            model.Description = txtDesc;
            model.Price = txtPrice;
            model.PriceSale = txtSalePrice;
            model.ProductNm = txtName;
            model.ProductCategory = category;

            if (files.Count > 0)
            {
                string path = Server.MapPath("/Upload/");
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].ContentLength > 0)
                    {
                        img.Add(WebUtill.ImageSave(path, files[i]));
                    }
                }
            }
            model.ImagUrl = img;
            new Dac_Product().ProductSave(model);
            return Redirect("/Pr/Product");
        }

        public RedirectResult ProductUpdate(string id = "", string selCategory = "", string txtName = "", int txtPrice = 0, int txtSalePrice = 0, string txtDesc = "", string selUse = "")
        {
            ProductModel product = new Dac_Product().ProductView(ObjectId.Parse(id));
            HttpFileCollectionBase files = Request.Files;
            List<string> img = new List<string>();
            ProductModel model = new ProductModel();
            CompanyModel company = new Dac_Company().CompanyInfoDetail(ObjectId.Parse(ConfigurationManager.AppSettings["COM"]));
            ProductCategoryModel category = new Dac_Product().ProductCategorySelectById(ObjectId.Parse(selCategory));
            model.Id = ObjectId.Parse(id);
            model.Company = company;
            model.Description = txtDesc;
            model.Price = txtPrice;
            model.PriceSale = txtSalePrice;
            model.ProductNm = txtName;
            model.ProductCategory = category;
            model.UseYn = selUse == "0" ? true : false;

            if (files.Count > 0)
            {
                string path = Server.MapPath("/Upload/");
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].ContentLength > 0)
                    {
                        img.Add(WebUtill.ImageSave(path, files[i]));
                    }
                }
            }

            if (product.ImagUrl != null)
            {
                foreach (var data in product.ImagUrl)
                {
                    img.Add(data);
                }
            }
            model.ImagUrl = img;
            new Dac_Product().ProductSave(model);
            return Redirect("/Pr/Product");
        }

        public JsonResult GetCategory()
        {
            List<ProductCategoryModel> model = new Dac_Product().ProductCategorySelect(ObjectId.Parse(ConfigurationManager.AppSettings["COM"])).ToList();
            var data = from e in model
                       select new
                       {
                           name = e.ProductCategoryNm,
                           Id = e.Id.ToString()
                       };
            return Json(data);
        }
        public JsonResult ProductDelete(string id = "")
        {
            new Dac_Product().ProductDelete(ObjectId.Parse(id));
            return Json("");
        }
        public JsonResult ImageDelet(string name = "")
        {
            bool result = true;
            string id = WebUtill.Request("id");
            try
            {
                if (name != "" && id != "")
                {
                    ProductModel product = new Dac_Product().ProductView(ObjectId.Parse(id));
                    List<string> img = new List<string>();
                    string path = Server.MapPath("/Upload/");
                    WebUtill.ImageDelete(path, name);
                    foreach (var data in product.ImagUrl)
                    {
                        if (data != name)
                        {
                            img.Add(data);
                        }
                    }
                    product.ImagUrl = img;
                    new Dac_Product().ProductSave(product);
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
