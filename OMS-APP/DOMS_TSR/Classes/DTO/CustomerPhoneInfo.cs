using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class CustomerPhoneInfo
    {
        public String CustomerCode { get; set; }
        public String PhoneNumber { get; set; }
        public String CustomerName { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CustomerPhone { get; set; }
        public String PhoneType { get; set; }
        public String CustomerPhoneType { get; set; }

        public String CreateBy { get; set; }

        public String CreateDate { get; set; }

        public int? countCustomerPhone { get; set; }
        public String UpdateBy { get; set; }
        public String LookupValue { get; set; }
        
        public String FlagDelete { get; set; }

        public String UpdateDate { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

        public int PhoneId { get; set; }
        public String CustomerContactTel { get; set; }
    }
}