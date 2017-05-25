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
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var groupByHeadquarters = from manufacturer in manufacturers
                                 join car in cars on manufacturer.Name equals car.Manufacturer
                                     into carGroup
                                 orderby manufacturer.Name
                                 select new
                                 {
                                     Manufacturer = manufacturer,
                                     Cars = carGroup
                                 } into results
                                 group results by results.Manufacturer.Headquarters;
            foreach (var group in groupByHeadquarters)
            {
                Console.WriteLine($"{group.Key}");

                foreach (var car in group.SelectMany(g => g.Cars).OrderByDescending(c => c.Combined).Take(3))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }

            }

            var groupQueryJoin = from manufacturer in manufacturers
                                 join car in cars on manufacturer.Name equals car.Manufacturer
                                     into carGroup
                                 orderby manufacturer.Name
                                 select new
                                 {
                                     Manufacturer = manufacturer,
                                     Cars = carGroup
                                 };
            foreach (var group in groupQueryJoin)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            var groupQueryMethodJoin = manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer, 
                (m, g) => new
                {
                    Manufacturer = m,
                    Cars = g
                })
                .OrderByDescending(m => m.Manufacturer.Name);
            foreach (var group in groupQueryMethodJoin)
            {
                Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            var groupQuery = from car in cars
                             group car by car.Manufacturer.ToUpper() into manufacturer
                             orderby manufacturer.Key
                             select manufacturer;

            foreach (var group in groupQuery)
            {
                Console.WriteLine($"{group.Key} has {group.Count()} cars");
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            var groupQueryMethod = cars.GroupBy(c => c.Manufacturer.ToUpper()).OrderBy(g => g.Key);
            foreach (var group in groupQueryMethod)
            {
                Console.WriteLine($"{group.Key} has {group.Count()} cars");
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            var joinQuery = from car in cars
                            join manufacturer in manufacturers
                                on new { car.Manufacturer, car.Year }
                                equals
                                new { Manufacturer = manufacturer.Name, manufacturer.Year }
                            orderby car.Combined descending, car.Name
                            select new
                            {
                                manufacturer.Headquarters,
                                car.Name,
                                car.Combined
                            };
            foreach (var car in joinQuery.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }

            var joinQueryMethod = cars.Join(manufacturers,
                c => new { c.Manufacturer, c.Year },
                m => new { Manufacturer = m.Name, m.Year },
                (c, m) => new
                {
                    m.Headquarters,
                    c.Name,
                    c.Combined
                })
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name);

            foreach (var car in joinQueryMethod.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }

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