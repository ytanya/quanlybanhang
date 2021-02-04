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
    public partial class frmInsertProductStore : Form
    {
        SqlCommand cm;
        SqlConnection cn;
        SqlDataReader dr;

        ListViewItem lst;
        ListViewItem lstStore;

        frmLogin login = new frmLogin();


        public frmInsertProductStore()
        {
            InitializeComponent();

            cn = new SqlConnection(login.connection);
            cn.Open();
            dateEXP.Value = DateTime.Now.AddMonths(6);
            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmInsertProductStore_Load(object sender, EventArgs e)
        {
            this.getDataListProduct();
            this.getDataListProductInStoreByDate(dateInsertView.Value);
            generateID();
            addCombox();
        }
        public void getDataListProduct()
        {
            //displaying data from Database to lstview
            try
            {
                listProduct.Items.Clear();
                listProduct.Columns.Clear();
                listProduct.Columns.Add("Mã Sản Phẩm", 100);
                listProduct.Columns.Add("Tên Sản Phẩm", 210);

                listProduct.Columns.Add("Quy Cách", 150);
                listProduct.Columns.Add("Giá Đại Lý", 80);
                listProduct.Columns.Add("Giá Cửa Hàng", 80);
                listProduct.Columns.Add("Giá bán Lẻ", 80);
                listProduct.Columns.Add("Trọng lượng", 80);
                listProduct.Columns.Add("Ghi Chú", 100);

                string sql2 = @"SELECT [ID],[Descrip],[specification] ,[PriceAgency] ,[PriceStore],[Price],[weight],[Note] from tblProduct";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listProduct.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[4].ToString());
                    lst.SubItems.Add(dr[5].ToString());
                    lst.SubItems.Add(dr[6].ToString());
                    lst.SubItems.Add(dr[7].ToString());
                    //if (Convert.ToInt32(dr[6].ToString()) == 0)
                    //{

                    //    lst.ForeColor = Color.Crimson;


                    //}
                    //else if (Convert.ToInt32(dr[6].ToString()) <= Convert.ToInt32(dr[9].ToString()))
                    //{
                    //    lst.ForeColor = Color.Orange;
                    //}
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void getDataListProductByName(string productName)
        {
            //displaying data from Database to lstview
            try
            {
                listProduct.Items.Clear();
                listProduct.Columns.Clear();
                listProduct.Columns.Add("Mã Sản Phẩm", 100);
                listProduct.Columns.Add("Tên Sản Phẩm", 210);

                listProduct.Columns.Add("Quy Cách", 150);
                listProduct.Columns.Add("Giá Đại Lý", 80);
                listProduct.Columns.Add("Giá Cửa Hàng", 80);
                listProduct.Columns.Add("Giá bán Lẻ", 80);
                listProduct.Columns.Add("Trọng lượng", 80);
                listProduct.Columns.Add("Ghi Chú", 100);

                string sql2 = @"SELECT [ID],[Descrip],[specification] ,[PriceAgency] ,[PriceStore],[Price],[weight],[Note] from tblProduct where Descrip like '" + productName + "%'";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listProduct.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[4].ToString());
                    lst.SubItems.Add(dr[5].ToString());
                    lst.SubItems.Add(dr[6].ToString());
                    lst.SubItems.Add(dr[7].ToString());
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
            string format = "dd/MM/yyyy";
            lblTimer.Text = time.ToString(format);
            lblDate.Text = time.ToString();
        }

        private void listProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIDProduct.Text = listProduct.FocusedItem.Text;
            txtNameProduct.Text = listProduct.FocusedItem.SubItems[1].Text;
            txtSpect.Text = listProduct.FocusedItem.SubItems[2].Text;
            txtPriceA.Text = string.Format("{0:#,##0.00}", double.Parse(listProduct.FocusedItem.SubItems[3].Text));
            txtPriceS.Text = string.Format("{0:#,##0.00}", double.Parse(listProduct.FocusedItem.SubItems[4].Text));
            txtPriceR.Text = string.Format("{0:#,##0.00}", double.Parse(listProduct.FocusedItem.SubItems[5].Text));
            dateEXP.Value = DateTime.Now.AddMonths(6);
            txtStockIn.Text = "0";
            txtNote.Text = "";
        }
        public void addCombox()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Name", typeof(string));

            try
            {

                string sql2 = @"SELECT [ID],[Name] from tblStore ORDER BY Name";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = dr[0].ToString();
                    row["Name"] = dr[1].ToString(); 
                    dt.Rows.Add(row);
                }
                cboStore.DataSource = dt;
                cboStore.DisplayMember = "Name";
                cboStore.ValueMember = "ID";
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FrmAdminMenu frmAM = new FrmAdminMenu();
            frmAM.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAddStore frmAS = new frmAddStore();
            frmAS.Show();
        }

        private void txtThang_TextChanged(object sender, EventArgs e)
        { 
            if (txtThang.Text != "")
            {
                dateEXP.Value = DateTime.Now.AddMonths(Int32.Parse(txtThang.Text));
            }
        }

        private void txtPriceA_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceA.Text))
            {
                txtPriceA.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceA.Text));
            }
        }

        private void txtPriceS_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceS.Text))
            {
                txtPriceS.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceS.Text));
            }
        }

        private void txtPrieR_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceR.Text))
            {
                txtPriceR.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceR.Text));
            }
        }

        private void txtStockIn_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStockIn.Text))
            {
                txtStockIn.Text = string.Format("{0:#,##0.00}", double.Parse(txtStockIn.Text));
            }
        }

        private void label6_TextChanged(object sender, EventArgs e)
        {
            getDataListProductByName(txtSearch.Text);
        }

        private void dateInsertView_ValueChanged(object sender, EventArgs e)
        {
            getDataListProductInStoreByDate(dateInsertView.Value);
        }
        public void getDataListProductInStoreByDate(DateTime insertDate)
        {
            //displaying data from Database to lstview
            try
            {
                listInStore.Items.Clear();
                listInStore.Columns.Clear();
                listInStore.Columns.Add("Mã Nhập Hàng", 0);
                listInStore.Columns.Add("Mã Hàng Nhập", 100);
                listInStore.Columns.Add("Tên Sản Phẩm", 150);
                listInStore.Columns.Add("Quy Cách", 100);
                listInStore.Columns.Add("Giá Đại Lý", 100);
                listInStore.Columns.Add("Giá Cửa Hàng", 100);
                listInStore.Columns.Add("Giá Bán Lẻ", 100);
                listInStore.Columns.Add("Số Lượng NK", 100);
                listInStore.Columns.Add("Ngày Hết Hạng", 100);
                listInStore.Columns.Add("Ghi Chú", 200);
                listInStore.Columns.Add("IDStore", 0);
                listInStore.Columns.Add("Tên Kho Nhập", 0);
                listInStore.Columns.Add("Ngày Nhập Mặt Hàng", 0);

                string sql2 = @"SELECT        InStore.ID, InStore.IDStore, tblStore.Name, InStore.IDProduc, tblProduct.Descrip, tblProduct.specification, InStore.PriceA, InStore.PriceS, InStore.PriceR, InStore.DateEXP, InStore.Stock, InStore.Note, InStore.InsertDate,tblStore.ID";
                sql2 = sql2 + " FROM            InStore INNER JOIN";
                sql2 = sql2 + "                 tblProduct ON InStore.IDProduc = tblProduct.ID INNER JOIN";
                sql2 = sql2 + "    tblStore ON InStore.IDStore = tblStore.ID";
                sql2 = sql2 + " WHERE        (InStore.InsertDate = '" + Convert.ToDateTime(insertDate).ToString("yyyy-MM-dd")  + "')";

                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lstStore = listInStore.Items.Add(dr[0].ToString());
                    lstStore.SubItems.Add(dr[3].ToString());
                    lstStore.SubItems.Add(dr[4].ToString());
                    lstStore.SubItems.Add(dr[5].ToString());
                    lstStore.SubItems.Add(dr[6].ToString());
                    lstStore.SubItems.Add(dr[7].ToString());
                    lstStore.SubItems.Add(dr[8].ToString());
                    lstStore.SubItems.Add(dr[10].ToString());
                    lstStore.SubItems.Add(dr[9].ToString());
                    lstStore.SubItems.Add(dr[11].ToString());
                    lstStore.SubItems.Add(dr[13].ToString());
                    lstStore.SubItems.Add(dr[2].ToString());
                    lstStore.SubItems.Add(dr[12].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void getDataListProductInStoreByDateAndName(DateTime insertDate,string productName)
        {
            //displaying data from Database to lstview
            try
            {
                listInStore.Items.Clear();
                listInStore.Columns.Clear();
                listInStore.Columns.Add("Mã Nhập Hàng", 0);
                listInStore.Columns.Add("Mã Hàng Nhập", 100);
                listInStore.Columns.Add("Tên Sản Phẩm", 150);
                listInStore.Columns.Add("Quy Cách", 100);
                listInStore.Columns.Add("Giá Đại Lý", 100);
                listInStore.Columns.Add("Giá Cửa Hàng", 100);
                listInStore.Columns.Add("Giá Bán Lẻ", 100);
                listInStore.Columns.Add("Số Lượng NK", 100);
                listInStore.Columns.Add("Ngày Hết Hạng", 100);
                listInStore.Columns.Add("Ghi Chú", 200);
                listInStore.Columns.Add("IDStore", 0);
                listInStore.Columns.Add("Tên Kho Nhập", 0);
                listInStore.Columns.Add("Ngày Nhập Mặt Hàng", 0);

                string sql2 = @"SELECT        InStore.ID, InStore.IDStore, tblStore.Name, InStore.IDProduc, tblProduct.Descrip, tblProduct.specification, InStore.PriceA, InStore.PriceS, InStore.PriceR, InStore.DateEXP, InStore.Stock, InStore.Note, InStore.InsertDate,tblStore.ID";
                sql2 = sql2 + " FROM            InStore INNER JOIN";
                sql2 = sql2 + "                 tblProduct ON InStore.IDProduc = tblProduct.ID INNER JOIN";
                sql2 = sql2 + "    tblStore ON InStore.IDStore = tblStore.ID";
                sql2 = sql2 + " WHERE        (InStore.InsertDate = '" + Convert.ToDateTime(insertDate).ToString("yyyy-MM-dd") + "')";
                sql2 = sql2 + "   and tblProduct.Descrip Like '" + productName + "%'";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lstStore = listInStore.Items.Add(dr[0].ToString());
                    lstStore.SubItems.Add(dr[3].ToString());
                    lstStore.SubItems.Add(dr[4].ToString());
                    lstStore.SubItems.Add(dr[5].ToString());
                    lstStore.SubItems.Add(dr[6].ToString());
                    lstStore.SubItems.Add(dr[7].ToString());
                    lstStore.SubItems.Add(dr[8].ToString());
                    lstStore.SubItems.Add(dr[10].ToString());
                    lstStore.SubItems.Add(dr[9].ToString());
                    lstStore.SubItems.Add(dr[11].ToString());
                    lstStore.SubItems.Add(dr[13].ToString());
                    lstStore.SubItems.Add(dr[2].ToString());
                    lstStore.SubItems.Add(dr[12].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddtoCart_Click(object sender, EventArgs e)
        {
            if (txtIDProduct.Text == "")
            {
                MessageBox.Show("Vui lòng chọn mặt hàng.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    string sql = @"INSERT INTO [dbo].[InStore] ([ID],[IDStore],[IDProduc],[PriceA],[PriceS],[PriceR],[DateEXP],[Stock],[Note],[InsertDate],[CreateDate],[UserID]) ";
                            sql = sql + " VALUES(@ID,@IDStore,@IDProduc,@PriceA,@PriceS,@PriceR,@DateEXP,@Stock,@Note, @InsertDate,@CreateDate,@UserID)";

                    cm = new SqlCommand(sql, cn);
                    cm.Parameters.AddWithValue("@ID", txtIDCode.Text);
                    cm.Parameters.AddWithValue("@IDStore", cboStore.SelectedValue);
                    cm.Parameters.AddWithValue("@IDProduc", txtIDProduct.Text);
                    cm.Parameters.AddWithValue("@PriceA", double.Parse(txtPriceA.Text));
                    cm.Parameters.AddWithValue("@PriceS",  double.Parse(txtPriceS.Text));
                    cm.Parameters.AddWithValue("@PriceR", double.Parse(txtPriceR.Text));
                    cm.Parameters.AddWithValue("@DateEXP", dateEXP.Value);
                    cm.Parameters.AddWithValue("@Stock", string.Format("{0:#,##0.00}", double.Parse(txtStockIn.Text)));
                    cm.Parameters.AddWithValue("@Note", txtNote.Text);
                    cm.Parameters.AddWithValue("@InsertDate", dateInsert.Value);
                    cm.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    cm.Parameters.AddWithValue("@UserID", login.getUserID());

                    cm.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu lại thành công!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    generateID();
                    clearText();
                }
                catch (SqlException l)
                {
                    MessageBox.Show("Vui lòng nhập lại thông tin. Mã kho bị trùng lặp!");
                    //MessageBox.Show(l.Message);
                }

            }
            getDataListProductInStoreByDate(dateInsertView.Value);
            txtPriceA.Focus();
        }

        public void clearText()
        {
            txtIDProduct.Text = "";
            txtNameProduct.Text = "";
            txtSpect.Text = "";
            txtPriceA.Text = "";
            txtPriceS.Text = "";
            txtPriceS.Text = "";
            txtStockIn.Text = "";
            txtNote.Text = "";
            dateInsert.Value = DateTime.Now;
            lblMNH.Text = ".";
            txtThang.Text = "6";
        }
        public void generateID()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            txtIDCode.Text = "INS" + result;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            getDataListProductByName(txtSearch.Text);
        }

        private void listInStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMNH.Text = listInStore.FocusedItem.Text;
            txtIDProduct.Text = listInStore.FocusedItem.SubItems[1].Text;
            txtNameProduct.Text = listInStore.FocusedItem.SubItems[2].Text;
            txtSpect.Text = listInStore.FocusedItem.SubItems[3].Text;
            txtPriceA.Text = string.Format("{0:#,##0.00}", double.Parse(listInStore.FocusedItem.SubItems[4].Text));
            txtPriceS.Text = string.Format("{0:#,##0.00}", double.Parse(listInStore.FocusedItem.SubItems[5].Text));
            txtPriceR.Text = string.Format("{0:#,##0.00}", double.Parse(listInStore.FocusedItem.SubItems[6].Text));
            txtStockIn.Text = string.Format("{0:#,##0.00}", double.Parse(listInStore.FocusedItem.SubItems[7].Text));
            txtNote.Text = listInStore.FocusedItem.SubItems[9].Text;
            dateEXP.Text = listInStore.FocusedItem.SubItems[8].Text;
            dateInsert.Text = listInStore.FocusedItem.SubItems[12].Text;
            cboStore.Text = listInStore.FocusedItem.SubItems[11].Text;
        }

        private void btnDelOR_Click(object sender, EventArgs e)
        {
            if (listInStore.Items.Count == 0 || lblMNH.Text == ".")
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
            txtSearch.Focus();
        }
        public void deleteRecords()
        {
            try
            {

                //   listView1.FocusedItem.Remove();
                string del = "DELETE from InStore where ID='" + lblMNH.Text + "'";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                clearText();
                getDataListProductInStoreByDate(dateInsertView.Value);
                generateID();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Không có sản phẩm nào được xóa", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                cm.Parameters.AddWithValue("@Description", "Item: " + lblMNH.Text + " đã bị xóa từ form nhập hàng!");
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            getDataListProductInStoreByDateAndName(dateInsertView.Value, textBox1.Text);
        }
    }
}
