using System.IO;
using System.Runtime.Serialization;

namespace Task.TestHelpers
{
    public class XmlDataContractSerializerTester<T> : SerializationTester<T, XmlObjectSerializer>
    {
        public XmlDataContractSerializerTester(
            XmlObjectSerializer serializer, bool showResult = false) : base(serializer, showResult)
        {
        }

        internal override T Deserialization(MemoryStream stream)
        {
            return (T) Serializer.ReadObject(stream);
        }

        internal override void Serialization(T data, MemoryStream stream)
        {
            Serializer.WriteObject(stream, data);
        }
    }
}