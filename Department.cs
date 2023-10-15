using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesWinApp
{
    public class Department
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentDepartmentId { get; set; }
        [ForeignKey("ParentDepartmentId")]
        public Department? ParentDepartment { get; set; }
        public int ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public Employee Manager { get; set; }
        public Status DepartmentStatus { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
