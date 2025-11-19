using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public abstract class BaseInfo
    {
        public int? ID { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? count { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}