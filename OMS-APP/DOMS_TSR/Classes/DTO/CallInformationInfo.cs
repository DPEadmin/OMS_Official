using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOMS_TSR.Classes.DTO
{
    public class CallInformationInfo
    {
        public int? CallInId { get; set; }
        public int? CustomerId { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String MerchantCode { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String CallInNumber { get; set; }
        public String FlagDelete { get; set; }
        public String CONTACTSTATUS { get; set; }
        public String ContactTel { get; set; }
        public String Mobile { get; set; }
        public String Mail { get; set; }
        public String Title { get; set; }
        public String Gender { get; set; }
        public String Identification { get; set; }
        public String BirthDate { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String MaritalStatusCode { get; set; }
        public String MaritalStatusName { get; set; }
        public String OccupationCode { get; set; }
        public String OccupationName { get; set; }
        public String Occupation { get; set; }
        public String HomePhone { get; set; }
        public int? Income { get; set; }
        public int? Age { get; set; }
        public String OrderCode { get; set; }
    }
}