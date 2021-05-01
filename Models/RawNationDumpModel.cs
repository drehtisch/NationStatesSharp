using System.Collections.Generic;
using System.Xml.Serialization;

namespace NationStatesSharp.Models
{
    [XmlRoot(ElementName = "NATION")]
    public class RawNationDumpModel
    {
        [XmlElement(ElementName = "NAME")]
        public string Name;

        [XmlElement(ElementName = "TYPE")]
        public string Type;

        [XmlElement(ElementName = "FULLNAME")]
        public string FullName;

        [XmlElement(ElementName = "MOTTO")]
        public string Motto;

        [XmlElement(ElementName = "CATEGORY")]
        public string Category;

        [XmlElement(ElementName = "UNSTATUS")]
        public string WAStatus;

        [XmlElement(ElementName = "ENDORSEMENTS")]
        public string Endorsements;

        [XmlElement(ElementName = "ISSUES_ANSWERED")]
        public int IssuesAnswered;

        [XmlElement(ElementName = "FREEDOM")]
        public Freedom Freedom;

        [XmlElement(ElementName = "REGION")]
        public string RegionName;

        [XmlElement(ElementName = "POPULATION")]
        public int Population;

        [XmlElement(ElementName = "TAX")]
        public double Tax;

        [XmlElement(ElementName = "ANIMAL")]
        public string Animal;

        [XmlElement(ElementName = "CURRENCY")]
        public string Currency;

        [XmlElement(ElementName = "DEMONYM")]
        public string Demonym;

        [XmlElement(ElementName = "DEMONYM2")]
        public string Demonym2;

        [XmlElement(ElementName = "DEMONYM2PLURAL")]
        public string Demonym2Plural;

        [XmlElement(ElementName = "FLAG")]
        public string FlagUrl;

        [XmlElement(ElementName = "MAJORINDUSTRY")]
        public string MajorIndustry;

        [XmlElement(ElementName = "GOVTPRIORITY")]
        public string GovtPriority;

        [XmlElement(ElementName = "GOVT")]
        public Govt Govt;

        [XmlElement(ElementName = "FOUNDED")]
        public string Founded;

        [XmlElement(ElementName = "FIRSTLOGIN")]
        public int FirstLogin;

        [XmlElement(ElementName = "LASTLOGIN")]
        public int LastLogin;

        [XmlElement(ElementName = "LASTACTIVITY")]
        public string LastActivity;

        [XmlElement(ElementName = "INFLUENCE")]
        public string Influence;

        [XmlElement(ElementName = "FREEDOMSCORES")]
        public FreedomScores FreedomScores;

        [XmlElement(ElementName = "PUBLICSECTOR")]
        public double PublicSector;

        [XmlElement(ElementName = "DEATHS")]
        public Deaths Deaths;

        [XmlElement(ElementName = "LEADER")]
        public string Leader;

        [XmlElement(ElementName = "CAPITAL")]
        public string Capital;

        [XmlElement(ElementName = "RELIGION")]
        public string Religion;

        [XmlElement(ElementName = "FACTBOOKS")]
        public int FactbookCount;

        [XmlElement(ElementName = "DISPATCHES")]
        public int DispatchCount;

        [XmlElement(ElementName = "DBID")]
        public int DBId;
    }

    [XmlRoot(ElementName = "NATIONS")]
    public class NATIONS
    {
        [XmlElement(ElementName = "NATION")]
        public List<RawNationDumpModel> Nations { get; set; }

        [XmlAttribute(AttributeName = "api_version")]
        public int ApiVersion { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}