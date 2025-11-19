using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class CampaignCategoryInfo
    {
        public int? CampaignCategoryId { get; set; }
        public String CampaignCategoryIdDelete { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String PicCampaignUrl { get; set; }
        public String PictureCampaignUrl { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? count { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}