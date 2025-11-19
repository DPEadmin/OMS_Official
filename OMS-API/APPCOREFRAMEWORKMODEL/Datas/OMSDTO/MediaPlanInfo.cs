using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class MediaPlanInfo
    {
        public int? MediaPlanId { get; set; }
        public String MediaPlanDate { get; set; }
        public String MediaPlanEndDate { get; set; }
        public String MediaPlanTime { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String ProgramName { get; set; }
        public String SALE_CHANNEL { get; set; }
        public String Duration { get; set; }
        public String MediaPhone { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String CreateBy { get; set; }
        public String CreateDate { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String MediaPlanidList { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int? countMediaPlan { get; set; }
        public String Active { get; set; }
        public String TIME_START { get; set; }
        public String TIME_END { get; set; }
        public String MEDIA_CHANNEL { get; set; }
        public String MediaPlanDateStart { get; set; }
        public String MediaPlanDateEnd { get; set; }
        public String FlagApprove { get; set; }


        public String DelayStartTime { get; set; }
        public String DelayEndTime { get; set; }

    }

    public class L_MediaPlan
    {
        public List<MediaPlanInfo> L_MediaPlanInfo { get; set; }
    }

    public class MediaPlanListReturn
    {
        public int? MediaPlanId { get; set; }
        public String MediaPlanDate { get; set; }
        public String MediaPlanEndDate { get; set; }
        public String MediaPlanTime { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String ProgramName { get; set; }
        public String SALE_CHANNEL { get; set; }
        public String SALE_CHANNEL_CODE { get; set; }
        public int? Duration { get; set; }
        public String MediaPhone { get; set; }
        public String MediaPhoneCode { get; set; }
        public String CampaignCode { get; set; }
        public String CampaignName { get; set; }
        public String CreateBy { get; set; }
        public String CreateDate { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String MediaPlanidList { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int? countMediaPlan { get; set; }
        public String Active { get; set; }
        public String TIME_START { get; set; }
        public String TIME_END { get; set; }
        public String MEDIA_CHANNEL { get; set; }
        public String MEDIA_CHANNEL_NAME { get; set; }
        public String MediaPlanDateStart { get; set; }
        public String MediaPlanDateEnd { get; set; }
        public String FlagApprove { get; set; }

        public String DelayStartTime { get; set; }
        public String DelayEndTime { get; set; }

    }
    public class MediaPhoneInfo
    {

        public int? MediaPhoneId { get; set; }
        public string  MediaPhoneIdlist { get; set; }
        public String MediaPhoneNo { get; set; }
        public String MediaPhoneCode { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String Active { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int countMediaPhone { get; set; }
        public String TIME_START { get; set; }
        public String TIME_END { get; set; }
        public String MEDIA_CHANNEL { get; set; }
        public String MediaPlanDateStart { get; set; }
        public String MediaPlanDateEnd { get; set; }

    }

    public class MediaChannelInfo
    {
        public int? MediaChannelId { get; set; }
        public string MediaChannelIdlist { get; set; }
        public String MerchantName { get; set; }
        public String MerchantCode {get; set;}
        public String Codeval { get; set; }
        public String Code { get; set; }
        public String name_th { get; set; }
        public String name_en { get; set; }
        public String Active { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int countMediaChannel { get; set; }
        public String SaleChannelCode { get; set; }
        
    }
    public class MediaProgramInfo
    {
        public int? MediaProgramId { get; set; }
        public string MediaProgramIdlist { get; set; }                 
        public String MediaProgramName { get; set; }
        public String MediaChannelCode { get; set; }
        public String Active { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
    }

    public class MediaPhoneSubInfo
    {
        public String MediaPhoneSubCode { get; set; }
        public int? MediaPhoneSubId { get; set; }
        public string MediaPhoneSubIdlist { get; set; }
        public String MediaPhoneSubNo { get; set; }
        public String MediaPhoneNo { get; set; }
        public String Active { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int countMediaPhoneSub { get; set; }
    }
}
