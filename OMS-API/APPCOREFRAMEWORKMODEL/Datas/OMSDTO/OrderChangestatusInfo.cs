using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APPCOREMODEL.Datas.OMSDTO
{
    public class OrderChangestatusInfo
    {
        public string ordercode { get; set; }
        public int orderid { get; set; }
        public string orderstatus { get; set; }
        public string orderstate { get; set; }
        public string updateBy { get; set; }
        public string FlagApproved { get; set; }
        public string Confirmno { get; set; }
        public string MerchantCode { get; set; }
        public string MerchantMapCode { get; set; }

        public string OrderTracking { get; set; }
        
    }


  
}
