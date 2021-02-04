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


namespace skelot
{
    public partial class frmConversion : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        ListViewItem lst;
        frmLogin login = new frmLogin();
        public frmConversion()
        {
            InitializeComponent();
        }

        private void frmConversion_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(login.connection);
            cn.Open();
            getData();
            generateID();
            timer1.Start();
        }
        public void generateID()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            txtIDCode.Text = "CNV" + result;
        }
        public void getData()
        {
            //displaying data from Database to lstview
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.Columns.Add("Mã Số", 90);
                listView1.Columns.Add("Đơn Vị 1", 150);
                listView1.Columns.Add("Chuyển Đổi Từ", 100);

                listView1.Columns.Add("Đơn Vị 2", 150);
                listView1.Columns.Add("Chuyển Đổi Đến", 100);
                listView1.Columns.Add("Quy Cách", 180);
                listView1.Columns.Add("Ghi Chú", 180);

                string sql2 = @"Select * from tblConversion";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listView1.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[4].ToString());
                    lst.SubItems.Add(dr[5].ToString());
                    lst.SubItems.Add(dr[6].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FrmAdminMenu frmAM = new FrmAdminMenu();
            frmAM.Show();
        }
        public void InsertTrail()
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", DateTime.Now);
                cm.Parameters.AddWithValue("@Transactype", "Insertion");
                cm.Parameters.AddWithValue("@Description", "Conversion:" + txtIDCode.Text + " Đã được thêm vào!");
                cm.Parameters.AddWithValue("@Authority", login.getUserID());


                cm.ExecuteNonQuery();
                //   MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (txtfromcon.Text == "" || txttocon.Text == "" || txtfromnum.Text == "0" || txttonum.Text == "0" )
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    string sql = @"INSERT INTO tblConversion ([ID],[fromCon],[fromNum],[toCon],[toNum],[spec],[note]) ";
                    sql = sql + " VALUES (@IDCon,@fromCon,@fromNum,@toCon,@toNum,@spec,@note)";
                    cm = new SqlCommand(sql, cn);
                    cm.Parameters.AddWithValue("@IDCon", txtIDCode.Text);
                    cm.Parameters.AddWithValue("@fromCon", txtfromcon.Text);
                    cm.Parameters.AddWithValue("@fromNum", txtfromnum.Text);
                    cm.Parameters.AddWithValue("@toCon", txttocon.Text);
                    cm.Parameters.AddWithValue("@toNum", txttonum.Text);
                    cm.Parameters.AddWithValue("@spec", txtspec.Text);
                    cm.Parameters.AddWithValue("@note", txtnote.Text);
                    cm.ExecuteNonQuery();

                    MessageBox.Show("Đã lưu thành công!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InsertTrail();
                    this.Clear();
                    getData();
                    generateID();
                }
                catch (SqlException l)
                {
                    MessageBox.Show("Vui lòng nhập lại thông tin. ID đã bị trùng lặp!");
                    MessageBox.Show(l.Message);
                }

            }
        }
        public void Clear()
        {
            txtfromcon.Text = "";
            txtfromnum.Text = "";
            txttonum.Text = "";
            txttocon.Text = "";
            txtspec.Text = "";
            txtnote.Text = "";
            lblTempID.Text = "";
            txtfromcon.Focus();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTempID.Text = listView1.FocusedItem.Text;
            //lblTempName.Text = listView1.FocusedItem.SubItems[2].Text;
            txtfromcon.Text = listView1.FocusedItem.SubItems[1].Text;
            txtfromnum.Text = listView1.FocusedItem.SubItems[2].Text;
            txttocon.Text = listView1.FocusedItem.SubItems[3].Text;
            txttonum.Text = listView1.FocusedItem.SubItems[4].Text;
            txtspec.Text = listView1.FocusedItem.SubItems[5].Text;
            txtnote.Text = listView1.FocusedItem.SubItems[6].Text;
        }
        public void DeleteTrail()
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", DateTime.Now);
                cm.Parameters.AddWithValue("@Transactype", "Deletion");
                cm.Parameters.AddWithValue("@Description", "Item: " + lblTempID.Text + " đã được xóa!");
                cm.Parameters.AddWithValue("@Authority", login.getUserID());


                cm.ExecuteNonQuery();
                //   MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (SqlException l)
            {
                MessageBox.Show("Vui lòng nhập lại. tên người dùng bị trùng!");
                MessageBox.Show(l.Message);
            }


        }
        public void deleteRecords()
        {
            try
            {

                //   listView1.FocusedItem.Remove();
                string del = "DELETE from tblConversion where ID='" + lblTempID.Text + "'";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                Clear();
                getData();
                generateID();
            }
            catch (Exception)
            {
                MessageBox.Show("Không có sản phẩm nào được xóa", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0 || lblTempID.Text == "")
            {
                MessageBox.Show("Không có gì đễ xóa!. Vui lọng chọn sản phẩm.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa sản phẩm này không?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DeleteTrail();
                    deleteRecords();
                }
            }
            this.Clear();
            txtfromcon.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            //   string format = "MMM-dd-yyy HH:mm:ss";
            //lblTimer.Text = time.ToString(format);
            lblDate.Text = time.ToString();
        }
        public void AllDelTrail()
        {

            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                cm.Parameters.AddWithValue("@Transactype", "Deletion");
                cm.Parameters.AddWithValue("@Description", "Tất cả dữ liệu Conversion đã bị xóa!");
                cm.Parameters.AddWithValue("@Authority", login.getUserID());

                cm.ExecuteNonQuery();

            }
            catch (SqlException l)
            {
                MessageBox.Show("Vui lòng nhập lại thông tin. tên người dùng đã bị trùng!");
                MessageBox.Show(l.Message);
            }

        }
        public void DeleteAll()
        {

            try
            {

                // listView1.FocusedItem.Remove();
                string del = "DELETE from tblConversion ";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                getData();
                Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa hết các sản phẩm không?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                AllDelTrail();
                DeleteAll();
                generateID();
            }
        }
    }
}
