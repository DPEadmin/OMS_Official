using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class  PointInfo
    {
        //PointRate
        public int? PointId { get; set; }
        public Double? Price { get; set; }
        public int? GetPoint { get; set; }
        public String MerchantMapCode { get; set; }

        //PointRange
        public String PointCode { get; set; }
        public String PointName { get; set; }
        public int? PointBegin { get; set; }
        public int? PointEnd { get; set; }
        public String FlagDelete { get; set; }
        public int? countPointRange { get; set; }
        public int? PointSequence { get; set; }
        //Promotion
        public String Propoint { get; set; }
        public String PointType { get; set; }
        public String CompanyCode { get; set; }
        public String FlagPatent { get; set; }
        public int? PatentAmount { get; set; }
        public String DiscountCode { get; set; }

        //Product
        public int? ExchangeAmount { get; set; }
        public int? ExchangePoint { get; set; }
        public String CouponType { get; set; }
    }
    public class ReportPointInfo
    {
        public int? Id { get; set; }
        public String MerchantCode { get; set; }
        public String PromotionCode { get; set; } 
        public String PromotionName { get; set; }

        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CustomerCode { get; set; }
        public String CustomerFullName { get; set; }
        public String CustomerFName { get; set; }
        public String CustomerLName { get; set; }
        public String ContactTel { get; set; }
        public String ProductName { get; set; }
        public String PointTypeCode { get; set; }
        public String PointTypeName { get; set; }
        public String PointRangeCode { get; set; }
        public String PointRangeName { get; set; }
        public String ProPointCode { get; set; }
        public String ProPointName { get; set; }
        public int? Amount { get; set; }
        public int? PointNum { get; set; }
        public int? countgv { get; set; }
        public String ActionCode { get; set; }
        public String ActionName { get; set; }    
        public String OrderCode { get; set; }
        public Double? TotalPrice { get; set; }
        public String CompanyCode { get; set; }
        public String CompanyNameTH { get; set; }

    }
}
