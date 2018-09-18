using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Join_method
{
    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepId { get; set; }
    }
    class Department
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Department> departments = new List<Department>()
            {
                new Department(){ Id = 1, Country = "Ukraine", City = "Donetsk" },
                new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                new Department(){ Id = 3, Country = "France", City = "Paris" },
                new Department(){ Id = 4, Country = "Russia", City = "Moscow" }
            };
            List<Employee> employees = new List<Employee>()
            {
                new Employee() { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2},
                new Employee(){Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
                new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3},
                new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2},
                new Employee(){Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4},
                new Employee(){Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2},
                new Employee(){ Id = 7, FirstName = "Nikita", LastName = "Krotov", Age = 27, DepId = 4}
            };

            // 1) Упорядочить имена и фамилии сотрудников по алфавиту, которые проживают в Украине. Выполнить запрос немедленно.

            var ua = (from e in employees
                     join d in departments on
                     e.DepId equals d.Id
                     orderby e.FirstName, e.LastName
                     where d.Country == "Ukraine"
                     select new { FN = e.FirstName, LN = e.LastName, Cntr = d.Country }).ToList();

            var uasec = employees.Join(
                    departments,
                    e => e.DepId,
                    d => d.Id,
                    (e, d) => new { FN = e.FirstName, LN = e.LastName, Cntr = d.Country }
                ).OrderBy(e => e.FN).Where(d => d.Cntr == "Ukraine").ToList();


            foreach (var item in ua)
                Console.WriteLine($"{item.FN} {item.LN} {item.Cntr}");

            // 2) Отсортировать сотрудников по возрастам, по убыванию. Вывести Id, FirstName, LastName, Age. Выполнить запрос немедленно.

            Console.WriteLine("================================");

            var ages = (from e in employees
                        orderby e.Age descending
                        select e).ToList();

            var agessec = employees.OrderByDescending(e => e.Age);

            foreach (var item in agessec)
                Console.WriteLine($"{item.Id} {item.FirstName} {item.LastName} {item.Age}");

            // 3) Сгруппировать студентов по возрасту. Вывести возраст и сколько раз он встречается в списке.

            Console.WriteLine("================================");
            Console.WriteLine("Age | Count");

            var group = from e in employees
                        group e by e.Age 
                        into a
                        select new
                        {
                            Age = a.Key,
                            Count = a.Count(),
                        };

            var groupsec = employees.GroupBy(e => e.Age).Select(e => new
            {
                Age = e.Key,
                Count = e.Count(),
            });

            foreach (var item in groupsec)
            {
                Console.WriteLine($"{item.Age}  | {item.Count}");
            }
        }
    }
}
