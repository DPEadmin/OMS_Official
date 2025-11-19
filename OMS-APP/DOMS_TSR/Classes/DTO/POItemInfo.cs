  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class POItemInfo
    {
        public int? POItemId { get; set; }
        public String POItemCode { get; set; }
        public String POCode { get; set; }
        public String POCodeValidate { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }
        public int QTY { get; set; }
        public Double? Price { get; set; }
        public Double? TotPrice { get; set; }
        public Double? SumPrice { get; set; }
        public Double? DiscountAmount { get; set; }
        public Double? DiscountPercent { get; set; }
        public Double? DiscountBill { get; set; }
        public String UnitName { get; set; }
        public String CreateBy { get; set; }
        public int? countPOItem { get; set; }
        public int? RunningNo { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
    }
}
