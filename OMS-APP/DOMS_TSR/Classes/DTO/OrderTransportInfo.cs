using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class OrderTransportInfo
    {
        public int? OrderPaymentId { get; set; }
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
        public string TransportOther { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeName { get; set; }
        public string TransportTypeOther { get; set; }
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
