using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NationStatesSharp
{
    public class RequestWorker
    {
        public const long API_REQUEST_INTERVAL = 6125000; //0,6 s + additional 0,0125 s as buffer -> 0,6125 s
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
                await _semaphore.WaitAsync().ConfigureAwait(false);
                var request = _requestQueue.Dequeue();
                await _dataService.ExecuteRequestAsync(request).ContinueWith(_continue);
            }
        }
    }
}