using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class UserLoginInfo
    {
        public int? UserLoginId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String EmpCode { get; set; }
        public String EmpCodeTemp { get; set; }
        public int? countLogin { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}