using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.Common
{
    public class ReturnMonth
    {
        /*
         * Create By Jakkapong Subinwong
         * jakkapong.s@gdl-technology.com
         * Last Update 26/06/2013
         */
        /// <summary>
        ///type : 
        ///th  = Full month language Thailand.
        ///th2 = short mouth language Thailand.
        ///en =  Full month language English.
        ///en2 =  Short mount language English.
        /// </summary>
        public string ReturnMonths(Int32 month, string type) {
            type = type.ToLower();
            month = (month * 1);
            string stringValue = "";
            string[] month_th = new string[13];
            month_th[0] = "";
            month_th[1] = "มกราคม";
            month_th[2] = "กุมภาพันธ์";
            month_th[3] = "มีนาคม";
            month_th[4] = "เมษายน";
            month_th[5] = "พฤษภาคม";
            month_th[6] = "มิถุนายน";
            month_th[7] = "กรกฏาคม";
            month_th[8] = "สิงหาคม";
            month_th[9] = "กันยายน";
            month_th[10] = "ตุลาคม";
            month_th[11] = "พฤศจิกายน";
            month_th[12] = "ธันวาคม";
            //-----
            string[] month_th2 = new string[13];
            month_th2[0] = "";
            month_th2[1] = "ม.ค.";
            month_th2[2] = "ก.พ.";
            month_th2[3] = "มี.ค.";
            month_th2[4] = "เม.ย.";
            month_th2[5] = "พ.ค.";
            month_th2[6] = "มิ.ย.";
            month_th2[7] = "ก.ค.";
            month_th2[8] = "ส.ค.";
            month_th2[9] = "ก.ย.";
            month_th2[10] = "ต.ค.";
            month_th2[11] = "พ.ย.";
            month_th2[12] = "ธ.ค.";
            //-----
            string[] month_en = new string[13];
            month_en[0] = "";
            month_en[1] = "January";
            month_en[2] = "February";
            month_en[3] = "March";
            month_en[4] = "April";
            month_en[5] = "May";
            month_en[6] = "June";
            month_en[7] = "July";
            month_en[8] = "August";
            month_en[9] = "September";
            month_en[10] = "October";
            month_en[11] = "November";
            month_en[12] = "December";
            //-----
            string[] month_en2 = new string[13];
            month_en2[0] = "";
            month_en2[1] = "Jan";
            month_en2[2] = "Feb";
            month_en2[3] = "Mar";
            month_en2[4] = "Apr";
            month_en2[5] = "May";
            month_en2[6] = "Jun";
            month_en2[7] = "Jul";
            month_en2[8] = "Aug";
            month_en2[9] = "Sep";
            month_en2[10] = "Oct";
            month_en2[11] = "Nov";
            month_en2[12] = "Dec";

            if (type == "th"){
                if (month == 0){
                    stringValue = month_th[month].ToString();
                }
                else{
                    stringValue = month_th[month].ToString();
                }

            }
            else if (type == "en") {
                if (month == 0){
                    stringValue = month_en[month].ToString();
                }
                else{
                    stringValue = month_en[month].ToString();
                }

            }
            else if (type == "th2"){
                if (month == 0) {
                    stringValue = month_th2[month].ToString();
                }
                else{
                    stringValue = month_th2[month].ToString();
                }

            }
            else if (type == "en2"){
                if (month == 0) {
                    stringValue = month_en2[month].ToString();
                }
                else{
                    stringValue = month_en2[month].ToString();
                }
            }
            return stringValue;
        }
    }
}
