using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class CampaignCategoryInfo
    {
        public int? CampaignCategoryId { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String CampaignCategoryIdDelete { get; set; }
        public String PicCampaignUrl { get; set; }
        public String PictureCampaignUrl { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String MerchantMapCode { get; set; }

        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
    public class CampaignCategoryReturn
    {
        public int? CampaignCategoryId { get; set; }
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
        public int countCampaignCategory { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public string CampaignCategoryIdDelete { get; set; }
    }
    public class CampaignCategoryImageListInfo
    {
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryImageUrl { get; set; }
        public String CampaignCategoryImageName { get; set; }
        public String CampaignCategoryImageBase64 { get; set; }
        public String CampaignCategoryImagedecryptBase64 { get; set; }
        public String FlagDelete { get; set; }
    }
    public class CampaignCategoryImageListReturn
    {
        public int? CampaignCategoryImageId { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryImageUrl { get; set; }
        public String CampaignCategoryImageName { get; set; }
        public String CampaignCategoryImageBase64 { get; set; }
        public String CampaignCategoryImagedecryptBase64 { get; set; }
        public String FlagDelete { get; set; }
    }
}
