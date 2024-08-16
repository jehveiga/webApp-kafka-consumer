using Confluent.Kafka;
using System.IO.Compression;
using System.Text.Json;

namespace WebApp.Kafka.Infrastructure.MessageKafka.Serializador
{
    internal class DeserializerConsumer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            using var memoryStream = new MemoryStream(data.ToArray());
            using var zip = new GZipStream(memoryStream, CompressionMode.Decompress, true);

            return JsonSerializer.Deserialize<T>(zip)!;
        }
    }
}
