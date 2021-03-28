using NationStatesSharp.Enums;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NationStatesSharp
{
    internal class RequestPriorityQueue : SimplePriorityQueue<Request, int>
    {
        /// <summary>
        /// The queue detected, during the last Enqueue operation, that no one is waiting for new items. It is therefore reasonable to assume that the consumer died.
        /// Use this event to detected consumer failures and for restarting these.
        /// </summary>
        public event EventHandler Jammed;

        public new void Enqueue(Request item, int priority)
        {
            if (item.Status == RequestStatus.Pending)
            {
                if (!isWaiting && Count > 2)
                {
                    Jammed?.Invoke(this, new EventArgs());
                }
                base.Enqueue(item, priority);
                _waitCompletionSource?.TrySetResult(true);
                isWaiting = false;
            }
            if (this.Any(item => item.Status != RequestStatus.Pending))
            {
                this.Where(item => item.Status != RequestStatus.Pending).ToList().ForEach(item => TryRemove(item));
            }
        }

        public new void EnqueueWithoutDuplicates(Request item, int priority) => throw new NotImplementedException();

        private TaskCompletionSource<bool> _waitCompletionSource;
        private bool isWaiting = false;

        public Task<bool> WaitForNextItemAsync(CancellationToken cancellationToken)
        {
            if (this.Any(t => t.Status == RequestStatus.Pending))
            {
                return Task.FromResult(true);
            }
            else
            {
                if (!isWaiting)
                {
                    _waitCompletionSource = new TaskCompletionSource<bool>();
                    isWaiting = true;
                }
                cancellationToken.Register(() => _waitCompletionSource.TrySetResult(false));
                return _waitCompletionSource.Task;
            }
        }
    }
}