using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ProvinceInfo
    {
        public int? ProvinceId { get; set; }
        public String ProvinceCode { get; set; }
        public String ProvinceName { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public String UpdateBy { get; set; }
        public String ProvinceName_Ins { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }

    public class ProvinceListReturn
    {
        public int? ProvinceId { get; set; }
        public String ProvinceCode { get; set; }
        public String ProvinceName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countProvince { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }

        public String ProvinceName_Ins { get; set; }
    }
}
