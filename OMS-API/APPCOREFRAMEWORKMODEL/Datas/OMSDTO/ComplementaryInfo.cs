using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ComplementaryInfo
    {
        public int? ComplementaryId { get; set; }
        public String ProductCode { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String Amount { get; set; }
        public String PromotionDetailInfoId { get; set; }
        public String ComplementaryId_List { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
    public class ComplementaryListReturn
    {
        public int? ComplementaryId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String Unit { get; set; }
        public String UnitName { get; set; }
        public String Amount { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionDetailInfoId { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countComplementaryInfo { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public int Price { get; set; }
        public String ProductCategoryName { get; set; }
        public String ProductCategoryCode { get; set; }

    }
}
