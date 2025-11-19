using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class  ProductBrandInfo
    {
        public int? ProductBrandId { get; set; }
        public String ProductBrandCode { get; set; }
        public String ProductBrandName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countProductBrand { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
     
    }
}
