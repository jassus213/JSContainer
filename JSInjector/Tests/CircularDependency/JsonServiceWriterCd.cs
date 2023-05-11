using System.IO;
using JSInjector.Tests.CircularDependency.EfficiencyTest;
using JSInjector.Tests.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JSInjector.Tests.CircularDependency
{
    public class JsonServiceWriterCd : IJsonTestWriter<EntityEffiency>
    {
        public void WriteTestInfo(EntityEffiency entity)
        {
            var path =
                "C:\\Users\\JaSSuS\\Projects\\JSInjector\\JSInjector\\Tests\\CircularDependency\\EfficiencyTest\\EffiencyTestsCd.json";

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            var jsonString = JsonConvert.SerializeObject(entity);
            File.AppendAllText(path, jsonString);
        }
    }
}