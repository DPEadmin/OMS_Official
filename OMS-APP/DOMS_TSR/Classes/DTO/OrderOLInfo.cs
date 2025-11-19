using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class OrderOLInfo
    {
        public String LotNo { get; set; }
        public String MerchantMapCode { get; set; }
        public String OrderCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CreateDate { get; set; }
        public String CreateDateTo { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Amount { get; set; }
        public String LookupValue { get; set; }
        public String UnitName { get; set; }
        public String NetPrice { get; set; }
        public String confirmno { get; set; }
        public String PaymentTypeName { get; set; }
        public String ContactTel { get; set; }
        public String CustomerCode { get; set; }
        public String ReceiveDate { get; set; }
        public String DeliveryDate { get; set; }
        public String FullName { get; set; }
        public String OrderStatusCode { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String OrderTypeCode { get; set; }
        public String CustomerContact { get; set; }
        public String DeliveryDateFrom { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String OrderStateCode { get; set; }
        public String CreateBy { get; set; }
        public String FlagApproved { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int CountReport { get; set; }
        public String sellName { get; set; }
        public String OrderNote { get; set; }
        public String CampaignCategoryName { get; set; }
        public String addresscustomerdetail { get; set; }
        public String addresscustomerdetailzipcode { get; set; }
        public String TransportPrice { get; set; }
        public decimal orderTotalPrice { get; set; }
        public String priceAmountproduct { get; set; }
        public String OrderStatusName { get; set; }
        public String OrderStateName { get; set; }
        public String OrderTracking { get; set; }
        public String TaxId { get; set; }

        public String SumbyProductprice { get; set; }

        public String SALE_CODE { get; set; }
        public String SALE_FNAME { get; set; }
        public String SALE_LNAME { get; set; }
        public String SKU { get; set; }

        public String CAMPAIGN_CODE { get; set; }
        public String CAMPAIGN_NAME { get; set; }

        public String Gender { get; set; }
        public String MerchantName { get; set; }
        public String CustomerBirthdate { get; set; }
        public String Age { get; set; }

    }

    public class OrderBarChartInfo
    {
        public String Month { get; set; }
        public String Amount { get; set; }
    }
    public class OrderPieChartInfo
    {
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public String Amount { get; set; }
        public String Date { get; set; }


    }
    public class OrderOLExcelInfo
    {
        public String OrderCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CreateDate { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Amount { get; set; }

        //public String UnitName { get; set; }
        public String NetPrice { get; set; }
        //public String confirmno { get; set; }


    }
    public class OrderPaymenttypeInfo
    {
        public String OrderCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CreateDate { get; set; }
        public String CreateDateTo { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Amount { get; set; }
        public String LookupValue { get; set; }
        public String UnitName { get; set; }
        public String NetPrice { get; set; }
        public String confirmno { get; set; }
        public String PaymentTypeName { get; set; }
        public String ContactTel { get; set; }
        public String CustomerCode { get; set; }
        public String ReceiveDate { get; set; }
        public String DeliveryDate { get; set; }
        public String FullName { get; set; }
        public String OrderStatusCode { get; set; }
        public String ChannelCode { get; set; }
        public String OrderTypeCode { get; set; }
        public String CustomerContact { get; set; }
        public String DeliveryDateFrom { get; set; }
        public String DeliveryDateTo { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String OrderStateCode { get; set; }
        public String CreateBy { get; set; }
        public String FlagApproved { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

        public int CountReport { get; set; }
        public String sellName { get; set; }
        public String OrderNote { get; set; }
        public String CampaignCategoryName { get; set; }
        public String addresscustomerdetail { get; set; }
        public String addresscustomerdetailzipcode { get; set; }
        public String TransportPrice { get; set; }
        public decimal orderTotalPrice { get; set; }
        public String priceAmountproduct { get; set; }
        public String OrderStatusName { get; set; }
        public String OrderStateName { get; set; }
        public String OrderTracking { get; set; }

        public String TaxId { get; set; }

        public String SumbyProductprice { get; set; }

        public String SALE_CODE { get; set; }
        public String SALE_NAME { get; set; }
        public String SALE_FNAME { get; set; }
        public String SALE_LNAME { get; set; }
        public String SKU { get; set; }

        public String CAMPAIGN_CODE { get; set; }
        public String CAMPAIGN_NAME { get; set; }

        public String Gender { get; set; }
        public String CustomerBirthdate { get; set; }
        public String Age { get; set; }
        public String Sumprice { get; set; }
        public String PercentVat { get; set; }
        public String Vat { get; set; }
        //Payment Credit

        public string CardType { get; set; }
        public string CardNo { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }
        public string CVCNo { get; set; }

        public string Installment { get; set; }
        public string InstallmentPrice { get; set; }
        public string FirstInstallment { get; set; }
        public string CardIssuename { get; set; }
        public string CitizenId { get; set; }
        public string BirthDate { get; set; }
        public string BankCode { get; set; }
        public string BankBranch { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string PaymentOtherdetail { get; set; }
        public string MpayNum { get; set; }
        public string MpayName { get; set; }
        public string PaymentGateway { get; set; }
    }
    public class OrderHistory
    {
        public String OrderCode { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CustomerName { get; set; }
        public String CreateDate { get; set; }
        public String CreateDateTo { get; set; }
        public String Date { get; set; }
        public String Time { get; set; }
        public String ContactStatus { get; set; }
        public String OrderSituation { get; set; }
        public String OrderType { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

        public int CountReport { get; set; }
        public String OrderNote { get; set; }

        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String NetPrice { get; set; }
        public String Amount { get; set; }
        public String TransportPrice { get; set; }
        public decimal orderTotalPrice { get; set; }
        public String SumbyProductprice { get; set; }
        public String Sumprice { get; set; }
        public String PercentVat { get; set; }
        public String Vat { get; set; }

    }

}