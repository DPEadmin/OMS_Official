using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class OrderDetailInfo
    {
        public String OrderCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStateCode { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String BUCode { get; set; }
        public Double? NetPrice { get; set; }
        public Double? Price { get; set; }
        public Double? Vat { get; set; }
        public String UpdateBy { get; set; }
        public String CustomerCode { get; set; }
        public int? runningNo { get; set; }
        public String PromotionCode { get; set; }
        public String ProductCode { get; set; }
        public String PromotionDetailId { get; set; }
        public String Unit { get; set; }
        public String InventoryCode { get; set; }
        public String FlagProSetHeader { get; set; }
    }

    public class OrderDetailListReturn
    {
        public int? OrderDetailId { get; set; }
        public String OrderCode { get; set; }
        public String OrderCodeList { get; set; }
        public String table { get; set; }
        public String OrderStatus { get; set; }
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
        public Double? Price { get; set; }
        public Double? ProductPrice { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String PromotionDetailId { get; set; }
        public String Unit { get; set; }
        public String UnitName { get; set; }
        public Double? NetPrice { get; set; }
        public Double? SumPrice { get; set; }
        public int Amount { get; set; }
        public Double? DiscountAmount { get; set; }
        public Double? Vat { get; set; }
        public int? DiscountPercent { get; set; }
        public String OrderTypeCode { get; set; }
        public String OrderTypeName { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public String OrderStateCode { get; set; }
        public String OrderStateName { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String BUCode { get; set; }
        public String BUName { get; set; }
        public Double? TotalPrice { get; set; }
        public String Address { get; set; }
        public String Subdistrict { get; set; }
        public String District { get; set; }
        public String Province { get; set; }
        public String Zipcode { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countOrder { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public int? runningNo { get; set; }
        public String CustomerName { get; set; }
        public String Phonenumber { get; set; }
        public Double? TransportPrice { get; set; }
        public Double? sumTotalPrice { get; set; }
        public Double? sumVat { get; set; }
        public String InventoryCode { get; set; }
        public String FlagProSetHeader { get; set; }

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
    }
}
