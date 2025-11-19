using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class PromotionCombosetInfo
    {
        public int? PromotionId { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String PromotionTypeCode { get; set; }
        public String PromotionDesc { get; set; }
        public String StartDate { get; set; }
        public String StartDateTo { get; set; }
        public String EndDate { get; set; }
        public String EndDateTo { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int? CampaignPromotionId { get; set; }
        public String CampaignCode { get; set; }
        public String Active { get; set; }
        public Decimal? Status { get; set; }
        public String PromotionCodeEliminate { get; set; }
        public String PromotionStatusCode { get; set; }
        public String FreeShippingCode { get; set; }
        public String MOQFlag { get; set; }
        public int? MinimumQty { get; set; }
        public int? DiscountPercent { get; set; }
        public Double? DiscountAmount { get; set; }
        public String LockAmountFlag { get; set; }
        public String LockCheckbox { get; set; }
        public int? DefaultAmount { get; set; }
        public int? ComplementaryAmount { get; set; }
        public int? ProductDiscountPercent { get; set; }
        public Double? ProductDiscountAmount { get; set; }

        public Double? MinimumTotPrice { get; set; }
        public String RedeemFlag { get; set; }
        public String ComplementaryFlag { get; set; }
        public String PromotionLevel { get; set; }
        public Double? GroupPrice { get; set; }

        public String PicturePromotionUrl { get; set; }
        public String CombosetFlag { get; set; }
        public String CombosetName { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String CampaignType { get; set; }
        public String FlagComboset { get; set; }
    }

    public class PromotionCombosetListReturn
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
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String StartDate { get; set; }
        public String StartDateTo { get; set; }
        public String EndDate { get; set; }
        public String EndDateTo { get; set; }
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
        public String Status { get; set; }
        public String PromotionStatusCode { get; set; }
        public String PromotionStatusName { get; set; }
        public String FreeShippingCode { get; set; }
        public String FreeShippingName { get; set; }
        public String MOQFlag { get; set; }
        public int? MinimumQty { get; set; }
        public int? DiscountPercent { get; set; }
        public Double? DiscountAmount { get; set; }
        public String LockAmountFlag { get; set; }
        public String LockCheckbox { get; set; }
        public int? DefaultAmount { get; set; }
        public int? ComplementaryAmount { get; set; }
        public int? ProductDiscountPercent { get; set; }
        public Double? ProductDiscountAmount { get; set; }

        public Double? MinimumTotPrice { get; set; }
        public String RedeemFlag { get; set; }
        public String ComplementaryFlag { get; set; }
        public String PromotionLevel { get; set; }
        public String PromotionLevelName { get; set; }
        public Double? GroupPrice { get; set; }

        public String PicturePromotionUrl { get; set; }
        public String CombosetFlag { get; set; }
        public String CombosetName { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String CampaignType { get; set; }
        public String FlagComboset { get; set; }
        public String PicturePromotionTypeUrl { get; set; }
    }

    public class L_promotioncombosetdata
    {
        public List<PromotionInfo> PromotionInfo { get; set; }
    }
}
