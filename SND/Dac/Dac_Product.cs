using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SND.Lib;
using SND.Models;


namespace SND.Dac
{
    public class Dac_Product
    {
        //category
        public IQueryable<ProductCategoryModel> ProductCategorySelect(ObjectId comapnyId)
        {
            var query = Query<ProductCategoryModel>.EQ(e => e.Company.Id, comapnyId);
            return new MongoDacHelper("productCategory").Find<ProductCategoryModel>(query);
        }
        public ProductCategoryModel ProductCategorySelectById(ObjectId id)
        {
            var query = Query<ProductCategoryModel>.EQ(e => e.Id, id);
            return new MongoDacHelper("productCategory").FindOne<ProductCategoryModel>(query);
        }
        public void ProductCategorySave(ProductCategoryModel productCategory)
        {
            new MongoDacHelper("productCategory").Save<ProductCategoryModel>(productCategory);
        }
        public void ProductCategoryDelete(ObjectId id)
        {
            new MongoDacHelper("productCategory").Remove(id);
        }
        public IQueryable<ProductModel> ProductSelectById(ObjectId id)
        {
            var query = Query<ProductModel>.EQ(e => e.ProductCategory.Id, id);
            return new MongoDacHelper("product").Find<ProductModel>(query);
        }
        //product
        public IQueryable<ProductModel> ProductSelect(ObjectId comapnyId, ObjectId ProductcategoryId)
        {
            var query = Query.And(Query<ProductModel>.EQ(e => e.Company.Id, comapnyId), Query<ProductModel>.EQ(e => e.ProductCategory.Id, ProductcategoryId));
            return new MongoDacHelper("product").Find<ProductModel>(query);
        }
        public IQueryable<ProductModel> ProductSelect(ObjectId comapnyId)
        {
            var query = Query<ProductModel>.EQ(e => e.Company.Id, comapnyId);
            return new MongoDacHelper("product").Find<ProductModel>(query);
        }
        public void ProductSave(ProductModel Product)
        {
            new MongoDacHelper("product").Save<ProductModel>(Product);
        }
        public void ProductDelete(ObjectId id)
        {
            new MongoDacHelper("product").Remove(id);
        }
        public ProductModel ProductView(ObjectId id)
        {
            var query = Query<ProductModel>.EQ(e => e.Id, id);
            return new MongoDacHelper("product").FindOne<ProductModel>(query);
        }
        //product Sell
        public void ProductSellMasterSave(ProductSellMasterModel model)
        {
            new MongoDacHelper("productSellMaster").Save<ProductSellMasterModel>(model);
        }
        public void ProductSellSave(ProductSellModel model)
        {
            new MongoDacHelper("productSell").Save<ProductSellModel>(model);
        }

        public IQueryable<ProductSellModel> ProductSellSelect(string cate, string prod, System.DateTime sdate, System.DateTime edate)
        {
            var andList = new List<IMongoQuery>();
            //andList.Add(Query<ProductSellModel>.EQ(e => e.Company.Id, comapnyId));
            andList.Add(Query.And(Query<ProductSellModel>.GTE(e => e.Indate, sdate), Query<ProductSellModel>.LTE(e => e.Indate, edate)));
            if (cate != "")
            {
                andList.Add(Query<ProductSellModel>.EQ(e => e.Product.ProductCategory.Id, ObjectId.Parse(cate)));
                
            }
            if (prod != "")
            {
                andList.Add(Query<ProductSellModel>.EQ(e => e.Product.Id, ObjectId.Parse(prod)));
            }
            var query = Query.And(andList);
            return new MongoDacHelper("productSell").Find<ProductSellModel>(query);
        }

         public IQueryable<ProductSellMasterModel> ProductSellMasterSelect(ObjectId comapnyId, System.DateTime sdate, System.DateTime edate)
         {
             var andList = new List<IMongoQuery>();
             andList.Add(Query<ProductSellMasterModel>.EQ(e => e.Company.Id, comapnyId));
             andList.Add(Query.And(Query<ProductSellMasterModel>.GTE(e => e.Indate, sdate), Query<ProductSellMasterModel>.LTE(e => e.Indate, edate)));
             var query = Query.And(andList);
             return new MongoDacHelper("productSellMaster").Find<ProductSellMasterModel>(query);
         }

         public IQueryable<ProductSellModel> ProductSellSelectOrNo(string orno)
         {
             var andList = new List<IMongoQuery>();
             andList.Add(Query<ProductSellModel>.EQ(e => e.OrderNo, orno));
             var query = Query.And(andList);
             return new MongoDacHelper("productSell").Find<ProductSellModel>(query);
         }
    }
}