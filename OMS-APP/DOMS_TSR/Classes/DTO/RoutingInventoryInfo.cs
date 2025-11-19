using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class RoutingMapInventoryInfo
    {
        public int? RoutinginventoryId { get; set; }
        public String Routing_code { get; set; }
        public String Inventory_Code { get; set; }

        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
    }
    public class RoutingMapInventoryDetailInfo
    {
        public String RoutinginventoryId { get; set; }
        public String Routing_code { get; set; }
        public String Inventory_Code { get; set; }
        public String Inventory_name { get; set; }
        public String Routing_name { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
    }
}