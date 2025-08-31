using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public class EmployeeSalary
    {
        public int EmployeeSalaryId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required, Range(30000, 200000)]
        public decimal Salary { get; set; }
    }
}