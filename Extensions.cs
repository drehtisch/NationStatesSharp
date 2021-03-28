using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NationStatesSharp
{
    public static class Extensions
    {
        internal static async Task<XDocument> ReadXmlAsync(this HttpResponseMessage httpResponse, ILogger logger, CancellationToken cancellationToken)
        {
            if (httpResponse is null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            if (httpResponse.IsSuccessStatusCode)
            {
                using (Stream stream = await httpResponse.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false))
                {
                    try
                    {
                        if (logger.IsEnabled(Serilog.Events.LogEventLevel.Verbose))
                        {
                            logger.Verbose(await httpResponse.Content.ReadAsStringAsync(cancellationToken));
                        }
                        return await XDocument.LoadAsync(stream, LoadOptions.None, cancellationToken);
                    }
                    catch (XmlException ex)
                    {
                        throw new ApplicationException($"A error while loading xml occured.", ex);
                    }
                }
            }
            else
            {
                return null;
            }
        }

        internal static async Task<Stream> ReadStreamAsync(this HttpResponseMessage httpResponse)
        {
            if (httpResponse is null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            return httpResponse.IsSuccessStatusCode ? await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false) : null;
        }

        internal static async Task<bool?> ReadBooleanAsync(this HttpResponseMessage httpResponse)
        {
            if (httpResponse is null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            if (httpResponse.IsSuccessStatusCode)
            {
                var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return bool.TryParse(content, out bool result) ? result : (bool?)null;
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<string> GetAttributeValuesByAttributeName(this IEnumerable<XElement> elements, string attributeName)
        {
            foreach (var element in elements)
            {
                yield return element.GetAttributeValueByAttributeName(attributeName);
            }
        }

        public static Task WaitForAllResponsesAsync(this IEnumerable<Request> requests, CancellationToken cancellationToken) => Task.WhenAll(requests.Select(r => r.WaitForResponseAsync(cancellationToken)));

        public static Task WaitForAllResponsesAsync(this IEnumerable<Request> requests) => requests.WaitForAllResponsesAsync(CancellationToken.None);

        public static string GetAttributeValueByAttributeName(this XElement element, string attributeName) => element.Attributes().FirstOrDefault(e => e.Name == attributeName)?.Value;

        public static IEnumerable<XElement> FilterDescendantsByNameAndValue(this XDocument document, string nodeName, string value) => document?.Descendants(nodeName).Where(e => e.Value == value);

        public static string GetFirstValueByNodeName(this XDocument document, string nodeName) => document?.Descendants(nodeName).FirstOrDefault()?.Value;

        public static IEnumerable<XElement> GetParentsOfFilteredDescendants(this XDocument document, string nodeName, string value) => document?.FilterDescendantsByNameAndValue(nodeName, value).Select(e => e.Parent);
    }
}