using Azure.Messaging.Abstractions;
using MessagePack;
using Serialization.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Azure.Messaging.Serialization.Binary
{
    public class MessagePackSerializer : IBinarySerializer
    {
         

        IFormatterResolver formatterResolver;

        public string ContentType { get; }

        public MessagePackSerializer(IFormatterResolver formatterResolver)
        {
            this.formatterResolver = formatterResolver;
            this.ContentType = "application/message-pack";
        }

        public MessagePackSerializer()
        {
            this.formatterResolver =  DefaultFormatterResolver.Instance;
        }


        public async Task<T> Deserialize<T>(byte[] body, SerializationSettings settings)
        {
            T result = default(T);
            using (MemoryStream stream = new MemoryStream())
            {
                result = await global::MessagePack.MessagePackSerializer.DeserializeAsync<T>(stream, this.formatterResolver);
            }

            return result;
        }

        public async Task<byte[]> Serialize<T>(T message, SerializationSettings settings)
        {
            byte[] result;
            using (MemoryStream stream = new MemoryStream())
            {
                await global::MessagePack.MessagePackSerializer.SerializeAsync(stream, message, this.formatterResolver);

                using (BinaryReader reader = new BinaryReader(stream))
                {
                    result = reader.ReadBytes((int)stream.Length);
                }
            }

            return result;
        }
    }
}
