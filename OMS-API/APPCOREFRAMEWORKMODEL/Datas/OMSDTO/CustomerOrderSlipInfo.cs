using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APPCOREMODEL.Datas.OMSDTO
{
    public class CustomerOrderSlipInfo
    {
        public String CustomerCode { get; set; }
        public String Ordercode { get; set; }
        public String MerchantCode { get; set; }
        public String OrderSlipImageUrl { get; set; }
        public String FlagDelete { get; set; }
        public String Active { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }

    }

}

