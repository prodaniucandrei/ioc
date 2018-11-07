using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace MovieFinder
{
    public static class ResourceProvider
    {
        public static JObject GetConfig(string taxReturnName)
        {
            using (StreamReader r = new StreamReader($"{taxReturnName}.json"))
            {
                string json = r.ReadToEnd();
                return JObject.Parse(json);
            }
        }

        public static XDocument GetXDocument(string xmlFileName)
        {
            return XDocument.Parse(File.ReadAllText($"{xmlFileName}.xml"));
        }

        public static string GetJsonFilesAsString(string taxReturnName)
        {
            using (StreamReader r = new StreamReader($"{taxReturnName}.json"))
            {
                return r.ReadToEnd();
            }
        }
    }
}
