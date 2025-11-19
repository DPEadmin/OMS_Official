  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class ModuleInfo
    {
        public int? ModuleId { get; set; }
        public String ModuleCode { get; set; }
        public String ModuleName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String StatusCode { get; set; }
        public String StatusName { get; set; }
        public String CreateBy { get; set; }
        public Double? Price { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String ActiveFlag { get; set; }
    }
}
