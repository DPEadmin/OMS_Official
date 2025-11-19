using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class DriverInfo
    {
        public int? DriverId { get; set; }
        public String strDriverId { get; set; }
        public String Driver_No{ get; set; }
        public String TitleCode { get; set; }
        public String TitleName { get; set; }
        public String FName { get; set; }
        public String LName { get; set; }   
        public String Mobile { get; set; }
        public String FlagDelete { get; set; }
        public String FullName { get; set; }
        public String Gender { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countDriver{ get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String RoleCode { get; set; }
        public String RoleName { get; set; }
    }
}
