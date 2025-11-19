using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class PromotionImageInfo
    {
        public String PromotionCode { get; set; }
        public String PromotionImageUrl { get; set; }
        public String PromotionImageName { get; set; }
        public String PromotionImageBase64 { get; set; }
        public String PromotionImagedecryptBase64 { get; set; }
        public String FlagDelete { get; set; }
    }

    public class PromotionImageListReturn
    {
        public int? PromotionImageId { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionImageUrl { get; set; }
        public String PromotionImageName { get; set; }
        public String PromotionImageBase64 { get; set; }
        public String PromotionImagedecryptBase64 { get; set; }
        public String FlagDelete { get; set; }
    }
}
