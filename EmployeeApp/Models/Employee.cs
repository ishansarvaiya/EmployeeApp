﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string SSN { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty;

        [Required]
        public string Zip { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        public DateTime? ExitDate { get; set; }

        public ICollection<EmployeeSalary>? Salaries { get; set; }
    }
}