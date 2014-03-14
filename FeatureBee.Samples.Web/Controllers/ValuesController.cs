using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FeatureBee.Samples.Web.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("values")]
        public IEnumerable<string> Get()
        {
            if (Feature.IsEnabled("FeatureOne"))
                return new string[] { "value1", "value2" };

            return new string[] {};
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("values/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        [Route("values")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("values/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("values/{id}")]
        public void Delete(int id)
        {
        }
    }
}