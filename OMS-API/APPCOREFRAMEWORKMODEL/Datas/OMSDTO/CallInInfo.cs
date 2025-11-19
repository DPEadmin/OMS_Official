using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class CallInInfo
    {
        public String AgentId { get; set; }
        public String Type { get; set; }

    }

    public class CallInListReturn
    {
        public int? CallInId { get; set; }
        public String CallInNumber { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String DNIS { get; set; }
        public String BUCode { get; set; }
        public String AgentId { get; set; }
        public String EmpCode { get; set; }
        public String EmpName { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public String Type { get; set; }
        public int? countCallIn { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}
