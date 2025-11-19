using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ManageOrderModel
    {
        public OrderDataList OrderDataList { get; set; }
        public PaymentDataList PaymentDataList { get; set; }
        public string CustomerCode { get; set; }
        public TransportDataList TransportDataList { get; set; }
        public string EmpCode { get; set; }
        public InventoryDataList InventoryDataList { get; set; }
        public string InventoryCode { get; set; }

    }
}
