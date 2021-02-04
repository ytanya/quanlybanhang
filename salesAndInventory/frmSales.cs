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
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace skelot
{
    public partial class frmSales : Form
    {
        SqlCommand cm;
        SqlConnection cn;
        SqlDataReader dr;

        ListViewItem lst;
        ListViewItem lstStore;
        private static List<Stream> m_streams;
        private static int m_currentPageIndex = 0;
        frmLogin login = new frmLogin();

        public frmSales()
        {
            InitializeComponent();
            cn = new SqlConnection(login.connection);
            cn.Open();
        }

        private void frmSales_Load(object sender, EventArgs e)
        {

            this.getDataListProduct();
            //this.getDataListProductInStoreByDate(dateInsertView.Value);
            generateID();
            generateIDSale();
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
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void addCombox()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Name", typeof(string));

            try
            {

                string sql2 = @"SELECT [ID],[Name] from tblCustomer ORDER BY Name";
                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = dr[0].ToString();
                    row["Name"] = dr[1].ToString();
                    dt.Rows.Add(row);
                }
                cboCustomer.DataSource = dt;
                cboCustomer.DisplayMember = "Name";
                cboCustomer.ValueMember = "ID";
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
            txtIDCode.Text = "SAL" + result;
        }
        public void generateIDSale()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            txtMaSoBanHang.Text = "SLD" + result;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FrmAdminMenu frmAM = new FrmAdminMenu();
            frmAM.Show();
        }

        private void listProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIDProduct.Text = listProduct.FocusedItem.Text;
            txtName.Text = listProduct.FocusedItem.SubItems[1].Text;
            txtSpect.Text = listProduct.FocusedItem.SubItems[2].Text;
            if (txtloaigia.Text == "1") { txtPrice.Text = string.Format("{0:#,##0.00}", double.Parse(listProduct.FocusedItem.SubItems[3].Text)); }
            if (txtloaigia.Text == "2") { txtPrice.Text = string.Format("{0:#,##0.00}", double.Parse(listProduct.FocusedItem.SubItems[4].Text)); }
            if (txtloaigia.Text == "3") { txtPrice.Text = string.Format("{0:#,##0.00}", double.Parse(listProduct.FocusedItem.SubItems[5].Text)); }
            txtNote.Text = "";
        }

        private void rdAgency_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAgency.Checked == true) { txtloaigia.Text = "1"; }
            setPriceAgain();
        }

        private void rdStore_CheckedChanged(object sender, EventArgs e)
        {
            if (rdStore.Checked == true) { txtloaigia.Text = "2";  }
            setPriceAgain();
        }

        private void rdRetail_CheckedChanged(object sender, EventArgs e)
        {
            if (rdRetail.Checked == true) { txtloaigia.Text = "3"; }
            setPriceAgain();
        }
        public void setPriceAgain()
        {
            txtPrice.Text = "";
        }
        public void clearText()
        {
            txtIDProduct.Text = "";
            txtName.Text = "";
            txtSpect.Text = "";
            txtPrice.Text = "";
            txtNumberSalel.Text = "1";
            txtNote.Text = "";
            dateSale.Value = DateTime.Now;
            lblMNH.Text = ".";
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
                    string sql = @"INSERT INTO tblSales ([ID],[IDProduct],[numberSale],[priceSale],[dateSale],[IDCustomer],[note],[discount],[tax],[totalsale],[IDBill],[dateCreate],[dateupdate],[IDUser]) ";
                    sql = sql + " VALUES(@ID,@IDProduct,@numberSale,@priceSale,@dateSale,@IDCustomer,@note,@discount,@tax, @totalsale,@IDBill,@dateCreate,@dateupdate,@IDUser)";

                    cm = new SqlCommand(sql, cn);
                    cm.Parameters.AddWithValue("@ID", txtIDCode.Text);
                    cm.Parameters.AddWithValue("@IDProduct", txtIDProduct.Text);
                    cm.Parameters.AddWithValue("@numberSale", double.Parse(txtNumberSalel.Text));
                    cm.Parameters.AddWithValue("@priceSale", double.Parse(txtPrice.Text));
                    cm.Parameters.AddWithValue("@dateSale", dateSale.Value);
                    cm.Parameters.AddWithValue("@IDCustomer", cboCustomer.SelectedValue);
                    cm.Parameters.AddWithValue("@Note", txtNote.Text);
                    cm.Parameters.AddWithValue("@discount",  double.Parse(txtDiscount.Text));
                    cm.Parameters.AddWithValue("@tax",  double.Parse(txtTax.Text));
                    cm.Parameters.AddWithValue("@totalsale", double.Parse(txtTotal.Text));
                    cm.Parameters.AddWithValue("@IDBill", txtMaSoBanHang.Text);
                    cm.Parameters.AddWithValue("@dateCreate", DateTime.Now);
                    cm.Parameters.AddWithValue("@dateupdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@IDUser", login.getUserID());

                    cm.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu lại thành công!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    generateID();
                    clearText();
                }
                catch (SqlException l)
                {
                    MessageBox.Show("Vui lòng nhập lại thông tin. Mã kho bị trùng lặp!");
                    MessageBox.Show(l.Message);
                }

            }
            getDataListSaleByBill(txtMaSoBanHang.Text);
            txtName.Focus();
        }

        public void getDataListSaleByBill(string IDbill)
        {
            //displaying data from Database to lstview
            try
            {
                listSale.Items.Clear();
                listSale.Columns.Clear();
                listSale.Columns.Add("Mã Bán Hàng", 100);
                listSale.Columns.Add("Mã Sản Phẩm", 100);
                listSale.Columns.Add("Tên Sản Phẩm", 150);
                listSale.Columns.Add("Quy Cách", 100);
                listSale.Columns.Add("Giá Bán", 100);
                listSale.Columns.Add("Số Lượng", 100);
                listSale.Columns.Add("Chiết Khấu", 100);
                listSale.Columns.Add("Thuế", 100);
                listSale.Columns.Add("Thành Tiền", 100);
                listSale.Columns.Add("Ngày Bán", 200);
                listSale.Columns.Add("Mã Bill", 0);

                string sql2 = @"SELECT        tblSales.ID,tblSales.IDProduct, tblProduct.Descrip, tblProduct.specification,tblSales.priceSale, tblSales.numberSale,  tblSales.discount, tblSales.tax, tblSales.totalsale ,tblSales.dateSale,tblSales.IDBill";
                sql2 = sql2 + " FROM            tblSales INNER JOIN tblProduct ON tblSales.IDProduct = tblProduct.ID";
                sql2 = sql2 + " where           tblSales.IDBill = '" + IDbill + "'";

                cm = new SqlCommand(sql2, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    lstStore = listSale.Items.Add(dr[0].ToString());
                    lstStore.SubItems.Add(dr[1].ToString());
                    lstStore.SubItems.Add(dr[2].ToString());
                    lstStore.SubItems.Add(dr[3].ToString());
                    lstStore.SubItems.Add(string.Format("{0:#,##0.00}", double.Parse(dr[4].ToString())));
                    lstStore.SubItems.Add(dr[5].ToString());
                    lstStore.SubItems.Add(dr[6].ToString());
                    lstStore.SubItems.Add(dr[7].ToString());
                    lstStore.SubItems.Add(string.Format("{0:#,##0.00}", double.Parse(dr[8].ToString())));
                    lstStore.SubItems.Add(dr[9].ToString());
                    lstStore.SubItems.Add(dr[10].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            tinhtong();
        }

        private void txtNumberSalel_TextChanged(object sender, EventArgs e)
        {
            tinhtong();
        }
        public void tinhtong()
        {
            if (txtTax.Text == "" || txtDiscount.Text == "" || txtNumberSalel.Text == "" || txtPrice.Text == "") { return; }
            else
            {
                double tax, discout, totaldiscout, total;
                total = Convert.ToDouble(txtNumberSalel.Text) * Convert.ToDouble(txtPrice.Text);
                discout = total * Convert.ToDouble(txtDiscount.Text) / 100;
                totaldiscout = total - discout;
                tax = totaldiscout * Convert.ToDouble(txtTax.Text) / 100;
                txtTotal.Text = (totaldiscout + tax).ToString();
            }

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            tinhtong();
        }

        private void txtTax_TextChanged(object sender, EventArgs e)
        {
            tinhtong();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            generateIDSale();
            printBill();
        }
        private void printBill()
        {
            DataTable tbSale = new DataTable();
            string[] columnList = {"SaleID", "ProductID", "Decription", "Specification", "priceSale","numberSale","discount","tax","totalsale","dateSale","IDBill" };
            var columns = listSale.Columns.Count;

            foreach (var column in columnList)
                tbSale.Columns.Add(column);

            foreach (ListViewItem item in listSale.Items)
            {
                var cells = new object[columns];
                for (var i = 0; i < columns; i++)
                    cells[i] = item.SubItems[i].Text;
                tbSale.Rows.Add(cells);
            }

            LocalReport report = new LocalReport();
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string fullPath = Path.GetDirectoryName(Application.ExecutablePath).Remove(path.Length - 10) + @"\Report\rptProductMemo.rdlc";
            report.ReportPath = fullPath;
            report.DataSources.Add(new ReportDataSource("dsInvoice", tbSale));
            PrintToPrinter(report);
        }
        public static void PrintToPrinter(LocalReport report)
        {
            Export(report);

        }

        public static void Export(LocalReport report, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.26in</PageWidth>
                <PageHeight>8.3in</PageHeight>
                <MarginTop>1in</MarginTop>
                <MarginLeft>0.1in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>1in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (print)
            {
                Print();
            }
        }

        public static void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
    
}
