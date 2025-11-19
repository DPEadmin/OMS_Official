using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.Datas.OMSDTO
{
    public class EmpInfo
    {
        public String ActiveFlag { get; set; }
        public String TechnicianFlag { get; set; }
        public String EmpCode { get; set; }
        public String EmpCodeTemp { get; set; }
        public String EmpCodeValidate { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String UnitCode { get; set; }
        public int? EmpId { get; set; }
        public String EmpFname_TH { get; set; }
        public String EmpLname_TH { get; set; }
        public String EmpName_TH { get; set; }
        public String EmpName_EN { get; set; }
        public String EmpFname_EN { get; set; }
        public String EmpLname_EN { get; set; }
        public String Title_TH { get; set; }
        public String Title_EN { get; set; }
        public String CreateBy { get; set; }
        public String UpdateBy { get; set; }
        public String Mail { get; set; }
        public String Mobile { get; set; }
        public String PositionCode { get; set; }
        public String BUCode { get; set; }
        public String RefCode { get; set; }
        public String RefUserName { get; set; }
        public String EmpStatus { get; set; }
        public String EmpFName { get; set; }
        public String EmpLName { get; set; }
        public String BranchName { get; set; }
        public String BranchCode { get; set; }
        public String ExtensionID { get; set; }
        public String CompanyCode { get; set; }
        public String FlagDelete { get; set; }
        public String RoleCode { get; set; }
    }

    public class EmpListReturn
    {
        public int? EmpId { get; set; }
        public String EmpCode { get; set; }
        public String EmpCodeTemp { get; set; }
        public String EmpCodeValidate { get; set; }
        public String EmpName { get; set; }
        public String UserLoginId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String AgentId { get; set; }
        public String Title_TH { get; set; }
        public String TitleName_TH { get; set; }
        public String EmpFname_TH { get; set; }
        public String EmpLname_TH { get; set; }
        public String EmpName_TH { get; set; }
        public String EmpName_EN { get; set; }
        public String Title_EN { get; set; }
        public String TitleName_EN { get; set; }
        public String EmpFname_EN { get; set; }
        public String EmpLname_EN { get; set; }
        public String Mobile { get; set; }
        public String PositionCode { get; set; }
        public String PositionName { get; set; }
        public String ActiveFlag { get; set; }
        public String Mail { get; set; }
        public String EntryDate { get; set; }
        public String UpdateDate { get; set; }
        public String UpdateBy { get; set; }
        public String CreateDate { get; set; }
        public String CreateBy { get; set; }
        public String LoginEmpCode { get; set; }
        public String FlagDelete { get; set; }
        public String TechnicianFlag { get; set; }
        public String LookupValue { get; set; }
        public int? countEmp { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String RoleCode { get; set; }
        public String DepartmentCode { get; set; }
        public String UnitCode { get; set; }
        public String BUCode { get; set; }
        public String BUName { get; set; }
        public String RefCode { get; set; }
        public String RefUserName { get; set; }
        public String EmpFName { get; set; }
        public String EmpLName { get; set; }
        public String EmpStatus { get; set; }
        public String BranchName { get; set; }
        public String BranchCode { get; set; }
        public String ExtensionID { get; set; }
        public String Prefix { get; set; }
        
    }
    public class EmpListOneAppReturn
    {
        public String result_code { get; set; }
        public String result_data { get; set; }
        public String result_message { get; set; }
    }
}
