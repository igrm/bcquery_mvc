using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace bcquery_mvc
{
    public class ApiAddressTransactionsController : ApiController
    {
        [HttpGet]
        [Route("api/addresstx")]
        public async Task<IHttpActionResult> Get([FromUri]ApiCallModel values)
        {
            var result = await new BCQuery().GetAddressTransactions(values.reportingValue, values.page, values.limit);
            return Ok(result);
        }
    }
}
