using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class ReportInfo
    {
        public String ordercode { get; set; }
        public String Saledate { get; set; }
        public String Sale_Name { get; set; }
        public String CamCate_name { get; set; }
        public int? Customer_Name { get; set; }
        public String Customer_Moblie { get; set; }
        public String OrderLastProduct { get; set; }
        public String PromotionLastOrder { get; set; }
        public String AddressCustomer { get; set; }
        public String SaleDateLastOrder { get; set; }
        public String product_Current { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public int? OrderStatusCode { get; set; }
        public String orderstatusname { get; set; }
        public String OrderTracking { get; set; }
        public String OrderStateCode { get; set; }
        public String orderstatename { get; set; }
        public String DeliveryDateSale { get; set; }
        public String OrderNote { get; set; }
    }
    public class ReportSaleAstonInfo
    {
        public String ordercode { get; set; }
        public String Saledate { get; set; }
        public String Sale_Name { get; set; }
        public String CamCate_name { get; set; }
        public String Customer_Name { get; set; }
        public String Customer_Moblie { get; set; }
        public String OrderLastProduct { get; set; }
        public String PromotionLastOrder { get; set; }
        public String AddressCustomer { get; set; }
        public String SaleDateLastOrder { get; set; }
        public String product_Current { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String OrderStatusCode { get; set; }
        public String orderstatusname { get; set; }
        public String OrderTracking { get; set; }
        public String OrderStateCode { get; set; }
        public String orderstatename { get; set; }
        public String DeliveryDateSale { get; set; }
        public String OrderNote { get; set; }
    }
    public class ReportSaleOrderAmountInfo
    {
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Quanlity { get; set; }
        public String Amount { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
    }
    public class ReportSaleOrderInfo
    {

        public String Order_Code { get; set; }
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        //public String SALE_FNAME { get; set; }
        //public String SALE_LNAME { get; set; }
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
    public class ReportResultSOSAInfo
    {
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }

        public String ORDER_DATE { get; set; }
        public String CHANNEL { get; set; }
        public String BRAND { get; set; }
        public String ORDER_STAGE { get; set; }
        public String ORDER_STATUS { get; set; }
        public String TOTAL_ORDER { get; set; }
        public String TOTAL_QTY { get; set; }
        public String TOTAL_AMOUNT { get; set; }
    }
    public class ReaportCampaignPromotionByProductInfo
    {
        public String CAMPAIGN_CODE { get; set; }
        public String CAMPAIGN_NAME { get; set; }
        public String PROMOTION_NAME { get; set; }
        public String PROMOTION_CODE { get; set; }
        public String PRODUCT_NAME { get; set; }
        public String PRODUCT_CODE { get; set; }
        public String AMOUNT { get; set; }

        public String PRICE { get; set; }
        public String PAYMENT_TERM { get; set; }
        public String CHANNEL { get; set; }
        public String BRAND { get; set; }
        public String ORDER_DATE { get; set; }

        public String ORDER_NO { get; set; }
        public String ORDER_STATE { get; set; }
        public String ORDER_STATUS { get; set; }
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        public String CUSTOMER_CODE { get; set; }
        public String CUSTOMER_NAME { get; set; }

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

        public String TransportPrice { get; set; }
        public String SumbyProductprice { get; set; }
        public decimal orderTotalPrice { get; set; }


    }
    public class ReportResultCallinInfo
    {
        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        public String CALL_STATUS { get; set; }
        public String COUNT_CALL { get; set; }
        public String CHANNEL { get; set; }
    
        public String BKK_ORDER { get; set; }
        public String BKK_REVENUE { get; set; }
        public String UPC_ORDER { get; set; }
        public String UPC_REVENUE { get; set; }
        public String ORDER_DATE { get; set; }
        public String TOTAL_ORDER { get; set; }

    }
}

