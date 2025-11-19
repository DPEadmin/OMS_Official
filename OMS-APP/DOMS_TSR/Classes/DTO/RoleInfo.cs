using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class RoleInfo
    {
        public int? RoleId { get; set; }
        public int? EmpId { get; set; }
        public String RoleCode { get; set; }
        public String RoleCodeValidate { get; set; }
        public String RoleName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countRole { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String DepartmentCode { get; set; }
        public String DepartmentName { get; set; }
    }
}