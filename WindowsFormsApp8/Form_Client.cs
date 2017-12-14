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
    public partial class Form_Client : Form
    {
        public Form_Client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlCommand cmd = new SqlCommand("insert into Client(Name,INN,Address) " +
                    "values(@name,@inn,@address)", FormMain.con);
                FormMain.con.Open();
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@inn", textBox2.Text);
                cmd.Parameters.AddWithValue("@address", textBox3.Text);
                cmd.ExecuteNonQuery();
                FormMain.con.Close();
                MessageBox.Show("Record new client successfully");
                ClearData();
            }
        }
        private void ClearData()
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
