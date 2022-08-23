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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }

        MotherOfAllConnections Connect = new MotherOfAllConnections();
        string imglocation;
        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'shopriteDataSet2.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.shopriteDataSet2.Products);

            //fill combobox1 with dates
            Connect.openConn();
            SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT CategoryName FROM Categories", Connect.returnConn());
            //this.realGrade1AttendanceTableAdapter1.Fill(this.dessySoftDataSet155.RealGrade1Attendance);
            DataTable dt = new DataTable();

            da.Fill(dt);
            gunaComboBox1.DataSource = dt;
            gunaComboBox1.DisplayMember = "Categories";
            gunaComboBox1.ValueMember = "CategoryName";
            gunaComboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Main = new Form1();
            this.Close();
            Main.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            SaveCategory();
        }
        public void SaveCategory()
        {
            //validating
      

            if (String.IsNullOrWhiteSpace(bunifuTextBox3.Text.ToString()))
            {
                MessageBox.Show("The 'Product Name' Field Cannot be Null", "Enter The Product Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            //if (String.IsNullOrWhiteSpace(bunifuDatePicker1.Text.ToString()))
            //{
            //    MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
            //     bunifuDatePicker1.Focus();
            //    return;
            //}
            //if (String.IsNullOrWhiteSpace(bunifuDatePicker2.Text.ToString()))
            //{
            //    MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
            //    bunifuDatePicker2.Focus();
            //    return;
            //}
            if (String.IsNullOrWhiteSpace(gunaComboBox1.Text.ToString()))
            {
                MessageBox.Show("Select A Category Name", "Category Name", 0, MessageBoxIcon.Warning);
                gunaComboBox1.Focus();
                return;
            }
            if(bunifuPictureBox1.Image == null)
            {
                MessageBox.Show("Select Product Image", "Product Image", 0, MessageBoxIcon.Warning);
                bunifuPictureBox1.Focus();
                return;
            }
            try
            {
                Connect.openConn();
                //checking if student already exist
                SqlCommand chk = new SqlCommand("SELECT * FROM Products WHERE productName='" + bunifuTextBox3.Text + "'", Connect.returnConn());
                SqlDataReader dr = chk.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("Product WITH NAME '" + bunifuTextBox3.Text + "' IS ALREADY REGISTERED", "NOTICE", 0, MessageBoxIcon.Exclamation);
                   // bunifuTextBox3.Clear();
                    return;
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    byte[] images = null;
                    FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    images = brs.ReadBytes((int)stream.Length);
                    string sqlQuery = "insert into Products(productName,productPrice,productCat,expDate,manuDate,productImage,productQuantity) Values(@prodName,@prodPrice,@prodCat,@expDate,@manuDate,@prodImage,@productQty";
                    SqlCommand cmd = new SqlCommand(sqlQuery, Connect.returnConn());
                //cmd.Parameters.AddWithValue("@prodID", bunifuTextBox1.Text);
                    cmd.Parameters.Add(new SqlParameter("@prodImage", images));
                    cmd.Parameters.AddWithValue("@prodName", bunifuTextBox3.Text);
                    cmd.Parameters.AddWithValue("@prodPrice", bunifuTextBox2.Text);
                    cmd.Parameters.AddWithValue("@expDate", bunifuDatePicker2.Text);
                    cmd.Parameters.AddWithValue("@manuDate", bunifuTextBox2.Text);
                    cmd.Parameters.AddWithValue("@prodCat", gunaComboBox1.Text);
                    cmd.Parameters.AddWithValue("@productQty", bunifuTextBox1.Text);


                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Product With Name of '" + bunifuTextBox3.Text + "' Saved Successfully", "Saved!", 0, MessageBoxIcon.Information);
                        this.productsTableAdapter.Fill(this.shopriteDataSet2.Products);

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

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog dialog = new OpenFileDialog();
            // image filters  
            dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imglocation = dialog.FileName.ToString();
                // display image in picture box  
                bunifuPictureBox1.ImageLocation = imglocation;
                // image file path  
                // textBox1.Text = open.FileName;
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            bunifuPictureBox1.Image = null;
        }
    }
}
