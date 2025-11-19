using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ProductCategoryInfo
    {
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
    }
    public class ProductCategoryListReturn
    {
        public int? ProductCategoryId { get; set; }
        public String ProductCategoryCode { get; set; }
        public String ProductCategoryName { get; set; }
        public String FlagDelete { get; set; }
    }


    public class CategoryProductListReturn
    {
        public List<ProductCategoryListReturn> Category { get; set; }
        public List<ProductListReturn> Product { get; set; }

    }
}
