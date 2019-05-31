using Azure.Messaging.Serialization.Binary;
using MessagePack;
using System;
using System.IO;
using System.Threading.Tasks;
using VPFrameworks.Serialization.Abstractions;

namespace VPFrameworks.Serialization.Binary
{
    /// <summary>
    /// Serializes instances of objects to binary with the message pack format
    /// </summary>
    public class MessagePackSerializer : IBinarySerializer
    {
         

        IFormatterResolver formatterResolver;

        /// <summary>
        /// Gets or sets the content type
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Creates an instance of MessagePackSerializer with the especific Formatter resolver. this is useful if the application as is owns formaters resolvers
        /// </summary>
        /// <param name="formatterResolver"></param>
        public MessagePackSerializer(IFormatterResolver formatterResolver)
        {
            this.formatterResolver = formatterResolver;
            this.ContentType = "application/message-pack";
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public MessagePackSerializer()
        {
            this.formatterResolver =  DefaultFormatterResolver.Instance;
        }

        /// <summary>
        /// Deserializes byte[] to an instance of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="body"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<T> Deserialize<T>(byte[] body, SerializationSettings settings)
        {
            T result = default(T);
            using (MemoryStream stream = new MemoryStream())
            {
                result = await global::MessagePack.MessagePackSerializer.DeserializeAsync<T>(stream, this.formatterResolver);
            }

            return result;
        }

        /// <summary>
        /// Serializes an object to an array of Bytes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
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
