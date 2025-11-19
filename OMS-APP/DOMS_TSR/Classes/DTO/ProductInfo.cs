using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class  ProductInfo
    {
        public int? ProductId { get; set; }
        public String ProductCode { get; set; }
        public String ProductCodelist { get; set; }
        public String ProductName { get; set; }
        public String ProductDesc { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String SupplierCode { get; set; }
        public String SupplierName { get; set; }
        public String Description { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String ProductImageName { get; set; }
        public String ProductImageId { get; set; }
        public String ProductImageUrl { get; set; }
        public String ProductFileName { get; set; }
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }

        public String PromotionTypeCode { get; set; }
        public String OtherChoice { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }

        public String CompanyCode { get; set; }

         public String CompanyNameTH { get; set; }
         public String CompanyNameEN { get; set; }

        public Double? Price { get; set; }
        public String CarType { get; set; }
        public String MaintainType { get; set; }
        public Double? InsureCost { get; set; }
        public Double? FirstDamages { get; set; }
        public int? GarageQuan { get; set; }
        public Double? TransportPrice { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Balance { get; set; }
        public int? Intransit { get; set; }
        public String InventoryCode { get; set; }
        public int? InventoryDetailId { get; set; }
        public String PromotionDetailName { get; set; }
        public int? PromotionDetailId { get; set; }
        public Double? PromotionDetailPrice { get; set; }
        public int? Amount { get; set; }
        public Double? DiscountAmount { get; set; }
        public String Product_img1 { get; set; }
        public String Showcase_image11 { get; set; }
        public String Showcase_image43 { get; set; }
        public String URLvideo { get; set; }
        public String SKU_img1 { get; set; }
        public String ProdutAdditional { get; set; }
        public String WarrantyCondition { get; set; }
        public String WarrantyType { get; set; }
        public String WarrantyStartdate { get; set; }
        public String WarrantyEnddate { get; set; }
        public String PackageDanger { get; set; }
        public int? DiscountPercent { get; set; }
        public int? ComplementaryAmount { get; set; }
        public int? DefaultAmount { get; set; }
        public String LockAmountFlag { get; set; }
        public String LockCheckbox { get; set; }
        public String Unit { get; set; }
        public String UnitName { get; set; }
        public Double? ProductWidth { get; set; }
        public Double? ProductLength { get; set; }
        public Double? ProductHeigth { get; set; }
        public Double? PackageWidth { get; set; }
        public Double? PackageLength { get; set; }
        public Double? PackageHeigth { get; set; }
        public Double? Weight { get; set; }
        public int? countProduct { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public Double Transportprice { get; set; }
        public String TransportationTypeCode { get; set; }
        public String TransportationTypeName { get; set; }
        public int? countProductBOM { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String Sku { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }        
        public String AllergyRemark { get; set; }
        public String UpsellScript { get; set; }
        public string ProductNotInPromotionCode { get; set; }
        public String ProductCodeImportDup { get; set; }
        public string ProductCodeNotInComplementary { get; set; }

        public int? ExchangeAmount { get; set; }
        public int? ExchangePoint { get; set; }
        public String ExchangeRate { get; set; }
        public String FlagPointCoupon { get; set; }
        public String Propoint { get; set; }
        public String PropointName { get; set; } 
        public String EcomSpec { get; set; }
        public String Lazada_ItemId { get; set; }
        public String Lazada_skuId { get; set; }
        public int? Lazada_status { get; set; }
        public String Lazada_status_Name { get; set; }
        public String LazadaCategoryCode { get; set; }
    }

}
