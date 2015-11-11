using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SND.Lib;
using SND.Models;

namespace SND.Dac
{
    public class Dac_User
    {
        public IQueryable<LoginModel> UserSelect(string comapnyId)
        {
            
            if (comapnyId != "")
            {
                var andList = new List<IMongoQuery>();
                andList.Add(Query<LoginModel>.EQ(e => e.company.Id, ObjectId.Parse(comapnyId)));
                var query = Query.And(andList);
                return new MongoDacHelper("user").Find<LoginModel>(query);
            }
            else
            {
                return new MongoDacHelper("user").Find<LoginModel>();
            }
            
        }

        public void UserSave(LoginModel model)
        {
            new MongoDacHelper("user").Save(model);
        }
        public void UserDelete(ObjectId id)
        {
            new MongoDacHelper("user").Remove(id);
        }
        public LoginModel UserSelectOne(string email, string password)
        {
            var andList = new List<IMongoQuery>();
            andList.Add(Query<LoginModel>.EQ(e => e.email, email));
            andList.Add(Query<LoginModel>.EQ(e => e.password, password));
            var query = Query.And(andList);
            return new MongoDacHelper("user").FindOne<LoginModel>(query);
        }
    }
}