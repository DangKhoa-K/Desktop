// CategoryForm.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Lab4_2212393_NgoDangKhoa;
using System.Xml.Linq;

namespace Lab4_2212393_NgoDangKhoa
{
    public partial class CategoryForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public CategoryForm()
        {
            InitializeComponent();
            HookEvents();
            LoadCategories();
        }

        private void HookEvents()
        {
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += (s, e) => LoadCategories();
            dgvCategory.CellClick += DgvCategory_CellClick;
        }

        private void LoadCategories()
        {
            try
            {
                var dt = db.ExecuteQuery("SELECT Id, Name FROM Category ORDER BY Id");
                dgvCategory.DataSource = dt;
                dgvCategory.Columns["Id"].Width = 60;
                dgvCategory.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtId.Text = "";
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            dgvCategory.ClearSelection();
        }

        private void DgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvCategory.Rows[e.RowIndex];
            txtId.Text = row.Cells["Id"].Value.ToString();
            txtName.Text = row.Cells["Name"].Value.ToString();
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sql = "INSERT INTO Category(Name) VALUES (@Name)";
                int r = db.ExecuteNonQuery(sql, new[] { new SqlParameter("@Name", name) });
                if (r > 0)
                {
                    LoadCategories();
                    MessageBox.Show("Thêm nhóm món thành công.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Chọn 1 nhóm để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sql = "UPDATE Category SET Name=@Name WHERE Id=@Id";
                int r = db.ExecuteNonQuery(sql, new[]
                {
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Id", id)
                });
                if (r > 0)
                {
                    LoadCategories();
                    MessageBox.Show("Cập nhật thành công.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Chọn 1 nhóm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa nhóm (Id={id}) không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                var sql = "DELETE FROM Category WHERE Id=@Id";
                int r = db.ExecuteNonQuery(sql, new[] { new SqlParameter("@Id", id) });
                if (r > 0)
                {
                    LoadCategories();
                    MessageBox.Show("Xóa thành công.", "Thông báo");
                }
            }
            catch (SqlException sqlex) when (sqlex.Number == 547)
            {
                MessageBox.Show("Không thể xóa: nhóm đang được tham chiếu bởi món ăn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
