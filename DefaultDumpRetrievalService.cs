using NationStatesSharp.Enums;
using NationStatesSharp.Interfaces;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace NationStatesSharp
{
    public class DefaultDumpRetrievalService : IDumpRetrievalService
    {
        private IRequestDispatcher _dispatcher;
        private ILogger _logger;

        public DefaultDumpRetrievalService(IRequestDispatcher requestDispatcher, ILogger logger)
        {
            _dispatcher = requestDispatcher;
            _logger = logger;
        }

        public DefaultDumpRetrievalService(IRequestDispatcher requestDispatcher) : this(requestDispatcher, Log.Logger)
        {
        }

        public async Task<Stream> DownloadDumpAsync(DumpType type, CancellationToken cancellationToken)
        {
            switch (type)
            {
                case DumpType.Nations:
                    return await DownloadDumpAsync("pages/nations.xml.gz", cancellationToken);

                case DumpType.Regions:
                    return await DownloadDumpAsync("pages/regions.xml.gz", cancellationToken);

                case DumpType.Cards1:
                    return await DownloadDumpAsync("pages/cardlist_S1.xml.gz", cancellationToken);

                case DumpType.Cards2:
                    return await DownloadDumpAsync("pages/cardlist_S2.xml.gz", cancellationToken);

                default:
                    throw new InvalidOperationException($"Unsupported DumpType {type}");
            }
        }

        public async Task<Stream> DownloadDumpAsync(string url, CancellationToken cancellationToken)
        {
            var request = new Request(url, ResponseFormat.Stream);
            _dispatcher.Dispatch(request, 10000);
            await request.WaitForResponseAsync(cancellationToken).ConfigureAwait(false);
            return request.GetResponseAsStream();
        }

        public bool IsLocalDumpAvailableAndUpToDate(DumpType type)
        {
            switch (type)
            {
                case DumpType.Nations:
                    return IsLocalDumpAvailableAndUpToDate("nations.xml.gz");

                case DumpType.Regions:
                    return IsLocalDumpAvailableAndUpToDate("regions.xml.gz");

                case DumpType.Cards1:
                    return IsLocalDumpAvailableAndUpToDate("cardlist_S1.xml.gz");

                case DumpType.Cards2:
                    return IsLocalDumpAvailableAndUpToDate("cardlist_S2.xml.gz");

                default:
                    throw new InvalidOperationException($"Unsupported DumpType {type}");
            }
        }

        public bool IsLocalDumpAvailableAndUpToDate(string path)
        {
            if (File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                var outdated = fileInfo.LastWriteTimeUtc.Date != DateTime.UtcNow.Date;
                // Update time is 22:30 PM PST -> 6:30 AM UTC + 30 minutes buffer
                if (DateTime.UtcNow.TimeOfDay < new TimeSpan(7, 0, 0) && outdated)
                {
                    outdated = false;
                }
                if (outdated)
                {
                    _logger.Debug("Local DumpData ({path}) found but outdated.", path);
                }
                else
                {
                    _logger.Debug("Local DumpData ({path}) found.", path);
                }
                return !outdated;
            }
            else
            {
                _logger.Debug("No Local DumpData ({path}) found", path);
                return false;
            }
        }
    }
}