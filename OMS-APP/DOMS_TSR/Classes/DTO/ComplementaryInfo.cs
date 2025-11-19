using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class ComplementaryInfo
    {
        public int? ComplementaryId { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String Unit { get; set; }
        public String UnitName { get; set; }
        public String Amount { get; set; }
        public Double? Price { get; set; }
        public String PromotionDetailInfoId { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countComplementaryInfo { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
    }
}