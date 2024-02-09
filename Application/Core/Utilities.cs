using Rebus.Sagas;
using System.Xml;
using System.Xml.Linq;

namespace Application.Core
{
    internal static class Utilities
    {
        internal static IEnumerable<(string fullContraction, string endContraction)> GetSplitStringList(IEnumerable<string> contractions)
        {
            List<(string fullContraction, string endContraction)> result = new();
            foreach (string contraction in contractions)
            {
                result.Add((contraction, contraction.Split(".").Last()));
            }
            return result;
        }

        internal static string? ExtractSchemaVersion(XDocument xml)
        {
            var schemaVersion = xml.Root?.Name.Namespace.NamespaceName;

            if (schemaVersion == null || schemaVersion == "")
            {
                schemaVersion = xml.Root?.FirstAttribute?.Value.ToString();
            }

            return schemaVersion;
        }
        internal static string ExtractGeneralVersion(string version)
        {
            string[] versionArray = version.Split(":");
            string[] messageArray = versionArray[^1].Split(".");

            if (messageArray.Length > 1)
            {
                return $"{messageArray[0]?.ToUpper()}{messageArray?[1]?.ToUpper()}";
            }
            else if (versionArray.Length > 2)
            {
                return $"{versionArray[2]?.ToUpper()}";
            }

            return $"{version}";
        }

        internal static XmlDocument ToXmlDocument(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        internal static XDocument ToXDocument(XmlDocument xmlDocument)
        {
            using var nodeReader = new XmlNodeReader(xmlDocument);
            nodeReader.MoveToContent();
            return XDocument.Load(nodeReader);
        }

        /// <summary>
        /// Date Foramt: yyyy-MM-dd
        /// </summary>
        /// <returns></returns>
        internal static string GetISODateFormat()
        {
            return "yyyy-MM-dd";
        }

        /// <summary>
        /// Datetime Format: yyyy-MM-ddTHH:mm:ss.sss
        /// </summary>
        /// <returns></returns>
        internal static string GetISODateTimeFormat()
        {
            return "yyyy-MM-ddTHH:mm:ss.fff";
        }

    }
}