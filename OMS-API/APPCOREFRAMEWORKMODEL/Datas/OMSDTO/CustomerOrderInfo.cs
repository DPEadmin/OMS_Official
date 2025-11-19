using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class CustomerOrderInfo
    {
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String CustomerCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStateCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String shipmentdate { get; set; }
        public String OrderNote { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

    }

    public class CustomerOrderListReturn
    {
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public String ProductName { get; set; }
        public String OrderNote { get; set; }
        public String OrderStatus { get; set; }
        public String OrderStateCode { get; set; }
        public String OrderStateName { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String BUCode { get; set; }
        public String BUName { get; set; }
        public String NetPrice { get; set; }
        public String CampaignName { get; set; }
        public String TotalPrice { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateDateFrom { get; set; }
        public String CreateDateTo { get; set; }
        public String CreateBy { get; set; }

        public int? countOrder { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String ConfirmNo { get; set; }
        public String shipmentdate { get; set; }
        public String TransportName { get; set; }
        public String TransportCode { get; set; }
        public String CustomerPhone { get; set; }
    }
}
