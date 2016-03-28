using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BankService.Domain.Models
{
    public class AccountHolder
    {
        public AccountHolder()
        {
            this.Accounts = new List<Account>();
        }

        public ObjectId Id { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("accounts")]
        public IEnumerable<Account> Accounts { get; set; }
    }
}
