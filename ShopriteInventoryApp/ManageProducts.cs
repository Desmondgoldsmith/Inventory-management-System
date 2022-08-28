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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString();
                bunifuTextBox3.Text = row.Cells[1].Value.ToString();
                bunifuTextBox2.Text = row.Cells[2].Value.ToString();
                gunaComboBox1.Text = row.Cells[3].Value.ToString();
                bunifuDatePicker2.Text = row.Cells[4].Value.ToString();
                bunifuDatePicker1.Text = row.Cells[5].Value.ToString();
                Byte[] data = new Byte[0];
                data = (Byte[])row.Cells[6].Value;
                MemoryStream mem = new MemoryStream(data);
                bunifuPictureBox1.Image = Image.FromStream(mem);
                bunifuTextBox1.Text = row.Cells[7].Value.ToString();

            }
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

            //this.bunifuDatePicker1.Format = DateTimePickerFormat.Custom;
            //this.bunifuDatePicker1.CustomFormat = " ";

            //this.bunifuDatePicker2.Format = DateTimePickerFormat.Custom;
            //this.bunifuDatePicker2.CustomFormat = " ";



            textBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Main =  (Form1)Application.OpenForms["Form1"];
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
            if (String.IsNullOrWhiteSpace(bunifuDatePicker1.Text.ToString()))
            {
                MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
                bunifuDatePicker1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuDatePicker2.Text.ToString()))
            {
                MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
                bunifuDatePicker2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(gunaComboBox1.Text.ToString()))
            {
                MessageBox.Show("Select A Category Name", "Category Name", 0, MessageBoxIcon.Warning);
                gunaComboBox1.Focus();
                return;
            }
            if (bunifuPictureBox1.Image == null)
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
                    string sqlQuery = "insert into Products(productName,productPrice,productCat,expDate,manuDate,productImage,productQuantity) VALUES(@pName,@pPrice,@pCat,@eDate,@mDate,@pImage,@pQty)";
                    SqlCommand cmd = new SqlCommand(sqlQuery, Connect.returnConn());
                    //cmd.Parameters.AddWithValue("@prodID", bunifuTextBox1.Text);
                    cmd.Parameters.Add(new SqlParameter("@pImage", images));
                    cmd.Parameters.AddWithValue("@pName", bunifuTextBox3.Text);
                    cmd.Parameters.AddWithValue("@pPrice", bunifuTextBox2.Text);
                    cmd.Parameters.AddWithValue("@pCat", gunaComboBox1.Text);
                    cmd.Parameters.AddWithValue("@eDate", bunifuDatePicker2.Text);
                    cmd.Parameters.AddWithValue("@mDate", bunifuDatePicker1.Text);
                    cmd.Parameters.AddWithValue("@pQty", bunifuTextBox1.Text);


                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Product With Name of '" + bunifuTextBox3.Text + "' Saved Successfully", "Saved!", 0, MessageBoxIcon.Information);
                        this.productsTableAdapter.Fill(this.shopriteDataSet2.Products);
                        Clear();
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
            update();
        }

        public void update()
        {
            //validating


            if (String.IsNullOrWhiteSpace(bunifuTextBox3.Text.ToString()))
            {
                MessageBox.Show("The 'Product Name' Field Cannot be Null", "Enter The Product Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuDatePicker1.Text.ToString()))
            {
                MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
                bunifuDatePicker1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuDatePicker2.Text.ToString()))
            {
                MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
                bunifuDatePicker2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(gunaComboBox1.Text.ToString()))
            {
                MessageBox.Show("Select A Category Name", "Category Name", 0, MessageBoxIcon.Warning);
                gunaComboBox1.Focus();
                return;
            }
            if (bunifuPictureBox1.Image == null)
            {
                MessageBox.Show("Select Product Image", "Product Image", 0, MessageBoxIcon.Warning);
                bunifuPictureBox1.Focus();
                return;
            }

            try
            {
                MemoryStream stream = new MemoryStream();
                bunifuPictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                Byte[] pic = stream.ToArray();
                Connect.openConn();
                var query = "UPDATE Products set productName = @pName,productPrice=@pPrice,productCat=@pCat,expDate=@eDate,manuDate=@manuDate,productImage=@pImage,productQuantity=@pQty where id = @id";
                SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
                cmd.Parameters.AddWithValue("@id", textBox1.Text);
                cmd.Parameters.AddWithValue("@pName", bunifuTextBox3.Text);
                cmd.Parameters.AddWithValue("@pPrice", bunifuTextBox2.Text);
                cmd.Parameters.AddWithValue("@pCat", gunaComboBox1.Text);
                cmd.Parameters.AddWithValue("@eDate", bunifuDatePicker2.Text);
                cmd.Parameters.AddWithValue("@manuDate", bunifuDatePicker1.Text);
                cmd.Parameters.AddWithValue("@pImage", pic);
                cmd.Parameters.AddWithValue("@pQty", bunifuTextBox1.Text);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Product Registered Successfully", "Success", 0, MessageBoxIcon.Information);
                    this.productsTableAdapter.Fill(this.shopriteDataSet2.Products);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Error in saving Product Data", "Error", 0, MessageBoxIcon.Error);

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

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString();
                bunifuTextBox3.Text = row.Cells[1].Value.ToString();
                bunifuTextBox2.Text = row.Cells[2].Value.ToString();
                gunaComboBox1.Text = row.Cells[3].Value.ToString();
                bunifuDatePicker2.Text = row.Cells[4].Value.ToString();
                bunifuDatePicker1.Text = row.Cells[5].Value.ToString();
                Byte[] data = new Byte[0];
                data = (Byte[])row.Cells[6].Value;
                MemoryStream mem = new MemoryStream(data);
                bunifuPictureBox1.Image = Image.FromStream(mem);
                bunifuTextBox1.Text = row.Cells[7].Value.ToString();

            }

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Delete();
        }
        public void Delete()
        {
            //validating


            if (String.IsNullOrWhiteSpace(bunifuTextBox3.Text.ToString()))
            {
                MessageBox.Show("The 'Product Name' Field Cannot be Null", "Enter The Product Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuDatePicker1.Text.ToString()))
            {
                MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
                bunifuDatePicker1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuDatePicker2.Text.ToString()))
            {
                MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
                bunifuDatePicker2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(gunaComboBox1.Text.ToString()))
            {
                MessageBox.Show("Select A Category Name", "Category Name", 0, MessageBoxIcon.Warning);
                gunaComboBox1.Focus();
                return;
            }
            if (bunifuPictureBox1.Image == null)
            {
                MessageBox.Show("Select Product Image", "Product Image", 0, MessageBoxIcon.Warning);
                bunifuPictureBox1.Focus();
                return;
            }

            try
            {
                if (MessageBox.Show("This Action Cannot Be Reverced. Are You Sure You Want To Delete ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Connect.openConn();
                    var query = "delete from Products where id = @id";
                    SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Product Deleted Successfully", "Success", 0, MessageBoxIcon.Information);
                        this.productsTableAdapter.Fill(this.shopriteDataSet2.Products);
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
            bunifuTextBox3.Clear();
            bunifuTextBox2.Clear();
            gunaComboBox1.Text = "";
            bunifuDatePicker2.Text = "";
            bunifuDatePicker1.Text = "";
            bunifuPictureBox1.Image = null;
            bunifuTextBox1.Clear();
        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuPanel3_Click(object sender, EventArgs e)
        {

        }
    }
}