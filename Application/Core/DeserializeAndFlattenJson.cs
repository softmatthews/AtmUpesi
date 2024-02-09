using Microsoft.EntityFrameworkCore;
using ChoETL;
using JsonFlatten;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// using System.Xml.Schema;
// using System.Xml.Linq;

namespace Application.Core
{
    /// <summary>
    /// Helper class to deserialize and flatten Json Strings 
    /// </summary>
    public class DeserializeAndFlattenJson
    {
        /// <summary>
        /// Joins all JSON nested levels with a '.' and strips out namespaces in JSON
        /// </summary>
        /// <param name="json">JSON String containing all documents</param>
        /// <returns>Key Value Dictionary</returns>
        public static Dictionary<string, object> DeserializeAndFlatten(string json)
        {
            Dictionary<string, object> dict = new();
            JToken token = JToken.Parse(json);
            FillDictionaryFromJToken(dict, token, "");
            return dict;
        }

        private static void FillDictionaryFromJToken(Dictionary<string, object> dict, JToken token, string prefix)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (JProperty prop in token.Children<JProperty>())
                    {
                        var name = prop.Name.Contains(':') ? prop.Name.Split(':')[1] : prop.Name;
                        FillDictionaryFromJToken(dict, prop.Value, Join(prefix, name));
                    }
                    break;

                case JTokenType.Array:
                    int index = 0;
                    foreach (JToken value in token.Children())
                    {
                        FillDictionaryFromJToken(dict, value, Join(prefix, "[" + index.ToString() + "]"));
                        index++;
                    }
                    break;

                default:
                    dict.Add(prefix, ((JValue)token)?.Value ?? "");
                    break;
            }
        }

        private static string Join(string prefix, string name)
        {
            return (string.IsNullOrEmpty(prefix) ? name : prefix + "." + name);
        }

    }
}