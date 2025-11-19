using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.Common
{
    public static class MessageConst
    {
        public static string _DELETE_ERROR = " ! ไม่สามารถลบข้อมูล";

        public static string _LOGIN_ERROR = " ! กรุณาเข้าสู่ระบบก่อน";

        public static string _INSERT_ERROR = " ! ไม่สามารถบันทึกข้อมูล";

        public static string _INSERT_SUCCESS = " บันทึกข้อมูลสำเร็จ";

        public static string _UPDATE_ERROR = " ! ไม่สามารถแก้ไขข้อมูลได้";

        public static string _UPDATE_SUCCESS = " แก้ไขข้อมูลสำเร็จ";

        public static string _MSG_VALIDATE_PLEASEINSERT = "! กรุณาระบุข้อมูลให้ครบถ้วน";

        public static string _MSG_PLEASEINSERT = "! กรุณาระบุ";

        public static string _MSG_PLEASESELECT = "! กรุณาเลือกรายการ";

        public static string _MSG_NOT_DUPLICATE = "! ไม่สามารถซ้ำได้";

        public static string _CONFIRM_DELETE = "กรุณาเลือกลูกค้าที่ต้องการลบ";

        public static string _UPDATE_REQUERY_DAR_SUCCESS = "ระบบบันทึกข้อมูลเรียบร้อยแล้ว คุณต้องการดำเนินการต่อหรือไม่";
        
        public static string _UPDATE_REQUERY_DAR_ERROR = "!บันทึกข้อมูลผิดพลาด คุณแน่ใจที่จะ Query รหัสคำร้องต่อไปหรือไม่่";

        public static string _ACTION_SUBMIT = "บันทึกข้อมูลใหม่";
       
        public static string _ACTION_SUBMITEDIT = "แก้ไขข้อมูล";
        
        public static string _ACTION_SUPERUSER_SUBMITEDIT = "Superuser แก้ไขข้อมูล";
     
        public static string _ACTION_SAVEDRAFT = "Draft ข้อมูลใหม่ ";

        public static string _ACTION_APPROVE = "ผ่านการทบทวน";

        public static string _ACTION_REJECT = "ยกเลิกคำร้อง ";

        public static string _ACTION_REVISE = "ขอให้แก้ไขเอกสาร";

        public static string _ACTION_NOTAPPROVE = "ไม่อนุมัติ";

        public static string _ADDDISTRICT_ERROR = " ! กรุณาเลือกจังหวัดก่อน";

        public static string _DATA_NComplete = " ! พบข้อมูลซ้ำ";

        public static string _NOINVENTORYNAME = " ! ไม่พบข้อมูลคลังสินค้า";

        public static string _ValueNotValid = " ! กรุณาใส่จำนวนที่ไม่ติดลบ";

        public static string _ValueNotValidorInformat = " ! กรุณาใส่ข้อมูลเป็นตัวเลขจำนวนที่ไม่ติดลบ";
    }
}