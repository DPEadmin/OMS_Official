using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class IntegrationEmpInfo
    {
        public int? EmpId { get; set; }
        public String EmpCode { get; set; }
        public String EmpFName { get; set; }
        public String EmpLName { get; set; }
        public String EmpStatus { get; set; }
        public String RefCode { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String UpdateDate { get; set; }
        public String CreateDate { get; set; }
        public int? CountEmp { get; set; }
    }

    public class IntegrationEmpListReturn
    {
        public int? EmpId { get; set; }
        public String EmpCode { get; set; }
        public String EmpFName { get; set; }
        public String EmpLName { get; set; }
        public String EmpStatus { get; set; }
        public String RefCode { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String UpdateDate { get; set; }
        public String CreateDate { get; set; }
        public int? CountEmp { get; set; }
    }
}
