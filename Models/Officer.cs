using System.Collections.Generic;
using System.Xml.Serialization;

namespace NationStatesSharp.Models
{
    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(REGION));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (REGION)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "OFFICER")]
    public class Officer
    {
        [XmlElement(ElementName = "NATION")]
        public string Nation { get; set; }

        [XmlElement(ElementName = "OFFICE")]
        public string Office { get; set; }

        [XmlElement(ElementName = "AUTHORITY")]
        public string Authority { get; set; }

        [XmlElement(ElementName = "TIME")]
        public int Time { get; set; }

        [XmlElement(ElementName = "BY")]
        public string By { get; set; }

        [XmlElement(ElementName = "ORDER")]
        public int Order { get; set; }
    }

    [XmlRoot(ElementName = "OFFICERS")]
    public class Officers
    {
        [XmlElement(ElementName = "OFFICER")]
        public List<Officer> Officer { get; set; }
    }
}