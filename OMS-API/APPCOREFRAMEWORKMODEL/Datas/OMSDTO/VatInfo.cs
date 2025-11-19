using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class VatInfo
    {
        public int? VatId { get; set; }
        public String VatCode { get; set; }
        public String VatName { get; set; }
        public double? VatValue { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countVat { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagActive { get; set; }
        public String FlagDelete { get; set; }
    }

    public class VatListReturn
    {
        public int? VatId { get; set; }
        public String VatCode { get; set; }
        public String VatName { get; set; }
        public double? VatValue { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countVat { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagActive { get; set; }
        public String FlagDelete { get; set; }
    }
}
