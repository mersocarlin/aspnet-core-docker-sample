using BankService.Data.Contexts;
using BankService.Domain.Contracts;
using BankService.Domain.Models;

namespace BankService.Data.Repositories
{
    public class AccountHolderRepository : Repository<AccountHolder>, IAccountHolderRepository
    {
        private const string COLLECTION_NAME = "bank_data";

        public AccountHolderRepository(IMongoDBContext mongoDBContext)
            : base(mongoDBContext, COLLECTION_NAME)
        {

        }
    }
}
