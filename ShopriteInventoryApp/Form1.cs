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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //creating an instance of the MotherOfAllConnections class
        MotherOfAllConnections Connect = new MotherOfAllConnections();
        private void bunifuLabel6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var categories = new Categories();
            categories.Show();

        }

        private void bunifuLabel11_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connect.openConn();
            string query = "SELECT COUNT(*) FROM Categories ";
            SqlCommand cmd = new SqlCommand(query, Connect.returnConn());
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            bunifuLabel11.Text = count.ToString();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            var mainForm = new Form1();
            mainForm.Close();
        }
    }
}
