namespace Lab4_2212393_NgoDangKhoa
{
    partial class BillForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel topPanel;
        private System.Windows.Forms.Button btnLoad, btnAdd, btnEdit, btnDelete, btnExport, btnFilter, btnClearFilter;
        private System.Windows.Forms.Label lblFrom, lblTo, lblCashier;
        private System.Windows.Forms.DateTimePicker dtFrom, dtTo;
        private System.Windows.Forms.TextBox txtCashier;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgvBills;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.FlowLayoutPanel summaryPanel;
        private System.Windows.Forms.Label lblCount, lblTotal, lblTax, lblDiscount;
        private System.Windows.Forms.ContextMenuStrip ctxBills;
        private System.Windows.Forms.ToolStripMenuItem ctxView, ctxEdit, ctxDelete;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.Text = "Quản lý Hóa đơn (Bills)";
            this.ClientSize = new System.Drawing.Size(1150, 720);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            topPanel = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Top, Height = 64, Padding = new System.Windows.Forms.Padding(6) };
            btnLoad = new System.Windows.Forms.Button { Text = "Tải", AutoSize = true, Margin = new System.Windows.Forms.Padding(6) };
            btnAdd = new System.Windows.Forms.Button { Text = "Thêm", AutoSize = true, Margin = new System.Windows.Forms.Padding(6) };
            btnEdit = new System.Windows.Forms.Button { Text = "Sửa", AutoSize = true, Margin = new System.Windows.Forms.Padding(6) };
            btnDelete = new System.Windows.Forms.Button { Text = "Xóa", AutoSize = true, Margin = new System.Windows.Forms.Padding(6) };
            btnExport = new System.Windows.Forms.Button { Text = "Xuất CSV", AutoSize = true, Margin = new System.Windows.Forms.Padding(6) };

            lblFrom = new System.Windows.Forms.Label { Text = "Từ", Margin = new System.Windows.Forms.Padding(12, 12, 0, 0) };
            dtFrom = new System.Windows.Forms.DateTimePicker { Width = 130 };
            lblTo = new System.Windows.Forms.Label { Text = "Đến", Margin = new System.Windows.Forms.Padding(6, 12, 0, 0) };
            dtTo = new System.Windows.Forms.DateTimePicker { Width = 130 };
            lblCashier = new System.Windows.Forms.Label { Text = "Nhân viên", Margin = new System.Windows.Forms.Padding(6, 12, 0, 0) };
            txtCashier = new System.Windows.Forms.TextBox { Width = 140 };
            btnFilter = new System.Windows.Forms.Button { Text = "Lọc", AutoSize = true, Margin = new System.Windows.Forms.Padding(6) };
            btnClearFilter = new System.Windows.Forms.Button { Text = "Bỏ lọc", AutoSize = true, Margin = new System.Windows.Forms.Padding(6) };

            topPanel.Controls.AddRange(new System.Windows.Forms.Control[] { btnLoad, btnAdd, btnEdit, btnDelete, btnExport, lblFrom, dtFrom, lblTo, dtTo, lblCashier, txtCashier, btnFilter, btnClearFilter });
            this.Controls.Add(topPanel);

            splitMain = new System.Windows.Forms.SplitContainer { Dock = System.Windows.Forms.DockStyle.Fill, Orientation = System.Windows.Forms.Orientation.Vertical };
            splitMain.SplitterDistance = (int)(this.ClientSize.Width * 0.45);
            this.Controls.Add(splitMain);

            dgvBills = new System.Windows.Forms.DataGridView { Dock = System.Windows.Forms.DockStyle.Fill, ReadOnly = true, SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect, AllowUserToAddRows = false };
            dgvItems = new System.Windows.Forms.DataGridView { Dock = System.Windows.Forms.DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill };
            splitMain.Panel1.Controls.Add(dgvBills);
            splitMain.Panel2.Controls.Add(dgvItems);

            summaryPanel = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Bottom, Height = 44 };
            lblCount = new System.Windows.Forms.Label { Text = "Số hóa đơn: 0" };
            lblTotal = new System.Windows.Forms.Label { Text = "Tổng tiền: 0" };
            lblTax = new System.Windows.Forms.Label { Text = "Tổng thuế: 0" };
            lblDiscount = new System.Windows.Forms.Label { Text = "Tổng giảm giá: 0" };
            summaryPanel.Controls.AddRange(new System.Windows.Forms.Control[] { lblCount, lblTotal, lblTax, lblDiscount });
            splitMain.Panel1.Controls.Add(summaryPanel);

            ctxBills = new System.Windows.Forms.ContextMenuStrip();
            ctxView = new System.Windows.Forms.ToolStripMenuItem { Text = "Xem chi tiết" };
            ctxEdit = new System.Windows.Forms.ToolStripMenuItem { Text = "Sửa" };
            ctxDelete = new System.Windows.Forms.ToolStripMenuItem { Text = "Xóa" };
            ctxBills.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ctxView, ctxEdit, ctxDelete });
            dgvBills.ContextMenuStrip = ctxBills;
        }
    }
}
