using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class AddressInfo
    {
        public int? CustomerAddressId { get; set; }
        public String CustomerCode { get; set; }
        public String Province  { get; set; }
        public String SubDistrict { get; set; }
        public String District { get; set; }
        public String ProvinceCode { get; set; }
        public String SubDistrictCode { get; set; }
        public String DistrictCode { get; set; }
        public String Address { get; set; }
        
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? count { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String ZipCode { get; set; }
        
    }
}
