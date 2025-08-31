using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchName, string? searchTitle)
        {
            var employees = _context.Employees
                .Include(e => e.Salaries)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                employees = employees.Where(e => e.Name.Contains(searchName));
            }

            if (!string.IsNullOrWhiteSpace(searchTitle))
            {
                employees = employees.Where(e => e.Salaries.Any(s => s.Title.Contains(searchTitle) && s.ToDate == null));
            }

            var list = await employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.Name,
                    CurrentSalary = e.Salaries
                        .Where(s => s.ToDate == null)
                        .OrderByDescending(s => s.FromDate)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, string title, decimal salary)
        {
            if (ModelState.IsValid)
            {
                var newSalary = new EmployeeSalary
                {
                    Title = title,
                    Salary = salary,
                    FromDate = employee.JoinDate
                };
                employee.Salaries = new List<EmployeeSalary> { newSalary };

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.Include(e => e.Salaries)
                                                   .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employees.Any(e => e.EmployeeId == employee.EmployeeId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}