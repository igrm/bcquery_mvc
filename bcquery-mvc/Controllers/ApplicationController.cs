using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bcquery_mvc.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        [Route("")]
        public ActionResult Main()
        {
            return View(new TranslationViewModel());
        }

        public ActionResult BlocksByDate()
        {
            return PartialView(new TranslationViewModel());
        }

        public ActionResult BlockTransactions()
        {
            return PartialView(new TranslationViewModel());
        }

        public ActionResult AddressTransactions()
        {
            return PartialView(new TranslationViewModel());
        }

        public ActionResult About()
        {
            return PartialView(new TranslationViewModel());
        }
    }
}