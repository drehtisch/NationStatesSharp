using System.Collections.Generic;

namespace NationStatesSharp.Interfaces
{
    public interface IRequestDispatcher
    {
        void Dispatch(Request request, int priority);

        void Dispatch(IEnumerable<Request> requests, int priority);

        void Start();

        void Shutdown();
    }
}