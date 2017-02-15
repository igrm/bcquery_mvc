using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcquery_mvc
{
    public class ApiCallModel
    {
        public string order { get; set; }
        public string reportingValue { get; set;}
        public int page { get; set; }
        public int limit { get; set; }
    }
}