using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using Lab4_2212393_NgoDangKhoa;

namespace Lab4_2212393_NgoDangKhoa
{
    public partial class BillDetailsForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();
        public int BillId { get; private set; }

        public BillDetailsForm(int billId)
        {
            BillId = billId;
            InitializeComponent();
            LoadBillDetails();
        }

        private void LoadBillDetails()
        {
            try
            {
                var dt = db.ExecuteQuery(@"SELECT bd.Id, f.Name AS Product, bd.Quantity, bd.LineTotal
                                           FROM BillDetails bd
                                           LEFT JOIN Food f ON bd.FoodId = f.Id
                                           WHERE bd.BillId = @BillId", new SqlParameter[] { new SqlParameter("@BillId", BillId) });
                dgvDetails.DataSource = dt;
                dgvDetails.Columns["LineTotal"].DefaultCellStyle.Format = "N0";

                // load bill header
                var dtBill = db.ExecuteQuery("SELECT Id, DateCreated, StaffName, Discount, Tax, Total FROM Bill WHERE Id=@Id",
                    new SqlParameter[] { new SqlParameter("@Id", BillId) });
                if (dtBill.Rows.Count > 0)
                {
                    var r = dtBill.Rows[0];
                    txtBillId.Text = r["Id"].ToString();
                    txtDate.Text = Convert.ToDateTime(r["DateCreated"]).ToString("yyyy-MM-dd HH:mm");
                    txtStaff.Text = r["StaffName"].ToString();
                    txtDiscount.Text = r["Discount"].ToString();
                    txtTax.Text = r["Tax"].ToString();
                    txtTotal.Text = r["Total"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message);
            }
        }
    }
}
