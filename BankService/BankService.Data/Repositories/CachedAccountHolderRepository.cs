using BankService.Data.Contexts;
using BankService.Domain.Contracts;
using BankService.Domain.Models;
using MongoDB.Bson;
using StackExchange.Redis;
using System;
using System.Linq;

namespace BankService.Data.Repositories
{
    public class CachedAccountHolderRepository : ICachedAccountHolderRepository
    {
        private readonly IRedisContext redisContext;
        private readonly IDatabase database;

        public CachedAccountHolderRepository(IRedisContext redisContext)
        {
            this.redisContext = redisContext;
            this.database = this.redisContext.GetDatabase();
        }

        private RedisValue GetEntryValue(string field, HashEntry[] entries)
        {
            var entry = entries
                .AsEnumerable()
                .Where(e => e.Name.Equals(field))
                .FirstOrDefault();

            if (entry != null)
            {
                return entry.Value;
            }

            return RedisValue.Null;
        }

        public AccountHolder Get(string id)
        {
            string accountHolderHashKey = $"account:{id}";

            HashEntry[] entries = this.database.HashGetAll(accountHolderHashKey);

            if (entries == null || entries.Length == 0)
            {
                return null;
            }

            var accountHolder = new AccountHolder(
                id: new ObjectId(id),
                firstName: this.GetEntryValue("first_name", entries),
                lastName: this.GetEntryValue("last_name", entries)
            );

            var totalAccounts = Convert.ToInt32(this.GetEntryValue("accounts", entries));
            for (int i = 0; i < totalAccounts; i++)
            {
                var accountHashKey = $"{accountHolderHashKey}:acc:{i}";

                entries = this.database.HashGetAll(accountHashKey);

                if (entries == null || entries.Length == 0)
                {
                    continue;
                }

                var account = new Account(
                    type: this.GetEntryValue("account_type", entries),
                    balance: Convert.ToDouble(this.GetEntryValue("account_balance", entries)),
                    currency: this.GetEntryValue("currency", entries)
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

            string accountHolderHashKey = $"account:{accountHolder.Id.ToString()}";

            this.database.HashSet(
                accountHolderHashKey,
                new HashEntry[]
                {
                    new HashEntry("first_name", accountHolder.FirstName),
                    new HashEntry("last_name", accountHolder.LastName),
                    new HashEntry("accounts", accountHolder.Accounts.Count())
                }
            );

            var timeoutSpan = new TimeSpan(0, 0, this.redisContext.KeyTimeout);

            this.database.KeyExpire(accountHolderHashKey, timeoutSpan);

            accountHolder.Accounts
                .ToList()
                .ForEach((account) =>
                {
                    var index = accountHolder.Accounts.ToList().IndexOf(account);
                    var accountHashKey = $"{accountHolderHashKey}:acc:{index}";

                    this.database.HashSet(
                        accountHashKey,
                        new HashEntry[]
                        {
                            new HashEntry("account_type", account.Type),
                            new HashEntry("account_balance", account.Balance),
                            new HashEntry("currency", account.Currency)
                        }
                    );

                    this.database.KeyExpire(accountHashKey, timeoutSpan);
                });
        }
    }
}
