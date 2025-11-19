using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class L_Channeldata
    {
        public List<ChannelInfo> ChannelInfo { get; set; }
    }
    public class ChannelInfo
    {
        public String ChannelCode_val { get; set; }
        public int? ChannelId { get; set; }
        public String ChannelIdDelete { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }

        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countChannel { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        //
    }

    public class ChannelListReturn
    {
        
        public String ChannelCode_val { get; set; }
        public int? ChannelId { get; set; }
        public String ChannelIdDelete { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }
        public String MerchantCode { get; set; }
        public String MerchantName { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public int? countChannel { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public string FlagDelete { get; set; }
        public String Active { get; set; }
    }

    public class swaggerInfo
    {
        public String ChannelId { get; set; }
        public String ChannelCode { get; set; }
        public String ChannelName { get; set; }

    }
}
