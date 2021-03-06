using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NationStatesSharp.Interfaces
{
    public interface IHttpDataService
    {
        Task<HttpResponseMessage> ExecuteRequestAsync(Request request, CancellationToken cancellationToken);
    }
}