using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class L_orderdata
    {
        public string EmpCode { get; set; }
        public List<orderdataInfo> orderdataInfo { get; set; }
    }
    public class orderdataInfo
    {
        public int? OrderDetailID { get; set; }
        public string ProductComboName { get; set; }
        public string OrderCode { get; set; }
        public string OrderType { get; set; }
        public string OrderVehicle { get; set; }
        public string PromotionCode { get; set; }
        public string MerchantCode { get; set; }
        public decimal? Price { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? TransportPrice { get; set; }
        public decimal? SumPrice { get; set; }
        public string Customerpay { get; set; }
        public decimal? ReturnCashAMount { get; set; }
        public string PromotionDetailPrice { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PromotionDetailId { get; set; }
        public string Unit { get; set; }
        public string UnitName { get; set; }
        public string DiscountPercent { get; set; }
        public string DiscountAmount { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string DeliveryDate { get; set; }
        public string FlagDelete { get; set; }
        public string Amount { get; set; }
        public string DefaultAmount { get; set; }
        public decimal? Vat { get; set; }
        public int? PercentVat { get; set; }
        public decimal? NetPrice { get; set; }
        public string OrderStateCode { get; set; }
        public string OrderStatusCode { get; set; }
        public string BUCode { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? SubTotalPrice { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerPhone { get; set; }
        public string runningNo { get; set; }
        public string CampaignCode { get; set; }
        public string OrderTracking { get; set; }
        public string ParentProductCode { get; set; }
        public string ParentPromotionCode { get; set; }
        public string FlagCombo { get; set; }
        public string ComboCode { get; set; }
        public string ShippingBrand { get; set; }
        public string ComboName { get; set; }
        public string CampaignCategoryCode { get; set; }
        public string BranchCode { get; set; }
        public string SALEORDERTYPE { get; set; }
        public string LandmarkLat { get; set; }
        public string LandmarkLng { get; set; }
        public string OrderNote { get; set; }
       
        public string MediaChannelCode { get; set; }
        public string MediaPhone { get; set; }
        public string FlagApproved { get; set; }
        public string LockAmountFlag { get; set; }
        public string LockCheckbox { get; set; }
        public string FreeShipping { get; set; }
        public string FlagProSetHeader { get; set; }
        public string PromotionTypeCode { get; set; }
        public string PromotionTypeName { get; set; }
        public string MOQFlag { get; set; }
        public int? MinimumQty { get; set; }
        public int? ProductDiscountPercent { get; set; }
        public int? ProductDiscountAmount { get; set; }
        public string FreeShippingBill { get; set; }
        public string TradeFlag { get; set; }
        public string InventoryCode { get; set; }
        public string BranchOrderID { get; set; }
        public string FlagMediaPlan { get; set; }
        public string CallInfoID { get; set; }
        public string TaxID { get; set; }
        public string MediaPlanFlag { get; set; }
        public string Prefix { get; set; }
        public string ChannelCode { get; set; }
        public string ChannelName { get; set; }
        public string CamcatCode { get; set; }
        public string OrderSituation { get; set; } 
        public string MerchantMapCode { get; set; }
        public string MerchantMapName { get; set; }
        public string LotNo { get; set; }
        public string PlatformCode { get; set; }
        public string LeadTIBFlag { get; set; }
        public string GetInsurance_TIB { get; set; }

    }
}
