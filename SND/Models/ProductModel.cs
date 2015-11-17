using System.Collections.Generic;
using MongoDB.Bson;
namespace SND.Models
{
    public class ProductModel
    {
        public ObjectId Id { get; set; }
        public CompanyModel Company { get; set; }
        public ProductCategoryModel ProductCategory { get; set; }
        public string ProductNm { get; set; }
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public string Description { get; set; }
        public string Bom { get; set; }
        public List<string> ImagUrl { get; set; }
        public bool UseYn { get; set; }
    }
}