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
    public partial class AddEmployeeForm : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
        public AddEmployeeForm()
        {
            InitializeComponent();
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


        private void button1_Click(object sender, EventArgs e)
        {
            Employee newEmployee = new Employee
            {
                FullName = textBox1.Text,
                PersonnelNumber = textBox2.Text,
                Position = textBox4.Text,
                DepartmentId = comboBox1.SelectedIndex,
                Email = textBox3.Text,
                Phone = textBox5.Text,
                HireDate = dateTimePicker1.Value,
                TerminationDate = null,
                EmployeeStatus = Status.Active
            };
            EmployeeDataAccess dataAccess = new EmployeeDataAccess(connectionString);
            dataAccess.AddEmployee(newEmployee);
            
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var selectedDepartment = (KeyValuePair<int, string>)comboBox1.SelectedItem;
                string departmentName = selectedDepartment.Value;

                MessageBox.Show("Выбрано подразделение: " + departmentName);
            }
        }

        private void AddEmployeeForm_Load(object sender, EventArgs e)
        {
            FillComboBoxWithDepartments();
        }
    }
}
