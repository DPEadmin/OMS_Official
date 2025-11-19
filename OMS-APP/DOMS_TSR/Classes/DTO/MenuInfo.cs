using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class MenuInfo
    {
        public int? Id { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public int? ParentId { get; set; }
        public string Style { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string ModuleNo { get; set; }
        public string Ranking { get; set; }
        public string UpdateBy { get; set; }
        public string Updatedate { get; set; }
        public int? countMenu { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}
