using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class LandmarkInfo
    {
        public int? LandmarkId { get; set; }
        public String LandmarkCode { get; set; }
        public String LandmarkName { get; set; }
        public int CustomerAddressId { get; set; }
        public String Lat { get; set; }
        public String Long { get; set; }
        public String Distance { get; set; }
        public String LandmarkDesc { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}