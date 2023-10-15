using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesWinApp
{
    public class DepartmentDataAccess
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
        public DepartmentDataAccess(string connectionStr)
        {
            connectionString = connectionStr;
        }
        public void AddDepartment(Department newDepartment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Departments (Title, ParentDepartmentId, ManagerId, DepartmentStatus) " +
                               "VALUES (@Title, @ParentDepartmentId, @ManagerId, @ManagerId)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", newDepartment.Title);
                command.Parameters.AddWithValue("@ParentDepartmentId", newDepartment.ParentDepartmentId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ManagerId", newDepartment.ManagerId);
                command.Parameters.AddWithValue("@DepartmentStatus", newDepartment.DepartmentStatus);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Подразделение успешно добавлено");

                    // Успешно добавлено
                }
                catch (SqlException ex)
                {
                    // Обработка ошибки
                    MessageBox.Show("Ошибка при добавлении подразделения: " + ex.Message);
                }
            }
        }

        public void UpdateDepartment(Department department)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Departments " +
                               "SET Title = @Title, " +
                               "ParentDepartmentId = @ParentDepartmentId, " +
                               "ManagerId = @ManagerId, " +
                               "DepartmentStatus = @DepartmentStatus " +
                               "WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", department.Title);
                if (department.ParentDepartmentId.HasValue)
                {
                    command.Parameters.AddWithValue("@ParentDepartmentId", department.ParentDepartmentId);
                }
                else
                {
                    command.Parameters.AddWithValue("@ParentDepartmentId", DBNull.Value);
                }
                command.Parameters.AddWithValue("@ManagerId", department.ManagerId);
                command.Parameters.AddWithValue("@DepartmentStatus", department.DepartmentStatus);
                command.Parameters.AddWithValue("@Id", department.Id);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Изменения сохранены");
                    // Успешно обновлено
                }
                catch (SqlException ex)
                {
                    // Обработка ошибки
                    MessageBox.Show("Ошибка при обновлении подразделения: " + ex.Message);
                }
            }
        }
    }
}
