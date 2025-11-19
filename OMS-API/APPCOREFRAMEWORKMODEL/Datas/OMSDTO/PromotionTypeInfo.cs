using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class PromotionTypeInfo
    {
        public int? PromotionTypeId { get; set; }
        public String PromotionTypeCode { get; set; }
        public String PromotionTypeName { get; set; }
    }

    public class PromotionTypeListReturn
    {
        public int? PromotionTypeId { get; set; }
        public String PromotionTypeCode { get; set; }
        public String PromotionTypeName { get; set; }
    }
}
