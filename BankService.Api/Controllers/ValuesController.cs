using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace BankService.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private List<string> values = new List<string> () 
        {
            "value1",
            "value2",
            "value3",
            "value4",
            "value5", 
        };
        
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return values;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {  
            if (id <= 0 || id > values.Count) 
            {
                return null;
            }
            
            return values[id - 1];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
