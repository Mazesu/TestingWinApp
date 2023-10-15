using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesWinApp
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PersonnelNumber { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public Status EmployeeStatus { get; set; }

        public Employee() { }
        public Employee(string fullName, string personnelNumber, string position, Department department, string email, string phone, DateTime hireDate, DateTime? terminationDate, Status employeeStatus)
        {
            FullName = fullName;
            PersonnelNumber = personnelNumber;
            Position = position;
            Department = department;
            Email = email;
            Phone = phone;
            HireDate = hireDate;
            TerminationDate = terminationDate;
            EmployeeStatus = employeeStatus;
        }
    }
}
