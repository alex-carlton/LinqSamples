using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    internal class GroupExample
    {
        public static void GroupLinq(List<Car> cars, List<Manufacturer> manufacturers)
        {
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
        }
    }
}
