using MongoDB.Bson;

namespace SND.Models
{
    public class BomModel
    {
        public ObjectId Id { get; set; }
        public CompanyModel Company { get; set; }
        public BomCategoryModel BomCategory { get; set; }
        public string BomName { get; set; }
        public string Unit { get; set; }

    }
}