using Bogus;
using EmployeeApp.Models;

namespace EmployeeApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Employees.Any()) return;

            var faker = new Faker("en");
            var employees = new List<Employee>();

            for (int i = 0; i < 100; i++)
            {
                var dob = faker.Date.Past(64 - 22, DateTime.Today.AddYears(-22));
                var joinDate = faker.Date.Between(dob.AddYears(22), DateTime.Today);

                var employee = new Employee
                {
                    Name = faker.Name.FullName(),
                    SSN = faker.Random.Replace("###-##-####"),
                    DOB = dob,
                    Address = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.StateAbbr(),
                    Zip = faker.Address.ZipCode(),
                    Phone = faker.Phone.PhoneNumber("(###) ###-####"),
                    JoinDate = joinDate,
                    ExitDate = faker.Random.Bool(0.2f) ? faker.Date.Between(joinDate, DateTime.Today) : null
                };

                var title = faker.PickRandom(new[]
                {
                    "Software Engineer","Manager","Analyst","HR Specialist","Team Lead"
                });
                var salary = faker.Random.Decimal(50000, 150000);

                employee.Salaries = new List<EmployeeSalary>
                {
                    new EmployeeSalary
                    {
                        FromDate = joinDate,
                        Title = title,
                        Salary = salary
                    }
                };

                employees.Add(employee);
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}