using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace BankService.Domain.Models
{
    public class AccountHolder
    {
        protected AccountHolder()
        {
            this.Accounts = new List<Account>();
        }

        public AccountHolder(ObjectId id, string firstName, string lastName)
            : this()
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public ObjectId Id { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("accounts")]
        public IEnumerable<Account> Accounts { get; set; }

        public void AddAccount(Account account)
        {
            this.Accounts = this.Accounts.Concat(new List<Account>() { account });
        }
    }
}
