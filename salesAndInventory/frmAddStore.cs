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
    public partial class frmAddStore : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        frmLogin login = new frmLogin();
        ListViewItem lst;
        public frmAddStore()
        {
            InitializeComponent();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmAddStore_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(login.connection);
            cn.Open();
            getData();
            generateID();
            txtName.Focus();
        }
        public void getData()
        {
            //displaying data from Database to lstview
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.Columns.Add("Mã Kho Hàng", 90);
                listView1.Columns.Add("Tên Kho", 190);

                listView1.Columns.Add("Vi trí kho", 90);
                listView1.Columns.Add("Ghi Chú", 100);

                string sql2 = @"Select [ID],[Name],[Location],[Note] from tblStore";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listView1.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[2].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void generateID()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            txtIDCode.Text = "STR" + result;
        }

        public void generateOrderID()
        {

            var chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            //txtOrderID.Text = "OrID:" + result;
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" )
            {
                MessageBox.Show("Vui lòng nhập thông tin tên kho hàng.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    string sql = @"INSERT INTO [dbo].[tblStore]([ID],[Name],[Location],[Note],[CreateDate],[UpdateDate],[UserID]) VALUES(@IDStore,@Name,@Location,@Note,@CreateDate,@UpdateDate,@UserID)";
                    cm = new SqlCommand(sql, cn);
                    cm.Parameters.AddWithValue("@IDStore", txtIDCode.Text);
                    cm.Parameters.AddWithValue("@Name", txtName.Text);
                    cm.Parameters.AddWithValue("@Location", txtLocation.Text);
                    cm.Parameters.AddWithValue("@Note", txtNote.Text);
                    cm.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cm.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                    cm.Parameters.AddWithValue("@UserID", login.getUserID());

                    cm.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu lại thành công!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                    generateID();
                    clearText();
                }
                catch (SqlException l)
                {
                    MessageBox.Show("Vui lòng nhập lại thông tin. Mã kho bị trùng lặp!");
                    MessageBox.Show(l.Message);
                }

            }
            txtName.Focus();
        }
        public void clearText()
        {
            txtName.Text = "";
            txtLocation.Text = "";
            txtNote.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTempID.Text = listView1.FocusedItem.Text;
            txtName.Text = listView1.FocusedItem.SubItems[1].Text;
            txtLocation.Text = listView1.FocusedItem.SubItems[3].Text;
            txtNote.Text = listView1.FocusedItem.SubItems[2].Text;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0 || lblTempID.Text == "")
            {
                MessageBox.Show("Không có gì đễ xóa!. Vui lòng chọn kho hàng.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (MessageBox.Show("Bạn có muỗn xóa kho hàng này không?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    deleteRecords();
                }
            }
            txtName.Focus();
        }
        public void deleteRecords()
        {
            try
            {

                //   listView1.FocusedItem.Remove();
                string del = "DELETE from tblStore where ID='" + lblTempID.Text + "'";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                clearText();
                getData();
                generateID();
            }
            catch (Exception)
            {
                MessageBox.Show("Không có kho hàng nào được xóa", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {

                return;
            }

            if (MessageBox.Show("Bạn Có muốn xóa hết danh sách kho hàng?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                DeleteAll();
                generateID();
                txtName.Focus();
            }
        }
        public void DeleteAll()
        {

            try
            {

                // listView1.FocusedItem.Remove();
                string del = "DELETE * from tblStore ";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                getData();
                clearText();

            }
            catch (Exception)
            {
                MessageBox.Show("Không có kho nào được xóa", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmInsertProductStore frmIPS = new frmInsertProductStore();
            frmIPS.Show();
        }
    }
}
