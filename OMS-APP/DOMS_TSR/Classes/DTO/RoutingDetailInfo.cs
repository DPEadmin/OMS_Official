using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class RoutingDetailInfo
    {
        public int? RoutingDetailId { get; set; }
        public String ProvinceName { get; set; }
        public String DistrictName { get; set; }
        public String SubDistrictName { get; set; }
        public String Routing_code { get; set; }
        public String ProvinceCode { get; set; }
        public String SubDistrictCode { get; set; }
        public String DistrictCode { get; set; }
        public String PostCode { get; set; }
        public String FlagDelete { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countRoutingDetail { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

    }
}