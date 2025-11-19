using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class OrderInfo
    {
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String LotNo { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String ProductName { get; set; }
        public String BranchCode { get; set; }
        public String BranchName { get; set; }
        public String OrderStatus { get; set; }
        public String OrderStateCode { get; set; }
        public String OrderStateName { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CustomerName { get; set; }
        public String BUCode { get; set; }
        public String BUName { get; set; } 
        public String NetPrice { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String TotalPrice { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateDateFrom { get; set; }
        public String CreateDateTo{ get; set; }
        public String CreateBy { get; set; }
        public int? countOrder { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String ConfirmNo { get; set; }
        public String shipmentdate { get; set; }
        public String OrderNote { get; set; }
        public String TransportName { get; set; }
        public String TransportCode { get; set; }
        public String CustomerContact { get; set; }
        public String MerchantName { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String SaleOrderTypeName { get; set; }
        public String DeliveryDate { get; set; }
        public String ReceivedDate { get; set; }
        public String OrderTypeCode { get; set; }
        public String ORDERREJECTSTATUS { get; set; }
        public String OrderRejectRemark { get; set; }
        public int? sumTotalPrice { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String BranchOrderID { get; set; }
        public String FlagApproved { get; set; }
        public String SALEORDERTYPE { get; set; }
        public Decimal ? TransportPrice { get; set; }
        public String OrderTotalAmount { get; set; }
        public String Amount { get; set; }
        public String NotSent { get; set; }
        public String Sented { get; set; }
        public String InventoryName { get; set; }
        public String InventoryCode { get; set; }
        public String PaymentName { get; set; }
        public String GroupOrderStatusCode { get; set; }
        public String GroupOrderStateCode { get; set; }
        public String CallInfoId { get; set; }
        public String BackOrder { get; set; }
        public int? PercentVat { get; set; }
        public String vat { get; set; }

        public String OrderTrackingNo { get; set; }
        public String MediaPhone { get; set; }
        public String MerchantMapCode { get; set; }
      
    }
    public class OrderListReturn
    {
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderTypeName { get; set; }
        public String OrderStatusName { get; set; }
        public String ProductName { get; set; }
        public String OrderStatus { get; set; }
        public String OrderStateCode { get; set; }
        public String OrderStateName { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CustomerName { get; set; }
        public String BUCode { get; set; }
        public String BUName { get; set; }
        public String NetPrice { get; set; }
        public String CampaignCode { get; set; }
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
        public String OrderNote { get; set; }
        public String TransportName { get; set; }
        public String TransportCode { get; set; }
        public String OrderListDate { get; set; }
        public String DeliveryDate { get; set; }
        public String ReceivedDate { get; set; }


        public String RiderCode { get; set; }
        public String DistrictName { get; set; }
        public String SubDistrictName { get; set; }
        public String MerchantName { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public String Month { get; set; }
        public String Year { get; set; }

        public String OrderDate { get; set; }
        public String Amount { get; set; }
        public String ProductCode { get; set; }
        public String MerchantCode { get; set; }
        public String PromotionName { get; set; }
        public int? IndexPeriodDay { get; set; }

        public String CustomerContact { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String BranchCode { get; set; }
        public String BranchName { get; set; }
        public String SaleOrderTypeName { get; set; }
        public String OrderTypeCode { get; set; }
        public int? sumAmount { get; set; }
        public int? sumTotalPrice { get; set; }
        public String ORDERREJECTSTATUS { get; set; }
        public String CampaignCategoryName { get; set; }
        public String BranchOrderID { get; set; }
        public String OrderRejectRemark { get; set; }
        public String FlagApproved { get; set; }
        public String SALEORDERTYPE { get; set; }
        public Double? TransportPrice { get; set; }
        public String ShipmentStatus { get; set; }
        public String OrderTotalAmount { get; set; }
        public String InventoryName { get; set; }
        public String InventoryCode { get; set; }
        public String PaymentName { get; set; }

        public String GroupOrderStatusCode { get; set; }
        public String GroupOrderStateCode { get; set; }
        public String CallInfoId { get; set; }
        public String BackOrder { get; set; }
        public int? PercentVat { get; set; }
        public String OrderTrackingNo { get; set; }
        public String OrderSituation { get; set; }

        public String MerchantMapCode { get; set; }

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
