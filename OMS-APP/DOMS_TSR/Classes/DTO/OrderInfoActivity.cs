using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class OrderInfoActivity
    {
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public String OrderStateCode { get; set; }
        public String OrderStateName { get; set; }
        public String OrderNote { get; set; }
        public String Createdate { get; set; }
        public String CreateBy { get; set; }
    }
}
