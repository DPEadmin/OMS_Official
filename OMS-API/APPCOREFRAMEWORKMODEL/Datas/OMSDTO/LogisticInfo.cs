using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class LogisticInfo
    {
        public int? LogisticId { get; set; }
        public String LogisticIdDelete { get; set; }
        public String LogisticCode { get; set; }
        public String LogisticName { get; set; }
        public String FlagDelete { get; set; }
        public int Fee { get; set; }
        public double Weight { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String Type { get; set; }
        public int? countLogistic { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String LogisticType { get; set; }
        public String TypeCalWeight { get; set; }
        public String TypeCalSize { get; set; }
        public String TypeCalWeightSize { get; set; }
        public String status { get; set; }
        public String EstimatedTime { get; set; }
        public String WorkingDay { get; set; }

    }
    public class LogisticListReturn
    {
        public int? LogisticId { get; set; }
        public String LogisticCode { get; set; }
        public String LogisticName { get; set; }
        public String FlagDelete { get; set; }
        public int Fee { get; set; }
        public double Weight { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String Type { get; set; }
        public int? countLogistic { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String LogisticType { get; set; }
        public String TypeCalWeight { get; set; }
        public String TypeCalSize { get; set; }
        public String TypeCalWeightSize { get; set; }

        public String EstimatedTime { get; set; }
        public String status { get; set; }
        public String WorkingDay { get; set; }
    }

    public class LogisticDetailInfo
    {
        public int? LogisticDetailId { get; set; }
        public String LogisticDetailIdDelete { get; set; }
        public String LogisticCodeDetail { get; set; }      
        public String FlagDelete { get; set; }
        public int Fee { get; set; }
        public double PackageWidth { get; set; }
        public double PackageLength { get; set; }
        public double PackageHeigth { get; set; }
        public double PackageWLHFrom { get; set; }
        public double PackageWLHTo { get; set; }
  
        public String CalCalWeight { get; set; }
        public String CalWLH { get; set; }
        public double WeightFrom { get; set; }
        public double WeightTo { get; set; }
        public String UpdateDate { get; set; }

        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
      
        public int? countLogisticDetail { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String LogisticType { get; set; }
    }
    public class InsertLogisticAndDetailInfo
    {
        public String LogisticCode { get; set; }
        public String LogisticName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public List<LogisticDetailInfo> logisticDetails { get; set; }

    }

    public class LogisticListDetailReturn
    {
        public int? LogisticDetailId { get; set; }
        public String LogisticDetailIdDelete { get; set; }
        public String LogisticCodeDetail { get; set; }
        public String FlagDelete { get; set; }
        public int Fee { get; set; }
        public double PackageWidth { get; set; }
        public double PackageLength { get; set; }
        public double PackageHeigth { get; set; }

        public double WeightFrom { get; set; }
        public double WeightTo { get; set; }
        public String UpdateDate { get; set; }

        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public double PackageWLHFrom { get; set; }
        public double PackageWLHTo { get; set; }

        public String CalCalWeight { get; set; }
        public String CalWLH { get; set; }
        public int? countLogisticDetail { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String LogisticType { get; set; }
    }
}

