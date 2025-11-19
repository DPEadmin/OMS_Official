using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class MonthlyInfo
    {
        public int? Id { get; set; }
        public String Day { get; set; }
        public String GrandTotal { get; set; }
        public String Hour0 { get; set; }
        public String Hour1 { get; set; }
        public String Hour2 { get; set; }
        public String Hour3 { get; set; }
        public String Hour4 { get; set; }
        public String Hour5 { get; set; }
        public String Hour6 { get; set; }
        public String Hour7 { get; set; }
        public String Hour8 { get; set; }
        public String Hour9 { get; set; }
        public String Hour10 { get; set; }
        public String Hour11 { get; set; }
        public String Hour12 { get; set; }
        public String Hour13 { get; set; }
        public String Hour14 { get; set; }
        public String Hour15 { get; set; }
        public String Hour16 { get; set; }
        public String Hour17 { get; set; }
        public String Hour18 { get; set; }
        public String Hour19 { get; set; }
        public String Hour20 { get; set; }
        public String Hour21 { get; set; }
        public String Hour22 { get; set; }
        public String Hour23 { get; set; }
        public String DayStartMonth { get; set; }
        public String DayEndMonth { get; set; }
        public String ViewDataType { get; set; }
        public String TotalAmount { get; set; }
        public String OAllOrdercount { get; set; }
    }
    public class TotalMonthlyInfo
    {
        public String totalcall { get; set; }
        public String totalorder { get; set; }
        public String totalorderprice { get; set; }

    }

    public class ProductAmountInfo
    {
        public String ProductCode { get; set; }
        public String productname { get; set; }
   
        public String Quanlity { get; set; }
        public String Amount { get; set; }
    }

    public class PromotionAmountInfo
    {
        public String PromotionCode { get; set; }
        public String PromotionName { get; set; }
        public String Quanlity { get; set; }
        public String Amount { get; set; }
    }
}
