using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class L_inventorydata
    {
        public List<inventorydataInfo> inventorydataInfo { get; set;}
    }
    public class inventorydataInfo
    {
        public String InventoryCode { get; set; }
        public String ProductCode { get; set; }
        public String MerchantCode { get; set; }
        public int? QTY { get; set; }
        public int? Reserved { get; set; }
        public int? Reserving { get; set; }
        public int? Balance { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
    }
}
