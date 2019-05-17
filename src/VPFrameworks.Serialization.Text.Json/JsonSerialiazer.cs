using Newtonsoft.Json;
using Serialization.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Serialization.Text.Json
{
    public class JsonSerialiazer : ITextSerializer
    {

        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

        public Task<T> Deserialize<T>(string text, SerializationSettings settings)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

            try
            {                
                var clientSettings = settings as JsonSerializationSettings;
                T result = JsonConvert.DeserializeObject<T>(text, clientSettings.NewtonSoftSettings);

                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        
            return tcs.Task;
        }

        public Task<string> Serialize<T>(T entity, SerializationSettings settings)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            try
            {
                var clientSettings = settings as JsonSerializationSettings;
                var result =JsonConvert.SerializeObject(entity, clientSettings.NewtonSoftSettings.Formatting);
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }
    }
}
