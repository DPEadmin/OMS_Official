using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class ProductCategoryInfo
    {
        public int? ProductCategoryId { get; set; }
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
        public String FlagDelete { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countProductCategory { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}
