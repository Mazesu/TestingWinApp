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
    public partial class AddDepartmentForm : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
        public AddDepartmentForm()
        {
            InitializeComponent();
            FillComboBoxWithDepartments();
            FillComboBoxWithEmployees();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Department newdepartment = new Department
            {
                Title = textBox1.Text,
                ParentDepartmentId = comboBox1.SelectedIndex,
                ManagerId = comboBox2.SelectedIndex,
                DepartmentStatus = Status.Active
            };
            DepartmentDataAccess dataAccess = new DepartmentDataAccess(connectionString);
            dataAccess.AddDepartment(newdepartment);

            this.Close();
        }

        public void FillComboBoxWithDepartments()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Title FROM Departments";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        comboBox1.Items.Add(new KeyValuePair<int?, string>(null, "Нет"));
                        while (reader.Read())
                        {
                            int departmentId = (int)reader["Id"];
                            string departmentTitle = (string)reader["Title"];

                            var departmentPair = new KeyValuePair<int, string>(departmentId, departmentTitle);
                            comboBox1.Items.Add(departmentPair);
                        }
                    }
                }
            }
            comboBox1.DisplayMember = "Value"; // Отображать значение (название)
            comboBox1.ValueMember = "Key";    // Значение (Id) будет скрытым
        }

        public void FillComboBoxWithEmployees()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, FullName FROM Employees";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int employeeId = (int)reader["Id"];
                            string employeeFullName = (string)reader["FullName"];

                            var employeePair = new KeyValuePair<int, string>(employeeId, employeeFullName);
                            comboBox2.Items.Add(employeePair);
                        }
                    }
                }
            }
            comboBox2.DisplayMember = "Value"; // Отображать значение (название)
            comboBox2.ValueMember = "Key";    // Значение (Id) будет скрытым
        }
        private void AddDepartmentForm_Load(object sender, EventArgs e)
        {

        }


    }
}
