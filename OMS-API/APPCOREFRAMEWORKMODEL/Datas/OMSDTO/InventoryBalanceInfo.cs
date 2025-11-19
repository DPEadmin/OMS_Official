using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class InventoryBalanceInfo
    {
        public int? InventoryBalanceId { get; set; }
        public String EmpCode { get; set; }
        public String InventoryCode { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagActive { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String ProductCode { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Balance { get; set; }
    }

    public class InventoryBalanceListReturn
    {
        public String EmpCode { get; set; }
        public int? InventoryBalanceId { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagActive { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Balance { get; set; }
        public int? countInventoryBalance { get; set; }

    }
}
