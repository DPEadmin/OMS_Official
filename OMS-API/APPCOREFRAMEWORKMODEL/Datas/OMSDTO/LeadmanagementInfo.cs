using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class LeadManagementInfo
    {
        public int? LeadID { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerPhone { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String CustomerName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String CreateDateFrom { get; set; }
        public String CreateDateTo { get; set; }
        public String FlagDelete { get; set; }
        public String LeadManagementIdDelete { get; set; }
        public String AssignEmpCode { get; set; }
        public int? countLead { get; set; }
        public int? rowOFFSet { get; set; }
        public int? rowFetch { get; set; }

        public String REF_CODE { get; set; }
        public String LOT_NAME { get; set; }
        public String CHANNEL_FROM { get; set; }
        public String CHANNEL_TO { get; set; }
        public String MERCHANT_CODE { get; set; }
        public String MerchantName { get; set; }
        public String BRAND_NO { get; set; }
        public String MEDIA_PHONE { get; set; }
        public String PREFIX_TH { get; set; }
        public String FIRSTNAME_TH { get; set; }
        public String LASTNAME_TH { get; set; }

        public String FULL_NAME_TH { get; set; }
        public String MOBILE_1 { get; set; }
        public String MOBILE_2 { get; set; }
        public String MOBILE_3 { get; set; }
        public String MOBILE_4 { get; set; }

        public String MOBILE_5 { get; set; }
        public String MOBILE_6 { get; set; }
        public String PHONE_1 { get; set; }
        public String PHONE_2 { get; set; }
        public String PHONE_3 { get; set; }
        public String addr_no { get; set; }

        public String PLACE { get; set; }
        public String ADDR_SUBDISTRICT { get; set; }
        public String ADDR_SUBDISTRICT_ID { get; set; }
        public String ADDR_DISTRICT { get; set; }
        public String ADDR_DISTRICT_ID { get; set; }

        public String ADDR_PROVINCE { get; set; }
        public String ADDR_PROVINCE_ID { get; set; }
        public String ADDR_ZIPCODE { get; set; }
        public String PREVIOUS_SALE_NAME { get; set; }
        public String PREVIOUS_ORDER_DATE { get; set; }

        public String PREVIOUS_ORDER_BRAND { get; set; }
        public String PREVIOUS_PRODUCT { get; set; }
        public String PREVIOUS_ORDER_DATE_FROM { get; set; }

        public String PREVIOUS_ORDER_DATE_TO { get; set; }
        public String CAMPAIGN_ID { get; set; }

        public String AssignEmpName { get; set; }
        public String AssignFNAME { get; set; }
        public String AssignLNAME { get; set; }
        public String CallOutStatus { get; set; }
        public String Insurancetype { get; set; }
        public String Caryear { get; set; }
        public String Cartype { get; set; }
        public String Carmodel { get; set; }
        public String Carsubmodel { get; set; }
        public String Insurancedate { get; set; }
        public String Name { get; set; }
        public String Telephone { get; set; }
        public String Email { get; set; }
        public int? Productid { get; set; }
        public String Promotion_Code { get; set; }
        public String Status { get; set; }
        public String ProductName { get; set; }
        public String PromotionName { get; set; }
        public String NoHaveAssignEmpCode { get; set; }
        public String HaveAssignEmpCode { get; set; }
        public String ProductCode { get; set; }
        public String CampaignCode { get; set; }
        public int? PromotionDetailInfoId { get; set; }
        public double? Price { get; set; }
        public String UnitName { get; set; }
        public String FreeShipping { get; set; }
        public Double? ProductDiscountPercent { get; set; }
        public Double? ProductDiscountAmount { get; set; }
        public String LockCheckbox { get; set; }
        public String LockAmountFlag { get; set; }
        public int? DefaultAmount { get; set; }
        public String PromotionTypeCode { get; set; }
        public String MOQFlag { get; set; }
        public int? MinimumQty { get; set; }
        public Double? DiscountAmount { get; set; }
        public Double? DiscountPercent { get; set; }
        public Double? GroupPrice { get; set; }
        public String LeadCode { get; set; }
        public String CallStatus { get; set; }
        public String CallSituation { get; set; }
        public String Description { get; set; }
        public String OrderCode { get; set; }
        public String CusReason { get; set; }
        public String CusReasonOther { get; set; }
        public String TransactionTypeCode { get; set; }
        public String TransactionTypeName { get; set; }
        public String RecontactbackDate { get; set; }
        public String RecontactbactPeriodTime { get; set; }
        public String RecontactbactPeriodTimeName { get; set; }
        public String LeadStatusName { get; set; }
        public String SeachStatus { get; set; }

    }
    public class L_LeadManagementInfo
    {
        public List<LeadManagementInfo> L_LeadManagement { get; set; }
    }
}
