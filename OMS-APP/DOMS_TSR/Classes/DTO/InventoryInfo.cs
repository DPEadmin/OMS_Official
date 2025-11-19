using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class InventoryInfo
    {
        public int? InventoryId { get; set; }
        public String InventoryCodeValidate { get; set; }
        public String InventoryNameValidate { get; set; }
        public String InventoryCode { get; set; }
        public int? InventoryDetailId { get; set; }
        public String InventoryName { get; set; }
        public String EmpCode { get; set; }
        public int? ProductId { get; set; }
        public String ProductCode { get; set; }
        public String ProductCodeList { get; set; }
        public String ProductName { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Current { get; set; }
        public int? PickPack { get; set; }
        public int? Balance { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public String EmpInventoryFlagDelete { get; set; }
        public int? countInventory { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String CreateDateFrom { get; set; }
        public String CreateDateTo { get; set; }
        public String Address { get; set; }
        public String Province { get; set; }
        public String ProvinceName { get; set; }
        public String District { get; set; }
        public String DistrictName { get; set; }
        public String SubDistrict { get; set; }
        public String SubDistrictName { get; set; }
        public String PostCode { get; set; }
        public String ContactTel { get; set; }
        public String Fax { get; set; }
        public String Routing { get; set; }
        public String InvCenterFlag { get; set; }
        public String Lat { get; set; }
        public String Long { get; set; }
        public String Polygon { get; set; }
        public string CompanyCode { get; set; }
        public string AreaCode { get; set; }
    }
}
