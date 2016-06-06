using StockfighterClient.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockfighterClient
{
    public class Client : IDisposable
    {
        const string baseUrl = "https://api.stockfighter.io/";

        private HttpClient client;

        public Client()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
            client.DefaultRequestHeaders.Add("X-Starfighter-Authorization", "671e81eb23fab77549061c52e2d9d69b67428396");
        }

        /// <summary>
        /// Checks the heartbeat of the stockfighter API
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetHB()
        {
            var resp = await Get("/heartbeat");
            var bresp = await resp.Content.ReadAsAsync<BaseResponse>();
            return bresp.Ok;
        }

        /// <summary>
        /// Checks heartbeat of a specific venue - verifies venue exists and can be traded on
        /// </summary>
        /// <param name="venue"></param>
        /// <returns></returns>
        public async Task<bool> GetHB(string venue)
        {
            var resp = await Get(string.Format("venues/{0}/heartbeat", venue));
            return (await resp.Content.ReadAsAsync<BaseResponse>()).Ok;
        }

        /// <summary>
        /// Lists available stocks on a given venue
        /// </summary>
        /// <param name="venue"></param>
        /// <returns></returns>
        public async Task<StocksResponse> GetStocks(string venue)
        {
            return await Get<StocksResponse>("venues/{0}/stocks", venue);
        }

        /// <summary>
        /// Retrieves the orderbook for a stock
        /// </summary>
        /// <param name="venue"></param>
        /// <param name="stock"></param>
        /// <returns></returns>
        public async Task<OrderBookResponse> GetOrderBook(string venue, string stock)
        {
            return await Get<OrderBookResponse>("/venues/{0}/stocks/{1}", venue, stock);
        }

        public async Task<OrderStatusResponse> PostOrder(OrderRequest request)
        {
            string uri = string.Format("/venues/{0}/stocks/{1}/orders", request.Venue, request.Stock);
            var httpResp = await Post(uri, request);
            return await httpResp.Content.ReadAsAsync<OrderStatusResponse>();
        }

        public async Task<QuoteResponse> GetQuote(string venue, string stock)
        {
            return await Get<QuoteResponse>("/venues/{0}/stocks/{1}/quote", venue, stock);
        }

        public async Task<OrderStatusResponse> GetOrderStatus(long orderId, string venue, string stock)
        {
            return await Get<OrderStatusResponse>("/venues/{0}/stocks/{1}/orders/{2}", venue, stock, orderId);
        }

        public async Task<OrderStatusResponse> DeleteOrder(long orderId, string venue, string stock)
        {
            string uri = CleanUri(string.Format("/venues/{0}/stocks/{1}/orders/{2}", venue, stock, orderId));
            var httpResp = await client.DeleteAsync(uri);
            CheckResponse(uri, httpResp);
            return await httpResp.Content.ReadAsAsync<OrderStatusResponse>();
        }

        public async Task<OrderStatusCollectionResponse> GetAllOrders(string venue, string accountId)
        {
            return await Get<OrderStatusCollectionResponse>("/venues/{0}/accounts/{1}/orders", venue, accountId);
        }

        public async Task<OrderStatusCollectionResponse> GetAllOrders(string venue, string accountId, string stock)
        {
            return await Get<OrderStatusCollectionResponse>("/venues/{0}/accounts/{1}/stocks/{2}/orders", venue, accountId, stock);
        }

        #region Private

        private async Task<T> Get<T>(string uriFormat, params object[] parms) where T : BaseResponse
        {
            string uri = string.Format(uriFormat, parms);
            var httpResp = await Get(uri);
            var objResp = await httpResp.Content.ReadAsAsync<T>();
            if(!objResp.Ok)
            {
                var err = string.Format("Error occured in call to {0}: {1}", uri, objResp.Error);
                throw new Exception(err);
            }
            return objResp;
        }

        private async Task<HttpResponseMessage> Post<T>(string uri, T data)
        {
            uri = CleanUri(uri);
            var resp = await client.PostAsJsonAsync(uri, data);
            CheckResponse(uri, resp);
            return resp;
        }

        private void CheckResponse(string uri, HttpResponseMessage resp)
        {
            if (!resp.IsSuccessStatusCode)
            {
                var err = string.Format("Call to {0} failed with code {1}: {2}",
                                        uri,
                                        resp.StatusCode,
                                        resp.Content);
                throw new Exception(err);
            }
        }

        private async Task<HttpResponseMessage> Get(string uri)
        {
            uri = CleanUri(uri);
            HttpResponseMessage resp = await client.GetAsync(uri);
            CheckResponse(uri, resp);
            return resp;
        }

        private string CleanUri(string uri)
        {
            return string.Format("/ob/api/{0}", uri).Replace("//", "/");
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                client.Dispose();
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~API() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
