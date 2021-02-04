using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace skelot
{
    public partial class FrmUpdate : Form
    {
        SqlCommand cm;
        SqlConnection cn;
        SqlDataReader dr;
        ListViewItem lst;
        //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Data.accdb";
        frmLogin login = new frmLogin();


        public FrmUpdate()
        {

            InitializeComponent();
            cn = new SqlConnection(login.connection);
            cn.Open();
        }

        public void Clear() 
        {
            txtSearch.Text = "";
            txtProductID.Text = "";
            txtPriceA.Text = "";
            txtProductName.Text = "";
            txtPriceS.Text = "";
            txtPriceR.Text = "";
        }
        public void clearText()
        {
            txtSearch.Text = "";
            txtProductID.Text = "";
            txtPriceA.Text = "";
            txtProductName.Text = "";
            txtPriceS.Text = "";
            txtPriceR.Text = "";

        }
        public void getData2()
        {
            try
            {

                listView1.Items.Clear();
                listView1.Columns.Clear();

                listView1.Columns.Add("Barcode", 90);
                listView1.Columns.Add("Tên Sản Phẩm", 210);
                listView1.Columns.Add("Giá Đại Lý", 80);
                listView1.Columns.Add("Giá Cửa Hàng", 80);
                listView1.Columns.Add("Giá Bán Lẻ", 80);
                listView1.Columns.Add("EXP", 80);
                listView1.Columns.Add("Số Lượng Nhập Kho", 80);



                string sql = @"Select [ID],[Descrip],[PriceAgency],[PriceStore],[Price],[EXP],[Stock] from tblProduct where [Descrip] like N'" + txtSearch.Text + "%' ";
                cm = new SqlCommand(sql, cn);
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
     
        public void getData()
        {
            try
            {

                listView1.Items.Clear();
                listView1.Columns.Clear();

                listView1.Columns.Add("Barcode", 90);
                listView1.Columns.Add("Tên Sản Phẩm", 210);
                listView1.Columns.Add("Giá Đại Lý", 80);
                listView1.Columns.Add("Giá Cửa Hàng", 80);
                listView1.Columns.Add("Giá Bán Lẻ", 80);
                listView1.Columns.Add("EXP", 80);
                listView1.Columns.Add("Số Lượng Nhập Kho", 80);



                string sql = @"Select [ID],[Descrip],[PriceAgency],[PriceStore],[Price],[EXP],[Stock] from tblProduct";
                cm = new SqlCommand(sql, cn);
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
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmAdminMenu frm7 = new FrmAdminMenu();
            frm7.Show();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
          getData();
          timer1.Start();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void UpdateTrail() 
        {


            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                cm.Parameters.AddWithValue("@Transactype", "Updation");
                cm.Parameters.AddWithValue("@Description", "Item: '" + txtProductName.Text + "' was UPDATED!");
                cm.Parameters.AddWithValue("@Authority", "Admin");

                cm.ExecuteNonQuery();
            
            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again.!");
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
                cm.Parameters.AddWithValue("Transactype", "Deletion");
                cm.Parameters.AddWithValue("@Description", "Item: '" + txtProductName.Text + "' was DELETED from inventory!");
                cm.Parameters.AddWithValue("@Authority", "Admin");


                cm.ExecuteNonQuery();
           


            }
            catch (SqlException l)
            {
                MessageBox.Show("Vui lòng nhập thông tin lại.!");
                MessageBox.Show(l.Message);
            }
  
        }
       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtPriceA.Text == "" && txtPriceS.Text == "" && txtPriceR.Text == "")
            {
                return;
            }
          try
            {
                    if(txtProductID.Text == ""){

                        MessageBox.Show("Chọn thông tin sản phẩn trong danh sách trên!");
                        return;
                    }
               string up = @"UPDATE tblProduct SET [EXP]=convert(date,'" + dateEXP.Text + "', 103),[Stock]=" + System.Convert.ToDecimal(txtStock.Text) + ",[Price]=" + System.Convert.ToDecimal(txtPriceR.Text) + ",[PriceAgency]=" + System.Convert.ToDecimal(txtPriceA.Text) + ",[PriceStore]=" + System.Convert.ToDecimal(txtPriceS.Text) + " where [ID]='" + txtProductID.Text + "'";
                cm = new SqlCommand(up, cn);             
               
                cm.Parameters.AddWithValue("@Price", txtPriceA.Text);
          
                  cm.ExecuteNonQuery();
                  
                  UpdateTrail();
                  Clear();
                  getData();

                    MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Không có mặt hàng đễ cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            setFocusText();
            }
        public void setFocusText()
        {
            txtPriceA.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
              if (txtProductID.Text == "")
            {
                MessageBox.Show("Không thể xóa nếu không chọn mặt hàng", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa thông tin mặt hàng này không?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DeleteTrail();
                    deleteRecords();                   
                }
            }
            setFocusText();
        }
        public void deleteRecords()
        {
            try
            {
                listView1.FocusedItem.Remove();
                string del = "DELETE from tblProduct where ID='" + txtProductID.Text + "'";
                cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                Clear();

            }
            catch (Exception)
            {
                MessageBox.Show("Không có mặt hàng để xóa!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            setFocusText();
        }
   

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProductID.Text = listView1.FocusedItem.Text;
            txtProductName.Text = listView1.FocusedItem.SubItems[1].Text;

            txtPriceA.Text = string.Format("{0:#,##0.00}", double.Parse(listView1.FocusedItem.SubItems[2].Text));
            txtPriceS.Text = string.Format("{0:#,##0.00}", double.Parse(listView1.FocusedItem.SubItems[3].Text));
            txtPriceR.Text = string.Format("{0:#,##0.00}", double.Parse(listView1.FocusedItem.SubItems[4].Text));
            txtStock.Text = listView1.FocusedItem.SubItems[6].Text;

            dateEXP.Text = listView1.FocusedItem.SubItems[5].Text;
            txtPriceA.Focus();
          
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            getData2();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;

            lblDate.Text = time.ToString();
        }

        private void txtPriceA_Validated(object sender, EventArgs e)
        {

        }

        private void txtPriceS_Validated(object sender, EventArgs e)
        {

        }

        private void txtPriceR_Validated(object sender, EventArgs e)
        {

        }

        private void txtPriceR_Validated_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceR.Text))
            {
                txtPriceR.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceR.Text));
            }
        }

        private void txtPriceS_Validated_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceS.Text))
            {
                txtPriceS.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceS.Text));
            }
        }

        private void txtPriceA_Validated_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceA.Text))
            {
                txtPriceA.Text = string.Format("{0:#,##0.00}", double.Parse(txtPriceA.Text));
            }
        }
    }
    }
