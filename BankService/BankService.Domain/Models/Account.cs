using MongoDB.Bson.Serialization.Attributes;

namespace BankService.Domain.Models
{
    public class Account
    {
        protected Account()
        {

        }

        [BsonElement("account_type")]
        public string Type { get; set; }

        [BsonElement("account_balance")]
        public double Balance { get; set; }

        [BsonElement("currency")]
        public string Currency { get; set; }
    }
}
