using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ZXing;
using ClosedXML;
using DocumentFormat.OpenXml;
using Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;
using System.IO;

using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace skelot
{
    public partial class frmProductSearchName : Form
    {
        SqlCommand cm;
        SqlConnection cn;
        SqlDataReader dr;
        //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Data.accdb";

        ListViewItem lst;
        frmLogin login = new frmLogin();


        public frmProductSearchName()
        {
            InitializeComponent();
        }

        private void frmProductSearchName_Load(object sender, EventArgs e)
        {
            checkFolder(Environment.CurrentDirectory + "\\Barcode\\");
            barcode.Focus();
            cn = new SqlConnection(login.connection);
            cn.Open();
            this.getData();
        }

        public void getData()
        {
            //code for Displaying data from Database to lstView.

            try
            {
                listView2.Items.Clear();
                listView2.Columns.Clear();
                listView2.Columns.Add("Barcode", 90);
                listView2.Columns.Add("Tên Sản Phẩm", 190);
                listView2.Columns.Add("Giá bán Lẻ", 90);
                listView2.Columns.Add("Giá Đại Lý", 90);
                listView2.Columns.Add("Giá Cửa Hàng", 80);
                listView2.Columns.Add("Quy Cách", 100);
                listView2.Columns.Add("MFG", 120);
                listView2.Columns.Add("EXP", 120);
                listView2.Columns.Add("Ghi Chú", 190);

                string sql = @"SELECT [ID],[Descrip],[Price],[PriceAgency],[PriceStore],[specification],[MFG],[EXP],[Note]  FROM [dbo].[tblProduct] where Descrip like N'%" + txtSearch.Text + "%'";
                cm = new SqlCommand(sql, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listView2.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
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

        public string getTenSanPhamBarcode(string barcode)
        {
            string returnstring = "";
            string sql = @"Select * from tblProduct where ID like '" + barcode + "%'";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                returnstring = dr[1].ToString();
            }
            dr.Close();
            return returnstring;
        }

        public string getPriceDatabase(string code)
        {
            string returnstring = "";
            string sql = @"Select * from tblProduct where ID like '" + txtBarcode.Text + "%'";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (code == "1") { returnstring = dr[12].ToString(); }
                if (code == "2") { returnstring = dr[13].ToString(); }
                if (code == "3") { returnstring = dr[2].ToString(); }

            }
            dr.Close();
            return returnstring;
        }

        public string getPriceDatabase2(string code, string barcode)
        {
            string returnstring = "";
            string sql = @"Select * from tblProduct where ID like '" + barcode + "%'";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (code == "1") { returnstring = dr[12].ToString(); }
                if (code == "2") { returnstring = dr[13].ToString(); }
                if (code == "3") { returnstring = dr[2].ToString(); }

            }
            dr.Close();
            return returnstring;
        }

        public string getPriceDatabaseName(string code)
        {
            string returnstring = "";
            string sql = @"Select * from tblProduct where Descrip like '" + txtSearch.Text + "%'";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (code == "1") { returnstring = dr[12].ToString(); }
                if (code == "2") { returnstring = dr[13].ToString(); }
                if (code == "3") { returnstring = dr[2].ToString(); }

            }
            dr.Close();
            return returnstring;
        }

        public void getDataBarcode(string barcode)
        {
            //code for Displaying data from Database to lstView.

            try
            {
                listView2.Items.Clear();
                listView2.Columns.Clear();
                listView2.Columns.Add("Barcode", 90);
                listView2.Columns.Add("Tên Sản Phẩm", 190);
                listView2.Columns.Add("Giá bán Lẻ", 90);
                listView2.Columns.Add("Giá Đại Lý", 90);
                listView2.Columns.Add("Giá Cửa Hàng", 80);
                listView2.Columns.Add("Quy Cách", 100);
                listView2.Columns.Add("MFG", 120);
                listView2.Columns.Add("EXP", 120);
                listView2.Columns.Add("Ghi Chú", 190);

                string sql = @"SELECT [ID],[Descrip],[Price],[PriceAgency],[PriceStore],[specification],[MFG],[EXP],[Note]  FROM [dbo].[tblProduct] where ID like '%" + barcode + "%'";
                cm = new SqlCommand(sql, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lst = listView2.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
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

        private void frmProductSearchName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
            {
                lbloaigia.Text = "Giá Đại Lý";
                txtloai.Text = "1";
            }
            if (e.KeyCode == Keys.F2)
            {
                lbloaigia.Text = "Giá Cửa Hàng";
                txtloai.Text = "2";
            }
            if (e.KeyCode == Keys.F3)
            {
                lbloaigia.Text = "Giá Bán Lẻ";
                txtloai.Text = "3";
            }
            if (e.KeyCode == Keys.F12)
            {
                barcode.Focus();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            txttimkiem.Text = "1";
            this.getData();
            lbGia.Text = getPriceDatabaseName(txtSearch.Text);
        }
        public void checkFolder(string folderCheck)
        {
            bool exists = System.IO.Directory.Exists(folderCheck);

            if (!exists)
                System.IO.Directory.CreateDirectory(folderCheck);
        }
        public static void FromListView(System.Data.DataTable table, ListView lvw)
        {
            table.Clear();
            var columns = lvw.Columns.Count;

            foreach (ColumnHeader column in lvw.Columns)
                table.Columns.Add(column.Text);

            foreach (ListViewItem item in lvw.Items)
            {
                var cells = new object[columns];
                for (var i = 0; i < columns; i++)
                    cells[i] = item.SubItems[i].Text;
                table.Rows.Add(cells);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            printBarcodeName();
            System.Data.DataTable table = new System.Data.DataTable();
            FromListView(table, listView2);
           

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter="Excel Workbook|*.xlsx" })
            {
                sfd.InitialDirectory = Environment.CurrentDirectory + "\\Barcode\\";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(table,"Danh Sách Mặt Hàng");
                            var ws = workbook.Worksheet("Danh Sách Mặt Hàng");
                            // thiet lap chieu cao cho tat ca cac row trong file excel
                            ws.Row(1).Height = 15;
                            for (int i = 2; i < listView2.Items.Count +3; i++)
                            {
                                ws.Row(i).Height = 80;
                            }
                            // them barcode vao
                            for (int i = 0; i < listView2.Items.Count ; i++)
                            {
                                ws.AddPicture(Environment.CurrentDirectory + "\\Barcode\\" + listView2.Items[i].Text + ".bmp").MoveTo(ws.Cell(2+i, 10));
                            }
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("Tạo Thành Công!","Messagge",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(sfd.FileName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void printBarcodeName()
        {
            var barcodewriter = new BarcodeWriter();
            BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
            for (int i = 0; i < listView2.Items.Count; i++)
            {    
                writer.Write(listView2.Items[i].Text).Save(Environment.CurrentDirectory + "\\Barcode\\" + listView2.Items[i].Text + ".bmp");
                pictureBox1.Image = writer.Write(listView2.Items[i].Text);
            }
            
        }


        private void btnOkay_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmAdminMenu AdminMenu = new FrmAdminMenu();
            AdminMenu.Show();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            txttimkiem.Text = "2";
            this.getDataBarcode(txtBarcode.Text);
            lbGia.Text = getPriceDatabase(txtloai.Text);
            lbtensanpham.Text = getTenSanPhamBarcode(txtBarcode.Text);
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbtensanpham.Text = listView2.FocusedItem.SubItems[1].Text;
            lbquycach.Text = listView2.FocusedItem.SubItems[5].Text;
            if (txtloai.Text == "1") { lbGia.Text = string.Format("{0:#,##0.00}", double.Parse(listView2.FocusedItem.SubItems[3].Text)); }
            if (txtloai.Text == "2") { lbGia.Text = string.Format("{0:#,##0.00}", double.Parse(listView2.FocusedItem.SubItems[4].Text)); }
            if (txtloai.Text == "3") { lbGia.Text = string.Format("{0:#,##0.00}", double.Parse(listView2.FocusedItem.SubItems[2].Text)); }
        }
    }
}
