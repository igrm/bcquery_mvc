using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcquery_mvc
{
    public class TranslationViewModel
    {
        public string LBL_TITLE { get { return service.GetText("LBL_TITLE"); } }
        public string LBL_GETBLOCKSBYDATE { get { return service.GetText("LBL_GETBLOCKSBYDATE"); } }
        public string LBL_GETBLOCKTRANSACTIONS { get { return service.GetText("LBL_GETBLOCKTRANSACTIONS"); } }
        public string LBL_GETADDRESSTRANSACTIONS { get { return service.GetText("LBL_GETADDRESSTRANSACTIONS"); } }
        public string LBL_ABOUT { get { return service.GetText("LBL_ABOUT"); } }
        public string LBL_GETBLOCKSBYDATE_DESC { get { return service.GetText("LBL_GETBLOCKSBYDATE_DESC"); } }
        public string LBL_REPORTINGDATE { get { return service.GetText("LBL_REPORTINGDATE"); } }
        public string LBL_SEARCH { get { return service.GetText("LBL_SEARCH"); } }
        public string LBL_BLOCKHEIGHT { get { return service.GetText("LBL_BLOCKHEIGHT"); } }
        public string LBL_BLOCKHASH { get { return service.GetText("LBL_BLOCKHASH"); } }
        public string LBL_DATETIMECREATED { get { return service.GetText("LBL_DATETIMECREATED"); } }
        public string LBL_TRANSACTIONSCOUNT { get { return service.GetText("LBL_TRANSACTIONSCOUNT"); } }
        public string LBL_GETBLOCKTRANSACTIONS_DESC { get { return service.GetText("LBL_GETBLOCKTRANSACTIONS_DESC"); } }
        public string LBL_TRANSACTIONHASH { get { return service.GetText("LBL_TRANSACTIONHASH"); } }
        public string LBL_CONFIRMATIONCOUNT { get { return service.GetText("LBL_CONFIRMATIONCOUNT"); } }
        public string LBL_ADDRESS { get { return service.GetText("LBL_ADDRESS"); } }
        public string LBL_BTCAMOUNT { get { return service.GetText("LBL_BTCAMOUNT"); } }
        public string LBL_GETADDRESSTRANSACTIONS_DESC { get { return service.GetText("LBL_GETADDRESSTRANSACTIONS_DESC"); } }
        public string LBL_WEBAPP { get { return service.GetText("LBL_WEBAPP"); } }
        public string LBL_PROVIDES { get { return service.GetText("LBL_PROVIDES"); } }
        public string LBL_TYPE { get { return service.GetText("LBL_TYPE"); } }

        private TranslationService service;
        public TranslationViewModel()
        {
            service = new TranslationService();
        }
    }
}