using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class PaymentTypeInfo
    {
        public int? PaymentTypeId { get; set; }
        public String PaymentTypeCode { get; set; }
        public String PaymentTypeName { get; set; }
    }

    public class PaymentTypeListReturn
    {
        public int? PaymentTypeId { get; set; }
        public String PaymentTypeCode { get; set; }
        public String PaymentTypeName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countCompany { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
    }
}
