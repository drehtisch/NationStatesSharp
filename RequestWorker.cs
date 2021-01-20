using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NationStatesSharp
{
    public class RequestWorker
    {
        public const long API_REQUEST_INTERVAL = 6000000; //0,6 s
        private readonly HttpDataService _dataService;
        private readonly ILogger _logger;

        private readonly RequestPriorityQueue _requestQueue = new();
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public RequestWorker(string userAgent, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(userAgent)) throw new InvalidOperationException("No Request can be send when contact info hasn't been provided.");
            _dataService = new HttpDataService(userAgent, logger);
            _logger = logger;
            _requestQueue.Jammed += RequestQueue_Jammed;
        }

        private void RequestQueue_Jammed(object sender, EventArgs e)
        {
            _logger.Warning("RequestQueue jammed. No waiting consumers found. Consumers possibly died. Trying to restart worker.");
            RestartRequired?.Invoke(this, e);
        }

        public event EventHandler RestartRequired;

        public void Enqueue(Request request, int priority = 1000)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));
            _requestQueue.Enqueue(request, priority);
            _logger.Verbose("Request [{traceId}] has been queued. Queue size: {size}", request.TraceId, _requestQueue.Count);
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            async void _continue(Task<HttpResponseMessage> prev)
            {
                await Task.Delay(TimeSpan.FromTicks(API_REQUEST_INTERVAL)).ConfigureAwait(false);
                if (_semaphore.CurrentCount == 0)
                {
                    _semaphore.Release();
                }
            }
            while (await _requestQueue.WaitForNextItemAsync(cancellationToken).ConfigureAwait(false))
            {
                var ticks = DateTime.UtcNow.Ticks;
                var request = _requestQueue.Dequeue();
                _logger.Verbose("Request [{traceId}] has been dequeued. Queue size: {size}", request.TraceId, _requestQueue.Count);
                _logger.Verbose("[{traceId}]: Acquiring Semaphore", request.TraceId);
                await _semaphore.WaitAsync().ConfigureAwait(false);
                _logger.Verbose("[{traceId}]: Aquiring Semaphore took {duration}", request.TraceId, TimeSpan.FromTicks(DateTime.UtcNow.Ticks).Subtract(TimeSpan.FromTicks(ticks)));
                try
                {
                    var task = _dataService.ExecuteRequestAsync(request, cancellationToken);
                    _ = task.ContinueWith(_continue, cancellationToken);
                    var httpResponse = await task.ConfigureAwait(false);
                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        request.Fail(new HttpRequestFailedException($"{(int)httpResponse.StatusCode} - {httpResponse.ReasonPhrase}"));
                    }
                    if (request.ResponseFormat == ResponseFormat.Xml)
                    {
                        request.Complete(await httpResponse.ReadXmlAsync(_logger));
                    }
                    else if (request.ResponseFormat == ResponseFormat.Boolean)
                    {
                        request.Complete(await httpResponse.ReadBooleanAsync());
                    }
                    else
                    {
                        throw new NotImplementedException($"Unknown ResponseFormat: {request.ResponseFormat}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "A error occurred.");
                    request.Fail(ex);
                }
            }
        }
    }

    public static class Extensions
    {
        public static async Task<XmlDocument> ReadXmlAsync(this HttpResponseMessage httpResponse, ILogger logger)
        {
            if (httpResponse is null)
                throw new ArgumentNullException(nameof(httpResponse));
            if (httpResponse.IsSuccessStatusCode)
            {
                using (Stream stream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    try
                    {
                        XmlDocument xml = new XmlDocument();
                        if (logger.IsEnabled(Serilog.Events.LogEventLevel.Verbose))
                        {
                            logger.Verbose(await httpResponse.Content.ReadAsStringAsync());
                        }
                        xml.Load(stream);
                        return xml;
                    }
                    catch (XmlException ex)
                    {
                        throw new ApplicationException($"A error while loading xml occured.", ex);
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public static async Task<bool?> ReadBooleanAsync(this HttpResponseMessage httpResponse)
        {
            if (httpResponse is null)
                throw new ArgumentNullException(nameof(httpResponse));
            if (httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return bool.TryParse(content, out bool result) ? (bool?)result : (bool?)null;
            }
            else
            {
                return null;
            }
        }
    }
}