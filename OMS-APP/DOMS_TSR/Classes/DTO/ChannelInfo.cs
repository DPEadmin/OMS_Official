using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class ChannelInfo
    {
        public int? ChannelId { get; set; }
        public String ChannelIdDelete { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countChannel { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String Active { get; set; }

    }
}
