using BankService.Domain.Contracts;
using BankService.Domain.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

namespace BankService.Api.Controllers
{
    [Route("api/[controller]")]
    public class BankController : Controller
    {
        private readonly IAccountHolderRepository accountHolderRepository;
        private readonly ICachedAccountHolderRepository cachedAccountHolderRepository;

        public BankController(IAccountHolderRepository repository, ICachedAccountHolderRepository cachedRepository)
        {
            this.accountHolderRepository = repository;
            this.cachedAccountHolderRepository = cachedRepository;
        }

        [HttpGet]
        public IEnumerable<AccountHolder> Get()
        {
            return this.accountHolderRepository.GetWithLimit(100);
        }

        [HttpGet("{id}")]
        public AccountHolder Get(string id)
        {
            var accountHolder = this.cachedAccountHolderRepository.Get(id);

            if (accountHolder != null)
            {
                return accountHolder;
            }

            accountHolder = this.accountHolderRepository.GetById(id);

            this.cachedAccountHolderRepository.Set(accountHolder);

            return accountHolder;
        }
    }
}
