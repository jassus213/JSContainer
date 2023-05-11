using System.IO;
using JSInjector.Tests.CircularDependency.EfficiencyTest;
using JSInjector.Tests.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JSInjector.Tests.CircularDependency
{
    public class JsonServiceWriterCircularDependency : IJsonTestWriter<EntityEffiency>
    {
        public void WriteTestInfo(EntityEffiency entity)
        {
            var path =
                "C:\\Users\\JaSSuS\\Projects\\JSInjector\\JSInjector\\Tests\\CircularDependency\\EfficiencyTest\\EffiencyTestsCd.json";
            
            var jsonString = JsonConvert.SerializeObject(entity, JsonSettings.DefaultFormatting);
            File.AppendAllText(path, jsonString);
        }
    }
}