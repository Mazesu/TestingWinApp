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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EmployeesWinApp
{
    public partial class UpdateDepartmentForm : Form
    {
        private Department department;
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
        public UpdateDepartmentForm(Department department)
        {
            InitializeComponent();
            FillComboBoxWithDepartments();
            FillComboBoxWithEmployees();
            this.department = department;
            textBox6.Text = department.Id.ToString();
            textBox1.Text = department.Title;
            comboBox1.SelectedValue = department.ParentDepartmentId;
            comboBox2.SelectedValue = department.ManagerId;

            int? selectedDepartmentId = department.ParentDepartmentId ?? -1; // Выберите значение по умолчанию, если ParentDepartmentId равен null
            var selectedDepartmentItem = comboBox1.Items
                .OfType<KeyValuePair<int, string>>()
                .FirstOrDefault(item => item.Key == selectedDepartmentId);

            if (!selectedDepartmentItem.Equals(default(KeyValuePair<int, string>)))
            {
                comboBox1.SelectedItem = selectedDepartmentItem;
            }


            int? selectedManagerId = department.ManagerId;
            foreach (KeyValuePair<int, string> kvp in comboBox2.Items)
            {
                if (kvp.Key == selectedManagerId)
                {
                    comboBox2.SelectedItem = kvp;
                    break;
                }
            }

        }

        private void UpdateDepartmentForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (department != null) // проверка на null
            {
                department.Title = textBox1.Text;
                if (comboBox1.SelectedValue != null)
                {
                    department.ParentDepartmentId = (int)comboBox1.SelectedValue;
                }

                if (comboBox2.SelectedValue != null)
                {
                    department.ManagerId = (int)comboBox2.SelectedValue;
                }

                DepartmentDataAccess dataAccess = new DepartmentDataAccess(connectionString);
                dataAccess.UpdateDepartment(department);
                this.Close();
            }
            else
            {
                MessageBox.Show("Произошла ошибка. Пожалуйста, попробуйте снова.");
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedItem is KeyValuePair<int, string> selectedDepartment)
                {
                    department.ParentDepartmentId = selectedDepartment.Key;
                }
                else if (comboBox1.SelectedItem is KeyValuePair<int?, string> selectedNullableDepartment)
                {
                    department.ParentDepartmentId = selectedNullableDepartment.Key;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                var selectedManager = (KeyValuePair<int, string>)comboBox2.SelectedItem;
                department.ManagerId = selectedManager.Key;
            }
        }
    }
}
