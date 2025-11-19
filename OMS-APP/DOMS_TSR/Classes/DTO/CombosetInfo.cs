using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class CombosetInfo
    {
        public int? PMDID { get; set; }
        public int? CombosetId { get; set; }
        public String CombosetCode { get; set; }
        public String CombosetName { get; set; }
        public String PromotionDetailName { get; set; }
        public String CampaignName { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String CampaignCode { get; set; }
        public int? count { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public double CombosetPrice { get; set; }
        public String FlagDelete { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public int? countComboset { get; set; }
        public String StartDatePromotionCombo { get; set; }
        public String EndDatePromotionCombo { get; set; }
        public String FlagActive { get; set; }
}
}
