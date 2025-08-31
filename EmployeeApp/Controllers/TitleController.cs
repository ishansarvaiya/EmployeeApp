using EmployeeApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    public class TitleController : Controller
    {
        private readonly AppDbContext _context;

        public TitleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var titles = _context.EmployeeSalaries
                .GroupBy(s => s.Title)
                .Select(g => new
                {
                    Title = g.Key,
                    MinSalary = g.Min(x => x.Salary),
                    MaxSalary = g.Max(x => x.Salary)
                })
                .ToList();

            return View(titles);
        }
    }
}