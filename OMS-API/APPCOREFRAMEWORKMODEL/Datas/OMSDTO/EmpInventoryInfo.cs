using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class EmpInventoryInfo
    {
        public int? EmpInventoryId { get; set; }
        public String EmpCode { get; set; }
        public String InventoryCode { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }

    }
    public class EmpInventoryListReturn
    {
        public int? EmpInventoryId { get; set; }
        public String EmpCode { get; set; }
        public String InventoryCode { get; set; }
        public String InventoryName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countEmpInventory { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String InventoryFlagDelete { get; set; }
    }
}
