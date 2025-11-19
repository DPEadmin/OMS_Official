using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class ContactHistoryInfo
    {
        
        public String CustomerCode { get; set; }
        public String ContactStatus { get; set; }
        public String ContactResult { get; set; }
        public String LookupType { get; set; }
        public String ContactDesc { get; set; }
        public String OrderCode { get; set; }
        public String ConHisInfo { get; set; }
        public int? ContactId { get; set; }

        public String ContactInfoCode { get; set; }

        public String ContactInfoName { get; set; }

        public String CreateDate { get; set; }

        public String UpdateBy { get; set; }

        public String UpdateDate { get; set; }

        public String FlagDelete { get; set; }

        public String ContactName { get; set; }

        public String CreateBy { get; set; }
    }
}