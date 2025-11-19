using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class DistrictInfo
    {
        public int? DistrictId { get; set; }
        public int Amphur_Id { get; set; }
        public String DistrictCode { get; set; }
        public String DistrictName { get; set; }
        public String SubDistrictCode { get; set; }
        public String ProvinceCode { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countDistrict { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
    }
}
