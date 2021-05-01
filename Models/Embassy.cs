using System.Collections.Generic;
using System.Xml.Serialization;

namespace NationStatesSharp.Models
{
    [XmlRoot(ElementName = "EMBASSY")]
    public class Embassy
    {
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "EMBASSIES")]
    public class Embasssies
    {
        [XmlElement(ElementName = "EMBASSY")]
        public List<Embassy> Embassy { get; set; }
    }
}