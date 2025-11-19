using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class PromotionDetailInfo
    {
        public int? PromotionDetailId { get; set; }
        public int? PromotionDetailInfoId { get; set; }
  
        public String PromotionDetailName { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String AllergyRemark { get; set; }
        public String ProductDesc { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String LockAmountFlag { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public Double? Price { get; set; }
        public Double? ProductPrice { get; set; }
        public Double? SumPrice { get; set; }
        public int? Amount { get; set; }   
        public int? PointNum { get; set; }
        public Double? DiscountPercent { get; set; }
        public Double? DiscountAmount { get; set; }
        public int? DefaultAmount { get; set; }
        public int? ComplementaryAmount { get; set; }
        public String Unit { get; set; }
        public String UnitName { get; set; }
        public int? countPromotionDetail { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String RecipeCode { get; set; }
        public String RecipeName { get; set; }
        public String CombosetCode { get; set; }
        public String Comboname { get; set; }
        public String CustomerCode { get; set; }
        public String RunningNo { get; set; }
        public String OrderCode { get; set; }
        public String FlagCombo { get; set; }
        public String LockCheckbox { get; set; }
        public String PromotionTypeCode { get; set; }
        public String PromotionTypeName { get; set; }
        public String FreeShippingCode { get; set; }
        public String MOQFlag { get; set; }
        public int? MinimumQty { get; set; }
        public int? MinimumQtyTier2 { get; set; }
        public Double? GroupPrice { get; set; }
        public String FlagProSetHeader { get; set; }
        public String ParentProductCode { get; set; }
        public String ColorCode { get; set; }
        public String CampaignCategory { get; set; }
        public String ComboCode { get; set; }
        public String ComboName { get; set; }
        public Double? ProductDiscountPercent { get; set; }
        public Double? ProductDiscountPercentTier2 { get; set; }
        public Double? ProductDiscountAmount { get; set; }
        public Double? ProductDiscountAmountTier2 { get; set; }

        public Double? PromotionDiscountPercent { get; set; }
        public Double? PromotionDiscountAmount { get; set; }
        public Double? PriceB4ofPrdDisc { get; set; }
        public float ProductWidth { get; set; }
        public float ProductLength { get; set; }
        public float ProductHeigth { get; set; }
        public float PackageWidth { get; set; }
        public float PackageLength { get; set; }
        public float PackageHeigth { get; set; }
        public float Weight { get; set; }
        public String ItemType { get; set; }
        public String PromotionDetailIDList { get; set; }
        public String ComplementaryFlag { get; set; }
        public String Lazada_ItemId { get; set; }
        public String Lazada_skuId { get; set; }
        public int? QuotaOnHand { get; set; }
        public int? QuotaReserved { get; set; }
        public int? QuotaBalance { get; set; }

    }
}
