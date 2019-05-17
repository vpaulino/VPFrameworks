using Newtonsoft.Json;
using Serialization.Abstractions;
using System.Collections.Generic;
using System.Text;

namespace Serialization.Text.Json
{
    public class JsonSerializationSettings : SerializationSettings
    { 

        public JsonSerializationSettings(JsonSerializerSettings settings) : base("application/json", Encoding.UTF8)
        {
            this.NewtonSoftSettings = settings;
        }

        public JsonSerializerSettings NewtonSoftSettings { get; set; }

    }
}