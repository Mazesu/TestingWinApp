using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace EmployeesWinApp
{
    public class EmployeeDataAccess
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
        public EmployeeDataAccess(string connectionStr)
        {
            connectionString = connectionStr;
        }

        
        public Department GetDepartmentById(int departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Departments WHERE Id = @DepartmentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DepartmentId", departmentId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Department department = new Department
                        {
                            Id = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            ParentDepartmentId = reader["ParentDepartmentId"] is DBNull ? null : (int?)reader["ParentDepartmentId"],
                            ManagerId = (int)reader["ManagerId"],
                            DepartmentStatus = (Status)reader["DepartmentStatus"],
                        };
                        department.Employees = GetEmployeesByDepartmentId(departmentId);

                        return department;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Employees WHERE DepartmentId = @DepartmentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DepartmentId", departmentId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department department = GetDepartmentById(departmentId);
                        Employee employee = new Employee(
                            (string)reader["FullName"],
                            (string)reader["PersonnelNumber"],
                            (string)reader["Position"],
                            department,
                            (string)reader["Email"],
                            (string)reader["Phone"],
                            (DateTime)reader["HireDate"],
                            (DateTime?)reader["TerminationDate"],
                            (Status)reader["EmployeeStatus"]
                        );
                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }
        public void AddEmployee(Employee newEmployee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Employees (FullName, PersonnelNumber, Position, DepartmentId, Email, Phone, HireDate, TerminationDate, EmployeeStatus) " +
                               "VALUES (@FullName, @PersonnelNumber, @Position, @DepartmentId, @Email, @Phone, @HireDate, @TerminationDate, @EmployeeStatus)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FullName", newEmployee.FullName);
                command.Parameters.AddWithValue("@PersonnelNumber", newEmployee.PersonnelNumber);
                command.Parameters.AddWithValue("@Position", newEmployee.Position);
                command.Parameters.AddWithValue("@DepartmentId", newEmployee.DepartmentId);
                command.Parameters.AddWithValue("@Email", newEmployee.Email);
                command.Parameters.AddWithValue("@Phone", newEmployee.Phone);
                command.Parameters.AddWithValue("@HireDate", newEmployee.HireDate);
                command.Parameters.AddWithValue("@TerminationDate", newEmployee.TerminationDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EmployeeStatus", (int)newEmployee.EmployeeStatus);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Сотрудник успешно добавлен");
                    
                    // Успешно добавлено
                }
                catch (SqlException ex)
                {
                    // Обработка ошибки
                    MessageBox.Show("Ошибка при добавлении сотрудника: " + ex.Message);
                }
            }
        }

        public void TerminateEmployee(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Employees SET TerminationDate = @TerminationDate, EmployeeStatus = @EmployeeStatus WHERE Id = @EmployeeId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TerminationDate", DateTime.Now);
                command.Parameters.AddWithValue("@EmployeeStatus", Status.Closed);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Сотрудник успешно уволен");
                    // Успешно уволен
                }
                catch (SqlException ex)
                {
                    // Обработка ошибки
                    MessageBox.Show("Ошибка при увольнении сотрудника: " + ex.Message);
                }
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Employees " +
                               "SET FullName = @FullName, " +
                               "PersonnelNumber = @PersonnelNumber, " +
                               "Position = @Position, " +
                               "DepartmentId = @DepartmentId, " +
                               "Email = @Email, " +
                               "Phone = @Phone, " +
                               "HireDate = @HireDate, " +
                               "TerminationDate = @TerminationDate, " +
                               "EmployeeStatus = @EmployeeStatus " +
                               "WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FullName", employee.FullName);
                command.Parameters.AddWithValue("@PersonnelNumber", employee.PersonnelNumber);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@Phone", employee.Phone);
                command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                command.Parameters.AddWithValue("@TerminationDate", employee.TerminationDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EmployeeStatus", (int)employee.EmployeeStatus);
                command.Parameters.AddWithValue("@Id", employee.Id);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Изменения сохранены");
                    // Успешно обновлено
                }
                catch (SqlException ex)
                {
                    // Обработка ошибки
                    MessageBox.Show("Ошибка при обновлении сотрудника: " + ex.Message);
                }
            }
        }
        public List<Employee> SearchEmployeesByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Employees WHERE FullName LIKE @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", "%" + name + "%"); // Ищем частичное совпадение имени
                List<Employee> employees = new List<Employee>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            // Заполните объект Employee данными из результата запроса
                        };
                        employees.Add(employee);
                    }
                }
                return employees;
            }
        }

        public Employee SearchEmployeeByPersonnelNumber(string personnelNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Employees WHERE PersonnelNumber = @PersonnelNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonnelNumber", personnelNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            // Заполните объект Employee данными из результата запроса
                        };
                        return employee;
                    }
                }
            }
            return null; // Если сотрудник не найден
        }


    }
}
