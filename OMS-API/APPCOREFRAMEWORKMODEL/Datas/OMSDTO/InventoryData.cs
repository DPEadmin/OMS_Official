using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class InventoryData
    {

        public string InventoryCode { get; set; }
        public string ProductCode { get; set; }
        public int? Qty { get; set; }
        public int? Reserved { get; set; }
        public int? Reserving { get; set; }
        public int? Balance { get; set; }
    }

    public class InventoryDataList
    {
        public List<InventoryData> InventoryData { get; set; }
    }
}
