using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine.library.Core
{
   internal static class SerializerHelper
    {
        private static JsonSerializerSettings DeserializeObject()
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            var settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
            return settings;
        }
        public static JsonSerializerSettings Settings = DeserializeObject();
    }
}
