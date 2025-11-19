using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class VoucherInfo
    {
        public int? VoucherId { get; set; }
        public String VoucherIdList { get; set; }
        public String VoucherCode { get; set; }
        public String VoucherCode_Validate { get; set; }
        public String VoucherName { get; set; }
        public String VoucherTypeCode { get; set; }
        public String VoucherTypeName { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }
        public Double? Price { get; set; }
        public int? Quantity { get; set; }
        public int? Reserve { get; set; }
        public int? Balance { get; set; }
        public string FlagUnlimit { get; set; }
        public String Remark { get; set; }
        public String StartDate { get; set; }
        public String StartDateFrom { get; set; }
        public String StartDateTo { get; set; }
        public String EndDate { get; set; }
        public String EndDateFrom { get; set; }
        public String EndDateTo { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countVoucher { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

    }

    public class VoucherListReturn
    {
        public int? VoucherId { get; set; }
        public String VoucherIdList { get; set; }
        public String VoucherCode { get; set; }
        public String VoucherCode_Validate { get; set; }
        public String VoucherName { get; set; }
        public String VoucherTypeCode { get; set; }
        public String VoucherTypeName { get; set; }
        public String CampaignCategoryCode { get; set; }
        public String CampaignCategoryName { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }
        public Double? Price { get; set; }
        public int? Quantity { get; set; }
        public int? Reserve { get; set; }
        public int? Balance { get; set; }
        public string FlagUnlimit { get; set; }
        public String Remark { get; set; }
        public String StartDate { get; set; }
        public String StartDateFrom { get; set; }
        public String StartDateTo { get; set; }
        public String EndDate { get; set; }
        public String EndDateFrom { get; set; }
        public String EndDateTo { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countVoucher { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

    }
}
