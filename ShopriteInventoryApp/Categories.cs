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

namespace ShopriteInventoryApp
{
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
        }
        MotherOfAllConnections Connect = new MotherOfAllConnections();

        private void bunifuPanel3_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mainForm = (Form1)Application.OpenForms["Form1"];
            this.Close();
            mainForm.Show();
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'shopriteDataSet.Categories' table. You can move, or remove it, as needed.
            this.categoriesTableAdapter.Fill(this.shopriteDataSet.Categories);

            //load id into textbox60
            GenerateID();

            //disable buttons
            bunifuButton4.Enabled = false;
            bunifuButton3.Enabled = false;
            textBox1.Visible = false;

        }

        public void GenerateID()
        {

            //load id into textbox60
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            string addons = "#CD";
            bunifuTextBox1.Text = addons+r;
        }
        private void Randomgenerator()
        {
           
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
        }

        //SAVE CATEGORY
        public void SaveCategory()
        {
            //validating
            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text.ToString()))
            {
                MessageBox.Show("The 'ID' Textbox Cannot Be Null","Enter The ID",0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }

            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text.ToString()))
            {
                MessageBox.Show("The 'Category Name' Field Cannot be Null", "Enter The Category Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }

            try
            {
                Connect.openConn();
                //checking if student already exist
                SqlCommand chk = new SqlCommand("SELECT * FROM Categories WHERE CategoryName='" + bunifuTextBox2.Text + "'", Connect.returnConn());
                SqlDataReader dr = chk.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("Category WITH NAME '" + bunifuTextBox2.Text + "' IS ALREADY REGISTERED", "NOTICE", 0, MessageBoxIcon.Exclamation);
                    bunifuTextBox2.Clear();
                    return;
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    string sqlQuery = "Insert into Categories(CategoryName,CategoryID) Values(@catName,@CatID)";
                    SqlCommand cmd = new SqlCommand(sqlQuery, Connect.returnConn());
                    cmd.Parameters.AddWithValue("@CatID", bunifuTextBox1.Text);
                    cmd.Parameters.AddWithValue("@catName", bunifuTextBox2.Text);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Category With ID of '" + bunifuTextBox1.Text + "' Saved Successfully", "Saved!", 0, MessageBoxIcon.Information);
                        this.categoriesTableAdapter.Fill(this.shopriteDataSet.Categories);
                        Reset();
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

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow click = bunifuDataGridView1.Rows[e.RowIndex];
                bunifuTextBox2.Text = click.Cells[1].Value.ToString();
                bunifuTextBox1.Text = click.Cells[2].Value.ToString();
                textBox1.Text = click.Cells[0].Value.ToString();

                bunifuButton4.Enabled = true;
                bunifuButton3.Enabled = true;
                bunifuButton2.Enabled = false;
            }
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow click = bunifuDataGridView1.Rows[e.RowIndex];
                bunifuTextBox2.Text = click.Cells[1].Value.ToString();
                bunifuTextBox1.Text = click.Cells[2].Value.ToString();


                bunifuButton4.Enabled = true;
                bunifuButton3.Enabled = true;
                bunifuButton2.Enabled = false;

            }
        }
        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            Reset();
        }

        public void Reset()
        {
            bunifuTextBox1.Clear();
            GenerateID();
            bunifuTextBox2.Clear();
            bunifuButton2.Enabled = true;
            bunifuButton4.Enabled = false;
            bunifuButton3.Enabled = false;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            SaveCategory();

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text))
            {
                MessageBox.Show("The ID field cannot be Null", "Error", 0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text))
            {
                MessageBox.Show("The 'Category Name' field cannot be Null", "Error", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            try
            {
                Connect.openConn();
                var query = "update Categories set CategoryName = @CatName,CategoryID=@CatID where id = @id";
                SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
                cmd.Parameters.AddWithValue("@CatName", bunifuTextBox2.Text);
                cmd.Parameters.AddWithValue("@CatID", bunifuTextBox1.Text);
                cmd.Parameters.AddWithValue("@id", textBox1.Text);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Category with mame '" + bunifuTextBox2.Text + "' Updated Successfully ", "Success", 0, MessageBoxIcon.Information);
                    this.categoriesTableAdapter.Fill(this.shopriteDataSet.Categories);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.closeConn();
            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text))
            {
                MessageBox.Show("The ID field cannot be Null", "Error", 0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text))
            {
                MessageBox.Show("The 'Category Name' field cannot be Null", "Error", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            try
            {

                Connect.openConn();
                if (MessageBox.Show("THIS ACTION CAN'T BE REVERSED. ARE YOU SURE YOU WANT TO DELETE?", "DELETE RECORD", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    var query = "Delete from Categories where id = @id";
                    SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Category with name '" + bunifuTextBox1.Text + "' is deleted successfully!", "Deleted!", 0, MessageBoxIcon.Warning);
                        this.categoriesTableAdapter.Fill(this.shopriteDataSet.Categories);
                        bunifuTextBox2.Clear();
                    }
                }else
                {
                    this.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connect.closeConn();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void categoriesBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuColorTransition1_ColorChanged(object sender, Bunifu.UI.WinForms.BunifuColorTransition.ColorChangedEventArgs e)
        {

        }
    }
}
