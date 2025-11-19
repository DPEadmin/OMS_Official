using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class POInfo
    {
        public int? POId { get; set; }
        public String POCode { get; set; }
        public String POCodeList { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String MerchantCode { get; set; }
        public  String MerchantName { get; set; }
        public Double? Price { get; set; }
        public String StatusCode { get; set; }
        public String RequestDate { get; set; }
        public String RequestDateTo { get; set; }
        public String ExpectDate { get; set; }
        public String ExpectDateTo { get; set; }
        public String POObjectiveCode { get; set; }
        public String Description { get; set; }
        public String PaymentMethodCode { get; set; }
        public String PaymentTermCode { get; set; }
        public String PODate { get; set; }
        public String PODateTo { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateDateTo { get; set; }
        public String CreateBy { get; set; }
        public String CreateByFNameTH { get; set; }
        public String CreateByLNameTH { get; set; }
        public String CreateByNameTH { get; set; }
        public String UpdateByFNameTH { get; set; }
        public String UpdateByLNameTH { get; set; }
        public String UpdateByNameTH { get; set; }
        public String FlagDelete { get; set; }
        public String POCodeValidate { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String WFStatusCode { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String Credit { get; set; }
    }
    public class POListReturn
    {
        public int? POId { get; set; }
        public int? POItemId { get; set; }
        public String POCode { get; set; }
        public String POCodeList { get; set; }
        public String POCodeValidate { get; set; }
        public String RequestDate { get; set; }
        public String ExpectDate { get; set; }
        public String POObjectiveCode { get; set; }
        public String POObjectiveName { get; set; }
        public String Description { get; set; }
        public String PaymentMethodCode { get; set; }
        public String PaymentMethodName { get; set; }
        public String PaymentTermCode { get; set; }
        public String PaymentTermName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String PODate { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }
        public String WFStatusCode { get; set; }
        public String WFStatusName { get; set; }
        public String CreateBy { get; set; }
        public Double? Price { get; set; }
        public int? countPO { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public int? QTY { get; set; }
        public String Credit { get; set; }
        public String CreateByFNameTH { get; set; }
        public String CreateByLNameTH { get; set; }
        public String CreateByNameTH { get; set; }
        public String UpdateByFNameTH { get; set; }
        public String UpdateByLNameTH { get; set; }
        public String UpdateByNameTH { get; set; }
    }
}
