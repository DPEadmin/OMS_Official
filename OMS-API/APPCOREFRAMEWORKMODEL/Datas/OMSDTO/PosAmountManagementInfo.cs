using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class PosAmountManagementInfo
    {
        public int? ID { get; set; }
        public String Merchant_Code { get; set; }
        public String Type_Recive { get; set; }
        public String Type_Spend { get; set; }
        public float Amount { get; set; }
        public String Terminal_ID { get; set; }
        public String CREATE_DATE { get; set; }
        public String CREATE_BY { get; set; }
        public String UPDATE_DATE { get; set; }

        public String UPDATE_BY { get; set; }
        public String FLAG_DELETE { get; set; }
    }

    public class PosAmountManagementReturn
    {
        public int? ID { get; set; }
        public String Merchant_Code { get; set; }
        public String Type_Recive { get; set; }
        public String Type_Spend { get; set; }
        public float Amount { get; set; }
        public String Terminal_ID { get; set; }
        public String CREATE_DATE { get; set; }
        public String CREATE_BY { get; set; }
        public String UPDATE_DATE { get; set; }

        public String UPDATE_BY { get; set; }
        public String FLAG_DELETE { get; set; }
    }
}
