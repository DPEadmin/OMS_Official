using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class ProductImageInfo
    {
        
        public String ProductCode { get; set; }
        public String ProductImageUrl { get; set; }
        public String ProductImageName { get; set; }
        public String ProductImageBase64 { get; set; }
        public String ProductImagedecryptBase64 { get; set; }
        public String FlagDelete { get; set; }

    }
    public class ProductImageListReturn
    {
        public int? ProductImageId { get; set; }
        public String ProductCode { get; set; }
        public String ProductImageUrl { get; set; }
        public String ProductImageName { get; set; }
        public String ProductImageBase64 { get; set; }
        public String ProductImagedecryptBase64 { get; set; }       
        public String FlagDelete { get; set; }
    }
}
