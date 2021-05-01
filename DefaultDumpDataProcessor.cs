using NationStatesSharp.Interfaces;
using NationStatesSharp.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NationStatesSharp
{
    public class DefaultDumpDataProcessor : IDumpDataProcessor
    {
        public Task<IEnumerable<RawNationDumpModel>> ParseNationDumpAsync(Stream stream)
        {
            var xmlSerializer = new XmlSerializer(typeof(NATIONS));
            var nations = (NATIONS)xmlSerializer.Deserialize(stream);
            return Task.FromResult((IEnumerable<RawNationDumpModel>)nations.Nations);
        }

        public Task<IEnumerable<RawRegionDumpModel>> ParseRegionDumpAsync(Stream stream)
        {
            var xmlSerializer = new XmlSerializer(typeof(REGIONS));
            var regions = (REGIONS)xmlSerializer.Deserialize(stream);
            return Task.FromResult((IEnumerable<RawRegionDumpModel>)regions.Regions);
        }
    }
}