using NationStatesSharp.Enums;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NationStatesSharp.Interfaces
{
#pragma warning disable CS1591

    public interface IDumpRetrievalService
    {
#pragma warning restore CS1591

        /// <summary>
        /// Downloads a dump of the specified type as compressed Stream
        /// </summary>
        /// <param name="type">Type of dump to download.</param>
        /// <param name="cancellationToken">The cancellationToken to cancel that request.</param>
        /// <returns>The compressed stream from the HTTP Response.</returns>
        Task<Stream> DownloadDumpAsync(DumpType type, CancellationToken cancellationToken);

        /// <summary>
        /// Downloads a dump from the url as compressed Stream
        /// </summary>
        /// <param name="url">The url to download the dump from.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The compressed stream from the HTTP Response.</returns>
        Task<Stream> DownloadDumpAsync(string url, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the default file name of the specified dump type.
        /// </summary>
        /// <param name="type">Type of dump to get the name.</param>
        /// <returns>The default dump file name.</returns>

        string GetDumpFileNameByDumpType(DumpType type);

        /// <summary>
        /// Returns if a dump of the specified type is available and up to date.
        /// </summary>
        /// <param name="type">Type of dump to get the availabilty from.</param>
        /// <returns>If a local dump is available and up to date.</returns>
        /// <remarks>NOTE: The official dump update time (22:30 PM PST -> 6:30 AM UTC) is compared to the last write time of that file to determine if it is outdated or not.</remarks>
        bool IsLocalDumpAvailableAndUpToDate(DumpType type);

        /// <summary>
        /// Returns if a dump of the specified path is available and up to date.
        /// </summary>
        /// <param name="path">The path where the local dump file is located.</param>
        /// <returns>If a local dump is available and up to date.</returns>
        /// <remarks>NOTE: The official dump update time (22:30 PM PST -> 6:30 AM UTC) is compared to the last write time of that file to determine if it is outdated or not.</remarks>
        bool IsLocalDumpAvailableAndUpToDate(string path);
    }
}