using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{

    public class ClickToCallInfo
    {
        public String extension { get; set; }
        public String callnumber { get; set; }
        public List<calldata> calldata { get; set; } = new List<calldata>();
    }

    public class calldata
    {
        public String id { get; set; }
        public String campaignid { get; set; }
        public String type { get; set; }
        public String callerid { get; set; }
    }

}