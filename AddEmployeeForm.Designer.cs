namespace EmployeesWinApp
{
    partial class AddEmployeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            textBox1 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            comboBox1 = new ComboBox();
            departmentBindingSource = new BindingSource(components);
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)departmentBindingSource).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Crimson;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(80, 31);
            label1.TabIndex = 0;
            label1.Text = "ФИО";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Crimson;
            label2.Location = new Point(12, 49);
            label2.Name = "label2";
            label2.Size = new Size(234, 31);
            label2.TabIndex = 1;
            label2.Text = "Табельный номер";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Crimson;
            label3.Location = new Point(12, 85);
            label3.Name = "label3";
            label3.Size = new Size(150, 31);
            label3.TabIndex = 2;
            label3.Text = "Должность";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Crimson;
            label4.Location = new Point(12, 127);
            label4.Name = "label4";
            label4.Size = new Size(199, 31);
            label4.TabIndex = 3;
            label4.Text = "Подразделение";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Crimson;
            label5.Location = new Point(12, 163);
            label5.Name = "label5";
            label5.Size = new Size(84, 31);
            label5.TabIndex = 4;
            label5.Text = "Email";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.Crimson;
            label6.Location = new Point(12, 199);
            label6.Name = "label6";
            label6.Size = new Size(118, 31);
            label6.TabIndex = 5;
            label6.Text = "Телефон";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.Crimson;
            label7.Location = new Point(12, 236);
            label7.Name = "label7";
            label7.Size = new Size(170, 31);
            label7.TabIndex = 6;
            label7.Text = "Дата приема";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(252, 9);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(344, 32);
            textBox1.TabIndex = 7;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePicker1.Location = new Point(252, 236);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(344, 32);
            dateTimePicker1.TabIndex = 12;
            // 
            // comboBox1
            // 
            comboBox1.DataBindings.Add(new Binding("DataContext", departmentBindingSource, "Id", true));
            comboBox1.DataBindings.Add(new Binding("SelectedItem", departmentBindingSource, "Id", true));
            comboBox1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(252, 123);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(344, 31);
            comboBox1.TabIndex = 13;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // departmentBindingSource
            // 
            departmentBindingSource.DataSource = typeof(Department);
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(252, 48);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(344, 32);
            textBox2.TabIndex = 14;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.Location = new Point(252, 160);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(344, 32);
            textBox3.TabIndex = 15;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox4.Location = new Point(252, 85);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(344, 32);
            textBox4.TabIndex = 16;
            // 
            // textBox5
            // 
            textBox5.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox5.Location = new Point(252, 198);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(344, 32);
            textBox5.TabIndex = 17;
            // 
            // button1
            // 
            button1.Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(252, 274);
            button1.Name = "button1";
            button1.Size = new Size(344, 47);
            button1.TabIndex = 18;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // AddEmployeeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(602, 329);
            Controls.Add(button1);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(comboBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AddEmployeeForm";
            Text = "Добавить сотрудника";
            Load += AddEmployeeForm_Load;
            ((System.ComponentModel.ISupportInitialize)departmentBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private ComboBox comboBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Button button1;
        private BindingSource departmentBindingSource;
    }
}