using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class LCCInfo
    {
        public int? LCCID { get; set; }
        public String Account_Name { get; set; }
        public int? Limit { get; set; }
        public int? CountRun { get; set; }
        public int? Expire { get; set; }
        public int? X { get; set; }
    }
}