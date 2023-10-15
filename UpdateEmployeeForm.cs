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
    public partial class UpdateEmployeeForm : Form
    {
        private Employee employee;
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
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

            comboBox1.DisplayMember = "Value"; // Отобразить название
            comboBox1.ValueMember = "Key";    // Значение (Id) будет скрытым
        }


        public UpdateEmployeeForm(Employee employee)
        {
            InitializeComponent();
            FillComboBoxWithDepartments();
            this.employee = employee;

            textBox6.Text = employee.Id.ToString();
            textBox1.Text = employee.FullName;
            textBox2.Text = employee.PersonnelNumber;
            textBox4.Text = employee.Position;
            comboBox1.SelectedValue = employee.DepartmentId;
            textBox3.Text = employee.Email;
            textBox5.Text = employee.Phone;
            dateTimePicker1.Value = employee.HireDate;
            if (employee.TerminationDate.HasValue)
            {
                dateTimePicker2.Value = employee.TerminationDate.Value;
            }
            else
            {
                dateTimePicker2.Visible = false;
                label8.Visible = false;
                dateTimePicker2.Checked = false; 
            }

            int selectedDepartmentId = employee.DepartmentId;
            foreach (KeyValuePair<int, string> item in comboBox1.Items)
            {
                if (item.Key == selectedDepartmentId)
                {
                    comboBox1.SelectedItem = item;
                    break;
                }
            }
        }

        private void UpdateEmployeeForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var selectedDepartment = (KeyValuePair<int, string>)comboBox1.SelectedItem;
                employee.DepartmentId = selectedDepartment.Key;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (employee != null) // проверка на null
            {
                employee.FullName = textBox1.Text;
                employee.PersonnelNumber = textBox2.Text;
                employee.Position = textBox4.Text;
                if (comboBox1.SelectedValue != null)
                {
                    employee.DepartmentId = (int)comboBox1.SelectedValue;
                }
                employee.Email = textBox3.Text;
                employee.Phone = textBox5.Text;
                employee.HireDate = dateTimePicker1.Value;
                if (dateTimePicker2.Checked) // Проверить, выбрана ли дата
                {
                    employee.TerminationDate = dateTimePicker2.Value;
                }
                else
                {
                    employee.TerminationDate = null; // Если дата не выбрана, то оставить TerminationDate как null
                }

                EmployeeDataAccess dataAccess = new EmployeeDataAccess(connectionString);
                dataAccess.UpdateEmployee(employee);
                this.Close();
            }
            else
            {
                MessageBox.Show("Произошла ошибка. Пожалуйста, попробуйте снова.");
            }

        }


    }
}
