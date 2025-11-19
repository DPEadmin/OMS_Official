using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class VechicleInfo
    {
        public int? VechicleId { get; set; }
        public String strVechicleId { get; set; }
        public String Vechicle_No { get; set; }
        public String Vechicle_Band { get; set; }
        public String Name_Band { get; set; }
        public String Vechicle_Lookup { get; set; }
        public String Name_TypeCar { get; set; }
        public String FlagDelete { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String CarId { get; set; }
        public int? countVechicle{ get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String Vechicle_Model { get; set; }
        public String Active { get; set; }
        public String Vechicle_Active { get; set; }
        // public List<EmpDBControlInfo> L_EmpDBControlnfo { get; set; }new List<JobAssignDetail>();
        public List<EmpDBControlInfo> L_EmpDBControlnfo { get; set; } = new List<EmpDBControlInfo>();
    }

    public class L_VechicleAndDBInfo
    {
        public List<VechicleInfo> L_VechicleInfo { get; set; } = new List<VechicleInfo>();
        public List<EmpDBControlInfo> L_EmpDBControlnfo { get; set; } = new List<EmpDBControlInfo>();
    }
}
