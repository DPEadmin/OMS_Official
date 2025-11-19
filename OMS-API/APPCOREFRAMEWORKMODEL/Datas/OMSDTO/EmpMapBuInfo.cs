using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class EmpMapBuInfo
    {
        public int? EmpMapBuId { get; set; }
        public String EmpCode { get; set; }
        public String Bu { get; set; }
        public String Role { get; set; }
        public String Levels { get; set; }
        public String BuCodeInit { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }

    public class EmpMapBuListReturn
    {
        public int? EmpMapBuId { get; set; }
        public String EmpCode { get; set; }
        public String Bu { get; set; }
        public String Role { get; set; }
        public String Levels { get; set; }
        public String EmpFname_TH { get; set; }
        public String EmpLname_TH { get; set; }
        public String EmpName_TH { get; set; }
        public int? countEmpMapBU { get; set; }
    }
}
