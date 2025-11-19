using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALEORDER.DTO
{
    public class MerchantInfo
    {
     
        public int? MerchantId { get; set; }
        public string MerchantCode { get; set; }
        public string MerchantName { get; set; }
        public string MerCodeValidate { get; set; }
        public string MerchantType { get; set; }
        public string CompanyCode { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; }
        public string SubDistrictCode { get; set; }
        public string DistrictCode { get; set; }
        public string ProvinceCode { get; set; }
        public string ZipCode { get; set; }
        public string ContactTel { get; set; }
        public string FaxNum { get; set; }
        public string Email { get; set; }
        public string FlagDelete { get; set; }
        public string ActiveFlag { get; set; }
        public string ActiveFlagName { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public int countMerchant { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public double Distance { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Username { get; set; }
        public string PictureMerchantURL { get; set; }
        public string MerchantAddress { get; set; }
        public string ProvinceName { get; set; }
        public string DistictName { get; set; }
        public string SubDistictName { get; set; }
    }
}
