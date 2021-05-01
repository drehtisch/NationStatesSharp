using System.Collections.Generic;
using System.Xml.Serialization;

namespace NationStatesSharp.Models
{
    [XmlRoot(ElementName = "REGION")]
    public class RawRegionDumpModel
    {
        [XmlElement(ElementName = "NAME")]
        public string Name { get; set; }

        [XmlElement(ElementName = "FACTBOOK")]
        public string Factbook { get; set; }

        [XmlElement(ElementName = "NUMNATIONS")]
        public int NumNations { get; set; }

        [XmlElement(ElementName = "NATIONS")]
        public string Nations { get; set; }

        [XmlElement(ElementName = "DELEGATE")]
        public string Delegate { get; set; }

        [XmlElement(ElementName = "DELEGATEVOTES")]
        public int DelegateVotes { get; set; }

        [XmlElement(ElementName = "DELEGATEAUTH")]
        public string DelegateAuth { get; set; }

        [XmlElement(ElementName = "FOUNDER")]
        public string Founder { get; set; }

        [XmlElement(ElementName = "FOUNDERAUTH")]
        public string FounderAuth { get; set; }

        [XmlElement(ElementName = "OFFICERS")]
        public Officers Officers { get; set; }

        [XmlElement(ElementName = "POWER")]
        public string Power { get; set; }

        [XmlElement(ElementName = "FLAG")]
        public string Flag { get; set; }

        [XmlElement(ElementName = "EMBASSIES")]
        public Embasssies Embassies { get; set; }

        [XmlElement(ElementName = "LASTUPDATE")]
        public int LastUpdate { get; set; }
    }

    [XmlRoot(ElementName = "REGIONS")]
    public class REGIONS
    {
        [XmlElement(ElementName = "REGION")]
        public List<RawRegionDumpModel> Regions { get; set; }

        [XmlAttribute(AttributeName = "api_version")]
        public int ApiVersion { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}