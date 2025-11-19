using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class CustomerAddressInfo
    {
        public int? CustomerAddressId { get; set; }
        public String CustomerCode { get; set; }
        public String Address { get; set; }
        public String Subdistrict { get; set; }
        public String District { get; set; }
        public String Province { get; set; }
        public String ZipCode { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String FlagActive { get; set; }
        public String AddressType { get; set; }
        public String Lat { get; set; }
        public String Long { get; set; }
        public String AreaCode { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String Ordercode { get; set; }
        public String UniqueID { get; set; }
        public String FlagDefault { get; set; }
        public String Address_Fullname { get; set; }
        public String Address_Tel { get; set; }

    }
    public class CustomerAddressListReturn
    {
        public int? CustomerAddressId { get; set; }
        public String Address { get; set; }
        public String Subdistrict { get; set; }
        public String District { get; set; }
        public String Province { get; set; }
        public String SubdistrictName { get; set; }
        public String DistrictName { get; set; }
        public String ProvinceName { get; set; }
        public String ZipCode { get; set; }
        public String CustomerCode { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countCustomerAdress { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagActive { get; set; }
        public String FlagActiveName { get; set; }
        public String AddressType { get; set; }
        public String AddressTypeName { get; set; }
        public String Lat { get; set; }
        public String Long { get; set; }
        public String AreaCode { get; set; }

        public String Ordercode { get; set; }
        public String UniqueID { get; set; }
        public String FlagDefault { get; set; }
        public String Address_Fullname { get; set; }
        public String Address_Tel { get; set; }

    }
}
