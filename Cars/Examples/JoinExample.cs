using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    internal class JoinExample : Program
    {
        public static void JoinLinq()
        {
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
        }
    }
}
