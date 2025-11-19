using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class OrderActivityInfo
    {
        public String OrderCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public String OrderStateName { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String Note { get; set; }
        public String EmpFName { get; set; }
        public String EmpLName { get; set; }
        public String EmpName { get; set; }
    }
}