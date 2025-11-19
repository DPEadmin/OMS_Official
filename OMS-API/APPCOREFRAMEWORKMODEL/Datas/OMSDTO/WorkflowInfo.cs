using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class WorkflowInfo
    {
        public Decimal? ID { get; set; }        
        public Decimal? OMSId { get; set; }
        public String OMSCode { get; set; }
        public String OMSName { get; set; }
        public String WFType { get; set; }
        public String WFTypeList { get; set; }
        public Decimal? Status { get; set; }
        public Decimal? WFId { get; set; }
        public String WFName { get; set; }
        public String WFStatusCode { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }

    }

    public class WorkflowListReturn
    {
        public Decimal? ID { get; set; }
        public int? WFStatusId { get; set; }
        public Decimal? EmpId { get; set; }
        public Decimal? OMSId { get; set; }
        public String OMSCode { get; set; }
        public String OMSName { get; set; }
        public String OMSType { get; set; }
        public Decimal? WFId { get; set; }
        public String WFType { get; set; }
        public String WFTypeList { get; set; }
        public String WFName { get; set; }
        public Decimal? Status { get; set; }
        public String StatusList { get; set; }
        public String StatusName { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String LastUpdate { get; set; }
        public Decimal? Department { get; set; }
        public String ActionType { get; set; }
        public String K2WFName { get; set; }
        public String WFStatusCode { get; set; }
        public String WFStatusName { get; set; }
        public String Active { get; set; }
    }
}
