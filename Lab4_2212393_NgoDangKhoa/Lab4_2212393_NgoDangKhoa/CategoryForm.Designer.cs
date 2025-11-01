// CategoryForm.Designer.cs
namespace Lab4_2212393_NgoDangKhoa
{
    partial class CategoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCategory;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // dgvCategory
            this.dgvCategory.Left = 12;
            this.dgvCategory.Top = 80;
            this.dgvCategory.Width = 560;
            this.dgvCategory.Height = 300;
            this.dgvCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCategory.ReadOnly = true;
            this.dgvCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategory.AllowUserToAddRows = false;
            this.dgvCategory.AllowUserToDeleteRows = false;

            // lblId
            this.lblId.Left = 12;
            this.lblId.Top = 12;
            this.lblId.Text = "Id:";
            this.lblId.AutoSize = true;

            // txtId (readonly)
            this.txtId.Left = 60;
            this.txtId.Top = 8;
            this.txtId.Width = 80;
            this.txtId.ReadOnly = true;

            // lblName
            this.lblName.Left = 160;
            this.lblName.Top = 12;
            this.lblName.Text = "Tên nhóm:";
            this.lblName.AutoSize = true;

            // txtName
            this.txtName.Left = 240;
            this.txtName.Top = 8;
            this.txtName.Width = 330;

            // buttons
            this.btnAdd.Left = 12;
            this.btnAdd.Top = 44;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Width = 80;

            this.btnUpdate.Left = 104;
            this.btnUpdate.Top = 44;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.Width = 90;

            this.btnDelete.Left = 206;
            this.btnDelete.Top = 44;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Width = 80;

            this.btnRefresh.Left = 306;
            this.btnRefresh.Top = 44;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Width = 80;

            // Form properties
            this.ClientSize = new System.Drawing.Size(584, 401);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvCategory, this.txtName, this.lblName,
                this.btnAdd, this.btnUpdate, this.btnDelete, this.btnRefresh,
                this.txtId, this.lblId
            });
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Nhóm món (Category)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
