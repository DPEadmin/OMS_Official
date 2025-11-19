using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class InventoryMovementInfo
    {
        public int? InventoryMovementId { get; set; }
        public int? InventoryDetailId { get; set; }
        public String InventoryMovementCode { get; set; }
        public String InventoryManualLotCode { get; set; }
        public String ProductCode { get; set; }
        public String POCode { get; set; }
        public String GRCode { get; set; }
        public String OrderNo { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String StatusCode { get; set; }
        public int? SeqId { get; set; }
        public int? SeqManId { get; set; }
        public String CreateDate { get; set; }
        public String CreateDateFrom { get; set; }
        public String CreateDateTo { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String ActiveFlag { get; set; }
        public String Remark { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String EmpFNameTH { get; set; }
        public String EmpLNameTH { get; set; }
        public String EmpName { get; set; }
        public String CountTop { get; set; }
        public decimal? Price { get; set; }

    }
    public class InventoryMovementListReturn
    {
        public int? InventoryMovementId { get; set; }
        public String InventoryMovementCode { get; set; }
        public String InventoryManualLotCode { get; set; }
        public int? InventoryDetailId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String OrderNo { get; set; }
        public String POCode { get; set; }
        public String GRCode { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }
        public int? SeqId { get; set; }
        public int? SeqManId { get; set; }
        public int? EntryDate { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String Remark { get; set; }
        public String ActiveFlag { get; set; }
        public String EmpFNameTH { get; set; }
        public String EmpLNameTH { get; set; }
        public String EmpName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public int? countInventoryMovement { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public decimal? Price { get; set; }

    }
}
