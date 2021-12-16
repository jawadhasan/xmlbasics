using System;
using System.Linq;
using System.Xml.Linq;

namespace XmlBasicsDemo.Application
{
    class Program
    {
        static string xmlFilePath = @"C:\test\devices.xml";
        static void Main(string[] args)
        {

            //Uncomment Functions below to run the code

            //CreateNewXMLDocument();
            //LoadXMLFile();
            //CreateXMLAttributeApproach();
            //QueryXML();


            Console.ReadLine();
        }


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
