using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class L_campaigndata
    {
        public List<CampaignInfo> CampaignInfo { get; set; }
    }
    public class CampaignInfo
    {
        public int? CampaignId { get; set; }
        public string CampaignCode { get; set; }
        public string CampaignName { get; set; }
        public string CampaignCategory { get; set; }
        public string CampaignType { get; set; }
        public string FlagComboSet { get; set; }
        public string FlagShowProductPromotion { get; set; }
        public string CompanyCode { get; set; }
        public string PictureCampaignUrl { get; set; }
        public string CampaignImageUrl { get; set; }
        public string CampaignSpec { get; set; }
        public string CampaignImageName { get; set; }
        public string CampaignImageBase64 { get; set; }
        public string StartDate { get; set; }
        public string NotifyDate { get; set; }
        public string ExpireDate { get; set; }
        public string UpdateBy { get; set; }
        public string CreateBy { get; set; }
        public string FlagDelete { get; set; }
        public string Active { get; set; }
        public string CAMPAIGNSTATUS { get; set; }
        public string OrderCode { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public string MediaPhone { get; set; }

        public String TIME_START_Mediaplan { get; set; }
        public String TIME_END_Mediaplan { get; set; }
        public String MediaPlanDate { get; set; }
        public String MediaPlanDateEnd { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantMapCode { get; set; }
    }

    public class CampaignListReturn
    {
        public int? CampaignId { get; set; }
        public string CampaignCode { get; set; }
        public string CampaignName { get; set; }
        public string CampaignCategory { get; set; }
        public string CampaignCategoryName { get; set; }
        public string CampaignType { get; set; }
        public string CampaignSpec { get; set; }
        public string CampaignSpecValue { get; set; }
        public string FlagComboSet { get; set; }
        public string FlagShowProductPromotion { get; set; }
        public string CompanyCode { get; set; }
        public string Sale_Channel { get; set; }
        public string ChannelName { get; set; }
        public string CompanyName { get; set; }
        public string OrderCode { get; set; }
        public string PictureCampaignUrl { get; set; }
        public string CampaignImageUrl { get; set; }
        public string CampaignImageName { get; set; }
        public int? SumCampaign { get; set; }
        public string NotifyDate { get; set; }
        public string EffectiveDate { get; set; }
        public string ExpireDate { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string StartDate { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public int? countCampaign { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public string FlagDelete { get; set; }
        public string Active { get; set; }
        public string CAMPAIGNSTATUS { get; set; }
        public String MediaPlanDate { get; set; }
        public String MediaPlanDateEnd { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantMapCode { get; set; }

    }
}
