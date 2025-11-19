using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class CompanyInfo
    {
        public int? CompanyId { get; set; }
        public String CompanyId_str { get; set; }
        public String CompanyCode { get; set; }
        public String CompanyNameTH { get; set; }
        public String CompanyNameEN { get; set; }
        public String CompanyKind { get; set; }
        public String AddressTH { get; set; }
        public String AddressEN { get; set; }
        public String Telephone { get; set; }
        public String Fax { get; set; }
        public String TechnicianCode { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

        public String MerchantMapCode { get; set; }

    }

    public class CompanyListReturn
    {
        public int? CompanyId { get; set; }
        public String CompanyCode { get; set; }
        public String CompanyNameTH { get; set; }
        public String CompanyNameEN { get; set; }
        public String CompanyKind { get; set; }
        public String AddressTH { get; set; }
        public String AddressEN { get; set; }
        public String Telephone { get; set; }
        public String Fax { get; set; }
        public String TechnicianCode { get; set; }
        public String TechnicianName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countCompany { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }

        public String MerchantMapCode { get; set; }
    }
}
