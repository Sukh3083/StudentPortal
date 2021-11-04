using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Panels
{
    public partial class Login : Form
    {
        string connectionString = "server=localhost;Initial Catalog=student_portal;user=root;password=root";
        MySqlConnection con;
        public Login()
        {
            InitializeComponent();
            con = new MySqlConnection(connectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (textUName.Text==""||textPassword.Text=="")
                {
                    MessageBox.Show("Please enter the username and password.");
                }
                else {

                    con.Open();
                    string sqlQuery = "select * from login_users where username=@username and password=@password";
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlQuery, con);
                    mySqlDataAdapter.SelectCommand.Parameters.AddWithValue("@username", textUName.Text);
                    mySqlDataAdapter.SelectCommand.Parameters.AddWithValue("@password", textPassword.Text);

                    DataSet ds = new DataSet();
                    mySqlDataAdapter.Fill(ds);
                    con.Close();

                    int count = ds.Tables[0].Rows.Count;
                    if (count == 1) {
                        MessageBox.Show("You have logged in successfully.");
                        Form1 form1 = new Form1();
                        this.Hide();
                        form1.Show();
                        
                    }
                    else
                        MessageBox.Show("Please enter the correct username and password.");
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                if (con != null)
                    con.Close();
            }
        }
    }
}
