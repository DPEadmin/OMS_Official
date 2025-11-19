using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class MediaOnAirInfo
    {
        public int? MediaOnAirId { get; set; }
        public String ChannelCode { get; set; }
        public String StatusActive { get; set; }
        public String StartTime { get; set; }
        public String EndDate { get; set; }
        public String EndTime { get; set; }
        public String CampaignCode { get; set; }
        public String PromotionCode { get; set; }
        public String CampaignMediaStartDate { get; set; }
        public String CampaignMediaExpireDate { get; set; }
        public String MediaPlanTime { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String ChannelName { get; set; }
        public String PlannerTime { get; set; }
        public String MediaPhone { get; set; }
        public String MediaSaleChannel { get; set; }
        public String MediaSaleChannelcode { get; set; }
    }

    public class MediaOnAirListReturn
    {
        public int? MediaOnAirId { get; set; }
        public String ChannelCode { get; set; }
        public String StatusActive { get; set; }
        public String StartTime { get; set; }
        public String EndDate { get; set; }
        public String EndTime { get; set; }        
        public String CampaignCode { get; set; }
        public String PromotionCode { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String ChannelName { get; set; }
        public String MediaPhone { get; set; }
        public String MediaSaleChannel { get; set; }
        public String MediaSaleChannelcode { get; set; }
    }
    public class MediaSaleChannelInfo
    {
        public int? ID { get; set; }
        public String CODE { get; set; }
        public String NAME_TH { get; set; }
        public String NAME_EN { get; set; }
    }
}
