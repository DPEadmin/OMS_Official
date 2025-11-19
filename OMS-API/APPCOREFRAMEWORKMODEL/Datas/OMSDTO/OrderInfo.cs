using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class OrderInfo
    {
        public String LotNo { get; set; }
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String BranchCode { get; set; }
        public String BranchName { get; set; }
        public String OrderStateCode { get; set; }
        public String OrderTypeCode { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CreateBy { get; set; }
        public String CreateDateFrom { get; set; }
        public String CreateDateTo { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String UpdateBy { get; set; }
        public String BUCode { get; set; }
        public String NetPrice { get; set; }
        public String shipmentdate { get; set; }
        public String ConfirmNo { get; set; }
        public String CreateDate { get; set; }
        public String OrderNote { get; set; }
        public String DeliveryDateFrom { get; set; }
        public String DeliveryDateTo { get; set; }
        public String ReceivedDateFrom { get; set; }
        public String ReceivedDateTo { get; set; }
        public String TransportCode { get; set; }

        public String RiderCode { get; set; }
        public String Month { get; set; }
        public String Year { get; set; }

        public String OrderDate { get; set; }
        public int? IndexPeriodDay { get; set; }
        public String ShipmentDateFrom { get; set; }
        public String ShipmentDateTo { get; set; }
        public String CustomerContact { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String MerchantCode { get; set; }
        public String SaleOrderTypeName { get; set; }
        public String FlagDelete { get; set; }
        public int? sumTotalPrice { get; set; }
        public String ORDERREJECTSTATUS { get; set; }
        public String OrderRejectRemark { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String FlagApproved { get; set; }
        public String SALEORDERTYPE { get; set; }
        public String ShipmentStatus { get; set; }
        public String OrderListDate { get; set; }
        public String InventoryName { get; set; }
        public String InventoryCode { get; set; }
        public String PaymentName { get; set; }
        public String GroupOrderStatusCode { get; set; }
        public String GroupOrderStateCode { get; set; }
        public String CallInfoId { get; set; }
        public String BackOrder { get; set; }
        public int? PercentVat { get; set; }
        public String MediaPhone { get; set; }
        public String BranchOrderID { get; set; }
        public String MerchantMapCode { get; set; }
        public String MerchantName { get; set; }
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
        public Decimal? TransportPrice { get; set; }
        public String ShipmentStatus { get; set; }
        public String OrderTotalAmount { get; set; }
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

        public string OrderSituation { get; set; }
        public String MerchantMapCode { get; set; }
   
    }


    public class FinishOrderReturn
    {
        public String unique_id { get; set; }
        public String customer_fname { get; set; }
        public String customer_lname { get; set; }
        public List<order_detail> order_detail { get; set; } = new List<order_detail>();
        public String channel { get; set; }
        public String InventoryName { get; set; }
        public String InventoryCode { get; set; }
        
    }

    public class order_detail
    {
        public String order_code { get; set;}
        public String brand_name { get; set;}
        public Double total_amount { get; set; }
        public String branch_name { get; set; }
        public String order_status { get; set; }
    }

}
