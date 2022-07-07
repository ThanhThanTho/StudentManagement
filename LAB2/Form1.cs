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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //load SQL data
            List<Student> a = SQLHandle.getAllStudent();
            dataGridView1.DataSource = a;

            //load major combobox data
            comboBox1.Items.Add("All");
            List<string> major = new List<string>();
            for (int i = 0; i < a.Count; i++)
            {
                if (!major.Contains(a[i].Major))
                {
                    major.Add(a[i].Major);
                }
            }
            for (int i = 0; i < major.Count; i++)
            {
                comboBox1.Items.Add(major[i]);
            }

            numericUpDown3.Maximum = 31;
            numericUpDown4.Maximum = DateTime.Now.Year;
            numericUpDown7.Maximum = DateTime.Now.Year;
            numericUpDown5.Value = DateTime.Now.Month;
            numericUpDown6.Value = DateTime.Now.Day;
            numericUpDown7.Value = DateTime.Now.Year;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Text = "STUDENT MANAGEMENT";
            dataGridView1.MultiSelect = false;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<bool> sex = new List<bool>();
            List<bool> active = new List<bool>();
            string major;
            if (comboBox1.Text == "All")
            {
                major = "";
            }
            else major = comboBox1.Text;
            //load data for sex radio button
            if (radioButton3.Checked)
            {
                sex.Add(true);
                sex.Add(false);
            }
            else if (radioButton1.Checked)
            {
                sex.Add(true);
            }
            else sex.Add(false);
            sex.Sort();
            active.Sort();
            //load data for active
            if (radioButton4.Checked)
            {
                active.Add(true);
                active.Add(false);
            }
            else if (radioButton5.Checked)
            {
                active.Add(true);
            }
            else active.Add(false);
            DateTime min = new DateTime((int)numericUpDown4.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            DateTime max = new DateTime((int)numericUpDown7.Value, (int)numericUpDown5.Value, (int)numericUpDown6.Value);
            string command = "select * from Student";
            DataTable table = SQLHandle.getData(command, null);
            List<Student> list = SQLHandle.ToStuList(table);
            dataGridView1.DataSource =
                list.Where(x => x.Id.ToString().Contains(textBox7.Text)
                 && sex.Contains(x.Sex)
                 && x.Name.ToLower().Contains(textBox5.Text.ToLower())
                 && x.Major.ToLower().Contains(major.ToLower())
                 && x.Scholarship >= (float)numericUpDown1.Value
                 && active.Contains(x.Active)
                 && (x.Dob >= min && x.Dob < max)).ToList();
        }

        public Dictionary<int, int> limit()
        {
            Dictionary<int, int> limit = new Dictionary<int, int>();
            limit.Add(1, 31); limit.Add(2, 29); limit.Add(3, 31);
            limit.Add(4, 30); limit.Add(5, 31); limit.Add(6, 30);
            limit.Add(7, 31); limit.Add(8, 31); limit.Add(9, 30);
            limit.Add(10, 31); limit.Add(11, 30); limit.Add(12, 31);
            return limit;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Dictionary<int, int> Limit = limit();
            numericUpDown3.Maximum = Limit[(int)numericUpDown2.Value];
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            Dictionary<int, int> Limit = limit();
            numericUpDown6.Maximum = Limit[(int)numericUpDown5.Value];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //load SQL data
            List<Student> a = SQLHandle.getAllStudent();
            dataGridView1.DataSource = a;

            dataGridView1.ClearSelection();


            //reset value 
            textBox7.Text = "";
            radioButton3.Checked = true;
            textBox5.Text = "";
            comboBox1.Text = "All";
            numericUpDown1.Value = 0;
            radioButton4.Checked = true;
            numericUpDown3.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown4.Value = 1970;
            numericUpDown6.Value = DateTime.Now.Day;
            numericUpDown5.Value = DateTime.Now.Month;
            numericUpDown7.Value = DateTime.Now.Year;

            button4.Enabled = false;
            button5.Enabled = false;

            comboBox2.Text = "Id";
            comboBox3.Text = "Ascending";
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button5.Enabled = true;
            button4.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to delete this Student?", "Check", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                string command = "delete from Student where ID = @i";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@i", System.Data.SqlDbType.Int);
                parameter[0].Value = id;
                SQLHandle.SQLExecute(command, parameter);

                //load SQL data
                dataGridView1.DataSource = SQLHandle.getAllStudent();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.Text = "Add student";
            a.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["Id"].Value);
            Form2 a = new Form2();
            a.Text = "Update student: " + id;
            a.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //create excel app
            Microsoft.Office.Interop.Excel._Application app
                = new Microsoft.Office.Interop.Excel.Application();
            //create workbook
            Microsoft.Office.Interop.Excel._Workbook workbook
                = app.Workbooks.Add(Type.Missing);
            //create new excel sheet
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            //storing headers
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            if (dataGridView1.RowCount > 0)
            {
                int rowEx = 2, rowData = 0;
                //storing every data in datagrid
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    DateTime date = Convert.ToDateTime(dataGridView1.Rows[rowData].Cells[3].Value);
                    string sex;
                    if (dataGridView1.Rows[rowData].Cells[2].Value.ToString().Equals("True"))
                    {
                        sex = "Male";
                    }
                    else sex = "Female";
                    worksheet.Cells[rowEx, 1] = dataGridView1.Rows[rowData].Cells[0].Value.ToString();
                    worksheet.Cells[rowEx, 2] = dataGridView1.Rows[rowData].Cells[1].Value;
                    worksheet.Cells[rowEx, 3] = sex;
                    worksheet.Cells[rowEx, 4] = date.Day + "/" + date.Month + "/" + date.Year;
                    worksheet.Cells[rowEx, 5] = dataGridView1.Rows[rowData].Cells[4].Value;
                    worksheet.Cells[rowEx, 6] = dataGridView1.Rows[rowData].Cells[5].Value.ToString();
                    worksheet.Cells[rowEx, 7] = dataGridView1.Rows[rowData].Cells[6].Value.ToString() + "%";
                    rowEx++;
                    rowData++;
                }
            }
            SaveFileDialog a = new SaveFileDialog();
            a.ShowDialog();
            if (a.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(a.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.
                    Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.Quit();
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            List<Student> list = DataToList();
            if (comboBox3.Text.Equals("Ascending"))
            {
                sortAsc(list);
            }
            else sortDes(list);

            dataGridView1.DataSource = list;
            dataGridView1.ClearSelection();
        }

        List<Student> DataToList()
        {
            List<Student> list = new List<Student>();
            int rowData = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowData].Cells[0].Value);
                string name = dataGridView1.Rows[rowData].Cells[1].Value.ToString();
                bool sex = Convert.ToBoolean(dataGridView1.Rows[rowData].Cells[2].Value);
                DateTime dob = Convert.ToDateTime(dataGridView1.Rows[rowData].Cells[3].Value);
                string major = dataGridView1.Rows[rowData].Cells[4].Value.ToString();
                bool active = Convert.ToBoolean(dataGridView1.Rows[rowData].Cells[5].Value);
                float sho = Convert.ToSingle(dataGridView1.Rows[rowData].Cells[6].Value);
                list.Add(new Student(id, name, sex, dob, major, active, sho));
                rowData++;
            }
            return list;
        }

        void sortAsc(List<Student> list)
        {
            if (comboBox2.Text.Equals("Id"))
            {
                list.Sort((x, y) => x.Id.CompareTo(y.Id));
            }
            else if (comboBox2.Text.Equals("Name"))
            {
                list.Sort((x, y) => x.Name.CompareTo(y.Name));
            }
            else if (comboBox2.Text.Equals("Sex"))
            {
                list.Sort((x, y) => x.Sex.CompareTo(y.Sex));
            }
            else if (comboBox2.Text.Equals("DoB"))
            {
                list.Sort((x, y) => x.Dob.CompareTo(y.Dob));
            }
            else if (comboBox2.Text.Equals("Major"))
            {
                list.Sort((x, y) => x.Major.CompareTo(y.Major));
            }
            else if (comboBox2.Text.Equals("Active"))
            {
                list.Sort((x, y) => x.Active.CompareTo(y.Active));
            }
            else if (comboBox2.Text.Equals("Scholarship"))
            {
                list.Sort((x, y) => x.Scholarship.CompareTo(y.Scholarship));
            }
        }

        void sortDes(List<Student> list)
        {
            if (comboBox2.Text.Equals("Id"))
            {
                list.Sort((x, y) => y.Id.CompareTo(x.Id));
            }
            else if (comboBox2.Text.Equals("Name"))
            {
                list.Sort((x, y) => y.Name.CompareTo(x.Name));
            }
            else if (comboBox2.Text.Equals("Sex"))
            {
                list.Sort((x, y) => y.Sex.CompareTo(x.Sex));
            }
            else if (comboBox2.Text.Equals("DoB"))
            {
                list.Sort((x, y) => y.Dob.CompareTo(x.Dob));
            }
            else if (comboBox2.Text.Equals("Major"))
            {
                list.Sort((x, y) => y.Major.CompareTo(x.Major));
            }
            else if (comboBox2.Text.Equals("Active"))
            {
                list.Sort((x, y) => y.Active.CompareTo(x.Active));
            }
            else if (comboBox2.Text.Equals("Scholarship"))
            {
                list.Sort((x, y) => y.Scholarship.CompareTo(x.Scholarship));
            }
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            List<Student> list = DataToList();
            if (comboBox3.Text.Equals("Ascending"))
            {
                sortAsc(list);
            }
            else sortDes(list);

            dataGridView1.DataSource = list;
            dataGridView1.ClearSelection();
        }
    }
}
