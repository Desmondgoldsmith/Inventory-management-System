using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ShopriteInventoryApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        MotherOfAllConnections Connect = new MotherOfAllConnections();
        Security Encrypt = new Security();
        string query;
        SqlCommand cmd;
        SqlDataReader dr;
        string role;
        private void Login_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(20, 20, 20);
            gunaPanel1.BackColor = Color.FromArgb(32, 32, 32);
            gunaPanel2.BackColor = Color.FromArgb(32, 32, 32);
            gunaLineTextBox1.BackColor = Color.FromArgb(32, 32, 32);
            gunaLineTextBox2.BackColor = Color.FromArgb(32, 32, 32);
            gunaLineTextBox2.UseSystemPasswordChar = true;

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaLineTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLineTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            vallogin();
        }

        public void vallogin()
        {
            //Validating textboxes
            if (string.IsNullOrWhiteSpace(gunaLineTextBox1.Text.ToString()))
            {

                MessageBox.Show("Please Input Your UserName", "UserName Required", 0, MessageBoxIcon.Information);
                gunaLineTextBox1.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(gunaLineTextBox2.Text.ToString()))
            {

                MessageBox.Show("Please Input Your Password", "Password Required", 0, MessageBoxIcon.Information);
                gunaLineTextBox2.Focus();
                return;
            }
            else
            {
                try
                {
                    //selecting from the table and comparing values in textboxes to values in table
                    Connect.openConn();
                    query = "SELECT * FROM Users WHERE userName = @uname  and  userPassword = @upass";
                    cmd = new SqlCommand(query, Connect.returnConn());
                    cmd.Parameters.AddWithValue("@uname",gunaLineTextBox1.Text);
                    cmd.Parameters.AddWithValue("@upass", Encrypt.PasswordHash(gunaLineTextBox2.Text));
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows == true)
                    {

                        if (dr.Read())
                        {

                            role = dr.GetValue(6).ToString();
                            if (dt.Rows.Count > 0 && role == "Admin")
                             {
                                 MessageBox.Show("Login Successful!","Success",0,MessageBoxIcon.Information);
                                var main = new Form1();
                                main.Show();
                                main.bunifuLabel2.Text = gunaLineTextBox1.Text;
                                main.bunifuLabel3.Text = role;

                                Byte[] data = new Byte[0];
                                data = (Byte[])dr.GetValue(7);
                                MemoryStream mem = new MemoryStream(data);
                                main.bunifuPictureBox1.Image = Image.FromStream(mem);

                                this.Hide();
                            }
                           else if (dt.Rows.Count > 0 && role == "Attendant")
                            {
                                MessageBox.Show("Login Successful!", "Success", 0, MessageBoxIcon.Information);
                                var main = new Form1();
                                main.Show();
                                main.bunifuLabel2.Text = gunaLineTextBox1.Text;
                                main.bunifuLabel3.Text = role;
                                main.bunifuButton5.Enabled = false;
                                Byte[] data = new Byte[0];
                                data = (Byte[])dr.GetValue(7);
                                MemoryStream mem = new MemoryStream(data);
                                main.bunifuPictureBox1.Image = Image.FromStream(mem);

                                this.Hide();
                            }

                            else
                            {
                        MessageBox.Show("Please enter Correct Username and Password");
                    }
                }
                   
                    }
                    else
                    {
                        MessageBox.Show("Wrong Username Or Password", "Failed To Login", 0, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("THE FOLLOWING ERROR OCCURED " + ex.Message);
                }
                finally
                {
                    Connect.closeConn();
                }
            }

        }

        private void gunaCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaCheckBox1.Checked)
            {
                gunaLineTextBox2.UseSystemPasswordChar = false;
            }
            else
            {
                gunaLineTextBox2.UseSystemPasswordChar = true;
            }
        }
    }

    }

    

              
             