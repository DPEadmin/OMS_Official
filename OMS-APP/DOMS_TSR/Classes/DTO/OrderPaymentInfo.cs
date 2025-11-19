using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class OrderPaymentInfo
    {
        public int? OrderPaymentId { get; set; }
        public string OrderCode { get; set; }
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
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countOrderPayment { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
    }
}
