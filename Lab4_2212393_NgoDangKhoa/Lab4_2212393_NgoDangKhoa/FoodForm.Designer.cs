namespace Lab4_2212393_NgoDangKhoa
{
    partial class FoodForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvFood;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblId;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "Quản lý Món ăn";
            this.ClientSize = new System.Drawing.Size(900, 520);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            lblFilter = new System.Windows.Forms.Label { Left = 12, Top = 12, Text = "Lọc theo nhóm:" };
            cmbCategoryFilter = new System.Windows.Forms.ComboBox { Left = 110, Top = 8, Width = 220, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };

            dgvFood = new System.Windows.Forms.DataGridView { Left = 12, Top = 44, Width = 860, Height = 300, ReadOnly = true, SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect, AllowUserToAddRows = false };

            lblId = new System.Windows.Forms.Label { Left = 12, Top = 360, Text = "Id:" };
            txtId = new System.Windows.Forms.TextBox { Left = 80, Top = 356, Width = 80, ReadOnly = true };

            lblName = new System.Windows.Forms.Label { Left = 180, Top = 360, Text = "Tên món:" };
            txtName = new System.Windows.Forms.TextBox { Left = 250, Top = 356, Width = 320 };

            lblUnit = new System.Windows.Forms.Label { Left = 12, Top = 400, Text = "Đơn vị:" };
            txtUnit = new System.Windows.Forms.TextBox { Left = 80, Top = 396, Width = 120 };

            lblPrice = new System.Windows.Forms.Label { Left = 220, Top = 400, Text = "Giá:" };
            txtPrice = new System.Windows.Forms.TextBox { Left = 260, Top = 396, Width = 120 };

            lblCategory = new System.Windows.Forms.Label { Left = 420, Top = 400, Text = "Nhóm:" };
            cmbCategory = new System.Windows.Forms.ComboBox { Left = 470, Top = 396, Width = 200, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };

            btnAdd = new System.Windows.Forms.Button { Left = 700, Top = 352, Width = 80, Text = "Thêm" };
            btnUpdate = new System.Windows.Forms.Button { Left = 700, Top = 388, Width = 80, Text = "Cập nhật", Enabled = false };
            btnDelete = new System.Windows.Forms.Button { Left = 792, Top = 352, Width = 80, Text = "Xóa", Enabled = false };
            btnRefresh = new System.Windows.Forms.Button { Left = 792, Top = 388, Width = 80, Text = "Làm mới" };

            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                lblFilter, cmbCategoryFilter, dgvFood,
                lblId, txtId, lblName, txtName,
                lblUnit, txtUnit, lblPrice, txtPrice,
                lblCategory, cmbCategory,
                btnAdd, btnUpdate, btnDelete, btnRefresh
            });
        }
    }
}
