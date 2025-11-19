using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class InventoryDetailInfo
    {
        public String ProductCodeValidate { get; set; }
        public String ProductNameValidate { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String POCode { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Current { get; set; }
        public int? Balance { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }       
        public String SupplierCode { get; set; }
        public int? SafetyStock { get; set; }
        public int? ReOrder { get; set; }
        public decimal? Price { get; set; }
    }
    public class InventoryDetailListReturn
    {
        public int? InventoryDetailId { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String InventoryDetailCode { get; set; }
        public String InventoryDetailName { get; set; }
        public String ProductCodeValidate { get; set; }
        public String ProductNameValidate { get; set; }
        public int? QTYValidate { get; set; }
        public int? ProductId { get; set; }
        public String ProductCode { get; set; }
        public String ProductCodeList { get; set; }
        public String ProductName { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Current { get; set; }
        public int? Balance { get; set; }
        public int? Intransit { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countInventoryDetail { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int? SafetyStock { get; set; }
        public int? ReOrder { get; set; }
        public decimal? Price { get; set; }
    }
    public class INV_DetailListData
    {
        public List<inventorydetaillistData> inventorydetaillistData { get; set; }
    }
    public class inventorydetaillistData
    {
        public string POCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Current { get; set; }
        public int? Balance { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string FlagDelete { get; set; }
    }
}
