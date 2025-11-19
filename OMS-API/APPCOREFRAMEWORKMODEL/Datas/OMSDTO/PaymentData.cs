using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class PaymentData
    {
        public string PaymentTypeCode { get; set; }
        public Double? PayAmount { get; set; }
        public int? Installment { get; set; }
        public Double? InstallmentPrice { get; set; }
        public Double? FirstInstallment { get; set; }
        public string CardIssuename { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public string CardTypeName { get; set; }
        public string CVCNo { get; set; }
        public string CardOwnerName { get; set; }
        public string CardExpMonth { get; set; }
        public string CardExpYear { get; set; }
        public string CitizenId { get; set; }
        public string BirthDate { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string PaymentOtherDetail { get; set; }
    }

    public class PaymentDataList
    {
        public List<PaymentData> PaymentData { get; set; }
    }
}
