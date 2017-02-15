using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace bcquery_mvc
{
    public class ApiBlockTransactionsController : ApiController
    {
        [HttpGet]
        [Route("api/blocktx")]
        public async Task<IHttpActionResult> Get([FromUri]ApiCallModel values)
        {
            var result = await new BCQuery().GetBlockTransactions(values.reportingValue, values.page, values.limit);
            return Ok(result);
        }
    }
}
