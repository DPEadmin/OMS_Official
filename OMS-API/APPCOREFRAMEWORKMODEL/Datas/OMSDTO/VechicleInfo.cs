using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class VechicleInfo
    {
        public int? VechicleId { get; set; }
        public String Routing_code { get; set; }
        public String Vechicle_No { get; set; }
        public String Vechicle_Lookup { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String Vechicle_Band { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String FlagDelete { get; set; }
        public String Vechicle_Model { get; set; }
        public String Active { get; set; }
        public String Vechicle_Active { get; set; }
        //public List<EmpDBControlInfo> L_EmpDBControlnfo { get; set; } 
        //public List<EmpDBControlInfo> L_EmpDBControlnfo { get; set; }
    }
    public class VechicleListReturn
    {
        public int? VechicleId { get; set; }
        public String Routing_code { get; set; }
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
        public int? countVechicle { get; set; }
        public String Vechicle_Model { get; set; }
        public String Active { get; set; }
        public String Vechicle_Active { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
    }
    public class L_VechicleAndDBInfo
    {
        public List<VechicleInfo> L_VechicleInfo { get; set; } = new List<VechicleInfo>();
    }
}

 
