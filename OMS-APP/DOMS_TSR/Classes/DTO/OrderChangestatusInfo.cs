using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class OrderChangestatusInfo 
    {
        public string ordercode { get; set; }
        public string orderid { get; set; }
        public string orderstatus { get; set; }
        public string orderstate { get; set; }     
        public string updateBy { get; set; }
        public string FlagApproved { get; set; }
        public string updateDate { get; set; }
        public string Confirmno { get; set; }
        public string OrderTracking { get; set; }
        public string MerchantMapCode { get; set; }

        public string OrderRouting { get; set; }
        public string OrderVechicle { get; set; }

        public string OrderNote { get; set; }



    }
    public class L_OrderChangestatus
    {       
        public List<OrderChangestatusInfo> L_OrderChangestatusInfo { get; set; } = new List<OrderChangestatusInfo>();

    }

    public class OrderChangeInventoryInfo
    {
        public string ordercode { get; set; }

        public string Inventorycode { get; set; }
        public string Productcode { get; set; }
        public string updateBy { get; set; }
        public string orderstatus { get; set; }
        public string orderstate { get; set; }
        public String OrderNote { get; set; }

        public string Inventorycode_Old { get; set; }
    }
    public class L_OrderChangeInventory
    {
        public List<OrderChangeInventoryInfo> L_OrderChangeInventoryInfo { get; set; } = new List<OrderChangeInventoryInfo>();

    }

}
