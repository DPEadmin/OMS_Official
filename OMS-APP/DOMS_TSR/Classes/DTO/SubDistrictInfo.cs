using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class SubDistrictInfo
    {
        public int? SubDistrictId { get; set; }
        public String Tumb_Code { get; set; }
        public String Aump_Code { get; set; }
        public String Prov_Code { get; set; }
        public String ProvinceCode { get; set; }
        public String DistrictCode { get; set; }
        public String SubDistrictCode { get; set; }
        public String SubDistrictName { get; set; }
        public String Zipcode { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countSubDistrict { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
    }
}
