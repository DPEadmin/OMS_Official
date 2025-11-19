using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class AreaInfo
    {
        public int? AreaId { get; set; }
        public String AreaCode { get; set; }
        public String AreaName { get; set; }
        public String Polygon { get; set; }
        public int? countArea { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

    }

    public class AreaListReturn
    {
        public int? AreaId { get; set; }
        public String AreaCode { get; set; }
        public String AreaName { get; set; }
        public String Polygon { get; set; }
        public int? countArea { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

    }
}
