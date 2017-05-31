using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    internal class AggregateExample
    {
        public static void AggregateLinq(List<Car> cars, List<Manufacturer> manufacturers)
        {
            var aggregating = from car in cars
                              group car by car.Manufacturer into carGroup
                              select new
                              {
                                  Name = carGroup.Key,
                                  Max = carGroup.Max(c => c.Combined),
                                  Min = carGroup.Min(c => c.Combined),
                                  Avg = carGroup.Average(c => c.Combined)
                              } into agg
                              orderby agg.Max descending
                              select agg;
            foreach (var item in aggregating)
            {
                Console.WriteLine($"{item.Name}");
                Console.WriteLine($"\t Max: {item.Max}");
                Console.WriteLine($"\t Min: {item.Min}");
                Console.WriteLine($"\t Avg: {item.Avg}");
            }
        }
    }
}
