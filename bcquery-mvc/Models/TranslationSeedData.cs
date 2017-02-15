using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace bcquery_mvc
{
    public class TranslationSeedData :DropCreateDatabaseAlways<TranslationEntities>
    {
        protected override void Seed(TranslationEntities context)
        {
            var lang = new Language() { LanguageCode="ENG" };
            context.Languages.Add(lang);
            foreach (var item in InitialData())
            {
                var ti = new TranslationItem() { TranslationItemCode = item.Key };
                var tt = new TranslationText() { TranslationTextValue = item.Value, TranslationItem = ti, Language = lang };
                context.TranslationItems.Add(ti);
                context.TranslationTexts.Add(tt);
            }
            context.Commit();
        }

        private static Dictionary<string, string> InitialData()
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();
            translation.Add("LBL_TITLE", "Bitcoin Blockchain Query Tool");
            translation.Add("LBL_GETBLOCKSBYDATE", "Get Blocks by date");
            translation.Add("LBL_GETBLOCKTRANSACTIONS", "Get Block transactions");
            translation.Add("LBL_GETADDRESSTRANSACTIONS", "Get Address Transactions");
            translation.Add("LBL_ABOUT", "About...");
            translation.Add("LBL_GETBLOCKSBYDATE_DESC", "Returns a list of blocks since a specified date. Each item in the list contains the block height, block hash, datetime created, transaction count.");
            translation.Add("LBL_REPORTINGDATE", "Reporting date");
            translation.Add("LBL_SEARCH", "Search");
            translation.Add("LBL_BLOCKHEIGHT", "Block height");
            translation.Add("LBL_BLOCKHASH", "Block hash");
            translation.Add("LBL_DATETIMECREATED", "Datetime created");
            translation.Add("LBL_TRANSACTIONSCOUNT", "Transactions count");
            translation.Add("LBL_GETBLOCKTRANSACTIONS_DESC", "Returns a list of transactions of a specified block. Each item in the list contains the transaction hash, confirmation count, a list of inputs, a list of outputs. For each item in the input/output list, the item contains the address, the \ntransaction hash of the input/output and the BTC amount.");
            translation.Add("LBL_TRANSACTIONHASH", "Transaction hash");
            translation.Add("LBL_CONFIRMATIONCOUNT", "Confirmation count");
            translation.Add("LBL_ADDRESS", "Address");
            translation.Add("LBL_BTCAMOUNT", "BTC amount");
            translation.Add("LBL_GETADDRESSTRANSACTIONS_DESC", "Returns a list of transactions of a specified address. Each item in the list contains the transaction hash, confirmation count, a list of inputs, a list of outputs. For each item in the input/output list, the item contains the address, the transaction hash of the input/output and the BTC amount.");
            translation.Add("LBL_WEBAPP", "Web app that reads blockchain data. ");
            translation.Add("LBL_PROVIDES", "App provides the following output:");
            translation.Add("LBL_TYPE", "Type");

            return translation;
        }
    }
}