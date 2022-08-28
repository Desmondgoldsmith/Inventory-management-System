using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopriteInventoryApp
{
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'shopriteDataSet6.Stocks' table. You can move, or remove it, as needed.
            this.stocksTableAdapter.Fill(this.shopriteDataSet6.Stocks);
            textBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mainForm =  (Form1)Application.OpenForms["Form1"];
            this.Close();
            mainForm.Show();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[1].Value.ToString();
            }
            var receipt = new PrintReports();
            receipt.textBox1.Text = this.textBox1.Text;
            receipt.Show();
        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }
    }
}
