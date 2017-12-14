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

namespace WindowsFormsApp8
{
    public partial class FormMain : Form
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\varan\source\repos\WindowsFormsApp8\WindowsFormsApp8\Database1.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        Form_Client child_client;
        Form_Product child_product;
        //ID variable used in Updating and Deleting Record  
        int ID = 0;
        public FormMain()
        {
            InitializeComponent();
            DisplayData();
        }
        private void FormMain_Activated(object sender, EventArgs e)
        {
            DisplayData();
        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Product", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            adapt = new SqlDataAdapter("select * from Client", con);
            DataTable dt2 = new DataTable();
            adapt.Fill(dt2);
            dataGridView2.DataSource = dt2;
            con.Close();
        }
        private void товарToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (child_product != null && !child_product.IsDisposed && child_product.Visible)
            {
                child_product.Activate();
                return;
            }

            if (child_product == null)
                child_product = new Form_Product();

            if (child_product.IsDisposed)
                child_product = new Form_Product();

            child_product.Show();
        }

        private void клиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {

                if (child_client != null && !child_client.IsDisposed && child_client.Visible)
                {
                    child_client.Activate();
                    return;
                }

                if (child_client == null)
                    child_client = new Form_Client();

                if (child_client.IsDisposed)
                    child_client = new Form_Client();

                child_client.Show();
        }

        private void свойстваТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
