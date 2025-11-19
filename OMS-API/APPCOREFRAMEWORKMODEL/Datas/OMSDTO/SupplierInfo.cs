using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class SupplierInfo
    {
        public int? SupplierId { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String Address { get; set; }
        public String SubDistrictCode { get; set; }
        public String DistrictCode { get; set; }
        public String ProvinceCode { get; set; }
        public String ZipNo { get; set; }
        public String TaxIdNo { get; set; }
        public String FaxNumber { get; set; }
        public String PhoneNumber { get; set; }
        public String Mail { get; set; }
        public String Contactor { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public String SupplierCode_Validate { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String ActiveFlag { get; set; }
        public String POCode { get; set; }
    }

    public class SupplierListReturn
    {
        public int? SupplierId { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierCode_Validate { get; set; }
        public String SupplierName { get; set; }
        public String ProvinceCode { get; set; }
        public String ProvinceName { get; set; }
        public String SubDistrictCode { get; set; }
        public String SubDistrictName { get; set; }
        public String DistrictName { get; set; }
        public String DistrictCode { get; set; }
        public String ZipNo { get; set; }
        public String Address { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countSupplier { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String PhoneNumber { get; set; }
        public String PhoneType { get; set; }
        public String FaxNumber { get; set; }
        public String TaxIdNo { get; set; }
        public String Mail { get; set; }
        public String Contactor { get; set; }
        public String ActiveFlag { get; set; }
        public String POCode { get; set; }
    }
}
