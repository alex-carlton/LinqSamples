using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var cars = ProcessFile("Data/fuel.csv");
            var manufacturers = ProcessManufacturers("Data/manufacturers.csv");

            //FilteringExample.FilterLinq(cars, manufacturers);

            //JoinExample.JoinLinq(cars, manufacturers);
            //GroupExample.GroupLinq(cars, manufacturers);
            //AggregateExample.AggregateLinq(cars, manufacturers);

            XMLExample.XmlLinq(cars);

            Console.ReadKey();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var toReturn = File.ReadAllLines(path).Where(l => l.Length > 1)
                .Select(l =>
                {
                    var columns = l.Split(',');

                    return new Manufacturer
                    {
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2])
                    };
                });

            return toReturn.ToList();
        }

        private static List<Car> ProcessFile(string path)
        {
            var toReturn = File.ReadAllLines(path).Skip(1).Where(l => l.Length > 1).ToCar();

            return toReturn.ToList();
        }
    }
}