using iTextSharp.text.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class InventoryDetailInfo
    {
        public InventoryDetailInfo(string productCode, int? qTY, int? v)
        {
            ProductCode = productCode;
            QTY = qTY;
        }

        public int? InventoryDetailId { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String InventoryDetailCode { get; set; }
        public String InventoryDetailName { get; set; }
        public String ProductCodeValidate { get; set; }
        public String ProductNameValidate { get; set; }
        public String MerchantCode {get; set;}
        public int? QTYValidate { get; set; }
        public int? ProductId { get; set; }
        public String ProductCode { get; set; }
        public String ProductCodeList { get; set; }
        public String ProductName { get; set; }
        public int? QTY { get; set; }
        public int? ReOrder { get; set; }
        public String ReOrderCode { get; set; }
        public int? Reserved { get; set; }
        public int? Current { get; set; }
        public int? Balance { get; set; }
        public int? Intransit { get; set; }
        public String CreateDate{ get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String ProductCategoryName { get; set; }
        public String SupplierName { get; set; } 
        public String SupplierCode { get; set; }
        public String MerchantName { get; set; }
        public int? countInventoryDetail { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String POCode { get; set; }
        public int? Amount { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public int? SafetyStock { get; set; }
        public String ProductCodeImportDup { get; set; }
        public decimal? Price { get; set; }
        public int? PickPack { get; set; }
    }

    public class InventoryDetailInfoNew
    {
        public int? InventoryDetailId { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String InventoryDetailCode { get; set; }
        public String InventoryDetailName { get; set; }
        public String ProductCodeValidate { get; set; }
        public String ProductNameValidate { get; set; }
        public String MerchantCode { get; set; } //ลองทำเพิ่มส่งพารามิเตอร์ไป Query
        public int? QTYValidate { get; set; }
        public int? ProductId { get; set; }
        public String ProductCode { get; set; }
        public String ProductCodeList { get; set; }
        public String ProductName { get; set; }
        public int? QTY { get; set; }
        public int? ReOrder { get; set; }
        public String ReOrderCode { get; set; }
        public int? Reserved { get; set; }
        public int? Current { get; set; }
        public int? PickPack { get; set; }
        public int? Balance { get; set; }
        public int? Intransit { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
        public String SupplierName { get; set; }
        public String MerchantName { get; set; }
        public int? countInventoryDetail { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String POCode { get; set; }
        public int? Amount { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public int? SafetyStock { get; set; }
        public String ProductCodeImportDup { get; set; }
        public String InventoryCodeTo { get; set; }
        public decimal? Price { get; set; }
    }

    public class InventoryForReport
    {
        public String UpdateDate { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String ProductCategoryName { get; set; }
        public String ProductBrandName { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Current { get; set; }
        public int? Balance { get; set; }
        public int? SafetyStock { get; set; }
        public int? ReOrder { get; set; }
        public int? Price { get; set; }
    }

    public class L_InventoryMove
    {
        public List<InventoryDetailInfoNew> L_InventoryDetailInfoNew { get; set; } = new List<InventoryDetailInfoNew>();
    }
}