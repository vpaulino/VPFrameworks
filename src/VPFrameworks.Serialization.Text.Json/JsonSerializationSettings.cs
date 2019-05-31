using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using VPFrameworks.Serialization.Abstractions;

namespace VPFrameworks.Serialization.Text.Json
{
    /// <summary>
    /// Settings to json serialization
    /// </summary>
    public class JsonSerializationSettings : SerializationSettings
    { 
        /// <summary>
        /// creates instance of <see cref="JsonSerializationSettings"/>
        /// </summary>
        /// <param name="settings"></param>
        public JsonSerializationSettings(JsonSerializerSettings settings) : base("application/json", Encoding.UTF8)
        {
            this.NewtonSoftSettings = settings;
        }

        /// <summary>
        /// Gets or sets the json settings
        /// </summary>
        public JsonSerializerSettings NewtonSoftSettings { get; set; }

    }
}