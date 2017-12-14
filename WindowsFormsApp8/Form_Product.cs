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

    public partial class Form_Product : Form
    {
        SqlCommand cmd;
        DataTable tbl;
        DataTable tbl_group;
        int n_b;
        int n_g;
        int ID = 0;
        string id_brand;
        string id_group;

        Con_combobox brand;
        Con_combobox group;
        public Form_Product()
        {
            InitializeComponent();
            brand = new Con_combobox(comboBox1, "Brand", "Brand", "BrandId");
            group = new Con_combobox(comboBox2, "Groups", "NameGroup", "GroupId");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            bool flag = true;
            flag = brand.Add_value("Бренд");
            flag = group.Add_value("Группу");
            if (!flag)
                return;
            if (textBox7.Text != "" && comboBox1.Text != "" && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" &&
                textBox4.Text != "" && comboBox2.Text != "" && textBox6.Text != "")
            {
                cmd = new SqlCommand("insert into Product(Name,Groups,Category,SubCategory,BrandId,Packaging,ShelfLife,Temperature) " +
                    "values(@name,@groups,@category,@subcategory,@brand,@packaging,@shelflife,@temperature)", FormMain.con);
                FormMain.con.Open();
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@groups", group.get_id());
                cmd.Parameters.AddWithValue("@category", textBox6.Text);
                cmd.Parameters.AddWithValue("@subcategory", textBox7.Text);
                cmd.Parameters.AddWithValue("@brand", brand.get_id());
                cmd.Parameters.AddWithValue("@packaging", textBox2.Text);
                cmd.Parameters.AddWithValue("@shelflife", textBox3.Text);
                cmd.Parameters.AddWithValue("@temperature", textBox4.Text);

                cmd.ExecuteNonQuery();
                FormMain.con.Close();
                ClearData();
            }
            else
            {
                label9.Visible = true;
            }
        }
        
        private void ClearData()
        {
            textBox7.Text = "";
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox2.Text = "";
            textBox6.Text = "";
            ID = 0;
            id_brand = "";
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * from Brand where Brand like '" + comboBox1.Text + "%'", FormMain.con);
            DataTable tbl = new DataTable();
            string str = comboBox1.Text;
            cmd.Fill(tbl);
            comboBox1.DataSource = tbl;
            comboBox1.DisplayMember = "Brand";
            comboBox1.ValueMember = "BrandId";
            //dataGridView1.DataSource = tbl;
            comboBox1.Text = str;
            comboBox1.SelectionStart = comboBox1.Text.Length;
            //label9.Text = comboBox1.SelectedIndex.ToString();
        }
    }

}
