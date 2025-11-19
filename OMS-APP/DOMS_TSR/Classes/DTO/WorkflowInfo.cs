using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class WorkflowInfo
    {
        public Decimal? ID { get; set; }
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
        public String WFStatusCode { get; set; }
        public String WFStatusName { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String LastUpdate { get; set; }
        public Decimal? Department { get; set; }
        public String ActionType { get; set; }
        public String K2WFName { get; set; }
    }

    public class k2Info
    {
        public String folio { get; set; }
        public String expectedDuration { get; set; }
        public String priority { get; set; }
        public dataFields dataFields = new dataFields();
    }
    public class dataFields
    {        
        public int? PromotionID { get; set; }
        public String Event { get; set; } //Data Value {'submit','revise','reject','cancel','approve'}
        public String Actor { get; set; }
        public String Remark { get; set; }

    }
    public class K2_OrderWFInfo
    {
        public int? id { get; set; }
        public String OrderCode { get; set; }
        public String procInstId { get; set; }
        public String CreateBy { get; set; }
    }
}
