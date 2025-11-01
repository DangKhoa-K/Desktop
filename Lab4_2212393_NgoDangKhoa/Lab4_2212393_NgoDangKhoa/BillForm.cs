using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lab4_2212393_NgoDangKhoa;

namespace Lab4_2212393_NgoDangKhoa
{
    public partial class BillForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public BillForm()
        {
            InitializeComponent();
            HookEvents();
            dtFrom.Value = DateTime.Today.AddDays(-30);
            dtTo.Value = DateTime.Today;
            LoadBills();
        }

        private void HookEvents()
        {
            btnLoad.Click += (s, e) => LoadBills();
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnExport.Click += BtnExport_Click;
            btnFilter.Click += BtnFilter_Click;
            btnClearFilter.Click += (s, e) => { txtCashier.Text = ""; dtFrom.Value = DateTime.Today.AddDays(-30); dtTo.Value = DateTime.Today; LoadBills(); };
            dgvBills.SelectionChanged += DgvBills_SelectionChanged;
            dgvBills.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) ShowBillDetails(); };
            ctxView.Click += (s, e) => ShowBillDetails();
            ctxEdit.Click += (s, e) => BtnEdit_Click(this, EventArgs.Empty);
            ctxDelete.Click += (s, e) => BtnDelete_Click(this, EventArgs.Empty);
        }

        private void LoadBills()
        {
            try
            {
                var dt = db.ExecuteQuery("SELECT Id, DateCreated, StaffName, Discount, Tax, Total FROM Bill ORDER BY DateCreated DESC");
                dgvBills.DataSource = dt;
                dgvBills.Columns["DateCreated"].HeaderText = "Ngày lập";
                dgvBills.Columns["Total"].DefaultCellStyle.Format = "N0";
                dgvBills.Columns["Tax"].DefaultCellStyle.Format = "N0";
                dgvBills.Columns["Discount"].DefaultCellStyle.Format = "N0";
                if (dgvBills.Rows.Count > 0)
                {
                    dgvBills.ClearSelection();
                    dgvBills.Rows[0].Selected = true;
                }
                RefreshSummary(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hóa đơn: " + ex.Message);
            }
        }

        private void RefreshSummary(DataTable dt)
        {
            int count = dt.Rows.Count;
            decimal total = 0, tax = 0, discount = 0;
            foreach (DataRow r in dt.Rows)
            {
                total += Convert.ToDecimal(r["Total"]);
                tax += Convert.ToDecimal(r["Tax"]);
                discount += Convert.ToDecimal(r["Discount"]);
            }
            lblCount.Text = $"Số hóa đơn: {count}";
            lblTotal.Text = $"Tổng tiền: {total:N0}";
            lblTax.Text = $"Tổng thuế: {tax:N0}";
            lblDiscount.Text = $"Tổng giảm giá: {discount:N0}";
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            var from = dtFrom.Value.Date;
            var to = dtTo.Value.Date;
            var cashier = txtCashier.Text.Trim();
            try
            {
                string sql = "SELECT Id, DateCreated, StaffName, Discount, Tax, Total FROM Bill WHERE DateCreated BETWEEN @From AND @To";
                SqlParameter[] ps;
                if (!string.IsNullOrEmpty(cashier))
                {
                    sql += " AND StaffName LIKE @Cashier";
                    ps = new SqlParameter[] {
                        new SqlParameter("@From", from),
                        new SqlParameter("@To", to.AddDays(1).AddSeconds(-1)),
                        new SqlParameter("@Cashier", "%" + cashier + "%")
                    };
                }
                else
                {
                    ps = new SqlParameter[] {
                        new SqlParameter("@From", from),
                        new SqlParameter("@To", to.AddDays(1).AddSeconds(-1))
                    };
                }
                var dt = db.ExecuteQuery(sql, ps);
                dgvBills.DataSource = dt;
                RefreshSummary(dt);
                if (dgvBills.Rows.Count > 0) { dgvBills.ClearSelection(); dgvBills.Rows[0].Selected = true; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc: " + ex.Message);
            }
        }

        private void DgvBills_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBills.SelectedRows.Count == 0) { dgvItems.Rows.Clear(); return; }
            var id = Convert.ToInt32(dgvBills.SelectedRows[0].Cells["Id"].Value);
            LoadBillItems(id);
        }

        private void LoadBillItems(int billId)
        {
            var dt = db.ExecuteQuery(@"SELECT f.Name AS Product, bd.Quantity, bd.LineTotal FROM BillDetails bd
                                       LEFT JOIN Food f ON bd.FoodId = f.Id WHERE bd.BillId=@BillId",
                                       new SqlParameter[] { new SqlParameter("@BillId", billId) });
            dgvItems.DataSource = dt;
            dgvItems.Columns["LineTotal"].DefaultCellStyle.Format = "N0";
        }

        private void ShowBillDetails()
        {
            if (dgvBills.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvBills.SelectedRows[0].Cells["Id"].Value);
            using (var dlg = new BillDetailsForm(id))
            {
                dlg.ShowDialog(this);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Create new bill with items via modal (simple UI): create in DB & then open details
            using (var dlg = new BillEditForm()) // we'll provide BillEditForm next (simple editor)
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Insert Bill and BillDetails
                    try
                    {
                        var inv = dlg.Bill; // Bill object containing Items
                        // insert bill
                        string insBill = "INSERT INTO Bill(DateCreated, StaffName, Discount, Tax, Total) OUTPUT INSERTED.Id VALUES(@Date, @Staff, @Disc, @Tax, @Total)";
                        var idObj = db.ExecuteScalar(insBill, new SqlParameter[] {
                            new SqlParameter("@Date", inv.Date),
                            new SqlParameter("@Staff", inv.StaffName),
                            new SqlParameter("@Disc", inv.Discount),
                            new SqlParameter("@Tax", inv.Tax),
                            new SqlParameter("@Total", inv.Total)
                        });
                        int newId = Convert.ToInt32(idObj);
                        // insert details
                        foreach (var it in inv.Items)
                        {
                            db.ExecuteNonQuery("INSERT INTO BillDetails(BillId, FoodId, Quantity, LineTotal) VALUES(@BillId, @FoodId, @Qty, @LineTotal)",
                                new SqlParameter[] {
                                    new SqlParameter("@BillId", newId),
                                    new SqlParameter("@FoodId", it.FoodId),
                                    new SqlParameter("@Qty", it.Quantity),
                                    new SqlParameter("@LineTotal", it.LineTotal)
                                });
                        }
                        LoadBills();
                        MessageBox.Show("Thêm hóa đơn thành công.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thêm hóa đơn: " + ex.Message);
                    }
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBills.SelectedRows.Count == 0) { MessageBox.Show("Chọn 1 hóa đơn để sửa."); return; }
            int id = Convert.ToInt32(dgvBills.SelectedRows[0].Cells["Id"].Value);
            using (var dlg = new BillEditForm(id))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var inv = dlg.Bill;
                    try
                    {
                        // update bill header
                        db.ExecuteNonQuery("UPDATE Bill SET DateCreated=@Date, StaffName=@Staff, Discount=@Disc, Tax=@Tax, Total=@Total WHERE Id=@Id",
                            new SqlParameter[] {
                                new SqlParameter("@Date", inv.Date),
                                new SqlParameter("@Staff", inv.StaffName),
                                new SqlParameter("@Disc", inv.Discount),
                                new SqlParameter("@Tax", inv.Tax),
                                new SqlParameter("@Total", inv.Total),
                                new SqlParameter("@Id", id)
                            });
                        // delete old details
                        db.ExecuteNonQuery("DELETE FROM BillDetails WHERE BillId=@BillId", new SqlParameter[] { new SqlParameter("@BillId", id) });
                        // insert new details
                        foreach (var it in inv.Items)
                        {
                            db.ExecuteNonQuery("INSERT INTO BillDetails(BillId, FoodId, Quantity, LineTotal) VALUES(@BillId, @FoodId, @Qty, @LineTotal)",
                                new SqlParameter[] {
                                    new SqlParameter("@BillId", id),
                                    new SqlParameter("@FoodId", it.FoodId),
                                    new SqlParameter("@Qty", it.Quantity),
                                    new SqlParameter("@LineTotal", it.LineTotal)
                                });
                        }
                        LoadBills();
                        MessageBox.Show("Cập nhật hóa đơn thành công.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật hóa đơn: " + ex.Message);
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBills.SelectedRows.Count == 0) { MessageBox.Show("Chọn 1 hóa đơn để xóa."); return; }
            int id = Convert.ToInt32(dgvBills.SelectedRows[0].Cells["Id"].Value);
            if (MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn {id}?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                db.ExecuteNonQuery("DELETE FROM BillDetails WHERE BillId=@Id", new SqlParameter[] { new SqlParameter("@Id", id) });
                db.ExecuteNonQuery("DELETE FROM Bill WHERE Id=@Id", new SqlParameter[] { new SqlParameter("@Id", id) });
                LoadBills();
                MessageBox.Show("Xóa hóa đơn thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog { Filter = "CSV|*.csv", FileName = "bills_export.csv" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                using (var sw = new StreamWriter(sfd.FileName))
                {
                    sw.WriteLine("Id,Date,Staff,Subtotal,Tax,Discount,Total");
                    foreach (DataGridViewRow r in dgvBills.Rows)
                    {
                        var id = r.Cells["Id"].Value;
                        var date = r.Cells["DateCreated"].Value;
                        var staff = r.Cells["StaffName"].Value;
                        // get subtotal from DB (simpler) or calculate
                        decimal subtotal = Convert.ToDecimal(db.ExecuteScalar("SELECT ISNULL(SUM(LineTotal),0) FROM BillDetails WHERE BillId=@Id", new SqlParameter[] { new SqlParameter("@Id", id) }) ?? 0);
                        sw.WriteLine($"\"{id}\",\"{date}\",\"{staff}\",{subtotal},{r.Cells["Tax"].Value},{r.Cells["Discount"].Value},{r.Cells["Total"].Value}");
                    }
                }
                MessageBox.Show("Đã xuất CSV.");
            }
        }
    }
}
