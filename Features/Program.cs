using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> square = x => x * x;

            Func<int, int, int> add = (x, y) =>
            {
                int temp = x + y;
                return temp;
            };

            Action<int> writeInt = x => Console.WriteLine(x);

            writeInt(square(add(3, 5)));
            
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Alex" },
                new Employee { Id = 2, Name = "Hugh" },
                new Employee { Id = 3, Name = "Scott" }
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee {Id = 4, Name = "Bobby"}
            };

            Action<string> writeString = x => Console.WriteLine(x);

            foreach (var employee in developers.Where(x => x.Name.StartsWith("A")))
            {
                writeString(employee.Name);
            }

            foreach(var employee in developers.Where(StartsWithH))
            {
                writeString(employee.Name);
            }

            writeInt(Custom.MyLinq.Count(sales));

            IEnumerator<Employee> enumerator = sales.GetEnumerator();
            while(enumerator.MoveNext())
            {
                writeString(enumerator.Current.Name);
            }

            var queryMethod = developers.Where(x => x.Name.Length == 5).OrderBy(y => y.Name);

            foreach (var employee in queryMethod)
            {
                writeString(employee.Name);
            }

            var querySQL = from developer in developers
                         where developer.Name.Length == 4
                         orderby developer.Name
                         select developer;

            foreach (var employee in querySQL)
            {
                writeString(employee.Name);
            }

            Console.ReadKey();
        }

        private static bool StartsWithH(Employee employee)
        {
            return employee.Name.StartsWith("H");
        }
    }
}
