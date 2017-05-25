using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    public class Program
    {
        public static List<Car> cars { get { return ProcessFile("Data/fuel.csv"); } }
        public static List<Manufacturer> manufacturers { get { return ProcessManufacturers("Data/manufacturers.csv"); } }

        private static void Main(string[] args)
        {
            FilteringExample.FilterLinq();

            JoinExample.JoinLinq();
            GroupExample.GroupLinq();
            AggregateExample.AggregateLinq();

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