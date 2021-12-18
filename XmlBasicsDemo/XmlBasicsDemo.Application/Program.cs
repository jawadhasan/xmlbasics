using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XmlBasicsDemo.Application
{
    class Program
    {
        static string xmlFilePath = @"C:\test\devices.xml";
        static string deviceXmlFilePath = @"C:\test\device.xml";
        static void Main(string[] args)
        {
            //Part-2

            var device = new Device
            {
                Id = 2,
                DeviceType = "RaspberryPi",
                Status = "Online"
            };

            var xml = device.Serialize();
            Console.WriteLine(xml);

            var deserilizeDevice = device.DeSerialize(xml);
            Console.WriteLine($"{deserilizeDevice.DeviceType}");


            //SerializeExample();
            //DeserializeExampe();



            #region Part-1 Uncomment Functions below to run the code
            //CreateNewXMLDocument();
            //LoadXMLFile();
            //CreateXMLAttributeApproach();
            //QueryXML();
            #endregion

            Console.ReadLine();
        }

        //******************Part-1*****************************

        public static void SerializeExample()
        {
            var device = new Device
            {
                Id = 1,
                DeviceType = "GateWay",
                Status = "Online"
            };

            //Serialize: .NET Object to XML
            var deviceXML = Serialize(device);

            //Write XML to File
            WriteXMLFile(deviceXML);
        }
        public static void DeserializeExampe()
        {
            //Deserilize: XML to .NET Object
            var deviceDeserilized = Deserialize(deviceXmlFilePath);

            Console.WriteLine($"{deviceDeserilized.Id}: {deviceDeserilized.DeviceType}");

        }

        public static string Serialize(Device device)
        {
            var result = string.Empty;

            //Create XMLSerializer
            var serializer = new XmlSerializer(typeof(Device));

            //Write to String
            using(var sw = new StringWriter())
            {
                serializer.Serialize(sw, device);
                result = sw.ToString();
            }

            return result;
        }
        public static Device Deserialize(string xmlFilePath)
        {
            var device = new Device();

            //Create XMLSerializer
            var serializer = new XmlSerializer(typeof(Device));

            //Read From File and Deserlize to .NET Object
            using(var fs = new FileStream(xmlFilePath, FileMode.Open))
            {
                device = (Device)serializer.Deserialize(fs);
                fs.Close();
            }
            return device;
        }
        public static void WriteXMLFile(string xml)
        {
            //other file ops....
            File.AppendAllText(deviceXmlFilePath, xml, Encoding.Unicode);
        }

        //******************Part-1*****************************

        //Example - Create NewXMLDocument using Code
        public static void CreateNewXMLDocument()
        {
            //use constructor XDocument
            var doc = new XDocument(

                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Sample Code"),

                new XElement("Devices",
                    new XElement("DeviceId", "10"),
                    new XElement("Type", "Gateway"),
                    new XElement("Status", "Online"))

                );


            doc.Save(xmlFilePath);
        }


        //Example - Load XML document from File
        public static void LoadXMLFile()
        {
            //Load XML file
            var doc = XDocument.Load(xmlFilePath);
            Console.WriteLine(doc.ToString());
        }


        //Example - Create Attribute based XML Document
        public static void CreateXMLAttributeApproach()
        {
            //use constructor XDocument
            var doc = new XDocument(

                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Attribute based approach"),

                new XElement("Devices",

                    new XElement("Device",
                        new XAttribute("Id", "10"),
                        new XAttribute("Type", "Gateway"),
                        new XAttribute("Status", "Online")),

                  new XElement("Device",
                        new XAttribute("Id", "20"),
                        new XAttribute("Type", "RaspberryPi"),
                        new XAttribute("Status", "Online")),

                      new XElement("Device",
                        new XAttribute("Id", "30"),
                        new XAttribute("Type", "ESP32"),
                        new XAttribute("Status", "Offline"))

                    )
                );

            //save the xml
            doc.Save(xmlFilePath);
        }


        //Example - Query XML
        public static void QueryXML()
        {
            //load XML
            var doc = XDocument.Load(xmlFilePath);

            //query
            var result = doc.Element("Devices")
                 .Elements("Device")
                 .Where(element => element.Attribute("Id").Value == "10")
                 .Select(element => element.Attribute("Status").Value);

            Console.WriteLine(result.FirstOrDefault());
        }
    }
}
