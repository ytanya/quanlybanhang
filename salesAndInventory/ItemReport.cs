using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace skelot
{
    public partial class ItemReport : Form
    {
        public SqlCommand cm;
        public SqlConnection cn;
        frmLogin frm = new frmLogin();
        public DataSet1 ds;
        public SqlDataAdapter das;

        public ItemReport()
        {
            InitializeComponent();
        }

        public void LoadData(string sql)
        {

        } 


        private void ItemReport_Load(object sender, EventArgs e)
        {

        }
    }
}
