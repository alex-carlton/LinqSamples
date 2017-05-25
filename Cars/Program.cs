using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");

            var result = cars.All(c => c.Manufacturer == "Ford");
            Console.WriteLine(result);

            var top = cars.OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name)
                .First(c => c.Manufacturer == "BMW" && c.Year == 2016);

            Console.WriteLine($"{top.Name}");

            var query = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                .OrderByDescending(c => c.Combined).ThenBy(c => c.Name)
                .Select(c => new { c.Manufacturer, c.Name, c.Combined });

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Name} : {car.Combined}");
            }

            var subsequence = cars.OrderBy(c => c.Manufacturer).SelectMany(c => c.Manufacturer).Take(5);
            foreach (var character in subsequence)
            {
                Console.WriteLine(character);
            }

            Console.ReadKey();
        }

        private static List<Car> ProcessFile(string path)
        {
            var query = File.ReadAllLines(path).Skip(1).Where(l => l.Length > 1).ToCar();

            return query.ToList();
        }
    }
}