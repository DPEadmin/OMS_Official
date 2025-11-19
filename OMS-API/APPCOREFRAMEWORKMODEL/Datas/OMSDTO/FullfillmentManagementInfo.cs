using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class L_OrderChangestatus
    {
        public List<OrderChangestatusInfo> L_OrderChangestatusInfo { get; set; }
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
        public List<OrderChangeInventoryInfo> L_OrderChangeInventoryInfo { get; set; }

    }
}
