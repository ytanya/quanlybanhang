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
    public partial class frmAddItems : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        // string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Data.accdb";

        ListViewItem lst;
        frmLogin login = new frmLogin();
        public frmAddItems()
        {
            InitializeComponent();
        }
        public void getManufacturer()
        {

            try
            {


                string sql2 = @"Select * from tblManufacturer";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboManufac.Items.Add(dr[1].ToString());

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            //   string format = "MMM-dd-yyy HH:mm:ss";
            //lblTimer.Text = time.ToString(format);
            lblDate.Text = time.ToString();
        }

        private void frmAddItems_Load(object sender, EventArgs e)
        {
            dateEXP.Value = DateTime.Now.AddMonths(6);
            cn = new SqlConnection(login.connection);
            cn.Open();
            getData();
            generateID();
            generateOrderID();
            getManufacturer();
            cboType.SelectedIndex = 0;
            cboSize.SelectedIndex = 0;
            cboManufac.SelectedIndex = 0;
            timer1.Start();
        }
        public void getData()
        {
            //displaying data from Database to lstview
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.Columns.Add("Mã Sản Phẩm", 90);
                listView1.Columns.Add("Tên Sản Phẩm", 190);
                listView1.Columns.Add("Trọng Lượng", 190);
                listView1.Columns.Add("Quy Cách", 190);

                listView1.Columns.Add("Giá Bán Lẻ", 90);
                listView1.Columns.Add("Giá Đại Lý", 90);
                listView1.Columns.Add("Giá Cửa Hàng", 90);


                listView1.Columns.Add("MFG", 90);
                listView1.Columns.Add("EXP", 90);
                listView1.Columns.Add("Số Lượng Nhập", 90);
                listView1.Columns.Add("Nhà Cung Cấp", 190);
                listView1.Columns.Add("Phân Loại", 190);
                listView1.Columns.Add("Kích Thước", 190);
                listView1.Columns.Add("Số Lượng Nhập Tối Thiểu", 90);
                listView1.Columns.Add("Ghi Chú", 190);

                string sql2 = @"SELECT  [ID] ,[Descrip]	,[weight] ,[specification],[Price],[PriceAgency],[PriceStore] ,[MFG],[EXP] ,[Stock],[Manufacturer],[Type],[Size],[CritLimit] ,[Note]  FROM [Database].[dbo].[tblProduct]";
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
                    lst.SubItems.Add(dr[7].ToString());
                    lst.SubItems.Add(dr[8].ToString());
                    lst.SubItems.Add(dr[9].ToString());
                    lst.SubItems.Add(dr[10].ToString());
                    lst.SubItems.Add(dr[11].ToString());
                    lst.SubItems.Add(dr[12].ToString());
                    lst.SubItems.Add(dr[13].ToString());
                    lst.SubItems.Add(dr[14].ToString());
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
            txtIDCode.Text = "GOD" + result;



        }

        public void generateOrderID()
        {

            var chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            txtOrderID.Text = "OrXHC" + result;



        }
        public void CLear()
        {
            //  txtIDCode.Text = "";
            txtName.Text = "";
            txtPriceStore.Text = "";
            cboType.Text = "-SELECT-";
            cboSize.Text = "-SELECT-";
            txtBrand.Text = "";
            txtStock.Text = "";


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTempID.Text = listView1.FocusedItem.Text;
            lblTempName.Text = listView1.FocusedItem.SubItems[2].Text;
            txtName.Text = listView1.FocusedItem.SubItems[1].Text;


            txtPriceAgency.Text = listView1.FocusedItem.SubItems[5].Text;
            txtPriceAgency.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceAgency.Text));
            txtPriceStore.Text = listView1.FocusedItem.SubItems[6].Text;
            txtPriceStore.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceStore.Text));
            txtPriceRetail.Text = listView1.FocusedItem.SubItems[4].Text;
            txtPriceRetail.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceRetail.Text));
            txtWeight.Text = listView1.FocusedItem.SubItems[2].Text;
            txtSpecification.Text = listView1.FocusedItem.SubItems[3].Text;
            txtStock.Text = listView1.FocusedItem.SubItems[9].Text;
            dateMFG.Text = listView1.FocusedItem.SubItems[7].Text;
            dateEXP.Text = listView1.FocusedItem.SubItems[8].Text;
            cboType.SelectedIndex = cboType.FindString(listView1.FocusedItem.SubItems[11].Text);
            cboSize.SelectedIndex = cboSize.FindString(listView1.FocusedItem.SubItems[12].Text);
            cboManufac.SelectedIndex = cboManufac.FindString(listView1.FocusedItem.SubItems[10].Text);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FrmAdminMenu frmAM = new FrmAdminMenu();
            frmAM.Show();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
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
                generateOrderID();

            }
        }
        public void DeleteAll()
        {

            try
            {

                // listView1.FocusedItem.Remove();
                string del = "DELETE from tblProduct ";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Successfully Deleted!");
                getData();
                Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public void Clear()
        {

            // txtOrderID.Text = "";
            // txtIDCode.Text = "";
            txtName.Text = "";
            txtPriceStore.Text = "";
            txtPriceAgency.Text = "";
            txtStock.Text = "";
            txtDeliveryDate.Text = "";
            txtBrand.Text = "";

            lblTempID.Text = "";

        }
        public void AllDelTrail()
        {

            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                cm.Parameters.AddWithValue("@Transactype", "Deletion");
                cm.Parameters.AddWithValue("@Description", "All Items from orders were REMOVED!");
                cm.Parameters.AddWithValue("@Authority", "Admin");

                cm.ExecuteNonQuery();

            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmAddManufac frmManufac = new frmAddManufac();
            frmManufac.ShowDialog();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtPriceStore.Text == "" || txtPriceAgency.Text == "" || txtPriceRetail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin cần thiết về giá thành sản phẩm!!!.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    string sql = @"INSERT INTO [dbo].[tblProduct]([ID],[Descrip],[weight],[specification],[Price],[PriceAgency],[PriceStore],[MFG],[EXP],[Stock],[Manufacturer],[Type],[Size],[CritLimit],[Note]) VALUES(@ID,@Descrip,@weight,@specification,@Price,@PriceAgency,@PriceStore,@MFG,@EXP,@Stock,@Manufacturer,@Type,@Size,@CritLimit,@Note)";
                    cm = new SqlCommand(sql, cn);
                    cm.Parameters.AddWithValue("@ID", txtIDCode.Text);
                    cm.Parameters.AddWithValue("@Descrip", txtName.Text);
                    cm.Parameters.AddWithValue("@weight", txtWeight.Text);
                    cm.Parameters.AddWithValue("@specification", txtSpecification.Text);
                    cm.Parameters.AddWithValue("@Price", System.Convert.ToDecimal(txtPriceRetail.Text));
                    cm.Parameters.AddWithValue("@PriceAgency", System.Convert.ToDecimal(txtPriceAgency.Text));
                    cm.Parameters.AddWithValue("@PriceStore", System.Convert.ToDecimal(txtPriceStore.Text));
                    cm.Parameters.AddWithValue("@MFG", dateMFG.Value);
                    cm.Parameters.AddWithValue("@EXP", dateEXP.Value);
                    cm.Parameters.AddWithValue("@Stock", System.Convert.ToDecimal(txtStock.Text));
                    cm.Parameters.AddWithValue("@Manufacturer", cboManufac.Text);
                    cm.Parameters.AddWithValue("@Type", cboType.Text);
                    cm.Parameters.AddWithValue("@Size", cboSize.Text);
                    cm.Parameters.AddWithValue("@CritLimit", txtCritLimit.Text);
                    cm.Parameters.AddWithValue("@Note", txtSpecification.Text);
                    cm.ExecuteNonQuery();
                    MessageBox.Show("Thêm mới thành công!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InsertTrail();
                    this.Clear();
                    cboType.SelectedIndex = 0;
                    cboSize.SelectedIndex = 0;
                    cboManufac.SelectedIndex = 0;
                    txtCritLimit.Text = "";
                    txtPriceRetail.Text = "";
                    txtSpecification.Text = "";
                    txtStock.Text = "0";
                    txtWeight.Text = "";
                    getData();
                    generateID();
                    generateOrderID();
                }
                catch (SqlException l)
                {
                    MessageBox.Show("Re-input again. ID may already be taken!");
                    MessageBox.Show(l.Message);
                }

            }
            txtName.Focus();
        }
        public void InsertTrail()
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                cm.Parameters.AddWithValue("@Transactype", "Insertion");
                cm.Parameters.AddWithValue("@Description", "Order:" + txtOrderID.Text + " has been sent to orders!");
                cm.Parameters.AddWithValue("@Authority", "Admin");


                cm.ExecuteNonQuery();
                //   MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }
        }
        public void DeleteTrail()
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                cm.Parameters.AddWithValue("@Transactype", "Deletion");
                cm.Parameters.AddWithValue("@Description", "Item: " + lblTempName.Text + " has been removed from orders!");
                cm.Parameters.AddWithValue("@Authority", "Admin");


                cm.ExecuteNonQuery();
                //   MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }


        }

        private void btnRemove_Click(object sender, EventArgs e)
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
            txtPriceRetail.Text = "";
            txtSpecification.Text = "";
            txtStock.Text = "";
            txtWeight.Text = "";
            txtName.Focus();
        }
        public void deleteRecords()
        {
            try
            {

                //   listView1.FocusedItem.Remove();
                string del = "DELETE from tblProduct where ID='" + lblTempID.Text + "'";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                Clear();
                getData();
                generateID();
                generateOrderID();

            }
            catch (Exception)
            {
                MessageBox.Show("Không có sản phẩm nào được xóa", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void dateEXP_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPriceAgency_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceAgency.Text))
            {
                txtPriceAgency.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceAgency.Text));
            }
            //txtPriceAgency.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceAgency.Text));
        }

        private void txtPriceAgency_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtPriceAgency_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPriceStore_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPriceRetail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPriceStore_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceStore.Text))
            {
                txtPriceStore.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceStore.Text));
            }
        }

        private void txtPriceRetail_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceRetail.Text))
            {
                txtPriceRetail.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceRetail.Text));
            }
        }
    }
}
