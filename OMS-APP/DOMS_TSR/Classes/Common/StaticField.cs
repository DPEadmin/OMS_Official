using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.Common
{
    public static class StaticField
    {
        public static readonly int PageSize = 15;
        public static readonly string SS_EmployeeLogin = "employeelogin";
        public static readonly string SS_ReportData = "module.report.require.data";

        //OrderMessage found in hidordermsg.Value in FullfillOrderManagement Folder
        public static string OrderMsg_01 = "01"; //check_Setup
        public static string OrderMsg_02 = "02"; //check_ReadyDeli
        public static string OrderMsg_03 = "03"; //check_FinishDeli
        public static string OrderMsg_04 = "04";
        public static string OrderMsg_05 = "05";
        public static string OrderMsg_06 = "06"; //check_OrderCancelled
        public static string OrderMsg_07 = "07";
        public static string OrderMsg_08 = "08";

        //Static Value in ImportAPILazadaOrder.aspx
        public static string ProductCodeImportLazada_P00001932 = "P00001932";
        public static string CampaignCodeImportLazada_LZD001 = "LZD001";
        public static string PromotionCodeImportLazada_Prowf005 = "Prowf005";
        public static string MerchantCodeImportLazada_GP2021001 = "GP2021001";
        public static string MerchantNameImportLazada_GP2021001 = "Grand Sport Shop Online";
        public static string BranchCodeImportLazada_01 = "01";
        public static string ChannelCodeImportLazada_ECOM01 = "ECOM01";
        public static string PlatformCodeImportLazada_LAZADA = "LAZADA";

        //OrderStatus Code Lookup
        public static string OrderStatus_01 = "01";
        public static string OrderStatus_02 = "02";
        public static string OrderStatus_03 = "03";
        public static string OrderStatus_04 = "04";
        public static string OrderStatus_05 = "05";
        public static string OrderStatus_06 = "06";
        public static string OrderStatus_07 = "07";
        public static string OrderStatus_08 = "08";
        public static string OrderStatus_09 = "09";
        public static string OrderStatus_10 = "10";
        public static string OrderStatus_11 = "11";

        //OrderState Code Lookup
        public static string OrderState_01 = "01";
        public static string OrderState_02 = "02";
        public static string OrderState_03 = "03";
        public static string OrderState_04 = "04";
        public static string OrderState_05 = "05";
        public static string OrderState_06 = "06";
        public static string OrderState_07 = "07";
        public static string OrderState_08 = "08";
        public static string OrderState_09 = "09";
        public static string OrderState_10 = "10";
        public static string OrderState_11 = "11";
        public static string OrderState_12 = "12";
        public static string OrderState_13 = "13";
        public static string OrderState_14 = "14";
        public static string OrderState_15 = "15";
        public static string OrderState_16 = "16";
        public static string OrderState_17 = "17";

        //SaleOrderType Code Lookup
        public static string SaleOrderType_01 = "01"; //ORDER NOW
        public static string SaleOrderType_02 = "02"; //PRE ORDER

        //PhoneType Code Lookup
        public static string PhoneType_01 = "01"; //Home
        public static string PhoneType_02 = "02"; //Mobile
        public static string PhoneType_03 = "03"; //Office

        //PromotionTypeCode : Table PromotionType
        public static string PromotionTypeCode00 = "00";
        public static string PromotionTypeCode01 = "01";
        public static string PromotionTypeCode02 = "02";
        public static string PromotionTypeCode03 = "03";
        public static string PromotionTypeCode04 = "04";
        public static string PromotionTypeCode05 = "05";
        public static string PromotionTypeCode06 = "06";
        public static string PromotionTypeCode07 = "07";
        public static string PromotionTypeCode08 = "08";
        public static string PromotionTypeCode09 = "09";
        public static string PromotionTypeCode10 = "10";
        public static string PromotionTypeCode11 = "11";
        public static string PromotionTypeCode12 = "12";
        public static string PromotionTypeCode13 = "13";
        public static string PromotionTypeCode14 = "14";
        public static string PromotionTypeCode15 = "15";
        public static string PromotionTypeCode16 = "16";
        public static string PromotionTypeCode17 = "17";
        public static string PromotionTypeCode18 = "18";
        public static string PromotionTypeCode19 = "19";
        public static string PromotionTypeCode20 = "20";
        public static string PromotionTypeCode21 = "21";

        //PromotionTypeName :Map by PromotionCode
        public static string PromotionTypeName01 = "ราคาปกติ";
        public static string PromotionTypNamee02 = "ฟรีค่าขนส่ง";
        public static string PromotionTypeName03 = "ส่วนลด";
        public static string PromotionTypeName04 = "ส่วนลด (เท่ากันทุกรายการ)";
        public static string PromotionTypeName05 = "ส่วนลดโปรโมชั่น";
        public static string PromotionTypeName06 = "จับกลุ่ม + กำหนดราคา";
        public static string PromotionTypeName07 = "กำหนดจำนวนขั้นต่ำ + ฟรีค่าขนส่ง";
        public static string PromotionTypeName08 = "กำหนดจำนวนขั้นต่ำ + ส่วนลดสินค้า";
        public static string PromotionTypeName09 = "กำหนดจำนวน + กำหนดราคา";
        public static string PromotionTypeName10 = "กำหนดยอดซื้อขั้นต่ำ + ส่วนลด";
        public static string PromotionTypeName11 = "กำหนดยอดซื้อขั้นต่ำ + แลกซื้อ";
        public static string PromotionTypeName12 = "กำหนดยอดซื้อขั้นต่ำ + ของแถมฟรี";
        public static string PromotionTypeName13 = "ราคาปกติ";
        public static string PromotionTypeName14 = "คอมโบ";
        public static string PromotionTypeName15 = "จับกลุ่ม + ฟรีค่าขนส่ง";
        public static string PromotionTypeName16 = "จับกลุ่ม + ส่วนลด";
        public static string PromotionTypeName17 = "กำหนดยอดซื้อขั้นต่ำ + ฟรีค่าขนส่ง";
        public static string PromotionTypeName18 = "ของแถมปกติ";
        public static string PromotionTypeName19 = "แบบ point";
        public static string PromotionTypeName20 = "ยิ่งซื้อยิ่งลด";
        public static string PromotionTypeName21 = "LazadaFlexiCombo";

        //MerchantCode : Table Merchant
        public static string MerchantCode_TIB = "TIB";

        //DiscountBillTypeCode : Table DiscountBillType
        public static string DiscountBillTypeCode_01 = "01"; //กำหนดยอดซื้อขั้นต่ำ + ส่วนลด
        public static string DiscountBillTypeCode_02 = "02"; //กำหนดยอดซื้อขั้นต่ำ + แลกซื้อ
        public static string DiscountBillTypeCode_03 = "03"; //กำหนดยอดซื้อขั้นต่ำ + ของแถมฟรี
        public static string DiscountBillTypeCode_04 = "04"; //กำหนดยอดซื้อขั้นต่ำ + ฟรีค่าขนส่ง
        public static string DiscountBillTypeCode_14 = "14"; //??

        //CampaignCategoryCode : Table CampaignCategory
        public static string CampaignCategoryCode_01 = "01"; //Start for Value CampaignCategory in TakeOrderRetail

        //LogisticCode : Table Logistic
        public static string TransportTypeCode_LOGIS01 = "LOGIS01"; //Logistic Type Other

        //ContactStatus Code Lookup
        public static string ContactStatus_01 = "01"; //ตัดสาย
        public static string ContactStatus_02 = "02"; //สายหลุด
        public static string ContactStatus_03 = "03"; //ติดต่อลูกค้าได้
        public static string ContactStatus_04 = "04"; //ไม่รับสาย
        public static string ContactStatus_05 = "05"; //ปฎิเสธ
        public static string ContactStatus_06 = "06"; //ลูกค้าสั่งซื้อสินค้า
        public static string ContactStatus_07 = "07"; //สายดี
        public static string ContactStatus_08 = "08"; //สายเสีย

        //CampaignSpec Code Lookup
        public static string CampaignSpec_01 = "01"; //TakeOrder
        public static string CampaignSpec_02 = "02"; //ecommerce

        //AddressType Code Lookup
        public static string AddressTypeCode01 = "01"; //Delivery
        public static string AddressTypeCode02 = "02"; //Receipt

        //OrderType Code Lookup
        public static string OrderTypeCode01 = "01"; //Normal
        public static string OrderTypeCode02 = "02"; //Refund
        public static string OrderTypeCode03 = "03"; //Return
        public static string OrderTypeCode04 = "04"; //Repair

        //OrderSituation Code Lookup
        public static string OrderSituation01 = "01"; //สั่งซื้อใหม่
        public static string OrderSituation02 = "02"; //ลูกค้าสอบถามข้อมูล
        public static string OrderSituation03 = "03"; //ติดต่อไม่ได้
        public static string OrderSituation04 = "04"; //สายว่างลูกค้าไม่รับสาย

        //PaymentMethod Code Lookup
        public static string PaymentMethod01 = "01"; //ชำระเงินสดปลายทาง
        public static string PaymentMethod02 = "02"; //เครดิต/เดบิตการ์ด
        public static string PaymentMethod03 = "03"; //Moto
        public static string PaymentMethod04 = "04"; //ไอแบ๊งกิ้ง
        public static string PaymentMethod05 = "05"; //เครื่องเอทีเอ็ม
        public static string PaymentMethod06 = "06"; //Bank Transfer

        //PaymentType Code Table : PaymentType
        public static string PaymentType01 = "01"; //เงินสด    
        public static string PaymentType02 = "02"; //ผ่อนชำระ

        public static string PaymentMethod_COD = "COD"; //COD Use in ImportAPILazadaOrder.aspx Only no have value in table Lookup

        //PointType Code Lookup
        public static string PointType01 = "01"; //Point
        public static string PointType02 = "02"; //Coupon
        public static string PointType03 = "03"; //Discount Code

        //PointAction Code Lookup
        public static string PointAction01 = "01"; //แลกของ
        public static string PointAction02 = "02"; //ใช้สิทธิ์
        public static string PointAction03 = "03"; //ได้รับ

        //CriteriaType Code Lookup
        public static string CriteriaType_AMOUNT = "AMOUNT"; //ตามมูลค่าการสั่งซื้อ
        public static string CriteriaType_QUANTITY = "QUANTITY"; //ตามจำนวนรายการ

        //DisCountType Code Lookup
        public static string DisCountType_money = "money"; //ส่วนลดเงินสด
        public static string DisCountType_discount = "discount"; //ส่วนลดเปอร์เซ็น

        //PromotionTag Code Lookup
        public static string TagPromotion_01 = "01"; //Special
        public static string TagPromotion_02 = "02"; //Free Shipping
        public static string TagPromotion_03 = "03"; //Flash Sale
        public static string TagPromotion_04 = "04"; //New Promotion
        public static string TagPromotion_05 = "05"; //WOW
        public static string TagPromotion_06 = "06"; //Sale
        public static string TagPromotion_07 = "07"; //Best Deal
        public static string TagPromotion_08 = "08"; //Hot Deal

        //Unit Code Lookup
        public static string Unit_10 = "10"; //ชุด
        public static string Unit_ชุด = "ชุด"; //ชุด

        //ProductTag Code Lookup :(LookupType : TAGPRODUCT)
        public static string ProductTag_01 = "01"; //สินค้าแนะนำ
        public static string ProductTag_02 = "02"; //จำกัดจำนวน

        //LazadaPromotionStatus Code Lookup (LookupType : LAZADAPROMOTION)
        public static int? LazadaPromotionStatus0 = 0; //Offline
        public static int? LazadaPromotionStatus1 = 1; //Online
        public static int? LazadaPromotionStatus2 = 2; //deactivate

        //ApplyScope Code Lookup (LookupType : APPLYSCOPE)
        public static string ApplyScope_ENTIRE_STORE = "ENTIRE_STORE"; //ทั้งร้าน
        public static string ApplyScope_ENTIRE_STORE_SPECIFIC_PRODUCTS = "SPECIFIC_PRODUCTS"; //เฉพาะบางสินค้า

        //MerchantCode Static for InventoryManagement.aspx
        public static string MerchantCode_InventoryManagement_LG001 = "LG001";
        //MerchantCode Static for Outbound\ClickToCall.aspx
        public static string MerchantCode_ClickToCall_TIB = "TIB";
        public static string MerchantName_ClickToCall_Toyota = "Toyota";

        //ActiveFlagName by ActiveFlagCode
        public static string ActiveFlag_Y = "Y";
        public static string ActiveFlag_N = "N";
        public static string ActiveFlag_Y_NameValue_Active = "Active";
        public static string ActiveFlag_N_NameValue_Inactive = "Inactive";

        //LookupType BU :Type
        public static string LookupType_BU = "BU";

        //LookupCode in LookupType BU
        public static string LookupTypeBU_LookupCode_INB = "INB";
        public static string LookupTypeBU_LookupCode_OUB = "OUB";
        public static string LookupTypeBU_LookupCode_FF = "FF";
        public static string LookupTypeBU_LookupCode_LAZADA = "LAZADA";

        //WfStatus Code : Table WF_Status
        public static string WfStatus_100 = "100";
        public static string WfStatus_200 = "200";
        public static string WfStatus_300 = "300";
        public static string WfStatus_400 = "400";
        public static string WfStatus_500 = "500";
        public static string WfStatus_1000 = "1000";
        public static string WfStatus_1100 = "1100";
        public static string WfStatus_1200 = "1200";

        public static string WfStatus_Savedraft = "Savedraft";
        public static string WfStatus_SubmitByRequestor = "SubmitByRequestor";
        public static string WfStatus_Approve = "Approve";
        public static string WfStatus_Revise = "Revise";
        public static string WfStatus_Reject = "Reject";

        public static string WfStatus_Revised = "Revised"; //Special for WF WorkflowPromotion binding with K2 status

        //POStatusCode : Make manual in CreatePO
        public static string CreatePO_StatusCode01 = "01";
        public static string CreatePO_StatusCode02 = "02";

        //WF_Type Code :Table WF_Type
        public static string WF_Type_10 = "10"; //Campaign Workflow
        public static string WF_Type_20 = "20"; //PO Workflow

        //LookupType ORDERSTATUS :Type
        public static string LookupType_ORDERSTATUS = "ORDERSTATUS";

        //LookupType ADDRESSTYPE :Type
        public static string LookupType_ADDRESSTYPE = "ADDRESSTYPE";

        //LookupType ORDERSTATE :Type
        public static string LookupType_ORDERSTATE = "ORDERSTATE";

        //LookupType ORDERTYPE :Type
        public static string LookupType_ORDERTYPE = "ORDERTYPE";

        //LookupType ORDERSITUATION :Type
        public static string LookupType_ORDERSITUATION = "ORDERSITUATION";

        //LookupType SALEORDERTYPE :Type
        public static string LookupType_SALEORDERTYPE = "SALEORDERTYPE";

        //LookupType PAYMENTMETHOD :Type
        public static string LookupType_PAYMENTMETHOD = "PAYMENTMETHOD";

        //LookupType PROPOINT :Type
        public static string LookupType_PROPOINT = "PROPOINT";

        //LookupType POINTACTION :Type
        public static string LookupType_POINTACTION = "POINTACTION";

        //LookupType POINTTYPE :Type
        public static string LookupType_POINTTYPE = "POINTTYPE";

        //LookupType SALEORDERTYPE :Type
        public static string LookupType_UNIT = "UNIT";

        //LookupType PROMOSTATUS :Type
        public static string LookupType_PROMOSTATUS = "PROMOSTATUS";

        //LookupType CARTYPE :Type
        public static string LookupType_CARTYPE = "CARTYPE";

        //LookupType MAINTAINTYPE :Type
        public static string LookupType_MAINTAINTYPE = "MAINTAINTYPE";

        //LookupType ACTIVESTATUS :Type
        public static string LookupType_ACTIVESTATUS = "ACTIVESTATUS";

        //LookupType DISCOUNTBILLSTATUS :Type
        public static string LookupType_DISCOUNTBILLSTATUS = "DISCOUNTBILLSTATUS";

        //LookupType PROMOTIONLEVEL :Type
        public static string LookupType_PROMOTIONLEVEL = "PROMOTIONLEVEL";

        //LookupType LAZADADISCOUNTTYPE :Type
        public static string LookupType_LAZADADISCOUNTTYPE = "LAZADADISCOUNTTYPE";

        //LookupType TAGPROMOTION :Type
        public static string LookupType_TAGPROMOTION = "TAGPROMOTION";

        //LookupType TAGPRODUCT :Type
        public static string LookupType_TAGPRODUCT = "TAGPRODUCT";

        //LookupType CONTACTSTATUS :Type
        public static string LookupType_CONTACTSTATUS = "CONTACTSTATUS";

        //LookupType CARDTYPE :Type
        public static string LookupType_CARDTYPE = "CARDTYPE";

        //LookupType BANK :Type
        public static string LookupType_BANK = "BANK";

        //LookupType BANKBRANCH :Type
        public static string LookupType_BANKBRANCH = "BANKBRANCH";

        //LookupType BANKBRANCH :Type
        public static string LookupType_BANKOWNER = "BANKOWNER";

        //LookupType INSTALLMENTCREDIT :Type
        public static string LookupType_INSTALLMENTCREDIT = "INSTALLMENTCREDIT";

        //LookupType ACCOUNTTYPE :Type
        public static string LookupType_ACCOUNTTYPE = "ACCOUNTTYPE";

        //LookupType CONTACTRESULT :Type
        public static string LookupType_CONTACTRESULT = "CONTACTRESULT";

        //LookupType MARITALSTATUS :Type
        public static string LookupType_MARITALSTATUS = "MARITALSTATUS";

        //LookupType TITLE :Type
        public static string LookupType_TITLE = "TITLE";

        //LookupType CAR_BAND :Type
        public static string LookupType_CAR_BAND = "CAR_BAND";

        //LookupType CAR_TYPE :Type
        public static string LookupType_CAR_TYPE = "CAR_TYPE";

        //LookupType VOUCHERTYPE :Type
        public static string LookupType_VOUCHERTYPE = "VOUCHERTYPE";

        //LookupType VOUCHERSTATUS :Type
        public static string LookupType_VOUCHERSTATUS = "VOUCHERSTATUS";

        //Redirect Click go to TakeOrder Parameter in Outbound\ContactHistory.aspx, Outbound\ContactHistory.aspx
        public static string CustomerCode_Param_ClickToTakeOrder = "sukanya.koseanto@hotmail.com";
        public static string MerchantCode_Param_ClickToTakeOrder = "ENT";
        public static string Refusername_Param_ClickToTakeOrder = "user";
        public static string Firstname_Param_ClickToTakeOrder = "อรวา";
        public static string Lastname_Param_ClickToTakeOrder = "นราวรรณ";
        public static string MerchantSession_Param_ClickToTakeOrder = "TIB";
        public static string MerchantSessionName_Param_ClickToTakeOrder = "โตโยต้า ลีสซิ่ง (ประเทศไทย) จำกัด";

        //ProductDetail.aspx.cs Lazada Product Property (use in ProductDetail.aspx.cs)
        public static string LazadaProduct_Lazada_status_1 = "1";
        public static string LazadaProduct_url = "https://api.lazada.co.th/rest";
        public static string LazadaProduct_appkey = "101668";
        public static string LazadaProduct_appSecret = "6f8SiWKWPpXrQfZ73XU54bWEgfd5bDOl";
        public static string LazadaProduct_AccessToken = "50000300d379SmpwowaZ5iTFiyE0Ql7chwhlyWHJgMlxequod1acff0be818Quxr";

        //ProductBrandCode Static Mock in Promotion\DiscountbillDetail.aspx, PromotionDetail.aspx
        public static string ProductBrandCode_MK0001 = "MK0001";
        public static string ProductBrandCode_MK000035 = "MK000035";
        public static string ProductBrandCode_YAYOI000002 = "YAYOI000002";

        //StatusWfforReq Static in Promotion\Promotion.aspx
        public static string StatusWfforReq_PromotionManagement = "PromotionManagement";

        //BearerToken APIFacebook "/api/PushMessageBroadcast" in PromotionDetail
        public static string API_Facebook_url = "https://localhost:44309";
        public static string BearerToken_Facebook_PushMessageBroadcast = "eyJhbGciOiJIUzI1NiJ9.R5HxOzBcJIi114Yed6jL6aLKOT_Fiyk9lc75RXVQI6RJ_9E3BbAPR2hLPOFgZhBb-W_ABlQsOkw2iTj6g_-35O_8ge6abSUXo6yhOM5qaZlxXshx4FA2DgqCWPFSENiI.xnSYv9Yfdg7et8Qf5KVz1Erer30YvzKozUDFk7Ubp3E";
        public static string BearerToken_Facebook_PostOnPageWall = "EABEZAerStRSkBACwOsZAWyw5snjbO25OBZAeiIJc2epTmdmxgDMI1AxZAf1PlynuZBVqwKoiPWduWiKdqBimx8QMjC24hZAPty45NmgzF5Ukp0mYuZBZCQFI7zlAcT2ZBpZBZBOGypbZBiItcZCj50EWFYStzU9poDAkurZAvDhb27ZAzSWpEh25SMGuj4lxlfJoHnz2L0ZD";
        public static string BearerToken_Facebook_PostOnPageWall_PageID = "112098074410355";

        //MerchantUpload filesize
        public static int filesize_5600000 = 5600000;

        //TakeOrder/TakeOrder.aspx
        public static string ChannelCode_Tel = "Tel";
        public static string TransportPrice_40 = "40"; //HardCode from start to show takeorder narmal 40 baht.

        //TakeOrderMK/TakeOrder.aspx
        public static string rowFetch_100000 = "100000";
        public static string FlagShowProductPromotion_PROMOTION = "PROMOTION";
        public static string FlagShowProductPromotion_PRODUCT = "PRODUCT";
        public static string branchcode_01 = "01";
        public static string MKChannelCode_02 = "02";
        public static string APIpath_TRD_FinishOrder = "http://doublep.three-rd.com:3230/api/1.0.0/oms/order?access_token=X2LCUDLQoWlqpZoNDhijsUvp9ytvVUAl1YIUSCjqm2BSTUVbGitzHBIJGBvdS8DS";

        //Company Code : Table : Company
        public static string CompanyCode_MK = "MK";

        //TakeOrderRetail
        public static string MerchantCode_ASTON = "ASTON";
        public static string FlagSubPromotionDetailMain_MainProduct = "MainProduct";
        public static string FlagSubPromotionDetailExchange_ExchangeProduct = "ExchangeProduct";
        public static string TakeOrderRetail_ParentProductCode_PromotionNewPrice = "-PromotionNewPrice";
        public static string TakeOrderRetail_ParentProductCode_PromotionHeadSet_x9 = "-x9"; //set header promotionset
        public static string TakeOrderRetail_ParentProductCode_PromotionChildSet_x99 = "-x99"; //set child promotionset
        public static string TakeOrderRetail_ParentProductCode_PromotionHeadSetCalPrice_y9 = "-y9"; //set header promotionset Default Price
        public static string TakeOrderRetail_ParentProductCode_PromotionChildSetCalPrice_y99 = "-y99"; //set child promotionset Default Price
        public static string TakeOrderRetail_ParentProductCode_PromotionMOQChildSet_99MOQ = "-99MOQ"; //set child promotion MOQ change to set
        public static string TakeOrderRetail_ParentProductCode_PromotionMOQHeadSet_9MOQ = "-9MOQ"; //set head promotion MOQ change to set
        public static string TakeOrderRetail_ParentProductCode_PromotionMOQOther_999MOQ = "-999MOQ"; //MOQ other type
        public static string TakeOrderRetail_BrachCodeForSaveOrder_01 = "01"; //set branchcode for retail
        public static string TakeOrderRetail_OrderInfoMapPRPOFileType_PO = "PO";//OrderInfoMapPRPOFileType
        public static string TakeOrderRetail_ProductCodeForGVProductINV_NoSelectItem_SP003 = "SP003"; //ProductCode For Initial GridView ProductInventory No Select
        public static string TakeOrderRetail_ProductNameForGVProductINV_NoSelectItem_SP003Name = "สายชาร์จที่จุดบุหรี่"; //ProductCode For Initial GridView ProductInventory No Select
        public static string TakeOrderRetail_OrderTransport_TIB01_SubDistrictCode = "102107"; //SubDistrictCode
        public static string TakeOrderRetail_OrderTransport_TIB01_SubDistrictName = "แสมดำ"; //SubDistrictName
        public static string TakeOrderRetail_OrderTransport_TIB01_DistrictCode = "1021"; //DistrictCode
        public static string TakeOrderRetail_OrderTransport_TIB01_DistrictName = "บางขุนเทียน"; //DistrictName
        public static string TakeOrderRetail_OrderTransport_TIB01_ProvinceName = "กรุงเทพมหานคร"; //ProvinceName
        public static string TakeOrderRetail_OrderTransport_TIB01_Zipcode = "10150"; //Zipcode
        public static string TakeOrderRetail_LeadStatusTIB_Closed = "Closed"; //TIB LeadStatus : Closed

        //UserManagement UserDetail.aspx.cs
        public static string UserDetail_rowfetch_100 = "100"; //show paging 100 row per page

        //PromotionWorkList.aspx.cs
        public static string PromotionWorkList_FinishFlag_Yes = "Yes";

        //EmpRole Code
        public static string EmpRoleCode_ADMIN = "ADMIN"; //ADMIN (Administrator)
    }
}
