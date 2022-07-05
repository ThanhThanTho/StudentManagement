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

namespace Project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //load data for the select data part
            List<Major> list = SQLHandle.getAllMajor();
            foreach (Major item in list)
            {
                comboBox1.Items.Add(item.Code);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void AddStudent()
        {
            bool check = true;
            List<Student> list = SQLHandle.getAllStudent();
            foreach (Student stu in list)
            {
                if (stu.Id == numericUpDown1.Value)
                {
                    check = false;
                    MessageBox.Show("Id already exist.");
                }
            }

            if (check)
            {
                bool sex, active;
                if (radioButton1.Checked)
                {
                    sex = true;
                }
                else sex = false;
                if (checkBox1.Checked)
                {
                    active = true;
                }
                else active = false;
                if (textBox5.Text.Length == 0)
                {
                    MessageBox.Show("Name must not empty.");
                }
                else
                {
                    string command = @"insert into Student (ID, Name, Sex, DoB, Major, Active, Scholarship)
                                   values (@id, @name, @sex, @dob, @major, @active, @scholar)";
                    SqlParameter[] parameter = new SqlParameter[7];
                    parameter[0] = new SqlParameter("@id", SqlDbType.Int);
                    parameter[0].Value = (int)numericUpDown1.Value;
                    parameter[1] = new SqlParameter("@name", SqlDbType.NVarChar);
                    parameter[1].Value = textBox5.Text;
                    parameter[2] = new SqlParameter("@sex", SqlDbType.Bit);
                    parameter[2].Value = sex;
                    parameter[3] = new SqlParameter("@dob", SqlDbType.DateTime);
                    parameter[3].Value = dateTimePicker1.Value;
                    parameter[4] = new SqlParameter("@major", SqlDbType.VarChar);
                    parameter[4].Value = comboBox1.Text;
                    parameter[5] = new SqlParameter("@active", SqlDbType.Bit);
                    parameter[5].Value = active;
                    parameter[6] = new SqlParameter("@scholar", SqlDbType.Float);
                    parameter[6].Value = (float)numericUpDown2.Value;
                    if (SQLHandle.SQLExecute(command, parameter) > 0)
                    {
                        MessageBox.Show("Add Student Successfully!");
                        this.Close();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Text.Equals("Add student"))
            {
                AddStudent();
            }
            else UpdateStudent();
        }

        public int getID()
        {
            string[] words = this.Text.Split(' ');
            int id = Convert.ToInt32(words[2]);
            return id;
        }

        public void UpdateStudent()
        {
            List<Student> list = SQLHandle.getAllStudent();
            List<int> ids = new List<int>();
            foreach (Student item in list)
            {
                ids.Add(item.Id);
            }
            ids.Remove(getID());
            if (ids.Contains((int)numericUpDown1.Value))
            {
                MessageBox.Show("Cannot update a existed id.");
            }
            else if (textBox5.Text.Length == 0)
            {
                MessageBox.Show("Name must not be empty");
            }
            else
            {
                bool sex, active;
                if (radioButton1.Checked)
                {
                    sex = true;
                }
                else sex = false;
                if (checkBox1.Checked)
                {
                    active = true;
                }
                else active = false;
                string command = @"update Student 
                set ID = @id1, Name = @name, Sex = @sex, DoB = @dob, Major = @major, 
                Active = @active, Scholarship = @scho
                where ID = @id2";
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@id1", SqlDbType.Int);
                parameters[0].Value = (int)numericUpDown1.Value;
                parameters[1] = new SqlParameter("@name", SqlDbType.NVarChar);
                parameters[1].Value = textBox5.Text;
                parameters[2] = new SqlParameter("@sex", SqlDbType.Bit);
                parameters[2].Value = sex;
                parameters[3] = new SqlParameter("@dob", SqlDbType.DateTime);
                parameters[3].Value = dateTimePicker1.Value;
                parameters[4] = new SqlParameter("@major", SqlDbType.VarChar);
                parameters[4].Value = comboBox1.Text;
                parameters[5] = new SqlParameter("@active", SqlDbType.Bit);
                parameters[5].Value = active;
                parameters[6] = new SqlParameter("@scho", SqlDbType.Float);
                parameters[6].Value = (float)numericUpDown2.Value;
                parameters[7] = new SqlParameter("@id2", SqlDbType.Int);
                parameters[7].Value = getID();
                if (SQLHandle.SQLExecute(command, parameters) > 0)
                {
                    MessageBox.Show("Update Student Successfully!");
                    this.Close();
                }
            }
        }
    }
}
