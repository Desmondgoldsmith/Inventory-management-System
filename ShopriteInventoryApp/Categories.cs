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
            var mainForm = new Form1();
            this.Close();
            mainForm.Show();
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'shopriteDataSet.Categories' table. You can move, or remove it, as needed.
            this.categoriesTableAdapter.Fill(this.shopriteDataSet.Categories);

            //load id into textbox60
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            bunifuTextBox1.Text = r;

            //disable buttons
            bunifuThinButton23.Enabled = false;
            bunifuThinButton22.Enabled = false;

        }
        private void Randomgenerator()
        {
           
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            SaveCategory();
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
                string sqlQuery = "Insert into Categories(CategoryName,CategoryID) Values(@catName,@CatID)";
                SqlCommand cmd = new SqlCommand(sqlQuery, Connect.returnConn());
                cmd.Parameters.AddWithValue("@CatID", bunifuTextBox1.Text);
                cmd.Parameters.AddWithValue("@catName", bunifuTextBox2.Text);

                if(cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Category With ID of '"+ bunifuTextBox1.Text +"' Saved Successfully", "Saved!", 0, MessageBoxIcon.Information);
                    this.categoriesTableAdapter.Fill(this.shopriteDataSet.Categories);

                }
                else
                {
                    MessageBox.Show("Error In Saving Record", "Error!", 0, MessageBoxIcon.Error);
                    return;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",0,MessageBoxIcon.Error);
            }
            finally{
                Connect.closeConn();
            }
        }
    }
}
