using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeesWinApp
{
    public class EmployeeSearch
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
        public EmployeeSearch(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Employee> SearchByName(string name)
        {
            List<Employee> result = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Employees WHERE FullName LIKE @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", "%" + name + "%");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = (int)reader["Id"],
                            FullName = (string)reader["FullName"],
                            PersonnelNumber = (string)reader["PersonnelNumber"],
                            Position = (string)reader["Position"],
                            DepartmentId = (int)reader["DepartmentId"],
                            Email = (string)reader["Email"],
                            Phone = (string)reader["Phone"],
                            HireDate = (DateTime)reader["HireDate"],
                            TerminationDate = reader["TerminationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["TerminationDate"],
                        };

                        result.Add(employee);
                    }
                }
            }

            return result;
        }


        public List<Employee> SearchByPersonnelNumber(string personnelNumber)
        {
            List<Employee> result = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Employees WHERE PersonnelNumber LIKE @PersonnelNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonnelNumber", "%" + personnelNumber + "%");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = (int)reader["Id"],
                            FullName = (string)reader["FullName"],
                            PersonnelNumber = (string)reader["PersonnelNumber"],
                            Position = (string)reader["Position"],
                            DepartmentId = (int)reader["DepartmentId"],
                            Email = (string)reader["Email"],
                            Phone = (string)reader["Phone"],
                            HireDate = (DateTime)reader["HireDate"],
                            TerminationDate = reader["TerminationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["TerminationDate"],
                        };

                        result.Add(employee);
                    }
                }
            }

            return result;
        }

    }
}
