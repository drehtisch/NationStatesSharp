using System.Xml.Serialization;

namespace NationStatesSharp.Models
{
    [XmlRoot(ElementName = "FREEDOM")]
    public class Freedom
    {
        [XmlElement(ElementName = "CIVILRIGHTS")]
        public string CivilRights;

        [XmlElement(ElementName = "ECONOMY")]
        public string Economy;

        [XmlElement(ElementName = "POLITICALFREEDOM")]
        public string PoliticalFreedom;
    }

    [XmlRoot(ElementName = "FREEDOMSCORES")]
    public class FreedomScores
    {
        [XmlElement(ElementName = "CIVILRIGHTS")]
        public double CivilRights;

        [XmlElement(ElementName = "ECONOMY")]
        public double Economy;

        [XmlElement(ElementName = "POLITICALFREEDOM")]
        public double PoliticalFreedom;
    }
}