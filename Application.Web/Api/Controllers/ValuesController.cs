namespace Application.Web.Api.Controllers
{
    using System.Collections.Generic;

    using Application.Web.Api.Infrastructure;

    using Microsoft.AspNetCore.Mvc;

    public class ValuesController : BaseApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
