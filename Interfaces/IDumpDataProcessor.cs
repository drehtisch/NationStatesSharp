using NationStatesSharp.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NationStatesSharp.Interfaces
{
    public interface IDumpDataProcessor
    {
        Task<IEnumerable<RawNationDumpModel>> ParseNationDumpAsync(Stream stream);

        Task<IEnumerable<RawRegionDumpModel>> ParseRegionDumpAsync(Stream stream);
    }
}