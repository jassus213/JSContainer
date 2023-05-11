using System;
using Newtonsoft.Json;

namespace JSInjector.Tests.CircularDependency.EfficiencyTest
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EntityEffiency
    {
        [JsonProperty("Time to Finish First Test")]
        public TimeSpan FirstTest;
        [JsonProperty("Time to Finish Second Test")]
        public TimeSpan SecondTest;
    }
}