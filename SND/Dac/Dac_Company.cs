using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using SND.Lib;
using SND.Models;

namespace SND.Dac
{
    public class Dac_Company
    {
        public IQueryable<CompanyModel> CompanyInfo()
        {
            return new MongoDacHelper("company").FindAll<CompanyModel>();
        }
        public IQueryable<CompanyModel> CompanyInfo(string name)
        {
            return new MongoDacHelper("company").Find<CompanyModel>(Query.Matches("ComName", name));
        }
        public CompanyModel CompanyInfoDetail(ObjectId id)
        {
            return new MongoDacHelper("company").FindOne<CompanyModel>(Query.EQ("_id", id));
        }
        public void CompanySave(CompanyModel company)
        {
            new MongoDacHelper("company").Save<CompanyModel>(company);
        }
        public void CompanyDelete(ObjectId id)
        {
            new MongoDacHelper("company").Remove(id);
        }

    }
}