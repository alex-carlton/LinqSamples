using System;
using System.Linq;

namespace Cars
{
    public class FilteringExample : Program
    {
        public static void FilterLinq()
        {
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
        }
    }
}