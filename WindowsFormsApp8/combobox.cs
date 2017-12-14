using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public class Con_combobox
    {
        ComboBox control;
        DataTable tbl;
        int num;
        string id;
        string name_tbl;
        string f_tbl;
        string r_id;
        public Con_combobox(ComboBox con, string name, string f, string id)
        {
            control = con;
            name_tbl = name;
            f_tbl = f;
            this.id = id;
            control.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            control.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.control.TextChanged += this.TextChange;
            this.control.GotFocus += this.GotFocus;
            control.DisplayMember = f;
            control.ValueMember = id;
            control.Text = "";
            Update();
        }
        private void Update()
        {
            string str = control.Text;
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * from " + name_tbl + " ORDER BY " + f_tbl, FormMain.con);
            tbl = new DataTable();
            cmd.Fill(tbl);
            control.DataSource = tbl;
            control.Text = str;
        }
        public void TextChange(object sender, EventArgs e)
        {
            string str = control.Text;
            if (str.Length > 0)
            {
                str = char.ToUpper(str[0]) + str.Substring(1);
            }
            Search(str);
            int col = control.Items.Count;
            if (num < col) control.SelectedIndex = num;
            //else control.SelectedIndex = -1;
            control.Text = str;
            control.SelectionStart = control.Text.Length;
        }
        public void GotFocus(object sender, EventArgs e)
        {
            control.DroppedDown = true;
        }
        private void Search(string str)
        {
            num = 0;
            int col = control.Items.Count;
            if (col > 0)
            {
                while (num < col && tbl.Rows[num][1].ToString().StartsWith(str) != true)
                    num++;
            }
        }
        public bool Add_value(string q_str)
        {
            Search(control.Text);
            if (num == control.Items.Count)//control.Text.CompareTo(tbl.Rows[num][1].ToString()) != 0)
            {
                DialogResult dial = MessageBox.Show("Хотите ли вы добавить '" + control.Text + "' в " + q_str + "?", "Добавление нового " + q_str, MessageBoxButtons.YesNo);
                if (dial == DialogResult.Yes)
                {
                    string str = control.Text;
                    SqlCommand cmd = new SqlCommand("insert into " + name_tbl + " (" + f_tbl + ") values(@data)", FormMain.con);
                    FormMain.con.Open();
                    cmd.Parameters.AddWithValue("@data", control.Text);
                    cmd.ExecuteNonQuery();
                    FormMain.con.Close();
                    Update();
                    Search(str);
                    r_id = tbl.Rows[num][0].ToString();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Search(control.Text);
                r_id = tbl.Rows[num][0].ToString();
            }
            return true;
        }
        public string get_id()
        {
            return r_id;
        }
    }
}
