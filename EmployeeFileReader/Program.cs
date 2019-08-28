using System;
using System.Collections.Generic;
using EmployeeFileReader.Entities;
using System.IO;
using System.Globalization;
using System.Linq;

namespace EmployeeFileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employee> list = new List<Employee>();

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        string email = fields[1];
                        double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);

                        list.Add(new Employee(name, email, salary));
                    }
                }

                Console.Write("Enter salary: ");
                double param = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                var emails = list.Where(p => p.Salary > param).OrderBy(p => p.Email).Select(p => p.Email).DefaultIfEmpty();
                Console.WriteLine("Email of people whose salary is more than " + param.ToString("F2", CultureInfo.InvariantCulture));
                foreach (string email in emails)
                {
                    Console.WriteLine(email);
                }

                var sum = list.Where(p => p.Name[0] == 'M').Sum(p => p.Salary);
                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sum.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch(IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
