using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Cars
{
    internal class XMLExample
    {
        public static void XmlLinq(List<Car> records)
        {
            CreateXml(records);
            QueryXml();
        }

        private static void QueryXml()
        {
            var document = XDocument.Load("Data/fuel.xml");

            var query = from element in document.Descendants("Car")
                        where element.Attribute("Manufacturer")?.Value == "BMW"
                        select element.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml(List<Car> cars)
        {
            var document = new XDocument();

            var carsXml = new XElement("Cars",
                from car in cars
                select new XElement("Car",
                    new XAttribute("Name", car.Name),
                    new XAttribute("Combined", car.Combined),
                    new XAttribute("Manufacturer", car.Manufacturer))
            );

            document.Add(carsXml);
            document.Save("Data/fuel.xml");
        }
    }
}