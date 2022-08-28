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
using CrystalDecisions.CrystalReports.Engine;

namespace ShopriteInventoryApp
{
    public partial class PrintReports : Form
    {
        public PrintReports()
        {
            InitializeComponent();
        }
        MotherOfAllConnections Connect = new  MotherOfAllConnections();
        ReportDocument rd = new ReportDocument();


        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            try
            {

                Connect.openConn();
                string requery = "SELECT * FROM Stocks WHERE invoiceNum = '" + textBox1.Text + "'";
                SqlDataAdapter reda = new SqlDataAdapter(requery, Connect.returnConn());
                DataSet remydata = new DataSet();
                reda.Fill(remydata, "Stocks");
                Receipts redatax = new Receipts();
                redatax.SetDataSource(remydata);
                crystalReportViewer1.ReportSource = redatax;
                Connect.closeConn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var order = new Orders();
            order.Show();
        }
    }
}
