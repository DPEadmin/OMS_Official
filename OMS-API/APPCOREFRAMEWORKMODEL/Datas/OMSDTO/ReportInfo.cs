using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ReportOrderInfo
    {
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
    }


    public class ReportOrderListReturn
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
        public String NotSent { get; set; }
        public String Sented { get; set; }
        public String InventoryName { get; set; }
        public String InventoryCode { get; set; }
        public String PaymentName { get; set; }
        
    }


    public class ReportFinishOrderReturn
    {
        public String unique_id { get; set; }
        public String customer_fname { get; set; }
        public String customer_lname { get; set; }
        public List<order_detail> order_detail { get; set; } = new List<order_detail>();
        public String channel { get; set; }
        public String InventoryName { get; set; }
        public String InventoryCode { get; set; }
        
    }

    public class Reportorder_detail
    {
        public String order_code { get; set;}
        public String brand_name { get; set;}
        public Double total_amount { get; set; }
        public String branch_name { get; set; }
        public String order_status { get; set; }
    }
    public class ReportSaleOrderInfo
    {
      
        public String Order_Code { get; set; }
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        public String ORDER_NO { get; set; }
        public String ORDER_DATE { get; set; }
        public String CODE_PRODUCT { get; set; }
        public String PRODUCT_NAME { get; set; }
        public String BRAND { get; set; }
        public String CHANNEL { get; set; }
        public String AMOUNT { get; set; }
        public String PRICE { get; set; }
        public String DISC_PERCENT { get; set; }
        public String DISC_THB { get; set; }
        public String TOTAL_PRICE { get; set; }
        public String ORDER_STATUS { get; set; }
        public String FULFILL_STATUS { get; set; }
        public String PAYMENT_TERM { get; set; }
        public String LAST_UPDATE { get; set; }
        public String LAST_UPDATE_BY { get; set; }
        public int? countReportsaleOrder { get; set; }

        public String ORDER_NOTE { get; set; }
        public String ORDER_TRANS { get; set; }
        public String ORDER_TRANSPRICE { get; set; }
        public String ORDER_VAT { get; set; }
        public String PER_VAT { get; set; }
        public String FINAL_PRICE { get; set; }
        public String ORDER_TRACK { get; set; }

        public String CUS_CODE { get; set; }
        public String CUS_NAME { get; set; }
        public String CUS_MOBILE { get; set; }
        public String CUS_ADD { get; set; }
        public String CUS_POSTCODE { get; set; }
        public String CUS_ADD2 { get; set; }
        public String CUS_POSTCODE2 { get; set; }
        public String CUS_TAXID { get; set; }
        public String DELIVER_DATE { get; set; }

    }
    public class ReportSaleOrderAmountInfo
    {
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Quanlity { get; set; }
        public String Amount { get; set; }
    }
    public class CountReportSaleOrderInfo
    {
        public int? CountReportSaleOrder { get; set; }
    }
    public class ReportResultSOSAInfo
    {
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
    
        public String ORDER_DATE { get; set; }
        public String CHANNEL { get; set; }
        public String BRAND { get; set; }
         public String ORDER_STATUS { get; set; }
        public String FULFILL_STATUS { get; set; }
        public String TOTAL_PRICE { get; set; }
         public String NUM_OF_AMOUNT { get; set; }
        public String NUM_OF_ORDER { get; set; }
        public int? countReportsaleOrder { get; set; }
        public String ORDER_STAGE { get; set; }
        public String TOTAL_ORDER { get; set; }
        public String TOTAL_QTY { get; set; }
        public String TOTAL_AMOUNT { get; set; }

    }
    public class CountReportResultSOSAInfo 
    {
        public int? CountReportResultSOSA { get; set; }
    }
    public class ReaportCampaignPromotionByProductInfo
    {
        public String CAMPAIGN_CODE { get; set; }
        public String CAMPAIGN_NAME { get; set; }
        public String PROMOTION_NAME { get; set; }
        public String PRODUCT_NAME { get; set; }
        public String PRODUCT_CODE { get; set; }
        public String AMOUNT { get; set; }

        public String PRICE { get; set; }
        public String PAYMENT_TERM { get; set; }
        public String CHANNEL { get; set; }
        public String BRAND { get; set; }
        public String ORDER_DATE { get; set; }

        public String ORDER_NO { get; set; }
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        public String CUSTOMER_CODE { get; set; }
        public String CUSTOMER_NAME { get; set; }
        public String PROMOTION_CODE { get; set; }
        public String ORDER_STATUS { get; set; }
        public String ORDER_STATE { get; set; }


    }
    public class CountReaportCampaignPromotionByProductInfo
    {
        public int? CountReaportCampaignPromotionByProduct { get; set; }
    }
    public class ReaportMediaDailySaleInfo
    {
        public String MEDIA_DATE { get; set; }
        public String MEDIA_TIME { get; set; }
        public String MEDIA_PHONE { get; set; }
        public String CHANNEL { get; set; }
        public String PROGRAM_NAME { get; set; }

        public String CAMPAIGN_CODE { get; set; }
        public String CAMPAIGN_NAME { get; set; }
        public String PROMOTION_CODE { get; set; }
        public String PROMOTION_NAME { get; set; }
        public String PRODUCT_NAME { get; set; }

        public String AMOUNT { get; set; }
        public String PRICE { get; set; }

        public String PAYMENT_TERM { get; set; }
        public String ORDER_DATE { get; set; }
        public String ORDER_NO { get; set; }
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        public String CUSTOMER_CODE { get; set; }
        public String CUSTOMER_NAME { get; set; }

        public String TIME_START { get; set; }
        public String TIME_END { get; set; }
        public String DURATION { get; set; }
        public String PRODUCT_CODE { get; set; }
        public String TOTALPRICE { get; set; }
        public String SUMPRICE { get; set; }
        public String TRANSPORTPRICE { get; set; }

        public String TransportPrice { get; set; }
        public String SumbyProductprice { get; set; }
        public decimal orderTotalPrice { get; set; }


    }
    public class CountReaportMediaDailySaleInfo
    {
        public int? CountReaportMediaDailySale { get; set; }
    }
    public class ReaportPerformanceSOSAInfo
    {
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        public String CHANNEL { get; set; }
        public String TOTAL { get; set; }
        public String BKK_ORDER { get; set; }

        public String BKK_REVENUE { get; set; }
        public String UPC_ORDER { get; set; }
        public String UPC_REVENUE { get; set; }
        public String ORDER_DATE { get; set; }
      

    }

    public class CountReaportPerformanceSOSAInfo
    {
        public int? CountReaportPerformanceSOSA { get; set; }
    }
    public class ReportResultCallinInfo
    {
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        public String CALL_STATUS { get; set; }
        public String COUNT_CALL { get; set; }
        public String CHANNEL { get; set; }
        public String ORDER_TOTAL { get; set; }        
        public String BKK_ORDER { get; set; }
        public String BKK_REVENUE { get; set; }
        public String UPC_ORDER { get; set; }
        public String UPC_REVENUE { get; set; }
        public String ORDER_DATE { get; set; }

        public String TOTAL_ORDER { get; set; }


    }
    public class CountReportResultCallinInfo
    {
        public int? CountReportResultCallin { get; set; }
    }
}
