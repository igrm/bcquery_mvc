using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace bcquery_mvc
{
    public class BCQuery
    {
        private bool HasAttr(ExpandoObject expando, string key)
        {
            return ((IDictionary<string, Object>)expando).ContainsKey(key);
        }

        //implements conversion from unix timestamp to datatime object
        public DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(Convert.ToDouble(unixTimeStamp)).ToLocalTime();
            return dtDateTime;
        }

        //implements conversion from datetime object to unix timestamp
        public long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return Convert.ToInt64((TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds);
        }

        //binary search implementation
        private async Task<long> BinarySearch(long periodStartHeight, long periodEndHeight, DateTime observedDate)
        {
            //get the period middle height
            long median = periodEndHeight - ((periodEndHeight - periodStartHeight) / 2) - 1;

            //for debugging
            var left = new Tuple<long, long>(periodStartHeight, median);
            var right = new Tuple<long, long>(median + 1, periodEndHeight);

            //search implementation
            dynamic blockSet = await GetBlock(median);
            long unixtime = blockSet.time;

            DateTime resultDate = UnixTimeStampToDateTime(unixtime);
            if (resultDate.Date > observedDate.Date)
                return await BinarySearch(periodStartHeight, median, observedDate);
            else if (resultDate.Date < observedDate.Date)
                return await BinarySearch(median + 1, periodEndHeight, observedDate);

            return blockSet.height;
        }

        //implements get chain height by date
        public async Task<long> GetHeightByDate(long currentHeight, DateTime reportingDate)
        {
            return await BinarySearch(15000, currentHeight, reportingDate);
        }
        public async Task<dynamic> GetBlocksByDate(DateTime reportingDate, int pageNo, int rowsPerPage)
        {
            var result = new List<dynamic>();
            long currentHeight = await GetHeight();
            long startHeight = await GetHeightByDate(currentHeight, reportingDate);
            long[] border = new long[2] { startHeight + (pageNo * rowsPerPage), currentHeight+1};

            for (long i = startHeight + ((pageNo - 1) * rowsPerPage); i < border.Min(); i++)
            {
                dynamic block = await GetBlock(i);
                result.Add(new { height = block.height, hash = block.hash, created = UnixTimeStampToDateTime(block.time), txcount = block.n_tx });
            }

            return new { total = currentHeight - startHeight, totalPages = Math.Ceiling(((decimal)currentHeight - (decimal)startHeight) / (decimal)rowsPerPage), blocks = result };
        }

        public async Task<dynamic> GetBlockTransactions(string blockhash, int pageNo, int rowsPerPage)
        {
            var result = new List<dynamic>();

            //1. step - read specific block
            dynamic block = await GetBlock(blockhash);
            long currentHeight = await GetHeight();
            long confirmationCount = currentHeight - block.height + 1;

            //2. step - loop thru transactions in range of defined page
            long[] border = new long[] { ((pageNo) * rowsPerPage), block.n_tx};
            for (int i = ((pageNo - 1) * rowsPerPage); i < border.Min(); i++)
            {
                var resultItem = new { hash = block.tx[i].hash, confirmCount = confirmationCount, input = new List<dynamic>(), output = new List<dynamic>() };
                dynamic transaction = await GetTransaction(block.tx[i].hash);
                foreach(var input in transaction.inputs)
                {
                    if (HasAttr(input, "prev_out"))
                    {
                        dynamic prevTransaction = await GetTransaction(input.prev_out.tx_index);
                        resultItem.input.Add(new { address = input.prev_out.addr, hash = prevTransaction.hash, amount = (decimal)input.prev_out.value / 100000000 });
                    }
                }
                foreach(var @out in transaction.@out)
                {
                    resultItem.output.Add(new { address= HasAttr(@out, "addr") ? @out.addr : "<not provided>", hash = block.tx[i].hash, amount = (decimal) @out.value /100000000});
                }
                result.Add(resultItem);
            }

            return new { total = block.n_tx, totalPages = Math.Ceiling((decimal)block.n_tx / (decimal)rowsPerPage), tx = result };
        }

        public async Task<dynamic> GetAddressTransactions(string address, int pageNo, int rowsPerPage)
        {
            var result = new List<dynamic>();

            //1. step - read specific block
            dynamic addressTxs = await Read(String.Format("https://blockchain.info/address/{0}?limit={1}&offset={2}&format=json", address, rowsPerPage, (pageNo - 1) * rowsPerPage + 1));
            long currentHeight = await GetHeight();

            //2. step - loop thru transactions in range of defined page
            foreach (var transaction in addressTxs.txs)
            {
                var resultItem = new { hash = transaction.hash, confirmCount= currentHeight - transaction.block_height + 1, input = new List<dynamic>(), output = new List<dynamic>() };
                foreach (var input in transaction.inputs)
                {
                    if (HasAttr(input, "prev_out"))
                    {
                        dynamic prevTransaction = await GetTransaction(input.prev_out.tx_index);
                        resultItem.input.Add(new { address = input.prev_out.addr, hash = prevTransaction.hash, amount = (decimal)input.prev_out.value / 100000000 });
                    }
                }
                foreach (var @out in transaction.@out)
                {
                    resultItem.output.Add(new { address = HasAttr(@out, "addr") ? @out.addr : "<not provided>", hash = transaction.hash, amount = (decimal)@out.value / 100000000 });
                }
                result.Add(resultItem);
            }
            return new { total = addressTxs.n_tx, totalPages = Math.Ceiling((decimal)addressTxs.n_tx / (decimal)rowsPerPage), tx = result };
        }

        public async Task<dynamic> Read(string url)
        {
            ExpandoObject result = new ExpandoObject();
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<ExpandoObject>();
            }
            return result;
        }

        //implements read of best chain height
        public async Task<long> GetHeight()
        {
            dynamic blockcount = await Read("https://blockchain.info/latestblock");
            return blockcount.height;
        }

        //implements read of one specific block by height
        public async Task<dynamic> GetBlock(long height)
        {
            dynamic block = await Read(String.Format("https://blockchain.info/block-height/{0}?format=json", height));
            return block.blocks[0];
        }

        //implements read of one specific block by hash
        public async Task<dynamic> GetBlock(string hash)
        {
            dynamic block = await Read(String.Format("https://blockchain.info/rawblock/{0}", hash));
            return block;
        }

        //implements read of specific transaction by hash
        public async Task<dynamic> GetTransaction(string hash)
        {
            dynamic tx = await Read(String.Format("https://blockchain.info/rawtx/{0}", hash));
            return tx;
        }

        //implements read of specific transaction by index
        public async Task<dynamic> GetTransaction(long index)
        {
            dynamic tx = await Read(String.Format("https://blockchain.info/tx-index/{0}?format=json", index));
            return tx;
        }
    }
}