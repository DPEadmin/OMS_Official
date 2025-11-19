using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class DiscountBillInfo
    {
        public int? DiscountBillId { get; set; }
        public String DiscountBillCode { get; set; }
        public String DiscountBillName { get; set; }
        public String DiscountBillTypeCode { get; set; }
        public String FreeShipping { get; set; }
        public String StartDate { get; set; }   
        public String EndDate { get; set; }  
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String BrandCode { get; set; }
        public String Active { get; set; }
        public int? DiscountPercent { get; set; }
        public Double? DiscountAmount { get; set; }
        public String RedeemFlag { get; set; }
        public String ComplementaryFlag { get; set; }
        public String PromotionLevel { get; set; }
        public Double? MinimumTotPrice { get; set; }
        public int? countDiscountBill { get; set; }
        public String DiscountBillTypeName { get; set; }
        public String ProductBrandName { get; set; }
        public String FlagAddProduct { get; set; }
        public String DISCOUNTBILLSTATUS { get; set; }
        public String MerchantCode { get; set; }

    }

    public class DiscountBillListReturn
    {
        public int? DiscountBillId { get; set; }
        public String DiscountBillCode { get; set; }
        public String DiscountBillName { get; set; }
        public String DiscountBillTypeCode { get; set; }
        public String FreeShipping { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String BrandCode { get; set; }
        public String Active { get; set; }
        public int? DiscountPercent { get; set; }
        public Double? DiscountAmount { get; set; }
        public String RedeemFlag { get; set; }
        public String ComplementaryFlag { get; set; }
        public String PromotionLevel { get; set; }
        public Double? MinimumTotPrice { get; set; }
        public String DiscountBillTypeName { get; set; }
        public int? countDiscountBill { get; set; }
        public String ProductBrandName { get; set; }
        public String FlagAddProduct { get; set; }
        public String DISCOUNTBILLSTATUS { get; set; }
        public String MerchantCode { get; set; }
    }
    public class DiscountBillTypeInfo
    {
        public int? DiscountBillTypeId { get; set; }
        public String DiscountBillTypeCode { get; set; }
        public String DiscountBillTypeName { get; set; }
    }

    public class DiscountBillTypeListReturn
    {
        public int? DiscountBillTypeId { get; set; }
        public String DiscountBillTypeCode { get; set; }
        public String DiscountBillTypeName { get; set; }
    }
}
