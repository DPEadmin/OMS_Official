using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class BranchMapProductInfo
    {
        public int? BranchMapProductId { get; set; }
        public String BranchCode { get; set; }
        public String ProductCode { get; set; }
        public String ProductCodeList { get; set; }
        public String ProductName { get; set; }
        public String RecipeCode { get; set; }
        public String Active { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String ActiveCancelProduct { get; set; }

        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }

    public class BranchMapProductList
    {
        public int? BranchMapProductId { get; set; }
        public String BranchCode { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String Active { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String ActiveCancelProduct { get; set; }

        public int? countOrder { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}