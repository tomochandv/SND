using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace SND.Models
{
    public class StaticsModel
    {
        public string yyyy { get; set; }
        public string mm { get; set; }
        public int price { get; set; }
        public int count { get; set; }
    }

    public class StaticProductMasterModel
    {
        public string yyyy { get; set; }
        public string mm { get; set; }
        public List<StaticsProductProModel> ProductList { get; set; }
    }

    public class StaticsProductProModel
    {
        public string productName { get; set; }
        public ObjectId Id { get; set; }
        public int qty { get; set; }
        public int price { get; set; }
    }
}