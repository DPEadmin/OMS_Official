using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class SaleChannelInfo
    {
        public int? SaleChannelid { get; set; }
        public String ChannelCode { get; set; }
        public String StatusActive { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }

        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String SaleChannelCode { get; set; }
        public String SaleChannelName { get; set; }
        public String SaleChannelPhone { get; set; }
        public int? countSaleChannel { get; set; }
        public String SaleChannelidList { get; set; }
        public String ChannelName { get; set; }
    }

    public class SaleChannelListReturn
    {
        public int? SaleChannelid { get; set; }
        public String ChannelCode { get; set; }
        public String StatusActive { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public String SaleChannelidList { get; set; }

        public String UpdateBy { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String ChannelName { get; set; }
        public String SaleChannelCode { get; set; }
        public String SaleChannelName { get; set; }
        public int? countSaleChannel { get; set; }
        public String SaleChannelPhone { get; set; }
    }
}
