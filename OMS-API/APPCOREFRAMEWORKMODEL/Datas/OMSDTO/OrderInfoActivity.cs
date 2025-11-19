using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class OrderInfoActivity
    {
        public String OrderCode { get; set; }
    }

    public class OrderInfoActivityListReturn
    {
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public String OrderStateCode { get; set; }
        public String OrderStateName { get; set; }
        public String OrderNote { get; set; }
        public String Createdate { get; set; }
        public String CreateBy { get; set; }
    }
}
