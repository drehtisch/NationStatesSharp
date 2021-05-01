using System.Xml.Serialization;

namespace NationStatesSharp.Models
{
    [XmlRoot(ElementName = "GOVT")]
    public class Govt
    {
        [XmlElement(ElementName = "ADMINISTRATION")]
        public double Administration;

        [XmlElement(ElementName = "DEFENCE")]
        public double Defence;

        [XmlElement(ElementName = "EDUCATION")]
        public double Education;

        [XmlElement(ElementName = "ENVIRONMENT")]
        public double Environment;

        [XmlElement(ElementName = "HEALTHCARE")]
        public double Healthcare;

        [XmlElement(ElementName = "COMMERCE")]
        public double Commerce;

        [XmlElement(ElementName = "INTERNATIONALAID")]
        public double InternationalAid;

        [XmlElement(ElementName = "LAWANDORDER")]
        public double LawAndOrder;

        [XmlElement(ElementName = "PUBLICTRANSPORT")]
        public double PublicTransport;

        [XmlElement(ElementName = "SOCIALEQUALITY")]
        public double SocialEquality;

        [XmlElement(ElementName = "SPIRITUALITY")]
        public double Spirituality;

        [XmlElement(ElementName = "WELFARE")]
        public double Welfare;
    }
}