using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ReceiptReturnOrderInfo
    {
        public String OrderCode { get; set; }

    }
    public class ReceiptReturnOrderListReturn
    {
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String POCode { get; set; }
        public String InventoryMovement_id { get; set; }
        public String Amount { get; set; }
    }
}
