using System;
using System.Collections.Generic;

namespace Lab4_2212393_NgoDangKhoa
{
    [Serializable]
    public class Bill
    {
        public int ID { get; set; } // 0 nếu mới
        public DateTime Date { get; set; } = DateTime.Now;
        public string StaffName { get; set; } = "";
        public decimal Discount { get; set; } = 0; // tiền giảm
        public decimal Tax { get; set; } = 0; // tiền thuế
        public List<BillItem> Items { get; set; } = new List<BillItem>();

        public decimal SubTotal
        {
            get
            {
                decimal s = 0;
                foreach (var it in Items) s += it.LineTotal;
                return s;
            }
        }

        public decimal Total => SubTotal + Tax - Discount;
    }

    [Serializable]
    public class BillItem
    {
        public int FoodId { get; set; }
        public string Product { get; set; } = "";
        public decimal UnitPrice { get; set; } = 0;
        public int Quantity { get; set; } = 1;
        public decimal LineTotal => UnitPrice * Quantity;
    }
}
