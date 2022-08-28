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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
        }
        MotherOfAllConnections Connect = new MotherOfAllConnections();
        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Customers_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'shopriteDataSet5.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.shopriteDataSet5.Customers);
            GenerateID();
        }
        public void GenerateID()
        {

            //load id into textbox60
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            string addons = "#CID ";
            bunifuTextBox1.Text = addons + r;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            RegiterCustomers();
        }
        public void RegiterCustomers()
        {
            //validating


            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text.ToString()))
            {
                MessageBox.Show("The 'Customer ID' Field Cannot be Null", "Enter The Customer ID", 0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text.ToString()))
            {
                MessageBox.Show("The 'Customer Name' Field Cannot be Null", "Enter The Customer Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox3.Text.ToString()))
            {
                MessageBox.Show("The 'Phone Number' Field Cannot be Null", "Enter The Customer's Phone Number", 0, MessageBoxIcon.Warning);
                bunifuTextBox3.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox4.Text.ToString()))
            {
                MessageBox.Show("Enter The Customer's Address", "Customer's Address", 0, MessageBoxIcon.Warning);
                bunifuTextBox4.Focus();
                return;
            }

            try
            {
                Connect.openConn();
                //checking if student already exist
                SqlCommand chk = new SqlCommand("SELECT * FROM Customers WHERE customerName='" + bunifuTextBox2.Text + "'", Connect.returnConn());
                SqlDataReader dr = chk.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("CUSTOMER WITH NAME '" + bunifuTextBox2.Text + "' IS ALREADY REGISTERED", "NOTICE", 0, MessageBoxIcon.Exclamation);
                    // bunifuTextBox3.Clear();
                    return;
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    string sqlQuery = "insert into Customers(customerID,customerName,customerContact,customerAddress) VALUES(@cID,@cName,@cNumber,@cAdd)";
                    SqlCommand cmd = new SqlCommand(sqlQuery, Connect.returnConn());
                    cmd.Parameters.AddWithValue("@cID", bunifuTextBox1.Text);
                    cmd.Parameters.AddWithValue("@cName", bunifuTextBox2.Text);
                    cmd.Parameters.AddWithValue("@cNumber", bunifuTextBox3.Text);
                    cmd.Parameters.AddWithValue("@cAdd", bunifuTextBox4.Text);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Customer With Name  '" + bunifuTextBox2.Text + "' Saved Successfully", "Saved!", 0, MessageBoxIcon.Information);
                         Clear();
                        this.customersTableAdapter.Fill(this.shopriteDataSet5.Customers);


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
            updateCustomer();
        }

        public void updateCustomer()
        {
            //validating


            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text.ToString()))
            {
                MessageBox.Show("The 'Customer ID' Field Cannot be Null", "Enter The Customer ID", 0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text.ToString()))
            {
                MessageBox.Show("The 'Customer Name' Field Cannot be Null", "Enter The Customer Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox3.Text.ToString()))
            {
                MessageBox.Show("The 'Phone Number' Field Cannot be Null", "Enter The Customer's Phone Number", 0, MessageBoxIcon.Warning);
                bunifuTextBox3.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox4.Text.ToString()))
            {
                MessageBox.Show("Enter The Customer's Address", "Customer's Address", 0, MessageBoxIcon.Warning);
                bunifuTextBox4.Focus();
                return;
            }
            try
            {

                Connect.openConn();
                string query = "Update Customers set customerName = @cName, customerContact = @cNumber , customerAddress = @cAdd WHERE customerID = @cID";
                SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
                cmd.Parameters.AddWithValue("@cID", bunifuTextBox1.Text);
                cmd.Parameters.AddWithValue("@cName", bunifuTextBox2.Text);
                cmd.Parameters.AddWithValue("@cNumber", bunifuTextBox3.Text);
                cmd.Parameters.AddWithValue("@cAdd", bunifuTextBox4.Text);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Customer With Name  '" + bunifuTextBox2.Text + "' Updated Successfully", "Saved!", 0, MessageBoxIcon.Information);
                     Clear();
                    this.customersTableAdapter.Fill(this.shopriteDataSet5.Customers);

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

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            DeleteCustomer();
        }

        public void DeleteCustomer()
        {
            //validating
            if (String.IsNullOrWhiteSpace(bunifuTextBox1.Text.ToString()))
            {
                MessageBox.Show("The 'Customer ID' Field Cannot be Null", "Enter The Customer ID", 0, MessageBoxIcon.Warning);
                bunifuTextBox1.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text.ToString()))
            {
                MessageBox.Show("The 'Customer Name' Field Cannot be Null", "Enter The Customer Name", 0, MessageBoxIcon.Warning);
                bunifuTextBox2.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox3.Text.ToString()))
            {
                MessageBox.Show("The 'Phone Number' Field Cannot be Null", "Enter The Customer's Phone Number", 0, MessageBoxIcon.Warning);
                bunifuTextBox3.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(bunifuTextBox4.Text.ToString()))
            {
                MessageBox.Show("Enter The Customer's Address", "Customer's Address", 0, MessageBoxIcon.Warning);
                bunifuTextBox4.Focus();
                return;
            }
            try
            {
                if (MessageBox.Show("This Action Cannot Be Reverced. Are You Sure You Want To Delete ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Connect.openConn();
                    var query = "delete from Customers where customerID = @id";
                    SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
                    cmd.Parameters.AddWithValue("@id", bunifuTextBox1.Text);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Customer Deleted Successfully", "Success", 0, MessageBoxIcon.Information);
                        this.customersTableAdapter.Fill(this.shopriteDataSet5.Customers);
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
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox3.Clear();
            bunifuTextBox4.Clear();
            GenerateID();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];

                bunifuTextBox1.Text = row.Cells[0].Value.ToString();
                bunifuTextBox2.Text = row.Cells[1].Value.ToString();
                bunifuTextBox3.Text = row.Cells[2].Value.ToString();
                bunifuTextBox4.Text = row.Cells[3].Value.ToString();

            }
        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];

                bunifuTextBox1.Text = row.Cells[0].Value.ToString();
                bunifuTextBox2.Text = row.Cells[1].Value.ToString();
                bunifuTextBox3.Text = row.Cells[2].Value.ToString();
                bunifuTextBox4.Text = row.Cells[3].Value.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var main =  (Form1)Application.OpenForms["Form1"];
            main.Show();
        }
    }

}