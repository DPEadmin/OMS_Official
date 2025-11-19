using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class ProductStockInfo
    {
        public int? ProductStockId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public int? QTY { get; set; }
        public int? Balance { get; set; }
        public int? Reserved { get; set; }
        public int? Intransit { get; set; }
        public int? countProductStock { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
    }
}
