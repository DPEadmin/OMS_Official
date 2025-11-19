using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class L_paymentdata
    {
        public List<paymentdataInfo> paymentdataInfo { get; set; }
    }
    public class paymentdataInfo
    {
        public string OrderCode { get; set; }
        public string PaymentTypeCode { get; set; }
        public Double? Payamount { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string FlagDelete { get; set; }
        public string CardType { get; set; }
        public string CardNo { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }
        public string CVCNo { get; set; }

        public string Installment { get; set; }
        public string InstallmentPrice { get; set; }
        public string FirstInstallment { get; set; }
        public string CardIssuename { get; set; }
        public string CitizenId { get; set; }
        public string BirthDate { get; set; }
        public string BankCode { get; set; }
        public string BankBranch { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string PaymentOtherdetail { get; set; }
        public string MpayNum { get; set; }
        public string MpayName { get; set; }
        public string PaymentGateway { get; set; }

    }
          
}