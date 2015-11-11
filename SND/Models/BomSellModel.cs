using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SND.Models
{
    public class BomSellModel
    {
        public ObjectId Id { get; set; }
        public CompanyModel Company { get; set; }
        public BomModel Bom { get; set; }
        public OutCompanyModel OutCompany { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Indate { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public int TotalPrice { get; set; }

    }
}