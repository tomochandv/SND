using MongoDB.Bson;

namespace SND.Models
{
    public class ProductCategoryModel
    {
        public ObjectId Id { get; set; }
        public CompanyModel Company { get; set; }
        public string ProductCategoryNm { get; set; }
    }
}