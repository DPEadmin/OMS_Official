using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class RoutingInfo
    {
        public int? RoutingId { get; set; }
        public String Routing_code { get; set; }
        public String Routing_name { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
    }

    public class RoutingListReturn
    {
        public int? RoutingId { get; set; }
        public String Routing_code { get; set; }
        public String Routing_name { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countRouting { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}
