using MongoDB.Bson;

namespace SND.Models
{
    public class CompanyModel
    {
        public ObjectId Id { get; set; }
        public string ComName { get; set; }
        public string ComAddr { get; set; }
        public string ComTell { get; set; }
        public string ComCeoName { get; set; }
        public string ComCeoTell { get; set; }
    }
}