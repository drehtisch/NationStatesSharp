using System.Collections.Generic;
using System.Xml.Serialization;

namespace NationStatesSharp.Models
{
    [XmlRoot(ElementName = "DEATHS")]
    public class Deaths
    {
        [XmlElement(ElementName = "CAUSE")]
        public List<Cause> CAUSE;
    }

    [XmlRoot(ElementName = "CAUSE")]
    public class Cause
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type;

        [XmlText]
        public double Value;
    }
}