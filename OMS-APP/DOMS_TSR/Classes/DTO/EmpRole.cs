using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class EmpRole
    {
        public int? EmpRoleId { get; set; }
        public int? EmpId { get; set; }
        public String EmpCode { get; set; }
        public String RoleCode { get; set; }
        public String EmpFname_TH { get; set; }
        public String EmpLname_TH { get; set; }
        public String EmpName_TH { get; set; }
        public String RoleName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public int? countEmpRole { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String FlagDelete { get; set; }
        public String RoleFlagDelete { get; set; }
        public String WFType { get; set; }
        
    }
}