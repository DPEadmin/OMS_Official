using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ProductBOMInfo
    {
        public int? ProductBOMId { get; set; }
        public String ProductCode { get; set; }
        public String ProductBOM { get; set; }
        public String ProductBOMName { get; set; }
        public String Unit { get; set; }
        public String UnitName { get; set; }
        public int? QTY { get; set; }
        public String ProductDetail { get; set; }
        public String ProductBOMUrl { get; set; }
        public String ProductBOMFileName { get; set; }
        public String Base64Text { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countProductBOM { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String InventoryCode { get; set; }
    }
    public class ProductBOMListReturn
    {
        public int? ProductBOMId { get; set; }
        public String ProductCode { get; set; }
        public String ProductBOM { get; set; }
        public String ProductBOMName { get; set; }
        public String Unit { get; set; }
        public String UnitName { get; set; }
        public int? QTY { get; set; }
        public String ProductDetail { get; set; }
        public String ProductBOMUrl { get; set; }
        public String ProductBOMFileName { get; set; }
        public String Base64Text { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countProductBOM { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String InventoryCode { get; set; }
    }
}
