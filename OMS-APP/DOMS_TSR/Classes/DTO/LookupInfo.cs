using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class LookupInfo
    {
        public int? LookupId { get; set; }
        public String LookupCode { get; set; }
        public String LookupType { get; set; }
        public String LookupValue { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countLookup { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }

        public int? AccountId { get; set; }
        public String Branch { get; set; }
        public String AccountName { get; set; }
        public String AccountType { get; set; }
        public String AccountTypeName { get; set; }
        public String AccountNumber {get; set;}

        public String LockAmountFlag { get; set; }
        public String GroupLookupCode { get; set; }
    }
}
