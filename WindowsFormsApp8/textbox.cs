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
    class con_textbox
    {
        TextBox control;
        public con_textbox(TextBox con)
        {
            control = con;
            control.TextChanged += TextChange;
        }
        public void TextChange(object sender, EventArgs e)
        {
            if (control.Text.Length > 0)
            {
                control.Text = char.ToUpper(control.Text[0]) + control.Text.Substring(1);
            }
        }
}
}
