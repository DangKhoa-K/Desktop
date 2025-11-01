namespace Lab4_2212393_NgoDangKhoa
{
    partial class BillEditForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblBillIdLabel;
        private System.Windows.Forms.TextBox txtBillID;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.TextBox txtStaff;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.ComboBox cmbFood;
        private System.Windows.Forms.NumericUpDown nudQty;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblTotal;

        // columns names used in logic: colFoodId, colProduct, colQty, colUnitPrice, colLineTotal

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.lblBillIdLabel = new System.Windows.Forms.Label();
            this.txtBillID = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblStaff = new System.Windows.Forms.Label();
            this.txtStaff = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblTax = new System.Windows.Forms.Label();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.cmbFood = new System.Windows.Forms.ComboBox();
            this.nudQty = new System.Windows.Forms.NumericUpDown();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();

            // ========== controls properties ==========
            this.lblBillIdLabel.AutoSize = true;
            this.lblBillIdLabel.Location = new System.Drawing.Point(12, 12);
            this.lblBillIdLabel.Text = "Mã HD:";

            this.txtBillID.Location = new System.Drawing.Point(70, 8);
            this.txtBillID.Width = 120;
            this.txtBillID.ReadOnly = true;

            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(210, 12);
            this.lblDate.Text = "Ngày:";

            this.dtpDate.Location = new System.Drawing.Point(255, 8);
            this.dtpDate.Width = 200;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.CustomFormat = "yyyy-MM-dd HH:mm";

            this.lblStaff.AutoSize = true;
            this.lblStaff.Location = new System.Drawing.Point(12, 44);
            this.lblStaff.Text = "Nhân viên:";

            this.txtStaff.Location = new System.Drawing.Point(70, 40);
            this.txtStaff.Width = 220;

            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(310, 44);
            this.lblDiscount.Text = "Giảm:";

            this.txtDiscount.Location = new System.Drawing.Point(350, 40);
            this.txtDiscount.Width = 80;
            this.txtDiscount.Text = "0";

            this.lblTax.AutoSize = true;
            this.lblTax.Location = new System.Drawing.Point(440, 44);
            this.lblTax.Text = "Thuế:";

            this.txtTax.Location = new System.Drawing.Point(480, 40);
            this.txtTax.Width = 80;
            this.txtTax.Text = "0";

            this.cmbFood.Location = new System.Drawing.Point(12, 80);
            this.cmbFood.Width = 420;
            this.cmbFood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.nudQty.Location = new System.Drawing.Point(444, 80);
            this.nudQty.Width = 70;
            this.nudQty.Minimum = 1;
            this.nudQty.Maximum = 1000;
            this.nudQty.Value = 1;

            this.btnAddItem.Location = new System.Drawing.Point(524, 78);
            this.btnAddItem.Width = 100;
            this.btnAddItem.Text = "Thêm";

            this.btnRemoveItem.Location = new System.Drawing.Point(634, 78);
            this.btnRemoveItem.Width = 100;
            this.btnRemoveItem.Text = "Xóa";

            this.dgvItems.Location = new System.Drawing.Point(12, 120);
            this.dgvItems.Width = 760;
            this.dgvItems.Height = 300;
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // add columns
            var c1 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colFoodId", HeaderText = "FoodId", Visible = false };
            var c2 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colProduct", HeaderText = "Sản phẩm" };
            var c3 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colQty", HeaderText = "Số lượng" };
            var c4 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colUnitPrice", HeaderText = "Đơn giá" };
            var c5 = new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "colLineTotal", HeaderText = "Thành tiền" };
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { c1, c2, c3, c4, c5 });

            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(12, 430);
            this.lblSubtotal.Text = "Tổng hàng: 0";

            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(220, 430);
            this.lblTotal.Text = "Thành tiền: 0";

            this.btnSave.Location = new System.Drawing.Point(600, 460);
            this.btnSave.Width = 80;
            this.btnSave.Text = "Lưu";

            this.btnCancel.Location = new System.Drawing.Point(690, 460);
            this.btnCancel.Width = 80;
            this.btnCancel.Text = "Hủy";

            // form
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.lblBillIdLabel);
            this.Controls.Add(this.txtBillID);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblStaff);
            this.Controls.Add(this.txtStaff);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.lblTax);
            this.Controls.Add(this.txtTax);
            this.Controls.Add(this.cmbFood);
            this.Controls.Add(this.nudQty);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.Text = "Thêm / Sửa hóa đơn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
