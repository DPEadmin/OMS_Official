using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class EmpMapBu
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