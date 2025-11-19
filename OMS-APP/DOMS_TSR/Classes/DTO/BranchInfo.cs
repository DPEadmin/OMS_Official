using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class BranchInfo
    {
        public int? BranchId { get; set; }
        public String BranchCode { get; set; }
        public String BranchName { get; set; }
        public String BranchType { get; set; }
        public String CompanyCode { get; set; }
        public String TaxId { get; set; }
        public String Address { get; set; }
        public String SubDistrictCode { get; set; }
        public String DistrictCode { get; set; }
        public String ProvinceCode { get; set; }
        public String ZipCode { get; set; }
        public String ContactTel { get; set; }
        public String FaxNum { get; set; }
        public String Email { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String Lat { get; set; }
        public String Long { get; set; }
        public String CampaignCategory { get; set; }
        public String OnlineStatus { get; set; }
        public String TemporarilyCloseStatus { get; set; }
        public String Polygon { get; set; }
        public int? count { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public double Distance { get; set; }

    }
}
