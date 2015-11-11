using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SND.Models
{
    public class LoginModel
    {
        public ObjectId Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string tell { get; set; }
        [BsonRepresentation(BsonType.String)] 
        public UserGrade grade { get; set; }
        public CompanyModel company { get; set; }
    }
    public enum UserGrade
    {
        King,
        Night,
        Civil
    }
}