using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APPCOREMODEL.Datas.OMSDTO
{
    public class BankInfo
    {
        public String BankName { get; set; }
        public String BankAccountNumber { get; set; }
        public String BankAccountType { get; set; }
        public String AccountName { get; set; }
        public String BranchName { get; set; }
        public String MerchantCode { get; set; }
        public DateTime CreateDate { get; set; }
        public String CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String Active { get; set; }
    }

}

