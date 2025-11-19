using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class L_SubMainPromotionDetail
    {
        public List<SubMainPromotionDetailInfo> ChannelInfo { get; set; }
    }
    public class SubMainPromotionDetailInfo
    {
        public int? SubMainId { get; set; }
        public String SubMainIdDelete { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionDetailId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Amount { get; set; }
        public String FlagSubPromotionDetailMain { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countSubMain { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String CombosetCode { get; set; }

    }

    public class SubMainPromotionDetailListReturn
    {
        public int? SubMainId { get; set; }
        public String SubMainIdDelete { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionDetailId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Amount { get; set; }
        public String FlagSubPromotionDetailMain { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countSubMain { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String CombosetCode { get; set; }
    }
}
