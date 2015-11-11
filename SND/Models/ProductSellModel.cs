using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SND.Models
{
    public class ProductSellMasterModel
    {
        public ObjectId Id { get; set; }
        public CompanyModel Company { get; set; }
        public string OrderNo { get; set; }
        public int TotalPrice { get; set; }
        public string PayType { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Indate { get; set; }
    }
    public class ProductSellModel
    {
        public ObjectId Id { get; set; }
        public string OrderNo { get; set; }
        public ProductModel Product { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
        public string DiscountYn { get; set; }
        public string PayType { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Indate { get; set; }
    }
}