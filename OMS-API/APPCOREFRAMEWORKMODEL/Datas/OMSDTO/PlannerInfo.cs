using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class PlannerInfo
    {
        public int? Plannerid { get; set; }
        public String ChannelCode { get; set; }
        public String StatusActive { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }

        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int? countPlanner { get; set; }

        public String PlannerCode { get; set; }
        public String PlannerName { get; set; }
        public String CampaignCode { get; set; }
        public String ChannelName { get; set; }
        public String PlannerDuration { get; set; }
        public String PlannerDate { get; set; }
        public String PlannerTime { get; set; }
        public String PlannerProgramName { get; set; }
        public String PlanneridList { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String CampaignName { get; set; }
        public String SaleChannelName { get; set; }
        public String SaleChannelCode { get; set; }
    }

    public class PlannerListReturn
    {
        public int? Plannerid { get; set; }
        public String ChannelCode { get; set; }
        public String StatusActive { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public int? countPlanner { get; set; }

        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String PlannerCode { get; set; }
        public String PlannerName { get; set; }
        public String CampaignCode { get; set; }
        public String ChannelName { get; set; }
        public String PlannerDuration { get; set; }
        public String PlannerDate { get; set; }
        public String PlannerTime { get; set; }
        public String PlannerProgramName { get; set; }
        public String PlanneridList { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String CampaignName { get; set; }
        public String SaleChannelName { get; set; }
        public String SaleChannelCode { get; set; }
    }
}
