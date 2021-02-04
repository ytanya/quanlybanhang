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
    public partial class frmUpdateUnitMeasure : Form
    {
        SqlCommand cm;
        SqlConnection cn;
        SqlDataReader dr;

        ListViewItem lst;


        frmLogin login = new frmLogin();

        public frmUpdateUnitMeasure()
        {
            InitializeComponent();
            cn = new SqlConnection(login.connection);
            cn.Open();
            //timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //DateTime time = DateTime.Now;
            //string format = "dd/MM/yyyy";
            //lblTimer.Text = time.ToString(format);
            //lblDate.Text = time.ToString();
        }

        private void frmUpdateUnitMeasure_Load(object sender, EventArgs e)
        {
            this.getDataListProduct();
            getDataListConversion();
            generateID();
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

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void getDataListProductByName(string name)
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

                string sql2 = @"SELECT [ID],[Descrip],[specification] ,[PriceAgency] ,[PriceStore],[Price],[weight],[Note] from tblProduct where Descrip like '"+ name +"%'";
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
        public void getDataListConversion()
        {
            //displaying data from Database to lstview
            try
            {
                lstUnit.Items.Clear();
                lstUnit.Columns.Clear();
                lstUnit.Columns.Add("Mã Chuyển Đỗi", 100);
                lstUnit.Columns.Add("Đơn Vị 1", 100);
                lstUnit.Columns.Add("Chuyển Đỗi Từ", 100);
                lstUnit.Columns.Add("Đơn Vị 2", 100);
                lstUnit.Columns.Add("Chuyển Đỗi Thành", 100);
                lstUnit.Columns.Add("Quy Cách", 150);
                lstUnit.Columns.Add("Ghi Chú", 150);

                string sql2 = @"SELECT [ID],[fromCon],[fromNum],[toCon],[toNum],[spec],[note] FROM tblConversion";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = lstUnit.Items.Add(dr[0].ToString());
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
        public void getDataListConversionByfromCon(string fromCon)
        {
            //displaying data from Database to lstview
            try
            {
                lstUnit.Items.Clear();
                lstUnit.Columns.Clear();
                lstUnit.Columns.Add("Mã Chuyển Đỗi", 100);
                lstUnit.Columns.Add("Đơn Vị 1", 100);
                lstUnit.Columns.Add("Chuyển Đỗi Từ", 100);
                lstUnit.Columns.Add("Đơn Vị 2", 100);
                lstUnit.Columns.Add("Chuyển Đỗi Thành", 100);
                lstUnit.Columns.Add("Quy Cách", 150);
                lstUnit.Columns.Add("Ghi Chú", 150);

                string sql2 = @"SELECT [ID],[fromCon],[fromNum],[toCon],[toNum],[spec],[note] FROM tblConversion where fromCon like '" + fromCon + "%'";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = lstUnit.Items.Add(dr[0].ToString());
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
        public void generateID()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            txtIDCode.Text = "UNP" + result;
        }

        private void listProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIDProdcut.Text = listProduct.FocusedItem.Text;
            getDataUnitConverByProduct(txtIDProdcut.Text);
        }
        public void DeleteTrail()
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", DateTime.Now);
                cm.Parameters.AddWithValue("@Transactype", "Deletion");
                cm.Parameters.AddWithValue("@Description", "Item: " + txtMaNhap.Text + " đã được xóa!");
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
        private void btnAddtoCart_Click(object sender, EventArgs e)
        {
            if (txtIDProdcut.Text == "" || txtMaConversion.Text == "")
            {
                MessageBox.Show("Vui lòng chọ thông tin trên danh sách!.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    string sql = @"INSERT INTO [tblUnitConversion] ([ID],[IDProduct],[IDUnit],[dateApply],[dateCreate],[IDUser],[keyexist]) ";
                    sql = sql + " VALUES (@ID,@IDProduct,@IDUnit,@dateApply,@dateCreate,@IDUser,@keyexist)";
                    cm = new SqlCommand(sql, cn);
                    cm.Parameters.AddWithValue("@ID", txtIDCode.Text);
                    cm.Parameters.AddWithValue("@IDProduct", txtIDProdcut.Text);
                    cm.Parameters.AddWithValue("@IDUnit", txtMaConversion.Text);
                    cm.Parameters.AddWithValue("@dateApply", DateTime.Now);
                    cm.Parameters.AddWithValue("@dateCreate", DateTime.Now);
                    cm.Parameters.AddWithValue("@IDUser", login.getUserID());
                    cm.Parameters.AddWithValue("@keyexist", txtIDProdcut.Text + txtMaConversion.Text);
                    cm.ExecuteNonQuery();

                    MessageBox.Show("Đã lưu thành công!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InsertTrail();
                    this.Clear();
                    getDataUnitConverByProduct(txtIDProdcut.Text);
                    generateID();
                }
                catch (SqlException l)
                {
                    MessageBox.Show("Vui lòng nhập lại thông tin. ID đã bị trùng lặp!");
                    MessageBox.Show(l.Message);
                }

            }
        }
        public bool checkEx(string idProduct, string idUnit)
        {
            bool flag = false;
            try
            {

                string sql2 = @"SELECT COUNT(ID)   FROM[Database].[dbo].[tblUnitConversion]   ";
                
                sql2 = sql2 + " WHERE IDProduct = '" + idProduct + "' AND IDUnit = '" + idUnit + "'";

                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();

                dr.Read();
                if (dr.FieldCount > 0)
                {
                    flag= true;
                }
                else
                {
                    flag= false;
                }

                dr.Close();
                return flag;

            }
            catch (Exception ex)
            {
                return true;
            }
            
            
        }
        public void getDataUnitConverByProduct(string idProduct)
        {
            //displaying data from Database to lstview
            try
            {
                listUnitProduct.Items.Clear();
                listUnitProduct.Columns.Clear();
                listUnitProduct.Columns.Add("Mã Chuyển Đỗi", 100);
                //listUnitProduct.Columns.Add("Mã Sản Phẩm", 0);
                //listUnitProduct.Columns.Add("Mã Chuyển đỗi", 0);
                listUnitProduct.Columns.Add("Tên Sản Phẩm", 100);
                listUnitProduct.Columns.Add("Quy Cách", 100);
                listUnitProduct.Columns.Add("Đơn Vị 1", 100);
                listUnitProduct.Columns.Add("Chuyển Đỗi Từ", 100);
                listUnitProduct.Columns.Add("Đơn Vị 2", 100);
                listUnitProduct.Columns.Add("Chuyển Đỗi Thành", 100);

                string sql2 = @"SELECT        tblUnitConversion.ID, tblUnitConversion.IDProduct, tblUnitConversion.IDUnit, tblProduct.Descrip, tblProduct.specification, tblConversion.fromCon, tblConversion.fromNum, tblConversion.toCon, tblConversion.toNum";
                sql2 = sql2 + " FROM            tblUnitConversion INNER JOIN tblProduct ON tblUnitConversion.IDProduct = tblProduct.ID INNER JOIN tblConversion ON tblUnitConversion.IDUnit = tblConversion.ID";
                sql2 = sql2 + " where  tblUnitConversion.IDProduct ='" + idProduct +"'";

                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listUnitProduct.Items.Add(dr[0].ToString());
                    //lst.SubItems.Add(dr[1].ToString());
                    //lst.SubItems.Add(dr[2].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[4].ToString());
                    lst.SubItems.Add(dr[5].ToString());
                    lst.SubItems.Add(dr[6].ToString());
                    lst.SubItems.Add(dr[7].ToString());
                    lst.SubItems.Add(dr[8].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void InsertTrail()
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", DateTime.Now);
                cm.Parameters.AddWithValue("@Transactype", "Insertion");
                cm.Parameters.AddWithValue("@Description", "Unite :" + txtIDCode.Text + " đã được tạo ra!");
                cm.Parameters.AddWithValue("@Authority", login.getUserID());
                cm.ExecuteNonQuery();

            }
            catch (SqlException l)
            {
                MessageBox.Show(l.Message);
            }
        }
        public void Clear()
        {
            txtMaNhap.Text = "";
            txtSearch.Focus();
        }
        private void lstUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaConversion.Text = lstUnit.FocusedItem.Text;
        }
        public void deleteRecords()
        {
            try
            {

                //   listView1.FocusedItem.Remove();
                string del = "DELETE from tblUnitConversion where ID='" + txtMaNhap.Text + "'";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                Clear();
                getDataUnitConverByProduct(txtIDProdcut.Text);
                generateID();
            }
            catch (Exception)
            {
                MessageBox.Show("Không có sản phẩm nào được xóa", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        private void btnDelOR_Click(object sender, EventArgs e)
        {
            if (listUnitProduct.Items.Count == 0 || txtMaNhap.Text == "")
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
            txtSearch.Focus();
        }

        private void listUnitProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaNhap.Text = listUnitProduct.FocusedItem.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FrmAdminMenu frmAM = new FrmAdminMenu();
            frmAM.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            getDataListProductByName(txtSearch.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            getDataListConversionByfromCon(textBox1.Text);
        }
    }
}
