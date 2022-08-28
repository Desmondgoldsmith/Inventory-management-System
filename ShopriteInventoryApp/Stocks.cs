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
    public partial class Stocks : Form
    {
        public Stocks()
        {
            InitializeComponent();
        }
        MotherOfAllConnections Connect = new MotherOfAllConnections();

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bunifuTextBox1.Text))
                {
                    MessageBox.Show("Set The Invoice Number", "Invoice Number", 0, MessageBoxIcon.Error);
                    bunifuTextBox1.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuDatePicker1.Text))
                {
                    MessageBox.Show("Set The Invoice Date", "Invoice Date", 0, MessageBoxIcon.Error);
                    bunifuDatePicker1.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox2.Text))
                {
                    MessageBox.Show("Set the Customer Name", "Customer Name", 0, MessageBoxIcon.Error);
                    bunifuTextBox2.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox3.Text))
                {
                    MessageBox.Show("Set The Customer's Contact", "Contact", 0, MessageBoxIcon.Error);
                    bunifuTextBox3.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox4.Text))
                {
                    MessageBox.Show("Set The Customer's Address", "Address", 0, MessageBoxIcon.Error);
                    bunifuTextBox4.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox5.Text))
                {
                    MessageBox.Show("Set The Product Name", "Product Name", 0, MessageBoxIcon.Error);
                    bunifuTextBox5.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox6.Text))
                {
                    MessageBox.Show("Set The Product Price", "Price", 0, MessageBoxIcon.Error);
                    bunifuTextBox6.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox7.Text))
                {
                    MessageBox.Show("Set The Product Quantity", "Quantity", 0, MessageBoxIcon.Error);
                    bunifuTextBox7.Focus();
                    return;
                }
                else
                {

                    string prodName = bunifuTextBox5.Text;
                    string qty = bunifuTextBox7.Text;
                    string u_price =   bunifuTextBox6.Text;
                    string t_price = bunifuTextBox9.Text;
                    string[] row = { prodName, qty, u_price, t_price };
                    bunifuDataGridView1.Rows.Add(row);
                    //clear record
                    bunifuTextBox5.Clear();
                    bunifuTextBox6.Clear();
                    bunifuTextBox7.Clear();
                    bunifuTextBox9.Clear();
                    //calculating the sum of total prices
                    int sum = 0;
                    for (int i = 0; i < bunifuDataGridView1.Rows.Count; ++i)
                    {
                        sum += Convert.ToInt32(bunifuDataGridView1.Rows[i].Cells[3].Value);
                    }
                    
                    bunifuTextBox8.Text = sum.ToString();
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            ClearRecord();
        }
        public void ClearRecord()
        {
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox3.Clear();
            bunifuTextBox5.Clear();
            bunifuTextBox5.Clear();
            bunifuTextBox6.Clear();
            bunifuTextBox7.Clear();
            bunifuTextBox9.Clear();
            GenerateID();
        }

        private void Stocks_Load(object sender, EventArgs e)
        {
            GenerateID();
        }
        public void GenerateID()
        {

            //load id into textbox60
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            string addons = "#INV ";
            bunifuTextBox1.Text = addons + r;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var main =  (Form1)Application.OpenForms["Form1"];
            main.Show();
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = bunifuTextBox2.Text;
                SqlCommand cmd = new SqlCommand("select * from Customers  where CONCAT(customerName,customerID) like '%" + bunifuTextBox2.Text + "%' ", Connect.returnConn());
                Connect.openConn();
                   SqlDataReader rd;
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    bunifuTextBox3.Text = rd.GetValue(3).ToString();
                    bunifuTextBox4.Text = rd.GetValue(4).ToString();
                    //rd.Close();
                    if (String.IsNullOrWhiteSpace(bunifuTextBox2.Text))
                    {
                        bunifuTextBox3.Clear();
                        bunifuTextBox4.Clear();
                    }

                }
                else
                {
                    rd.Close();
                    bunifuTextBox2.Clear();
                }
                //rd.Close();
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

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {
            //search products
            try
            {
                string search = bunifuTextBox5.Text;
                SqlCommand cmd = new SqlCommand("select * from Products  where productName like '%" + bunifuTextBox5.Text + "%' ", Connect.returnConn());
                Connect.openConn();
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    bunifuTextBox6.Text = rd.GetValue(2).ToString();
                    //rd.Close();

                    if (String.IsNullOrWhiteSpace(bunifuTextBox5.Text))
                    {
                        bunifuTextBox6.Clear();
                    }

                }
                else 
                {
                    rd.Close();
                    bunifuTextBox6.Clear();
                }
                //rd.Close();
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

        private void bunifuTextBox6_TextChanged(object sender, EventArgs e)
        {
            //multiply price by qty to get the amt
            try
            {
                if ((!string.IsNullOrWhiteSpace(bunifuTextBox6.Text)) && (!string.IsNullOrEmpty(bunifuTextBox7.Text)))
                {
                    bunifuTextBox9.Text = (float.Parse(bunifuTextBox6.Text) * float.Parse(bunifuTextBox7.Text)).ToString();
                }
                else
                {
                    bunifuTextBox9.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("THE FOLLOWING ERROR OCCURED : " + ex.Message);
            }
        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (bunifuTextBox2.Text == "")
            {
                bunifuTextBox4.Clear();
            }
        }

        private void bunifuTextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox7_TextChanged(object sender, EventArgs e)
        {
            //multiply price by qty to get the amt
            try
            {
                if ((!string.IsNullOrWhiteSpace(bunifuTextBox6.Text)) && (!string.IsNullOrEmpty(bunifuTextBox7.Text)))
                {
                    bunifuTextBox9.Text = (float.Parse(bunifuTextBox6.Text) * float.Parse(bunifuTextBox7.Text)).ToString();
                }
                else
                {
                    bunifuTextBox9.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("THE FOLLOWING ERROR OCCURED : " + ex.Message);
            }
        }

        public void saveStock()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bunifuTextBox1.Text))
                {
                    MessageBox.Show("Set The Invoice Number", "Invoice Number", 0, MessageBoxIcon.Error);
                    bunifuTextBox1.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuDatePicker1.Text))
                {
                    MessageBox.Show("Set The Invoice Date", "Invoice Date", 0, MessageBoxIcon.Error);
                    bunifuDatePicker1.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox2.Text))
                {
                    MessageBox.Show("Set the Customer Name", "Customer Name", 0, MessageBoxIcon.Error);
                    bunifuTextBox2.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox3.Text))
                {
                    MessageBox.Show("Set The Customer's Contact", "Contact", 0, MessageBoxIcon.Error);
                    bunifuTextBox3.Focus();
                    return;
                }
                else if (string.IsNullOrWhiteSpace(bunifuTextBox4.Text))
                {
                    MessageBox.Show("Set The Customer's Address", "Address", 0, MessageBoxIcon.Error);
                    bunifuTextBox4.Focus();
                    return;
                }
                else
                {
                    bool shown = false;
                    for (int i = 0; i < bunifuDataGridView1.Rows.Count; i++)
                    {

                        int count = bunifuDataGridView1.Rows.Count;
                        SqlCommand cmd1 = new SqlCommand("insert into Stocks(invoiceNum,invoiceDate,customerName,customerContact,cAddress,productName,productPrice,productQuantity,Amount,grandTotal) values('" + bunifuTextBox1.Text + "','" + bunifuDatePicker1.Text + "','" + bunifuTextBox2.Text + "','" + bunifuTextBox3.Text + "','" + bunifuTextBox4.Text + "','" + bunifuDataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + bunifuDataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + bunifuDataGridView1.Rows[i].Cells[2].Value.ToString() + "','" + bunifuDataGridView1.Rows[i].Cells[3].Value.ToString() + "','" + bunifuTextBox8.Text + "')", Connect.returnConn());
                        Connect.openConn();
                        if (cmd1.ExecuteNonQuery() >= 1)
                        {

                            if (!shown)
                            {
                                MessageBox.Show("Stock Saved For Customer With Name '" + bunifuTextBox2.Text + "' Successfully", "Success!", 0, MessageBoxIcon.Information);
                                
                                shown = true;
                            }


                        }
                        else
                        {
                            MessageBox.Show("Error In Saving Invoice ", "Error!", 0, MessageBoxIcon.Error);

                        }



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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            saveStock();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            printReceipt();
        }

        public void printReceipt()
        {
            try
            {
                MessageBox.Show("Please Wait Patiently For The Receipts", "Saved", 0, MessageBoxIcon.Information);

                var reports = new PrintReports();
                reports.textBox1.Text = this.bunifuTextBox1.Text;
                Connect.closeConn();
                bunifuDataGridView1.Rows.Clear();
                reports.Show();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ClearRecord();

            }
        }
    }
}
