using BankService.Domain.Models;

namespace BankService.Domain.Contracts
{
    public interface ICachedAccountHolderRepository
    {
        AccountHolder Get(string id);
        void Set(AccountHolder accountHolder);
    }
}
