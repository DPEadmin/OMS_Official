using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class SubPromotionDetailInfo
    {
        public int? PromotionDetailId { get; set; }
        public int? SubMainPromotionDetailInfoId { get; set; }
        public int? SubExchangePromotionDetailInfoId { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String FlagShowProductPromotion { get; set; }
        public String FlagComboSet { get; set; }
        public String FlagSubPromotionDetailMain { get; set; }
        public String FlagSubPromotionDetailExchange { get; set; }
        public String SubMainExchangeID { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public Double? Price { get; set; }
        public int? Amount { get; set; }
        public String UNIT { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String Active { get; set; }
        public String FlagDelete { get; set; }
        public String FreeShipping { get; set; }
        public String CombosetCode { get; set; }
    }
    public class SubPromotionDetailListReturn
    {
        public int? PromotionDetailId { get; set; }
        public int? SubMainPromotionDetailInfoId { get; set; }
        public int? SubExchangePromotionDetailInfoId { get; set; }
        public String SubMainExchangeID { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String PromotionDetailName { get; set; }
        public String FlagShowProductPromotion { get; set; }
        public String FlagComboSet { get; set; }
        public String FlagSubPromotionDetailMain { get; set; }
        public String FlagSubPromotionDetailExchange { get; set; }
        public String ProductCode { get; set; }
        public String MainProductCode { get; set; }
        public String ExchangeProductCode { get; set; }
        public String ProductName { get; set; }
        public String MainProductName { get; set; }
        public String ExchangeProductName { get; set; }
        public Double? Price { get; set; }
        public int? Amount { get; set; }
        public String UNIT { get; set; }
        public String UNITName { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String Active { get; set; }
        public String FlagDelete { get; set; }
        public String FreeShipping { get; set; }
        public String CombosetCode { get; set; }
    }
}
