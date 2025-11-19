using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class CustomerInfo
    {
        public int? CustomerId { get; set; }
        public String CustomerCode { get; set; }
        public String Title { get; set; }
        public String TitleCode  { get; set; }
        public String TitleName { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CustomerName { get; set; }
        public String CustomerTypeCode { get; set; }
        public String CustomerTypeName { get; set; }
        public String MerchantCode { get; set; }
        public String Mail { get; set; }
        public String Mobile { get; set; }
        public String Identification { get; set; }
        public int? Income { get; set; }
        public String BirthDate { get; set; }
        public String Gender { get; set; }
        public String GenderName { get; set; }
        public String MaritalStatusCode { get; set; }
        public String MaritalStatusName { get; set; }
        public String OccupationCode { get; set; }
        public String OccupationName { get; set; }
        public String ContactTel { get; set; }
        public String HomePhone { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countCustomer { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int? Age { get; set; }  
        public int? PointNum { get; set; }
        public String PointName { get; set; }
        public String BeforeconvertAge { get; set; }
        public String FlagDelete { get; set; }
        public String PhoneNumber { get; set; }
        public String PhoneType { get; set; }
        public String CallinNumber { get; set; }
        public String Note { get; set; }
        public String Occupation { get; set; }
        public String TaxId { get; set; }
    }

    public class CustomerOrderInfo
    {
        public int? OrderId { get; set; }
        public String OrderCode { get; set; }
        public String OrderStatusCode { get; set; }
        public String OrderStatusName { get; set; }
        public String ProductName { get; set; }
        public String OrderStatus { get; set; }
        public String OrderStateCode { get; set; }
        public String OrderStateName { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String BUCode { get; set; }
        public String BUName { get; set; }
        public String NetPrice { get; set; }
        public String CampaignName { get; set; }
        public String TotalPrice { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateDateFrom { get; set; }
        public String CreateDateTo { get; set; }
        public String CreateBy { get; set; }

        public int? countOrder { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String ConfirmNo { get; set; }
        public String shipmentdate { get; set; }
        public String OrderNote { get; set; }
        public String TransportName { get; set; }
        public String TransportCode { get; set; }
        public String CustomerPhone { get; set; }
        public String Occupation { get; set; }
        public String TaxId { get; set; }
    }
}
