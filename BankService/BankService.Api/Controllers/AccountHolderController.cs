using BankService.Domain.Contracts;
using BankService.Domain.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;

namespace BankService.Api.Controllers
{
    [Route("api/accountHolders")]
    public class AccountHolderController : Controller
    {
        private readonly IAccountHolderRepository accountHolderRepository;
        private readonly ICachedAccountHolderRepository cachedAccountHolderRepository;

        public AccountHolderController(IAccountHolderRepository repository, ICachedAccountHolderRepository cachedRepository)
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
        public IActionResult Get(string id)
        {
            var accountHolder = this.cachedAccountHolderRepository.Get(id);

            if (accountHolder != null)
            {
                return new ObjectResult(accountHolder);
            }

            accountHolder = this.accountHolderRepository.GetById(id);

            if (accountHolder == null)
            {
                return new HttpNotFoundResult();
            }
            this.cachedAccountHolderRepository.Set(accountHolder);

            return new ObjectResult(accountHolder);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountHolder accountHolder)
        {
            if (accountHolder == null)
            {
                return HttpBadRequest();
            }

            try
            {
                this.accountHolderRepository.Create(accountHolder);

                return new ObjectResult(accountHolder);
            }
            catch (Exception ex)
            {
                return HttpBadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AccountHolder accountHolder)
        {
            if (id == null || accountHolder == null || !accountHolder.Id.ToString().Equals(id))
            {
                return HttpBadRequest();
            }

            var accountHolderFromDB = this.accountHolderRepository.GetById(id);

            if (accountHolderFromDB == null)
            {
                return new HttpNotFoundResult();
            }

            try
            {
                this.accountHolderRepository.Update(accountHolder, id);

                return new ObjectResult(accountHolderFromDB);
            }
            catch (Exception ex)
            {
                return HttpBadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var accountHolder = this.accountHolderRepository.GetById(id);

            if (accountHolder == null)
            {
                return new HttpNotFoundResult();
            }

            this.accountHolderRepository.Delete(id);
            this.cachedAccountHolderRepository.Remove(accountHolder);

            return new HttpOkResult();
        }
    }
}
