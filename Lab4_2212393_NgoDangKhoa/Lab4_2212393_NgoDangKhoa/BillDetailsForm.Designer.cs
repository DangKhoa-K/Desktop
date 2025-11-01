namespace Lab4_2212393_NgoDangKhoa
{
    partial class BillDetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.Label lblBillId, lblDate, lblStaff, lblDiscount, lblTax, lblTotal;
        private System.Windows.Forms.TextBox txtBillId, txtDate, txtStaff, txtDiscount, txtTax, txtTotal;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.Text = "Chi tiết hóa đơn";
            this.ClientSize = new System.Drawing.Size(760, 520);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            lblBillId = new System.Windows.Forms.Label { Left = 12, Top = 12, Text = "Mã HD:" };
            txtBillId = new System.Windows.Forms.TextBox { Left = 80, Top = 8, Width = 120, ReadOnly = true };

            lblDate = new System.Windows.Forms.Label { Left = 220, Top = 12, Text = "Ngày:" };
            txtDate = new System.Windows.Forms.TextBox { Left = 270, Top = 8, Width = 200, ReadOnly = true };

            lblStaff = new System.Windows.Forms.Label { Left = 12, Top = 44, Text = "Nhân viên:" };
            txtStaff = new System.Windows.Forms.TextBox { Left = 80, Top = 40, Width = 200, ReadOnly = true };

            lblDiscount = new System.Windows.Forms.Label { Left = 300, Top = 44, Text = "Giảm:" };
            txtDiscount = new System.Windows.Forms.TextBox { Left = 350, Top = 40, Width = 120, ReadOnly = true };

            lblTax = new System.Windows.Forms.Label { Left = 12, Top = 76, Text = "Thuế:" };
            txtTax = new System.Windows.Forms.TextBox { Left = 80, Top = 72, Width = 120, ReadOnly = true };

            lblTotal = new System.Windows.Forms.Label { Left = 220, Top = 76, Text = "Tổng:" };
            txtTotal = new System.Windows.Forms.TextBox { Left = 270, Top = 72, Width = 160, ReadOnly = true };

            dgvDetails = new System.Windows.Forms.DataGridView { Left = 12, Top = 110, Width = 720, Height = 380, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill };

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblBillId, txtBillId, lblDate, txtDate, lblStaff, txtStaff, lblDiscount, txtDiscount, lblTax, txtTax, lblTotal, txtTotal, dgvDetails
            });
        }
    }
}
