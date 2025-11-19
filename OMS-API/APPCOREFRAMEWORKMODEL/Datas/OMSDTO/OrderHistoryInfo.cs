using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class OrderHistoryInfo
    {
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String ContactTel { get; set; }
        public String ProductCode { get; set; }
        public int? Amount { get; set; }
        public Double? Price { get; set; }
        public String PromotionCode { get; set; }
        public String OrderCode { get; set; }
        public int? PromotionDetailId { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderTypeCode { get; set; }
        public String OrderTypeName { get; set; }
        public String CreateDate { get; set; }
        public String DeliveryDate { get; set; }
        public String ReceivedDate { get; set; }
        public String CreateDateFrom { get; set; }
        public String CreateDateTo { get; set; }
        public String DeliveryDateFrom { get; set; }
        public String DeliveryDateTo { get; set; }
        public String ReceivedDateFrom { get; set; }
        public String ReceivedDateTo { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
    public class OrderHistoryListReturn
    {
        public String ProductCode { get; set; }

        public String OrderCode { get; set; }
        public String ProductName { get; set; }
        public int? Amount { get; set; }
        public String Unit { get; set; }
        public String CustomerCode { get; set; }
        public String PromotionCode { get; set; }
        public int? PromotionDetailId { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public Double? Price { get; set; }
        public String OrderTypeCode { get; set; }
        public String OrderTypeName { get; set; }
        public String CreateDate { get; set; }
        public String DeliveryDate { get; set; }
        public String ReceivedDate { get; set; }
    }
}
