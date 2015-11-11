using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SND.Lib;
using SND.Models;

namespace SND.Dac
{
    public class Dac_Bom
    {
        public IQueryable<BomCategoryModel> BomCategorySelect(ObjectId comapnyId)
        {
            var query = Query<BomCategoryModel>.EQ(e => e.Company.Id, comapnyId);
            return new MongoDacHelper("bomCategory").Find<BomCategoryModel>(query);
        }
        public void BomCategorySave(BomCategoryModel bomCategory)
        {
            new MongoDacHelper("bomCategory").Save<BomCategoryModel>(bomCategory);
        }
        public IQueryable<BomModel> BomSelectById(ObjectId id)
        {
            return new MongoDacHelper("bom").Find<BomModel>(Query.EQ("Id", id));
        }
        public BomModel BomSelecOnetById(ObjectId id)
        {
            var query = Query<BomModel>.EQ(e => e.Id, id);
            return new MongoDacHelper("bom").FindOne<BomModel>(query);
        }
        public void BomCategoryDelete(ObjectId id)
        {
            new MongoDacHelper("bomCategory").Remove(id);
        }
        public IQueryable<BomModel> BomSelect(ObjectId comapnyId, ObjectId bomcategoryId)
        {
            var query = Query.And(Query<BomModel>.EQ(e => e.Company.Id, comapnyId), Query<BomModel>.EQ(e => e.BomCategory.Id, bomcategoryId));
            return new MongoDacHelper("bom").Find<BomModel>(query);
        }
        public IQueryable<BomModel> BomSelect(ObjectId comapnyId)
        {
            var query = Query<BomModel>.EQ(e => e.Company.Id, comapnyId);
            return new MongoDacHelper("bom").Find<BomModel>(query);
        }
        public void BomSave(BomModel bom)
        {
            new MongoDacHelper("bom").Save<BomModel>(bom);
        }
        public void BomDelete(ObjectId id)
        {
            new MongoDacHelper("bom").Remove(id);
        }
        public BomCategoryModel BomCategorySelectById(ObjectId id)
        {
            var query = Query<BomCategoryModel>.EQ(e => e.Id, id);
            return new MongoDacHelper("bomCategory").FindOne<BomCategoryModel>(query);
        }
        public IQueryable<OutCompanyModel> OutCompanySelect(ObjectId comapnyId)
        {
            var query = Query<OutCompanyModel>.EQ(e => e.Company.Id, comapnyId);
            return new MongoDacHelper("outcompany").Find<OutCompanyModel>(query);
        }
        public OutCompanyModel OutCompanySelecOnetById(ObjectId id)
        {
            var query = Query<OutCompanyModel>.EQ(e => e.Id, id);
            return new MongoDacHelper("outcompany").FindOne<OutCompanyModel>(query);
        }
        public void OutCompanySave(OutCompanyModel outcompany)
        {
            new MongoDacHelper("outcompany").Save<OutCompanyModel>(outcompany);
        }
        public void OutCompanyDelete(ObjectId id)
        {
            new MongoDacHelper("outcompany").Remove(id);
        }

        public void BomSellSave(BomSellModel model)
        {
            new MongoDacHelper("bomsell").Save(model);
        }
        public void BomSellDelete(ObjectId id)
        {
            new MongoDacHelper("bomsell").Remove(id);
        }
        public IQueryable<BomSellModel> BomSellSelect(ObjectId comapnyId, string bomcategoryId, string outComId, string bomId, System.DateTime sdate, System.DateTime edate)
        {
            var andList = new List<IMongoQuery>();
            andList.Add(Query<BomSellModel>.EQ(e => e.Company.Id, comapnyId));
            andList.Add(Query.And(Query<BomSellModel>.GTE(e => e.Indate, sdate), Query<BomSellModel>.LTE(e => e.Indate, edate)));
            if (bomcategoryId != "")
            {
                andList.Add(Query<BomSellModel>.EQ(e => e.Bom.BomCategory.Id, ObjectId.Parse(bomcategoryId)));
            }
            if (bomId != "")
            {
                andList.Add(Query<BomSellModel>.EQ(e => e.Bom.Id, ObjectId.Parse(bomId)));
            }
            if (outComId != "")
            {
                andList.Add(Query<BomSellModel>.EQ(e => e.OutCompany.Id, ObjectId.Parse(outComId)));
            }
            var query = Query.And(andList);
            return new MongoDacHelper("bomsell").Find<BomSellModel>(query);
        }

    }
}