using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DTO
{
    public class OrderHistoryInfo
    {
        public String ProductCode { get; set; }

        public String OrderCode { get; set; }
        public String ProductName { get; set; }
        public int? Amount { get; set; }
        public String Unit { get; set; }
        public String CustomerCode { get; set; }
        public String PromotionCode { get; set; }
        public int? PromotionDetailId { get; set; }
        public int? OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public Double? Price { get; set; }
        
    }
}