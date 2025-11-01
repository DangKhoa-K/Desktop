using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Lab4_2212393_NgoDangKhoa;

namespace Lab4_2212393_NgoDangKhoa
{
    public partial class BillEditForm : Form
    {
        // Model được bind với form (mới hoặc được truyền vào để sửa)
        public Bill Bill { get; set; }
        public BillEditForm() : this(null) { }

        public BillEditForm(int id)
        {
            InitializeComponent();
            HookEvents();
            Bill = new Bill();
            PopulateFoods(); // tải danh sách món vào combobox (nếu có DB helper)
        }

        // Constructor nhận Bill (dùng khi sửa)
        public BillEditForm(Bill bill)
        {
            InitializeComponent();
            HookEvents();
            Bill = bill ?? new Bill();
            PopulateFoods();
            LoadBillToForm();
        }

        private void HookEvents()
        {
            btnAddItem.Click += BtnAddItem_Click;
            btnRemoveItem.Click += BtnRemoveItem_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            dgvItems.CellValueChanged += (s, e) => UpdateSummaryFromGrid();
            dgvItems.RowsRemoved += (s, e) => UpdateSummaryFromGrid();
        }

        private void PopulateFoods()
        {
            // Nếu bạn có DatabaseHelper, gọi vào DB để lấy danh sách món (Id, Name, Price)
            // Nếu không, mình để rỗng để bạn tự fill từ DB. Example:
            // var dt = db.ExecuteQuery("SELECT Id, Name, Price FROM Food ORDER BY Name");
            // cmbFood.DisplayMember = "Name"; cmbFood.ValueMember = "Id"; cmbFood.DataSource = dt;

            // tạm: thêm vài item demo nếu DataSource null
            if (cmbFood.Items.Count == 0)
            {
                cmbFood.Items.Add(new ComboItem { Id = 0, Name = "-- Chọn món --", Price = 0 });
                cmbFood.Items.Add(new ComboItem { Id = 1, Name = "Món A", Price = 20000m });
                cmbFood.Items.Add(new ComboItem { Id = 2, Name = "Món B", Price = 30000m });
                cmbFood.DisplayMember = "Name";
                cmbFood.ValueMember = "Id";
                cmbFood.SelectedIndex = 0;
            }
        }

        private void LoadBillToForm()
        {
            if (Bill == null) return;
            txtBillID.Text = Bill.ID > 0 ? Bill.ID.ToString() : "";
            dtpDate.Value = Bill.Date;
            txtStaff.Text = Bill.StaffName;
            txtDiscount.Text = Bill.Discount.ToString(CultureInfo.InvariantCulture);
            txtTax.Text = Bill.Tax.ToString(CultureInfo.InvariantCulture);

            dgvItems.Rows.Clear();
            foreach (var it in Bill.Items)
            {
                int idx = dgvItems.Rows.Add();
                dgvItems.Rows[idx].Cells["colFoodId"].Value = it.FoodId;
                dgvItems.Rows[idx].Cells["colProduct"].Value = it.Product;
                dgvItems.Rows[idx].Cells["colQty"].Value = it.Quantity;
                dgvItems.Rows[idx].Cells["colUnitPrice"].Value = it.UnitPrice;
                dgvItems.Rows[idx].Cells["colLineTotal"].Value = it.LineTotal;
            }

            UpdateSummaryFromGrid();
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            // Lấy thông tin món từ combobox hoặc input
            if (cmbFood.SelectedItem == null)
            {
                MessageBox.Show("Chọn món trước.");
                return;
            }

            var sel = cmbFood.SelectedItem as ComboItem;
            if (sel == null || sel.Id == 0)
            {
                MessageBox.Show("Chọn món hợp lệ.");
                return;
            }

            decimal price = sel.Price;
            int qty = (int)nudQty.Value;

            // Nếu món đã tồn tại trong grid, cộng số lượng
            foreach (DataGridViewRow r in dgvItems.Rows)
            {
                if (Convert.ToInt32(r.Cells["colFoodId"].Value) == sel.Id)
                {
                    int old = Convert.ToInt32(r.Cells["colQty"].Value);
                    r.Cells["colQty"].Value = old + qty;
                    r.Cells["colLineTotal"].Value = (old + qty) * price;
                    UpdateSummaryFromGrid();
                    return;
                }
            }

            int idx = dgvItems.Rows.Add();
            dgvItems.Rows[idx].Cells["colFoodId"].Value = sel.Id;
            dgvItems.Rows[idx].Cells["colProduct"].Value = sel.Name;
            dgvItems.Rows[idx].Cells["colQty"].Value = qty;
            dgvItems.Rows[idx].Cells["colUnitPrice"].Value = price;
            dgvItems.Rows[idx].Cells["colLineTotal"].Value = qty * price;

            UpdateSummaryFromGrid();
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
            {
                dgvItems.Rows.RemoveAt(dgvItems.SelectedRows[0].Index);
                UpdateSummaryFromGrid();
            }
            else
            {
                MessageBox.Show("Chọn 1 dòng để xóa.");
            }
        }

        private void UpdateSummaryFromGrid()
        {
            decimal subtotal = 0;
            foreach (DataGridViewRow r in dgvItems.Rows)
            {
                if (r.IsNewRow) continue;
                decimal lt = 0;
                decimal.TryParse(Convert.ToString(r.Cells["colLineTotal"].Value), out lt);
                subtotal += lt;
            }

            lblSubtotal.Text = $"Tổng hàng: {subtotal:N0}";

            decimal disc = 0, tax = 0;
            decimal.TryParse(txtDiscount.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out disc);
            decimal.TryParse(txtTax.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out tax);

            lblTotal.Text = $"Thành tiền: {(subtotal + tax - disc):N0}";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validation cơ bản
            if (string.IsNullOrWhiteSpace(txtStaff.Text))
            {
                MessageBox.Show("Nhập tên nhân viên.");
                return;
            }

            // Build Bill object from form
            if (Bill == null) Bill = new Bill();

            Bill.Date = dtpDate.Value;
            Bill.StaffName = txtStaff.Text.Trim();
            decimal.TryParse(txtDiscount.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal disc);
            decimal.TryParse(txtTax.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tax);
            Bill.Discount = disc;
            Bill.Tax = tax;

            Bill.Items.Clear();
            foreach (DataGridViewRow r in dgvItems.Rows)
            {
                if (r.IsNewRow) continue;
                var it = new BillItem
                {
                    FoodId = Convert.ToInt32(r.Cells["colFoodId"].Value),
                    Product = Convert.ToString(r.Cells["colProduct"].Value),
                    Quantity = Convert.ToInt32(r.Cells["colQty"].Value),
                    UnitPrice = Convert.ToDecimal(r.Cells["colUnitPrice"].Value)
                };
                Bill.Items.Add(it);
            }

            if (!Bill.Items.Any())
            {
                MessageBox.Show("Hóa đơn phải có ít nhất 1 mặt hàng.");
                return;
            }

            // Trả về OK để parent (BillForm) xử lý lưu vào DB
            this.DialogResult = DialogResult.OK;
        }

        // Một helper class để mô tả item combobox nếu bạn không bind DataTable
        private class ComboItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public override string ToString() => Name;
        }
    }
}
