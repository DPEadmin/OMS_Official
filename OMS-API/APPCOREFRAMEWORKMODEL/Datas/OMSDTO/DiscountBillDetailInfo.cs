using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class DiscountBillDetailInfo
    {
        public int? DiscountBillDetailId { get; set; }
        public String DiscountBillCode { get; set; }

        public String ProductCode { get; set; }
        public String PromotionCode { get; set; }
        public String DiscountBillName { get; set; }
        public Decimal? Price { get; set; }
        public int? DiscountPercent { get; set; }
        public Decimal? DiscountAmount { get; set; }
        public String LockAmountFlag { get; set; }
        public String LockCheckbox { get; set; }
        public int? DefaultAmount { get; set; }
        public int? ComplementaryAmount { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String AllergyRemark { get; set; }
        public String ProductName { get; set; }
        public String ProductDesc { get; set; }
        public String PromotionName { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public Decimal? Amount { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
        public int? PromotionId { get; set; }
        public String ComplementaryFlag { get; set; }
        public String ChannelCode { get; set; }
        public String RecipeCode { get; set; }
        public String RecipeName { get; set; }
        public String CombosetCode { get; set; }
        public String CustomerCode { get; set; }
        public String CampaignCategoryCode { get; set; }
        public int? ProductDiscountPercent { get; set; }
        public int? ProductDiscountAmount { get; set; }
        public int? countDiscountBillDetail { get; set; }
        public String UnitName { get; set; }
        
    }

    public class DiscountBillDetailListReturn
    {
        public int? DiscountBillDetailId { get; set; }
        public String DiscountBillCode { get; set; }

        public String PromotionDetailName { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String DiscountBillName { get; set; }
        public String DiscountBillTypeCode { get; set; }
        public String DiscountBillTypeName { get; set; }
        
            
        public String ProductDesc { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String AllergyRemark { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String LockAmountFlag { get; set; }
        public String LockCheckbox { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public Double? Price { get; set; }
        public int? Amount { get; set; }
        public int? DiscountPercent { get; set; }
        public Double? DiscountAmount { get; set; }
        public int? DefaultAmount { get; set; }
        public int? ComplementaryAmount { get; set; }
        public String Unit { get; set; }
        public String UnitName { get; set; }
        public int? countPromotionDetail { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public int? PromotionId { get; set; }
        public String ComplementaryFlag { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String RecipeCode { get; set; }
        public String RecipeName { get; set; }
        public String CombosetCode { get; set; }
        public String ComboName { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String RunningNo { get; set; }
        public String OrderCode { get; set; }
        public String CustomerCode { get; set; }
        public String FlagCombo { get; set; }
        public String PromotionTypeCode { get; set; }
        public String PromotionTypeName { get; set; }
        public String FreeShippingCode { get; set; }
        public String MOQFlag { get; set; }
        public int? MinimumQty { get; set; }
        public Double? GroupPrice { get; set; }
        public int? ProductDiscountPercent { get; set; }
        public int? ProductDiscountAmount { get; set; }
        public int? PromotionDiscountPercent { get; set; }
        public int? PromotionDiscountAmount { get; set; }
        public int? countDiscountBillDetail { get; set; }
        
    }
}
