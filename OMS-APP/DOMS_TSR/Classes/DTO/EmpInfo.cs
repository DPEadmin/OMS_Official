using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SALEORDER.DTO
{
    public class EmpInfo
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
        public String ActiveFlagName { get; set; }
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
        public int? EmpExpire { get; set; }
        public int? countEmp { get; set; }
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }
        public String RoleCode { get; set; }
        public String DepartmentCode { get; set; }
        public String RefCode { get; set; }
        public String RefUserName { get; set; }
        public String BranchName { get; set; }
        public String BranchCode { get; set; }
        public String BuName { get; set; }
        public String BuCode { get; set; }
        public String ConnectionAPI { get; set; }
        public String MerchantCode { get; set; }
        public String CompanyCode { get; set; }
        public int? runningNo { get; set; }
        public int? NumberLeadAssign { get; set; }
        public String PercentLeadAssign { get; set; }
        public String ExtensionID { get; set; }
        public String Prefix { get; set; }
        
        
    }

    public class sendCreateempdatatoOneApp
    {
        public String EmpCode { get; set; } // EmpCode of OMS
        public String EmpFName { get; set; }
        public String EmpLName { get; set; }
        public String UserName { get; set; }
        public String UserLevel { get; set; } // Fix value = "01"
        public String EmpStatus { get; set; }
        //public String RefCode { get; set; } // Code refer to EmpCode interface between OneApp and OMS
        public String ConnectionAPI { get; set; }
        public List<EmpDBControlInfo> L_EmpDBControlnfo { get; set; }
       
    }

    public class sendUpdateempdatatoOneApp
    {
        public String EmpCode { get; set; } // EmpCode of OMS
        public String EmpFName { get; set; }
        public String EmpLName { get; set; }
        public String UserName { get; set; }
        public String UserLevel { get; set; } // Fix value = "01"
        public String EmpStatus { get; set; }
        public String RefCode { get; set; } // Code refer to EmpCode interface between OneApp and OMS
    }
    public class msgreturnOneAPP
    {
        public resultData resultData { get; set; }
        public string resultCode { get; set; }
        public string resultMessage { get; set; }
    }
    public class resultData
    {
        public string emp_code { get; set; }
        public string ref_code { get; set; }
    }
    public class EmpLoginInfo
    {
        public int? EmpId { get; set; }
        public String EmpCode { get; set; }
        public String UserLoginId { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String BranchCode { get; set; }
        public String CompanyCode { get; set; }
        public String EmpRoleCode { get; set; }
        public String CompanyUrl { get; set; }

        public String Severname { get; set; }
        public String DBUsername { get; set; }
        public String DBPassword { get; set; }
        public String Databasename { get; set; }
        public String ConnectionAPI { get; set; }
    }

    public class EmpDBControlInfo
    {
        public String Severname { get; set; }
        public String DBUsername { get; set; }
        public String DBPassword { get; set; }
        public String Databasename { get; set; }
    }
}
