using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class CustomerInfo
    {
        public int? CustomerId { get; set; }
        public String CusIdforDelete { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CustomerTypeCode { get; set; }
        public String OccupationCode { get; set; }
        public String ContactTel { get; set; }
        public String PhoneNumber { get; set; }
        public String HomePhone { get; set; }
        public String Currently { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String Mobile { get; set; }
        public int? Age { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? Income { get; set; }
        public int? IncomeFrom { get; set; }
        public int? IncomeTo { get; set; }
        public String MaritalStatusCode { get; set; }
        public String MaritalStatusName { get; set; }
        public String TitleCode { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        public String Gender { get; set; }
        public String Identification { get; set; }
        public String BirthDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String PhoneType { get; set; }
        public String Note { get; set; }
        public String MediaPhone { get; set; }
        public String MerchantCode { get; set; }

        //** Interface for OneApp**//
        public String UniqueID { get; set; } // ReferenceCode from OneApp to Customer Code in OMS System
        public String Channel { get; set; }
        public String RefCode { get; set; }  // ReferenceCode for Log in
        public String BrandNo { get; set; } // BrandCode both of OneApp and OMS
        public String address1 { get; set; }
        public String address2 { get; set; }
        public String subdistrict { get; set; }
        public String district { get; set; }
        public String province { get; set; }
        public String postcode { get; set; }
        public String address { get; set; }
        public String TaxId { get; set; }
        public String Username { get; set; }
        public String RefUsername { get; set; }
        public String CallInfoID { get; set; }
        public String MerchantCustomer { get; set; }
        public int? PointNum { get; set; }

        public String PointRangeCode { get; set; }
        public String PointCaseCondition { get; set; }
        public String Email { get; set; }
    }

    public class CustomerListReturn
    {
        public int? CustomerId { get; set; }
        public String CustomerCode { get; set; }
        public String Title { get; set; }
        public String TitleCode { get; set; }
        public String TitleName { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CustomerName { get; set; }
        public String CustomerTypeCode { get; set; }
        public String CustomerTypeName { get; set; }
        public String Mail { get; set; }
        public String ContactTel { get; set; }
        public String HomePhone { get; set; }
        public String Mobile { get; set; }
        public String Identification { get; set; }
        public String BirthDate { get; set; }
        public String Gender { get; set; }
        public String GenderName { get; set; }
        public String MaritalStatusCode { get; set; }
        public String MaritalStatusName { get; set; }
        public String OccupationCode { get; set; }
        public String OccupationName { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        public int? Income { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countCustomer { get; set; }
        public int? countCustomerPhone { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int? Age { get; set; }
        public String BeforeconvertAge { get; set; }
        public String FlagDelete { get; set; }
        public String PhoneNumber { get; set; }
        public String PhoneType { get; set; }
        public String CallinNumber { get; set; }
        public String Note { get; set; }
        public String MerchantCode { get; set; }
        
        //** Interface for OneApp**//
        public String UniqueID { get; set; }
        public String Channel { get; set; }
        public String RefCode { get; set; }  // ReferenceCode for Log in
        public String Brand { get; set; } // BrandCode both of OneApp and OMS
        public String MessageInsertorUpdateComplete { get; set; }
        public String address1 { get; set; }
        public String address2 { get; set; }
        public String subdistrict { get; set; }
        public String district { get; set; }
        public String province { get; set; }
        public String postcode { get; set; }
        public String TaxId { get; set; }
        public int? PointNum { get; set; }
        public int? NextPointNum { get; set; }
        public String PointName { get; set; }
        public String NextPointName { get; set; }
        public String Email { get; set; }
    }

    public class CustomerInfofromOneApp
    {
        public String contact_tel { get; set; }
        public String channel { get; set; }
        public String ref_code { get; set; }
        public String brand_no { get; set; }
        public String unique_id { get; set; }
    }

   public class CustomerListOneAppReturn
    {
        public String result_code { get; set; }
        public String order_url { get; set; }
        public String result_message { get; set; }
        public String unique_id { get; set; }
        public String address { get; set; }
        public String customercode { get; set; }
    }
}
