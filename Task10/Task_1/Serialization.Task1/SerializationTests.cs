using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialization.Task1.Models;

namespace Serialization.Task1
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void SmokeTest()
        {
            var serializer = new XmlSerializer(typeof(Catalog));
            
            var path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.xml");
            using var xmlReader = new XmlTextReader(File.OpenRead(path1));
            
            var data = serializer.Deserialize(xmlReader);

            var path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books_serialized.xml");
            serializer.Serialize(File.Create(path2), data);
        }
    }
}