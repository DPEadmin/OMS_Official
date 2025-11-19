using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class PosTerminalInfo
    {
        public int? ID { get; set; }
        public String Merchant_Code { get; set; }
        public String Terminal_ID { get; set; }
        public String CREATE_DATE { get; set; }
        public String CREATE_BY { get; set; }
        public String UPDATE_DATE { get; set; }

        public String UPDATE_BY { get; set; }
        public String FLAG_DELETE { get; set; }


    }
}
