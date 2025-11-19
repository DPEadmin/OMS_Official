using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class CampaignInfo
    {
        public int? CampaignId { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String CampaignCategory { get; set; }
        public String CampaignCategoryName { get; set; }
        public string FlagComboSet { get; set; }
        public string FlagShowProductPromotion { get; set; }
        public string CampaignType { get; set; }       
        public string CampaignSpec { get; set; }
        public string CampaignSpecValue { get; set; }
        public string PictureCampaignUrl { get; set; }
        public string CampaignImageUrl { get; set; }
        public string CampaignImageName { get; set; }
        public string CampaignImageBase64 { get; set; }
        public String OrderCode { get; set; }
        public int? SumCampaign { get; set; }
        public String StartDate { get; set; }
        public String NotifyDate { get; set; }
        public String EffectiveDate { get; set; }
        public String ExpireDate { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countCampaign { get; set; }
        public int rowOFFSet { get; set; }
        public string Sale_Channel { get; set; }
        public string ChannelName { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String Active { get; set; }
        public String CAMPAIGNSTATUS { get; set; }
        public String ActiveName { get; set; }
        public String TIME_START_Mediaplan { get; set; }
        public String TIME_END_Mediaplan { get; set; }
        public String FlagApproveMedia { get; set; }

        public String MediaPlanDate { get; set; }
        public String MediaPlanDateEnd { get; set; }
        public String MerchantCode { get; set; }

        public String MerchantMapCode { get; set; }
    }
}
