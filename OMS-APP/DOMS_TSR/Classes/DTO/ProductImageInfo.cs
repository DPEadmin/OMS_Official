using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALEORDER.DTO
{
    public class ProductImageInfo
    {
        public int ProductImageId { get; set; }
        public String ProductImageUrl { get; set; }
        public String ProductImageName { get; set; }
        public String ProductCode { get; set; }
        public String FlagDelete { get; set; }
    }
}