using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class DepartmentInfo
    {
        public String DepartmentCode { get; set; }
        public String DepartmentName { get; set; }
        public String FlagDelete { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String DepartmentCodeValidate { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public int? DepartmentId { get; set; }


    }

    public class DepartmentListReturn
    {
        public int? DepartmentId { get; set; }
        public String DepartmentCode { get; set; }
        public String DepartmentCodeValidate { get; set; }
        public String DepartmentName { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String FlagDelete { get; set; }
        public int? countDepartment { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
}
