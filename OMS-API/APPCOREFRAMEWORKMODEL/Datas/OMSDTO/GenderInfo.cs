using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class GenderInfo
    {
        public String GenderCode { get; set; }
        public String GenderName { get; set; }
    }
    public class GenderListReturn
    {
        public int? GenderId { get; set; }
        public String GenderCode { get; set; }
        public String GenderName { get; set; }
    }
}
