using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class L_transportdata
    {
     public List<transportdataInfo> transportdataInfo { get; set; }
    }
        public class transportdataInfo
    {
            public int? OrderTransportID { get; set; }
            public string AddressId { get; set; }
            public string OrderCode { get; set; }
            public string CustomerCode { get; set; }
            public string Address { get; set; }
            public string Address2 { get; set; }
            public string ProvinceCode { get; set; }
            public string ProvinceName { get; set; }
            public string DistrictCode { get; set; }
            public string DistrictName { get; set; }
            public string SubDistrictCode { get; set; }
            public string SubDistrictName { get; set; }
            public string BranchCode { get; set; }
            public string Zipcode { get; set; }
            public string TransportPrice { get; set; }
            public string LogisticCode { get; set; }
            public string LogisticName { get; set; }
            public string TransportType { get; set; }
            public string TransportTypeOther { get; set; }
            public string AddressType { get; set; }
            public string AddressTypeName { get; set; }
            public string FlagAddress { get; set; }
            public string CreateDate { get; set; }
            public string CreateBy { get; set; }
            public string UpdateDate { get; set; }
            public string UpdateBy { get; set; }
            public string Lat { get; set; }
            public string Long { get; set; }
            public string InventoryCode { get; set; }
            public int? CustomerAddressId { get; set; }
            public List<BranchInfo> NearestBranchList { get; set; }
        }
}
