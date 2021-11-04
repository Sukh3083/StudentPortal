using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace Panels
{
    public partial class Registration : Form
    {
        //string path = "Data Source=localhost;Initial Catalog=student_portal;user=root;password=root;Integrated Security=True";
        string connectionString = "server=localhost;Initial Catalog=student_portal;user=root;password=root";
        MySqlConnection con;
        MySqlDataAdapter adapter;
        DataTable dt;
        int ID;

        public Registration()
        {
            InitializeComponent();
            con = new MySqlConnection(connectionString);
            display();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textName.Text=="" || textEmail.Text==""||textPhone.Text==""||textAddess.Text==""||textCourse.Text=="" || textStudentId.Text=="") {
                MessageBox.Show("Please fill all the details.");
            }
            else {
                try
                {
                    con.Open();
                string sqlQuery = "INSERT INTO student ( Name, Email, Phone, Address, Course, Gender, StudentId) " +
                    "VALUES(@Name, @Email, @Phone, @Address, @Course, @Gender, @StudentId) ";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, con);
                mySqlCommand.Parameters.AddWithValue("@Name", textName.Text);
                mySqlCommand.Parameters.AddWithValue("@Email", textEmail.Text);
                mySqlCommand.Parameters.AddWithValue("@Phone", int.Parse(textPhone.Text));
                mySqlCommand.Parameters.AddWithValue("@Address", textAddess.Text);
                mySqlCommand.Parameters.AddWithValue("@Course", textCourse.Text);
                mySqlCommand.Parameters.AddWithValue("@StudentId", textStudentId.Text);
                string gender = "";
                if (radioMale.Checked)
                    gender = "Male";
                else
                    gender = "Female";
                mySqlCommand.Parameters.AddWithValue("@Gender", gender);
                mySqlCommand.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student details saved successfully.");
                clear();
                display();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (con != null)
                        con.Close();
                }
            }
        }

        public void clear() {
            textName.Text = "";
            textEmail.Text = "";
            textPhone.Text = "";
            textAddess.Text = "";
            textCourse.Text = "";
            textStudentId.Text = "";
        }

        public void display()
        {
            try
            {
                dt = new DataTable();
                con.Open();
                adapter = new MySqlDataAdapter("Select * from student", con);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textStudentId.Text= dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textName.Text= dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textAddess.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textCourse.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

            radioMale.Checked = true;
            radioFemale.Checked = false;

            if (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString()=="Female") {
                radioMale.Checked = false;
                radioFemale.Checked = true;
            }
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sqlQuery = "update student set StudentId=@StudentId, Name=@Name, Email=@Email, Phone=@Phone," +
                    " Address=@Address, Course=@Course, Gender=@Gender where ID = @ID ";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, con);
                mySqlCommand.Parameters.AddWithValue("@Name", textName.Text);
                mySqlCommand.Parameters.AddWithValue("@Email", textEmail.Text);
                mySqlCommand.Parameters.AddWithValue("@Phone", int.Parse(textPhone.Text));
                mySqlCommand.Parameters.AddWithValue("@Address", textAddess.Text);
                mySqlCommand.Parameters.AddWithValue("@Course", textCourse.Text);
                mySqlCommand.Parameters.AddWithValue("@StudentId", int.Parse(textStudentId.Text));
                mySqlCommand.Parameters.AddWithValue("@ID", ID);
                string gender = "";
                if (radioMale.Checked)
                    gender = "Male";
                else
                    gender = "Female";
                mySqlCommand.Parameters.AddWithValue("@Gender", gender);
                mySqlCommand.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Student details updated successfully.");
                clear();
                display();

            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sqlQuery = "Delete from student where ID=@ID";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, con);
                mySqlCommand.Parameters.AddWithValue("@ID", ID);
                mySqlCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Student details deleted successfully.");
                display();
                clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { 
                if(con != null)
                    con.Close();
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
