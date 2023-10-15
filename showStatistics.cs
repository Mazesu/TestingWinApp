using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeesWinApp
{
    public partial class showStatistics : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
        public showStatistics(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public showStatistics()
        {
            InitializeComponent();
            FillDepartmentComboBox();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;
            string selectedDepartment = comboBox1.SelectedItem as string;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL-запросы для статистики по принятию на работу и увольнению
                string hireQuery;
                string terminationQuery;

                if (selectedDepartment == "Все")
                {
                    hireQuery = "SELECT COUNT(*) FROM Employees " +
                                 "WHERE HireDate BETWEEN @StartDate AND @EndDate";

                    terminationQuery = "SELECT COUNT(*) FROM Employees " +
                                       "WHERE TerminationDate BETWEEN @StartDate AND @EndDate";
                }
                else
                {
                    hireQuery = "SELECT COUNT(*) FROM Employees " +
                                 "WHERE DepartmentId = (SELECT Id FROM Departments WHERE Title = @DepartmentTitle) " +
                                 "AND HireDate BETWEEN @StartDate AND @EndDate";

                    terminationQuery = "SELECT COUNT(*) FROM Employees " +
                                       "WHERE DepartmentId = (SELECT Id FROM Departments WHERE Title = @DepartmentTitle) " +
                                       "AND TerminationDate BETWEEN @StartDate AND @EndDate";
                }

                using (SqlCommand hireCommand = new SqlCommand(hireQuery, connection))
                using (SqlCommand terminationCommand = new SqlCommand(terminationQuery, connection))
                {
                    if (selectedDepartment != "Все")
                    {
                        hireCommand.Parameters.AddWithValue("@DepartmentTitle", selectedDepartment);
                        terminationCommand.Parameters.AddWithValue("@DepartmentTitle", selectedDepartment);
                    }

                    hireCommand.Parameters.AddWithValue("@StartDate", startDate);
                    hireCommand.Parameters.AddWithValue("@EndDate", endDate);

                    terminationCommand.Parameters.AddWithValue("@StartDate", startDate);
                    terminationCommand.Parameters.AddWithValue("@EndDate", endDate);

                    // Выполнение запросов
                    int hireCount = (int)hireCommand.ExecuteScalar();
                    int terminationCount = (int)terminationCommand.ExecuteScalar();

                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(selectedDepartment, startDate, endDate, hireCount, terminationCount);
                }
            }
        }

        private void FillDepartmentComboBox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL-запрос для получения названий всех подразделений
                string query = "SELECT Title FROM Departments";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Добавление результатов запроса в ComboBox
                    while (reader.Read())
                    {
                        string departmentTitle = (string)reader["Title"];
                        comboBox1.Items.Add(departmentTitle);
                    }
                }
            }

            // Добавление опции Все в ComboBox
            comboBox1.Items.Insert(0, "Все");
            comboBox1.SelectedIndex = 0;
        }


    }
}
