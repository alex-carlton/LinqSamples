using System.Collections.Generic;
using System.Xml.Linq;

namespace Cars
{
    public class XMLExample : Program
    {
        public void XmlWithLinq(List<Car> records)
        {
            var document = new XDocument();
            var cars = new XElement("Cars");

            foreach (var record in records)
            {
                var car = new XElement("Car");
                var name = new XElement("Name", record.Name);
                var combined = new XElement("Combined", record.Combined);
            }
        }
    }
}