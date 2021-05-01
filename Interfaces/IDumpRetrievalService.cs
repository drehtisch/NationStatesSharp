using NationStatesSharp.Enums;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NationStatesSharp.Interfaces
{
    public interface IDumpRetrievalService
    {
        Task<Stream> DownloadDumpAsync(DumpType type, CancellationToken cancellationToken);

        Task<Stream> DownloadDumpAsync(string url, CancellationToken cancellationToken);

        bool IsLocalDumpAvailableAndUpToDate(DumpType type);

        bool IsLocalDumpAvailableAndUpToDate(string path);
    }
}