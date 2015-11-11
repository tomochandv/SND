using MongoDB.Bson;

namespace SND.Models
{
    public class BomCategoryModel
    {
        public ObjectId Id { get; set; }
        public CompanyModel Company { get; set; }
        public string BomCategoryName { get; set; }
    }
}