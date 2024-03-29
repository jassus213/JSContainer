﻿using System.IO;
using JSContainer.Tests.CircularDependency.EfficiencyTest;
using JSContainer.Tests.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JSContainer.Tests.CircularDependency
{
    public class JsonServiceWriterCircularDependency : IJsonTestWriter<EntityEffiency>
    {
        public void WriteTestInfo(EntityEffiency entity)
        {
            var path =
                "C:\\Users\\JaSSuS\\Projects\\JSContainer\\JSContainer\\Tests\\CircularDependency\\EfficiencyTest\\EffiencyTestsCd.json";
            
            var jsonString = JsonConvert.SerializeObject(entity, JsonSettings.DefaultFormatting);
            File.AppendAllText(path, jsonString);
        }
    }
}