using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

	[XmlRoot(ElementName = "CAUSE")]
	public class Cause
	{

		[XmlAttribute(AttributeName = "type")]
		public string Type;

		[XmlText]
		public double Text;
	}

	[XmlRoot(ElementName = "DEATHS")]
	public class Deaths
	{

		[XmlElement(ElementName = "CAUSE")]
		public List<Cause> CAUSE;
	}

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
}
