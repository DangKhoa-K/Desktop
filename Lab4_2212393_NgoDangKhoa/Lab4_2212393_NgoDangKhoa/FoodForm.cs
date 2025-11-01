using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Lab4_2212393_NgoDangKhoa;
using System.Xml.Linq;

namespace Lab4_2212393_NgoDangKhoa
{
    public partial class FoodForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public FoodForm()
        {
            InitializeComponent();
            HookEvents();
            LoadCategoriesToCombo();
            LoadFoods();
        }

        private void HookEvents()
        {
            cmbCategoryFilter.SelectedIndexChanged += (s, e) => LoadFoods();
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += (s, e) => { LoadCategoriesToCombo(); LoadFoods(); };
            dgvFood.CellClick += DgvFood_CellClick;
        }

        private void LoadCategoriesToCombo()
        {
            var dt = db.ExecuteQuery("SELECT Id, Name FROM Category ORDER BY Name");
            var row = dt.NewRow();
            row["Id"] = 0;
            row["Name"] = "-- Tất cả --";
            dt.Rows.InsertAt(row, 0);
            cmbCategoryFilter.DisplayMember = "Name";
            cmbCategoryFilter.ValueMember = "Id";
            cmbCategoryFilter.DataSource = dt;
        }

        private void LoadFoods()
        {
            try
            {
                int catId = Convert.ToInt32(cmbCategoryFilter.SelectedValue ?? 0);
                string sql = catId == 0 ? "SELECT f.Id, f.Name, f.Unit, f.Price, f.CategoryId, c.Name AS CategoryName FROM Food f LEFT JOIN Category c ON f.CategoryId=c.Id ORDER BY f.Id"
                                        : "SELECT f.Id, f.Name, f.Unit, f.Price, f.CategoryId, c.Name AS CategoryName FROM Food f LEFT JOIN Category c ON f.CategoryId=c.Id WHERE f.CategoryId=@Cat ORDER BY f.Id";
                var param = catId == 0 ? null : new SqlParameter[] { new SqlParameter("@Cat", catId) };
                var dt = db.ExecuteQuery(sql, param);
                dgvFood.DataSource = dt;
                dgvFood.Columns["Id"].Width = 60;
                dgvFood.Columns["Price"].DefaultCellStyle.Format = "N0";
                dgvFood.Columns["CategoryName"].HeaderText = "Nhóm";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải món: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtUnit.Text = "";
            txtPrice.Text = "";
            cmbCategory.SelectedIndex = -1;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            dgvFood.ClearSelection();
        }

        private void DgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvFood.Rows[e.RowIndex];
            txtId.Text = row.Cells["Id"].Value.ToString();
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtUnit.Text = Convert.ToString(row.Cells["Unit"].Value);
            txtPrice.Text = Convert.ToString(row.Cells["Price"].Value);
            int catId = Convert.ToInt32(row.Cells["CategoryId"].Value);
            cmbCategory.SelectedValue = catId;
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private bool ValidateInput(out string name, out string unit, out double price, out int catId)
        {
            name = txtName.Text.Trim();
            unit = txtUnit.Text.Trim();
            price = 0;
            catId = 0;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Nhập tên món.");
                return false;
            }
            if (!double.TryParse(txtPrice.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out price) || price < 0)
            {
                MessageBox.Show("Giá không hợp lệ.");
                return false;
            }
            if (cmbCategory.SelectedValue == null || !int.TryParse(cmbCategory.SelectedValue.ToString(), out catId) || catId == 0)
            {
                MessageBox.Show("Chọn nhóm món.");
                return false;
            }
            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out var name, out var unit, out var price, out var catId)) return;
            try
            {
                string sql = "INSERT INTO Food(Name, Unit, Price, CategoryId) VALUES(@Name, @Unit, @Price, @CatId)";
                int r = db.ExecuteNonQuery(sql, new SqlParameter[] {
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Unit", unit),
                    new SqlParameter("@Price", price),
                    new SqlParameter("@CatId", catId)
                });
                if (r > 0)
                {
                    LoadFoods();
                    ClearForm();
                    MessageBox.Show("Thêm món thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id)) { MessageBox.Show("Chọn món để sửa."); return; }
            if (!ValidateInput(out var name, out var unit, out var price, out var catId)) return;
            try
            {
                string sql = "UPDATE Food SET Name=@Name, Unit=@Unit, Price=@Price, CategoryId=@CatId WHERE Id=@Id";
                int r = db.ExecuteNonQuery(sql, new SqlParameter[] {
                    new SqlParameter("@Name", name),
                    new SqlParameter("@Unit", unit),
                    new SqlParameter("@Price", price),
                    new SqlParameter("@CatId", catId),
                    new SqlParameter("@Id", id)
                });
                if (r > 0)
                {
                    LoadFoods();
                    ClearForm();
                    MessageBox.Show("Cập nhật thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật món: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id)) { MessageBox.Show("Chọn món để xóa."); return; }
            if (MessageBox.Show("Bạn có muốn xóa món này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                string sql = "DELETE FROM Food WHERE Id=@Id";
                int r = db.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@Id", id) });
                if (r > 0)
                {
                    LoadFoods();
                    ClearForm();
                    MessageBox.Show("Xóa thành công.");
                }
            }
            catch (SqlException sqlex) when (sqlex.Number == 547)
            {
                MessageBox.Show("Không thể xóa: món đang được tham chiếu trong hóa đơn.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }
    }
}
