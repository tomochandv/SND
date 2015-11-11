using MongoDB.Bson;

namespace SND.Models
{
    public class OutCompanyModel
    {
        public ObjectId Id { get; set; }
        public CompanyModel Company { get; set; }
        public string OutComName { get; set; }
        public string OutComAddr { get; set; }
        public string OutComTell { get; set; }
    }
}