using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class TransportData
    {
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

        public string FlagAddress { get; set; }
    }

    public class TransportDataList
    {
        public List<TransportData> TransportData { get; set; }
    }
}
