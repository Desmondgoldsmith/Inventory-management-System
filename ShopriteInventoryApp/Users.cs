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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        MotherOfAllConnections Connect = new MotherOfAllConnections();
        string imageLocation;
        Security Encrypt = new Security();

        private void Users_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'shopriteDataSet4.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.shopriteDataSet4.Users);
            GenerateID();
            textBox1.Visible = false;
        }
        public void GenerateID()
        {

            //load id into textbox60
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            string addons = "#UID ";
            bunifuTextBox1.Text = addons + r;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            RegiterUser();
        }

        public void RegiterUser()
        {
            //validating


            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text.ToString()))
            {
                MessageBox.Show("The 'User ID' Field Cannot be Null", "Enter The User ID", 0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text.ToString()))
            {
                MessageBox.Show("The 'User Name' Field Cannot be Null", "Enter The User Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox4.Text.ToString()))
            {
                MessageBox.Show("The 'Phone Number' Field Cannot be Null", "Enter The User's Phone Number", 0, MessageBoxIcon.Warning);
                bunifuTextBox4.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox5.Text.ToString()))
            {
                MessageBox.Show("Enter The User's Email", "USer's Email", 0, MessageBoxIcon.Warning);
                bunifuTextBox5.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox6.Text.ToString()))
            {
                MessageBox.Show("Enter User Password", "User Password", 0, MessageBoxIcon.Warning);
                bunifuTextBox6.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox7.Text.ToString()))
            {
                MessageBox.Show("Enter User Role", "User Role", 0, MessageBoxIcon.Warning);
                bunifuTextBox7.Focus();
                return;
            }
            if (bunifuPictureBox1.Image == null)
            {
                MessageBox.Show("Select User Image", "User Image", 0, MessageBoxIcon.Warning);
                bunifuPictureBox1.Focus();
                return;
            }
            if(bunifuTextBox6.Text != bunifuTextBox7.Text)
            {
                MessageBox.Show("Passwords Do Not Match!", "Password Error", 0, MessageBoxIcon.Warning);
                bunifuTextBox6.Focus();
                return;
            }
          

            try
            {
                Connect.openConn();
                //checking if student already exist
                SqlCommand chk = new SqlCommand("SELECT * FROM Users WHERE userName='" + bunifuTextBox2.Text + "'", Connect.returnConn());
                SqlDataReader dr = chk.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("USER WITH NAME '" + bunifuTextBox2.Text + "' IS ALREADY REGISTERED", "NOTICE", 0, MessageBoxIcon.Exclamation);
                    // bunifuTextBox3.Clear();
                    return;
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    byte[] images = null;
                    FileStream stream = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    images = brs.ReadBytes((int)stream.Length);
                    string sqlQuery = "insert into Users(userId,userName,phoneNumber,email,userPassword,userRole,userImage) VALUES(@uID,@uName,@pNumber,@eMail,@uPass,@uRole,@uImage)";
                    SqlCommand cmd = new SqlCommand(sqlQuery, Connect.returnConn());
                    cmd.Parameters.Add(new SqlParameter("@uImage", images));
                    cmd.Parameters.AddWithValue("@uID", bunifuTextBox1.Text);
                    cmd.Parameters.AddWithValue("@uName", bunifuTextBox2.Text);
                    cmd.Parameters.AddWithValue("@pNumber", bunifuTextBox4.Text);
                    cmd.Parameters.AddWithValue("@eMail", bunifuTextBox5.Text);
                    cmd.Parameters.AddWithValue("@uPass", Encrypt.PasswordHash(bunifuTextBox6.Text));
                    cmd.Parameters.AddWithValue("@uRole", gunaComboBox1.Text);


                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("User With Name  '" + bunifuTextBox2.Text + "' Saved Successfully'" + gunaComboBox1.Text + "'", "Saved!", 0, MessageBoxIcon.Information);
                        Clear();
                        this.usersTableAdapter.Fill(this.shopriteDataSet4.Users);
                        
                    }
                    else
                    {
                        MessageBox.Show("Error In Saving Record", "Error!", 0, MessageBoxIcon.Error);
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", 0, MessageBoxIcon.Error);
            }
            finally
            {
                Connect.closeConn();
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            UpdateUser();
        }

        public void UpdateUser()
        {
            //validating


            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text.ToString()))
            {
                MessageBox.Show("The 'User ID' Field Cannot be Null", "Enter The User ID", 0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text.ToString()))
            {
                MessageBox.Show("The 'User Name' Field Cannot be Null", "Enter The User Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox4.Text.ToString()))
            {
                MessageBox.Show("The 'Phone Number' Field Cannot be Null", "Enter The User's Phone Number", 0, MessageBoxIcon.Warning);
                bunifuTextBox4.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox5.Text.ToString()))
            {
                MessageBox.Show("Enter The User's Email", "USer's Email", 0, MessageBoxIcon.Warning);
                bunifuTextBox5.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox6.Text.ToString()))
            {
                MessageBox.Show("Enter User Password", "User Password", 0, MessageBoxIcon.Warning);
                bunifuTextBox6.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox7.Text.ToString()))
            {
                MessageBox.Show("Enter User Role", "User Role", 0, MessageBoxIcon.Warning);
                bunifuTextBox7.Focus();
                return;
            }
            if (bunifuPictureBox1.Image == null)
            {
                MessageBox.Show("Select User Image", "User Image", 0, MessageBoxIcon.Warning);
                bunifuPictureBox1.Focus();
                return;
            }
            if (bunifuTextBox6.Text != bunifuTextBox7.Text)
            {
                MessageBox.Show("Passwords Do Not Match!", "Password Error", 0, MessageBoxIcon.Warning);
                bunifuTextBox6.Focus();
                return;
            }

            try
            {
                MemoryStream stream = new MemoryStream();
                bunifuPictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                Byte[] pic = stream.ToArray();
                Connect.openConn();
                string query = "Update Users set  userName = @uName, phoneNumber = @pNumber , email = @eMail, userPassword = @uPass , userRole = @uRole , userImage = @uImage WHERE userId = @uID";
                SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
                cmd.Parameters.AddWithValue("@uImage", pic);
                cmd.Parameters.AddWithValue("@uID", bunifuTextBox1.Text);
                cmd.Parameters.AddWithValue("@uName", bunifuTextBox2.Text);
                cmd.Parameters.AddWithValue("@pNumber", bunifuTextBox4.Text);
                cmd.Parameters.AddWithValue("@eMail", bunifuTextBox5.Text);
                cmd.Parameters.AddWithValue("@uPass", Encrypt.PasswordHash(bunifuTextBox6.Text));
                cmd.Parameters.AddWithValue("@uRole", gunaComboBox1.Text);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("User With Name  '" + bunifuTextBox2.Text + "' Updated Successfully", "Saved!", 0, MessageBoxIcon.Information);
                    this.usersTableAdapter.Fill(this.shopriteDataSet4.Users);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Error In Saving Record", "Error!", 0, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.closeConn();
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog dialog = new OpenFileDialog();
            // image filters  
            dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imageLocation = dialog.FileName.ToString();
                // display image in picture box  
                bunifuPictureBox1.ImageLocation = imageLocation;
                // image file path  
                // textBox1.Text = open.FileName;
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            bunifuPictureBox1.Image = null;

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }
        public void DeleteUser()
        {
            //validating
            //validating


            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text.ToString()))
            {
                MessageBox.Show("The 'User ID' Field Cannot be Null", "Enter The User ID", 0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text.ToString()))
            {
                MessageBox.Show("The 'User Name' Field Cannot be Null", "Enter The User Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox4.Text.ToString()))
            {
                MessageBox.Show("The 'Phone Number' Field Cannot be Null", "Enter The User's Phone Number", 0, MessageBoxIcon.Warning);
                bunifuTextBox4.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox5.Text.ToString()))
            {
                MessageBox.Show("Enter The User's Email", "USer's Email", 0, MessageBoxIcon.Warning);
                bunifuTextBox5.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox6.Text.ToString()))
            {
                MessageBox.Show("Enter User Password", "User Password", 0, MessageBoxIcon.Warning);
                bunifuTextBox6.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox7.Text.ToString()))
            {
                MessageBox.Show("Enter User Role", "User Role", 0, MessageBoxIcon.Warning);
                bunifuTextBox7.Focus();
                return;
            }
            if (bunifuPictureBox1.Image == null)
            {
                MessageBox.Show("Select User Image", "User Image", 0, MessageBoxIcon.Warning);
                bunifuPictureBox1.Focus();
                return;
            }
            try
            {
                if (MessageBox.Show("This Action Cannot Be Reverced. Are You Sure You Want To Delete ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Connect.openConn();
                    var query = "delete from Users where userId = @id";
                    SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
                    cmd.Parameters.AddWithValue("@id", bunifuTextBox1.Text);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Product Deleted Successfully", "Success", 0, MessageBoxIcon.Information);
                        this.usersTableAdapter.Fill(this.shopriteDataSet4.Users);
                        Clear();
                    }
                    else
                    {
                        this.Show();

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.closeConn();
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            textBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox4.Clear();
            gunaComboBox1.Text = "";
            bunifuTextBox5.Text = "";
            bunifuTextBox6.Text = "";
            bunifuPictureBox1.Image = null;
            gunaComboBox1.Text = "";
            GenerateID();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];

                bunifuTextBox1.Text = row.Cells[0].Value.ToString();
                bunifuTextBox2.Text = row.Cells[1].Value.ToString();
                bunifuTextBox4.Text = row.Cells[2].Value.ToString();
                bunifuTextBox5.Text = row.Cells[3].Value.ToString();
                gunaComboBox1.Text = row.Cells[4].Value.ToString();
                //bunifuTextBox7.Text = row.Cells[5].Value.ToString();
                Byte[] data = new Byte[0];
                data = (Byte[])row.Cells[6].Value;
                MemoryStream mem = new MemoryStream(data);
                bunifuPictureBox1.Image = Image.FromStream(mem);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mainForm =  (Form1)Application.OpenForms["Form1"];
            this.Close();
            mainForm.Show();
        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];

                bunifuTextBox1.Text = row.Cells[0].Value.ToString();
                bunifuTextBox2.Text = row.Cells[1].Value.ToString();
                bunifuTextBox4.Text = row.Cells[2].Value.ToString();
                bunifuTextBox5.Text = row.Cells[3].Value.ToString();
                gunaComboBox1.Text = row.Cells[4].Value.ToString();
                //bunifuTextBox7.Text = row.Cells[5].Value.ToString();
                Byte[] data = new Byte[0];
                data = (Byte[])row.Cells[6].Value;
                MemoryStream mem = new MemoryStream(data);
                bunifuPictureBox1.Image = Image.FromStream(mem);

            }
        }
    }
}