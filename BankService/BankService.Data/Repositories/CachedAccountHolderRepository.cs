using BankService.Data.Contexts;
using BankService.Domain.Contracts;
using BankService.Domain.Models;
using MongoDB.Bson;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankService.Data.Repositories
{
    public class CachedAccountHolderRepository : ICachedAccountHolderRepository
    {
        private readonly IRedisContext redisContext;
        private readonly RedisClient redisClient;

        public CachedAccountHolderRepository(IRedisContext redisContext)
        {
            this.redisContext = redisContext;
            this.redisClient = this.redisContext.GetClient();
        }

        public AccountHolder Get(string id)
        {
            string accountHolderHashKey = $"account:{id}";

            Dictionary<string, string> entries = this.redisClient.GetAllEntriesFromHash(accountHolderHashKey);

            if (entries == null || entries.Count == 0)
            {
                return null;
            }
            
            var accountHolder = new AccountHolder(
                id: new ObjectId(id),
                firstName: entries["first_name"],
                lastName: entries["last_name"]
            );

            var totalAccounts = Convert.ToInt32(entries["accounts"]);
            for (int i = 0; i < totalAccounts; i++)
            {
                var accountHashKey = $"{accountHolderHashKey}:acc:{i}";

                entries = this.redisClient.GetAllEntriesFromHash(accountHashKey);

                if (entries == null || entries.Count == 0)
                {
                    continue;
                }

                var account = new Account(
                    type: entries["account_type"],
                    balance: Convert.ToDouble(entries["account_balance"]),
                    currency: entries["currency"]
                );

                accountHolder.AddAccount(account);
            }

            return accountHolder;
        }

        public void Set(AccountHolder accountHolder)
        {
            if (accountHolder == null)
            {
                return;
            }

            var timeoutSpan = new TimeSpan(0, 0, this.redisContext.KeyTimeout);

            string accountHolderHashKey = $"account:{accountHolder.Id.ToString()}";

            this.redisClient.SetEntryInHash(accountHolderHashKey, "first_name", accountHolder.FirstName);
            this.redisClient.SetEntryInHash(accountHolderHashKey, "last_name", accountHolder.LastName);
            this.redisClient.SetEntryInHash(accountHolderHashKey, "accounts", accountHolder.Accounts.Count().ToString());

            this.redisClient.ExpireEntryIn(accountHolderHashKey, timeoutSpan);

            accountHolder.Accounts
                .ToList()
                .ForEach((account) =>
                {
                    var index = accountHolder.Accounts.ToList().IndexOf(account);
                    var accountHashKey = $"{accountHolderHashKey}:acc:{index}";

                    this.redisClient.SetEntryInHash(accountHashKey, "account_type", account.Type);
                    this.redisClient.SetEntryInHash(accountHashKey, "account_balance", account.Balance.ToString());
                    this.redisClient.SetEntryInHash(accountHashKey, "currency", account.Currency);

                    this.redisClient.ExpireEntryIn(accountHashKey, timeoutSpan);
                });
        }
    }
}
