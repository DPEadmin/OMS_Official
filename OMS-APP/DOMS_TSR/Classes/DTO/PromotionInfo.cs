using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class PromotionInfo
    {
        public int? PromotionId { get; set; }
        public int? CampaignPromotionId { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String PromotionDesc { get; set; }
        public String PromotionTypeCode { get; set; }
        public String PromotionTypeName { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String StartDate { get; set; }
        public String StartDateTo { get; set; }
        public String EndDate { get; set; }
        public String EndDateTo { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String NotifyDate { get; set; }
        public string ExpireDate { get; set; }
        public String Name { get; set; }
        public String Lastname { get; set; }
        public int? countPromotion { get; set; }
        public int? countCampaignPromotion { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String Active { get; set; }
        public String PromotionStatusCode { get; set; }
        public String PromotionStatusName { get; set; }
        public String FreeShippingCode { get; set; }
        public String FreeShippingName { get; set; }
        public String MOQFlag { get; set; }
        public int? MinimumQty { get; set; }
        public int? MinimumQtyTier2 { get; set; }
        public Double? DiscountPercent { get; set; }
        public Double? DiscountAmount { get; set; }
        public String LockAmountFlag { get; set; }
        public String LockCheckbox { get; set; }
        public int? DefaultAmount { get; set; }
        public int? ComplementaryAmount { get; set; }
        public Double? ProductDiscountPercent { get; set; }
        public Double? ProductDiscountPercentTier2 { get; set; }
        public Double? ProductDiscountAmount { get; set; }
        public Double? ProductDiscountAmountTier2 { get; set; }
        public Double? MinimumTotPrice { get; set; }
        public String RedeemFlag { get; set; }
        public String ComplementaryFlag { get; set; }
        public String ComplementaryChangeAble { get; set; }
        public String PromotionLevel { get; set; }
        public String PromotionLevelName { get; set; }
        public Double? GroupPrice { get; set; }
        public String PicturePromotionUrl { get; set; }
        public String PromotionImageUrl { get; set; }
        public String CombosetName { get; set; }
        public String CombosetFlag { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String CampaignType { get; set; }
        public String FlagComboset { get; set; }
        public String PicturePromotionTypeUrl { get; set; }
        public String StartDatePromotion { get; set; }
        public String EndDatePromotion { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantMapCode { get; set; }
        public String Bu { get; set; }
        public String wfStatus { get; set; }
        public String wfFinishFlag { get; set; }
        public int? levels { get; set; }

        public String Propoint { get; set; }
        public String PropointName { get; set; }
        public String PointType { get; set; }
        public String PointTypeName { get; set; }
        public String CompanyCode { get; set; }
        public String CompanyNameEN { get; set; }
        public String FlagPatent { get; set; }
        public int? PatentAmount { get; set; }
        public String DiscountCode { get; set; }
        public String PointRangeCode { get; set; }
        public String PointRangeName { get; set; }
        public int? PromotionUsed { get; set; }
        public int? PromotionRemain { get; set; }

        public string ApplyScope { get; set; }
        public string CriteriaType { get; set; }
        public string DiscountType { get; set; } 
        public int? OrderNumbers { get; set; }
        public string CriteriaValueTier1 { get; set; }
        public string CriteriaValueTier2 { get; set; }
        public string CriteriaValueTier3 { get; set; }
        public string DiscountValueTier1 { get; set; } 
        public string DiscountValueTier2 { get; set; } 
        public string DiscountValueTier3 { get; set; }
        public int? LazadaPromotionStatus { get; set; }
        public string LazadaPromotionStatusName { get; set; }

        public string DiscountTypeName { get; set; }
        public string CriteriaTypeName { get; set; }
        public string ApplyScopeName { get; set; }
        public string LazadaPromotionId { get; set; }
        public string PromotionTagCode { get; set; }
        public string PromotionTagName { get; set; }
        public string ProductTagCode { get; set; }
        public string ProductTagName { get; set; }
        public string PromotionQuotaFlag { get; set; }
        public string RoleCode { get; set; }

    }
}
