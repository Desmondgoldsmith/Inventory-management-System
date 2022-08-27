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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(20, 20, 20);
            gunaPanel1.BackColor = Color.FromArgb(32, 32, 32);
            gunaPanel2.BackColor = Color.FromArgb(32, 32, 32);
            gunaLineTextBox1.BackColor = Color.FromArgb(32, 32, 32);
            gunaLineTextBox2.BackColor = Color.FromArgb(32, 32, 32);
        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaLineTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLineTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
