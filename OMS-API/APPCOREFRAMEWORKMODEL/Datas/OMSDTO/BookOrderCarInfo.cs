using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class BookOrderCarInfo
    {
        public int? BookId { get; set; }
        public String Booking_No { get; set; }
        public String Ordercode { get; set; }
        public String Driver_no { get; set; }
        public String Vechicle_id { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }

    }

    public class BookOrderCarListReturn
    {
        public int? BookId { get; set; }
        public String Booking_No { get; set; }
        public String Ordercode { get; set; }
        public String Driver_no { get; set; }
        public String FlagDelete { get; set; }
        public String Vechicle_id { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countDriver { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}
