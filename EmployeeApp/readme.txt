This is an Employee Management System built using ASP.NET Core MVC (.NET 8) with SQL Server as the database.
It demonstrates a full-stack web application with CRUD operations, relational data handling, and a polished Bootstrap UI.

Technology Stack
- C# / .NET 8 MVC
- Entity Framework Core (Code First)
- SQL Server
- Bogus (for realistic data seeding)
- Bootstrap 5 + Bootstrap Icons (UI styling)

Database Schema
Employee
- EmployeeId (PK, Identity)
- Name, SSN (Unique), DOB, Address, City, State, Zip, Phone
- JoinDate, ExitDate (nullable)

EmployeeSalary
- EmployeeSalaryId (PK, Identity)
- EmployeeId (FK → Employee)
- FromDate, ToDate (nullable), Title, Salary

Relationships:
- One Employee → Many Salaries

Features
- Employee List
  - Search by Name or Title
  - Displays current salary
  - Clear filter option

- Add/Edit/Delete Employee
  - Form validation with data annotations
  - Bootstrap floating labels & icons
  - Delete confirmation

- Title List
  - Shows all unique job titles
  - Displays min/max salary per title

- Database Seeding
  - 100+ realistic employees generated with Bogus
  - Employees aged 22–64
  - Random job titles & salaries (50k–150k)

How to Run
1. Clone the solution into Visual Studio 2022.
2. Update appsettings.json connection string with your SQL Server.
3. Run the project → the database is auto-created and seeded.
4. Navigate to:
   - /Employee → Employee List
   - /Title → Title List