using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace bcquery_mvc
{
    public class ApiBlocksController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("api/blocks")]
        public async Task<IHttpActionResult> Get([FromUri]ApiCallModel values)
        {
            var result = await new BCQuery().GetBlocksByDate(DateTime.Parse(values.reportingValue), values.page, values.limit);
            return Ok(result);
        }
    }
}