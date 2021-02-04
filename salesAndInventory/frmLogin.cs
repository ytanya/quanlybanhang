using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace skelot
{
    public partial class frmLogin : Form
    {
        SqlCommand cm;
        SqlConnection cn;
        SqlDataReader dr;

        //public string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\iClothing\quanlybanhang\quanlybanhang\SalesAndInventory\Database.mdf;Integrated Security=True;";
        public string connection = "Data Source="+ getDBName(Environment.CurrentDirectory + "\\Config.txt") + ";Initial Catalog=Database;MultipleActiveResultSets=true;Integrated Security=True";
        public string UserID;
        public string getUserID()
        {
            return UserID;
        }
        public static string getDBName(string filepath)
        {
            string lineOfText;
            var filestream = new System.IO.FileStream(filepath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            if ((lineOfText = file.ReadLine()) != null)
            {
                return lineOfText;
            }
            else
            return "";
        }
        public frmLogin()
        {
             
            InitializeComponent();
            UserID = txtUsername.Text;
            timer1.Start();
        }
     

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
        

            string sql = @"Select * from tblLogin where Username like '" + txtUsername.Text + "' and Password like '" + txtPassword.Text + "'";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            dr.Read();

            if (dr.HasRows)
            {
                this.Hide();
                dr.Close();

                InsertTrail();
                FrmAdminMenu frm2 = new FrmAdminMenu();
                frm2.ShowDialog();

              
            }
            else
            {
                MessageBox.Show("Access Denied! ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            UserID = txtUsername.Text;
        }
        public void InsertTrail() 
        {

            try
            {
                string sql = @"INSERT INTO tblLogTrail VALUES(@Dater,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblTime.Text);
                cm.Parameters.AddWithValue("@Description", "User: " + txtUsername.Text + " has successfully Logged In!");
                cm.Parameters.AddWithValue("@Authority", "Cashier");
         

                cm.ExecuteNonQuery();
                           
            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(connection);
            cn.Open();
            txtPassword.PasswordChar = '●';

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            FrmRegister frm4 = new FrmRegister();
            frm4.ShowDialog();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            FrmAdminLogin frm5 = new FrmAdminLogin();
            frm5.ShowDialog();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
           // string format = "MM-dd-yyy HH:mm:ss";
            lblTime.Text = time.ToString();
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
