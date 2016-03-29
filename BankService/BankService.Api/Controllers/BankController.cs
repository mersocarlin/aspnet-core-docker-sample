using BankService.Domain.Contracts;
using BankService.Domain.Models;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

namespace BankService.Api.Controllers
{
    [Route("api/[controller]")]
    public class BankController : Controller
    {
        private readonly IAccountHolderRepository repository;

        public BankController(IAccountHolderRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<AccountHolder> Get()
        {
            return this.repository.GetWithLimit(100);
        }

        [HttpGet("{id}")]
        public AccountHolder Get(string id)
        {
            return this.repository.GetById(id);
        }
    }
}
