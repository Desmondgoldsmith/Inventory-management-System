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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mainForm = new Form1();
            this.Close();
            mainForm.Show();
        }
    }
}
