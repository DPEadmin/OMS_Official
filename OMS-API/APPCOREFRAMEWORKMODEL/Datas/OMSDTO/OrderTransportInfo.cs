using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class OrderTransportInfo
    {
        public string OrderCode { get; set; }
        public string CustomerCode { get; set; }
        public string AddressType { get; set; }
    }

    public class OrderTransportListReturn
    {
        public int? OrderPaymentId { get; set; }
        public int? OrderTransportId { get; set; }
        public string OrderCode { get; set; }

        public string Address { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string SubDistrictCode { get; set; }
        public string SubDistrictName { get; set; }
        public string Zipcode { get; set; }
        public double? TransportPrice { get; set; }
        public string TransportType { get; set; }
        public string TransportTypeName { get; set; }
        public string TransportTypeOther { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeName { get; set; }
        public string CustomerCode { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countOrderPayment { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
    }
}
