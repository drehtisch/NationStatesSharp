using Serilog;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NationStatesSharp
{
    public class HttpDataService
    {
        private HttpMessageHandler _httpMessageHandler = null;
        private ILogger _logger;
        private string _userAgent;

        public HttpDataService(string userAgent, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(userAgent)) throw new InvalidOperationException("No Request can be send when contact info hasn't been provided.");
            if (logger is null) throw new ArgumentNullException(nameof(logger));
            _userAgent = userAgent;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> ExecuteRequestAsync(Request request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            using (HttpClient client = GetHttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", _userAgent);
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, request.Uri);
                _logger.Debug("[{traceId}] Executing {httpMethod}-Request to {requestUrl}", request.TraceId, requestMessage.Method, request.Uri);
                HttpResponseMessage response = await client.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.Error("[{traceId}] Request finished with response: {StatusCode}: {ReasonPhrase}", request.TraceId, (int)response.StatusCode, response.ReasonPhrase);
                }
                else
                {
                    _logger.Debug("[{traceId}] Request finished with response: {StatusCode}: {ReasonPhrase}", request.TraceId, (int)response.StatusCode, response.ReasonPhrase);
                }
                return response;
            }
        }

        public HttpClient GetHttpClient() => _httpMessageHandler != null ? new HttpClient(_httpMessageHandler) : new HttpClient();

        public void SetHttpMessageHandler(HttpMessageHandler httpMessageHandler)
        {
            _httpMessageHandler = httpMessageHandler;
        }
    }
}