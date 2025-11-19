using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class OrderData
    {
        public string PromotionCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductOrderType { get; set; }
        public Double? Price { get; set; }
        public string Unit { get; set; }
        public int? Amount { get; set; }
        public int? DefaultAmount { get; set; }
        public int? PromotionDetailId { get; set; }
        public Double? SumPrice { get; set; }

        public int? DiscountAmount { get; set; }
        public int? DiscountPercent { get; set; }
        public string OrderCode { get; set; }
        public string CustomerCode { get; set; }
        public string OrderStatusCode { get; set; }
        public string OrderStateCode { get; set; }
        public string BUCode { get; set; }
        public Double? NetPrice { get; set; }
        public Double? Vat { get; set; }
        public string UpdateBy { get; set; }
        public int? runningNo { get; set; }
        public Double Transportprice { get; set; }

        public string CampaignCode { get; set; }
    }

    public class OrderDataList
    {
        public List<OrderData> OrderData { get; set; }
    }

    public class InvoiceOrderInfo
    {
        public string productname { get; set; }
        public string qty { get; set; }
        public string Amount { get; set; }
        public string ordercode { get; set; }    
        public string productcode { get; set; }       
        public string unit { get; set; }
        public string discount { get; set; }
        public string customername { get; set; }
        public string customeraddress { get; set; }
        public string CustomerCode { get; set; }
        public string orderdate { get; set; }
             public string MerchantMapCode { get; set; }
    }
}
