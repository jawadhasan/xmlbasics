using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlBasicsDemo.Application
{
    public static class XMLSerializerHelper
    {
        public static string Serialize<T>(this T value)
        {
            var result = string.Empty;

            if(value != null)
            {
                //Create XMLSerializer
                var serializer = new XmlSerializer(typeof(T));

                //Write to String
                using (var sw = new StringWriter())
                {
                    serializer.Serialize(sw, value);
                    result = sw.ToString();
                }
            }
            return result;
        }

        public static T DeSerialize<T>(this T value, string xml)
        {
            T result = default(T);

            if (!string.IsNullOrEmpty(xml))
            {
                //Create XMLSerializer
                var serializer = new XmlSerializer(typeof(Device));

                //Read Using Memory Stream and Deserlize to .NET Object
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
                {
                    ms.Position = 0;
                    result = (T)serializer.Deserialize(ms);
                }
            }   
            return result;
        }
    }
}
