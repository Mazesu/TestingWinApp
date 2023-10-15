using System.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;

namespace EmployeesWinApp
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Mirage\\AppData\\Local\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB\\EmployeesWindowsApplication.mdf\";Integrated Security=True;Connect Timeout=30";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadEmployeesData();
            LoadDepartmentsData();
            FillDepartmentComboBox();
        }

        private void LoadEmployeesData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT e.Id, e.FullName, e.PersonnelNumber, e.Position, d.Title AS Department, e.Email, e.Phone, e.HireDate, e.TerminationDate " +
                               "FROM Employees e " +
                               "LEFT JOIN Departments d ON e.DepartmentId = d.Id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["Id"].HeaderText = "Id";
                dataGridView1.Columns["Id"].Width = 40;
                dataGridView1.Columns["FullName"].HeaderText = "Полное имя";
                dataGridView1.Columns["PersonnelNumber"].HeaderText = "Табельный номер";
                dataGridView1.Columns["PersonnelNumber"].Width = 75;
                dataGridView1.Columns["Position"].HeaderText = "Должность";
                dataGridView1.Columns["Department"].HeaderText = "Отдел";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Phone"].HeaderText = "Телефон";
                dataGridView1.Columns["HireDate"].HeaderText = "Дата приема на работу";
                dataGridView1.Columns["TerminationDate"].HeaderText = "Дата увольнения";
            }
        }

        private void LoadDepartmentsData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT d.Id, d.Title, p.Title AS ParentDepartment, e.FullName AS Manager " +
                               "FROM Departments d " +
                               "LEFT JOIN Departments p ON d.ParentDepartmentId = p.Id " +
                               "LEFT JOIN Employees e ON d.ManagerId = e.Id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                dataGridView2.Columns["Id"].HeaderText = "Id";
                dataGridView2.Columns["Id"].Width = 40;
                dataGridView2.Columns["Title"].HeaderText = "Наименование";
                dataGridView2.Columns["ParentDepartment"].HeaderText = "Головное подразделение";
                dataGridView2.Columns["Manager"].HeaderText = "Руководитель";
            }
        }

        private void добавитьНовогоСотрудникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEmployeeForm addEmployeeForm = new AddEmployeeForm();
            addEmployeeForm.ShowDialog();
            LoadEmployeesData();
        }

        private void уволитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedEmployeeId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

                EmployeeDataAccess dataAccess = new EmployeeDataAccess(connectionString);
                dataAccess.TerminateEmployee(selectedEmployeeId);

                // Обновить данные в DataGridView после увольнения
                LoadEmployeesData();
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для увольнения.");
            }
        }

        public Employee LoadEmployeeById(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Employees WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", employeeId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
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
                            HireDate = (DateTime)reader["HireDate"]
                        };
                        return employee;
                    }
                }
            }

            return null; // Если сотрудник не найден
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получить выбранную запись (строку)
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Получить значение Id сотрудника 
                int employeeId = (int)selectedRow.Cells["Id"].Value;

                // Загрузить данные сотрудника по Id из базы данных
                Employee employee = LoadEmployeeById(employeeId);

                // Передача объекта employee в форму
                UpdateEmployeeForm editForm = new UpdateEmployeeForm(employee);
                editForm.ShowDialog();
                LoadEmployeesData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для редактирования.");
            }
        }

        private void поИмениToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeSearch employeeSearch = new EmployeeSearch(connectionString);
            string selectedCategory = comboBox1.Text; // Получить выбранную категорию
            string searchText = textBox1.Text; // Получить введенный текст для поиска

            if (!string.IsNullOrEmpty(selectedCategory) && !string.IsNullOrEmpty(searchText))
            {
                List<Employee> searchResults = null;

                if (selectedCategory == "По имени")
                {
                    searchResults = employeeSearch.SearchByName(searchText);
                }
                else if (selectedCategory == "По табельному номеру")
                {
                    searchResults = employeeSearch.SearchByPersonnelNumber(searchText);
                }

                if (searchResults != null && searchResults.Any())
                {
                    dataGridView1.DataSource = searchResults;
                }
                else
                {
                    MessageBox.Show("Ничего не найдено.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите категорию и введите текст для поиска.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;

            LoadEmployeesData();
        }

        //Метод для заполнения ComboBox2 подразделениями
        public void FillDepartmentComboBox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL-запрос для выбора всех доступных подразделений
                string query = "SELECT Id, Title FROM Departments";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int departmentId = (int)reader["Id"];
                            string departmentTitle = (string)reader["Title"];

                            comboBox2.Items.Add(departmentTitle);
                        }
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDepartment = comboBox2.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedDepartment))
            {
                DisplayEmployeesByDepartment(selectedDepartment);
            }
        }

        private void DisplayEmployeesByDepartment(string departmentTitle)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string getDepartmentIdQuery = "SELECT Id FROM Departments WHERE Title = @Title";
                SqlCommand getDepartmentIdCommand = new SqlCommand(getDepartmentIdQuery, connection);
                getDepartmentIdCommand.Parameters.AddWithValue("@Title", departmentTitle);

                int departmentId = (int)getDepartmentIdCommand.ExecuteScalar(); // Получаем DepartmentId

                string query = "SELECT Id, FullName, PersonnelNumber, Position, Email, Phone, HireDate, TerminationDate " +
                               "FROM Employees " +
                               "WHERE DepartmentId = @DepartmentId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DepartmentId", departmentId);
                // Создаем DataTable и DataAdapter для получения данных
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                // Привязываем DataTable к DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showStatistics showStatistics = new showStatistics();
            showStatistics.ShowDialog();
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int id = (int)selectedRow.Cells["Id"].Value;

                // SQL-запрос для удаления записи из базы данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM Employees WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
                dataGridView1.Rows.Remove(selectedRow);
            }
        }
        public Department LoadDepartmentById(int departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Departments WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", departmentId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Department department = new Department
                        {
                            Id = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            ParentDepartmentId = reader["ParentDepartmentId"] == DBNull.Value ? (int?)null : (int)reader["ParentDepartmentId"],
                            ManagerId = (int)reader["ManagerId"]
                        };
                        return department;
                    }
                }
            }

            return null; // Если отделение не найдено
        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDepartmentForm addDepartmentForm = new AddDepartmentForm();
            addDepartmentForm.ShowDialog();
            LoadDepartmentsData();
        }

        private void изменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Получить выбранную запись (строку)
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Получить значение Id отделения 
                int depId = (int)selectedRow.Cells["Id"].Value;
                Department department = LoadDepartmentById(depId);
                // Загрузить данные отделения по Id из базы данных
                UpdateDepartmentForm DepEditForm = new UpdateDepartmentForm(department);
                DepEditForm.ShowDialog();
                LoadDepartmentsData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите подразделение для редактирования.");
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                int id = (int)selectedRow.Cells["Id"].Value;

                // SQL-запрос для удаления записи из базы данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM Departments WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
                dataGridView2.Rows.Remove(selectedRow);
            }
        }
        string excelFilePath = @"C:\Users\Mirage\Desktop\Projects\EmployeesWinApp\bin\file.xlsx";
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ImportEmployeesFromExcel(excelFilePath, connectionString);
            LoadDepartmentsData();
            LoadEmployeesData();       
        }

        private int GetFirstAvailableEmployeeId(SqlConnection connection)
        {
            using (var command = new SqlCommand("SELECT TOP 1 Id FROM Employees", connection))
            {
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    return (int)result;
                }
                return CreateDefaultEmployee(connection); 
            }
        }
        private int CreateDefaultEmployee(SqlConnection connection)
        {
            string defaultFullName = "Директор"; 
            string defaultPersonnelNumber = "0";
            string defaultPosition = "Директор";
            Status defaultEmployeeStatus = Status.Active; 

            using (var command = new SqlCommand("INSERT INTO Employees (FullName, PersonnelNumber, Position, EmployeeStatus) VALUES (@FullName, @PersonnelNumber, @Position, @EmployeeStatus); SELECT SCOPE_IDENTITY();", connection))
            {
                command.Parameters.AddWithValue("@FullName", defaultFullName);
                command.Parameters.AddWithValue("@PersonnelNumber", defaultPersonnelNumber);
                command.Parameters.AddWithValue("@Position", defaultPosition);
                command.Parameters.AddWithValue("@EmployeeStatus", defaultEmployeeStatus);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }



        public void ImportEmployeesFromExcel(string excelFilePath, string connectionString)
        {
            using (var workbook = new XLWorkbook(excelFilePath))
            {
                var departmentsWorksheet = workbook.Worksheet(2); // Лист для подразделений

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int defaultManagerId = GetFirstAvailableEmployeeId(connection);

                    // Сначала считываем данные из листа Departments
                    for (int row = 2; row <= departmentsWorksheet.LastRowUsed().RowNumber(); row++)
                    {
                        string departmentTitle = departmentsWorksheet.Cell(row, 2).Value.ToString();
                        string parentDepartmentTitle = departmentsWorksheet.Cell(row, 3).Value.ToString();
                        int? parentDepartmentId = null;

                        // Отдельный SQL-запрос для поиска ID подразделения по названию
                        using (var command = new SqlCommand("SELECT Id FROM Departments WHERE Title = @Title", connection))
                        {
                            command.Parameters.AddWithValue("@Title", parentDepartmentTitle);

                            var result = command.ExecuteScalar();
                            if (result != null)
                            {
                                parentDepartmentId = (int)result;
                            }
                        }

                        int managerId = defaultManagerId;
                        Status departmentStatus = Status.Active; // Устанавливаем статус "Active"

                        // SQL-запрос для вставки данных подразделения в базу данных
                        using (var command = new SqlCommand("INSERT INTO Departments (Title, ParentDepartmentId, ManagerId, DepartmentStatus) VALUES (@Title, @ParentDepartmentId, @ManagerId, @DepartmentStatus)", connection))
                        {
                            command.Parameters.AddWithValue("@Title", departmentTitle);
                            command.Parameters.AddWithValue("@ParentDepartmentId", parentDepartmentId ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ManagerId", managerId);
                            command.Parameters.AddWithValue("@DepartmentStatus", departmentStatus);

                            command.ExecuteNonQuery();
                        }
                    }

                    // Теперь считываем данные с листа Employees
                    var employeesWorksheet = workbook.Worksheet(1); // Лист для сотрудников

                    for (int row = 2; row <= employeesWorksheet.LastRowUsed().RowNumber(); row++)
                    {
                        string fullName = employeesWorksheet.Cell(row, 2).Value.ToString();
                        string personnelNumber = employeesWorksheet.Cell(row, 3).Value.ToString();
                        string position = employeesWorksheet.Cell(row, 4).Value.ToString();
                        string departmentTitle = employeesWorksheet.Cell(row, 5).Value.ToString();
                        string email = employeesWorksheet.Cell(row, 6).Value.ToString();
                        string phone = employeesWorksheet.Cell(row, 7).Value.ToString();
                        DateTime hireDate;
                        if (DateTime.TryParse(employeesWorksheet.Cell(row, 8).Value.ToString(), out hireDate))
                        {
                            // Дата успешно преобразована
                        }
                        else
                        {
                            hireDate = DateTime.Now;
                        }

                        DateTime? terminationDate;
                        if (DateTime.TryParse(employeesWorksheet.Cell(row, 9).Value.ToString(), out DateTime termDate))
                        {
                            terminationDate = termDate;
                        }
                        else
                        {
                            terminationDate = null;
                        }

                        Status employeeStatus = Status.Active; // Устанавливаем статус "Active"

                        // Отдельный SQL-запрос для поиска ID подразделения по названию
                        int departmentId;
                        using (var command = new SqlCommand("SELECT Id FROM Departments WHERE Title = @Title", connection))
                        {
                            command.Parameters.AddWithValue("@Title", departmentTitle);

                            var result = command.ExecuteScalar();
                            if (result != null)
                            {
                                departmentId = (int)result;
                            }
                            else
                            {
                                departmentId = 0;
                            }
                        }

                        // SQL-запрос для вставки данных сотрудника в базу данных
                        using (var command = new SqlCommand("INSERT INTO Employees (FullName, PersonnelNumber, Position, DepartmentId, Email, Phone, HireDate, TerminationDate, EmployeeStatus) VALUES (@FullName, @PersonnelNumber, @Position, @DepartmentId, @Email, @Phone, @HireDate, @TerminationDate, @EmployeeStatus)", connection))
                        {
                            command.Parameters.AddWithValue("@FullName", fullName);
                            command.Parameters.AddWithValue("@PersonnelNumber", personnelNumber);
                            command.Parameters.AddWithValue("@Position", position);
                            command.Parameters.AddWithValue("@DepartmentId", departmentId);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@Phone", phone);
                            command.Parameters.AddWithValue("@HireDate", hireDate);
                            command.Parameters.AddWithValue("@TerminationDate", terminationDate ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@EmployeeStatus", employeeStatus);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

        }




    }
}
